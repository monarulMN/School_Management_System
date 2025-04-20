﻿using Microsoft.AspNetCore.Identity;
using SchoolProject.Models;
using SchoolProject.Repositories;
using SchoolProject.Utilities;
using SchoolProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Services
{
    public class StudentService : IStudentService
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public StudentService(IUnitOfWork unitOfWork, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AddStudent(CreateStudentViewModel student)
        {
            ApplicationUser appUser = new ApplicationUser()
            {
                UserName = student.UserName,
                Email = student.Email
            };
            var result = await _userManager.CreateAsync(appUser, student.Password);
            if (result.Succeeded)
            {
                if(!await _roleManager.RoleExistsAsync("Student"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("Student"));
                }
                await _userManager.AddToRoleAsync(appUser, "Student");
 
            }
            student.KeyId = appUser.Id;
            var model = new CreateStudentViewModel().ConvertModel(student);
            await _unitOfWork.GenericRepository<Student>().AddAsync(model);
            _unitOfWork.Save();
        }

        public PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount = 0;
            List<StudentViewModel> viewModelList = new List<StudentViewModel>();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Student>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Student>().GetAll().ToList().Count;
                viewModelList = ConvertModelToViewModelList(modelList);

            }
            catch (Exception ex)
            {
                throw;
            }

            var result = new PagedResult<StudentViewModel>
            {
                Data = viewModelList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize

            };
            return result;
        }

        public int GetAllStudents()
        {
            var totalCount = _unitOfWork.GenericRepository<Student>().GetAll().ToList().Count;
            return totalCount;
        }

        private List<StudentViewModel> ConvertModelToViewModelList(List<Student> modelList)
        {
            return modelList.Select(x => new StudentViewModel(x)).ToList();
        }
    }
}

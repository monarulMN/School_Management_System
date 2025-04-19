using Microsoft.EntityFrameworkCore;
using SchoolPoject.Models;
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
    public class GradeService :IGradeService
    {
        private IUnitOfWork _unitOfWork;

        public GradeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Add(CreateGradeViewModel viewModel)
        {
            var model = new CreateGradeViewModel().Convert(viewModel);
            await _unitOfWork.GenericRepository<Grade>().AddAsync(model);
            _unitOfWork.Save();
        }

        public int AddGradeWithStudent(GradeViewModel grade, int sessionId, List<int> StudentList)
        {
            int count = 0;
            var model = new GradeViewModel().Convert(grade);
            foreach(var item in StudentList)
            {
                if (!_unitOfWork.GenericRepository<Enroll>().Exists(x => x.SessionId == sessionId && x.StudentId == item)
                {
                    model.Enrolls.Add(new Enroll()
                    {
                        StudentId = item,
                        GradeId = grade.Id,
                        SessionId = sessionId
                    });
                    count++;
                }
            }
        }

        public PagedResult<GradeViewModel> GetAll(int pageNumber, int pageSize)
        {
            int totalCount = 0;
            List<GradeViewModel> viewModelList = new List<GradeViewModel>();
            try
            {
                int Excluderecords = (pageSize *  pageNumber) - pageSize;
                var modelList = _unitOfWork.GenericRepository<Grade>().GetAll()
                    .Skip(Excluderecords).Take(pageSize).ToList();
                totalCount = _unitOfWork.GenericRepository<Grade>().GetAll().ToList().Count;
                viewModelList = ConvertModelToViewModelList(modelList);
            }
            catch(Exception ex)
            {
                throw;
            }
            var result = new PagedResult<GradeViewModel>()
            {
                Data = viewModelList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        public IEnumerable<GradeViewModel> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Grade>().GetAll();
            var viewModelList = ConvertModelToViewModelList(modelList.ToList()); 
            return viewModelList;
        }

        public GradeViewModel GetById(int gradeId)
        {
            var model = _unitOfWork.GenericRepository<Grade>()
                .GetByIdAsync(x => x.Id == gradeId, include: y => y.Include(a => a.Enrolls));
            var viewModel = new GradeViewModel(model);
            return viewModel;

        }

        private List<GradeViewModel> ConvertModelToViewModelList(List<Grade> modelList)
        {
            return modelList.Select(x => new GradeViewModel(x)).ToList();
        }
    }
}

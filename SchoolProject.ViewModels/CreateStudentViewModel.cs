﻿using SchoolPoject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class CreateStudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoin { get; set; } = DateTime.Now;
         public bool Selected { get; set; }
        public string KeyId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public Student ConvertModel(CreateStudentViewModel student)
        {
            return new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
                DateOfJoin = student.DateOfJoin,
                KeyId = student.KeyId
            };
        }
    }
}

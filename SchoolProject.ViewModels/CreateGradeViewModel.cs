using SchoolPoject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class CreateGradeViewModel
    {
        public string Name { get; set; }
        public Grade Convert(CreateGradeViewModel viewModel)
        {
            return new Grade { Name = viewModel.Name };
        }
    }
}

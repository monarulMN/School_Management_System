using SchoolPoject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.ViewModels
{
    public class CreateSessionViewModel
    {
        public string Start {  get; set; }
        public string End { get; set; }

        public Session Convert(CreateSessionViewModel viewModel)
        {
            return new Session
            {
                Start = viewModel.Start,
                End = viewModel.End,
            };
        }
    }
}

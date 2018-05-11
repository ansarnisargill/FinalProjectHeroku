using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalProject.Models;

namespace FinalProject.Models.ViewModels
{
    public class UserSignUpViewModel
    {
        public User user { get; set; }
        public HttpPostedFileBase image { get; set; }
    }
}
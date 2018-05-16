using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class PsyViewModel
    {
       public  Psychologist psychologist { get; set; }
       public  HttpPostedFileBase image { get; set; }
    }
}
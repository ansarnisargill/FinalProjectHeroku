using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class Result
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public User user {get; set;}
        public int BirthOrder { get; set; }
        public string Illness { get; set; }
        public string Education { get; set; }
        public bool IsReligios { get; set; }
        public bool SexuallyActive { get; set; }
        public int Siblings { get; set; }
        public DateTime DateOfTest { get; set; }
        public bool EmploymentStatus { get; set; }
        public int JobSatisfaction { get; set; }
        public bool AttachmentWithFamily { get; set; }
        public int HomeEnvironmentStrictness { get; set; }
        public bool Insomnia { get; set; }
        public int BDIScore { get; set; }

    }
}
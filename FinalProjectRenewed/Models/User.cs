using System;
using System.Collections.Generic;
namespace FinalProject.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsMarried { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public bool Sex { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }

    }
}
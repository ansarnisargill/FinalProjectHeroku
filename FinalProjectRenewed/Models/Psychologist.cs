using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class Psychologist
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
        public int PsyTypeID { get; set; }
        public PsyType psyType { get; set; }
        public string Education { get; set; }
        public int Experience { get; set; }
        public double RegistrationNo { get; set; }
        public double CNIC { get; set; }
        public double MobileNo { get; set; }
        public string Sex { get; set; }
        public DateTime JoinDate { get; set; }

    }
}
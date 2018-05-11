using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class Psychologist
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Name { get; set; }
        public string ImageAddress { get; set; }
        public int PsyTypeID { get; set; }
        public PsyType psyType { get; set; }
        public string Education { get; set; }
        public int Experience { get; set; }
        public int RegistrationNo { get; set; }
        public int CNIC { get; set; }
        public int MobileNo { get; set; }
        public string Sex { get; set; }
        public DateTime JoinDate { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required]
        public bool IsMarried { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth  { get; set; }
        public bool Sex { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string City { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string ImageUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public int? fbid { get; set; }

    }
}
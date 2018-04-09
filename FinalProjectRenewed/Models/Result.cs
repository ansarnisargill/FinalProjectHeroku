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
        public string HistoryOfIllness { get; set; }
        public string Education { get; set; }
        public string Religion { get; set; }
        public bool SexuallyActive { get; set; }
    }
}
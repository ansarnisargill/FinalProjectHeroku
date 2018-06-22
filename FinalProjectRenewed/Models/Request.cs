using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int? SessionID { get; set; }
        public Session session { get; set; }
        public int? PsychologistID { get; set; }
        public Psychologist psychologist { get; set; }
        public int? UserID { get; set; }
        public User user { get; set; }
        public int? ResultID { get; set; }
        public Result result { get; set; }
        public bool? Accepted { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProject.Models
{
    public class Session
    {
        public int ID { get; set; }
        public DateTime SessionDate { get; set; }
        public bool Status { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan StartingTime { get; set; }
        [DataType(DataType.Time)]
        public TimeSpan EndingTime { get; set; } 
        public int PsychologistID { get; set; }
        public Psychologist psychologist { get; set; }
        
    }
}
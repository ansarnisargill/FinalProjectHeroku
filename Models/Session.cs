using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FinalProjectHeroku.Models
{
    public class Session
    {
        public int ID { get; set; }
        public DateTime SessionDate { get; set; }
        public bool Status { get; set; }
        [DataType(DataType.Time)]
        public DateTime StartingTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndingTime { get; set; } 
        public int PsychologistID { get; set; }
        public Psychologist psychologist { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class PsySetting
    {
        public int ID { get; set; }
        public int PsychologistID { get; set; }
        public Psychologist psychologist { get; set; }
        public int DurationOfAppointment { get; set; }
        public TimeSpan StartOfDay { get; set; }
        public int NumberOfDaysToShow { get; set; }

    }
    
}
namespace FinalProject.Models
{
    public class History
    {
        public int ID { get; set; }
        public int PsychologitsID { get; set; }
        public Psychologist psychologist { get; set; }
        public int UserID { get; set; }
        public User user { get; set; }
        public int AppointmentID { get; set; }
        public Appointment appointment { get; set; }
        public string Description {get; set;}
    }
}
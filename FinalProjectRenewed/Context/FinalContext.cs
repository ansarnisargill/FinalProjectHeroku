using FinalProject.Models;
using System.Data.Entity;

namespace FinalProject.Context {
    public class FinalContext : DbContext {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Psychologist> Psychologists { get; set; }
        public DbSet<PsyType> PsyTypes { get; set; }
        public DbSet<Result> Results{get; set;}
        public DbSet<History> Histories{get; set;}
        public DbSet<Request> Requests{get; set;}
        public DbSet<Appointment> Appointments{get; set;}
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<PsySetting> PsySettings { get; set; }

       
       
    }
}
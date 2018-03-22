using FinalProjectHeroku.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProjectHeroku.Data
{
    public class FinalContext:DbContext
    {
        public DbSet<Student> Students{get;set;}
        public FinalContext(DbContextOptions<FinalContext> options) : base(options)
        {

        }
    }
}
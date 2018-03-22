using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinalProjectHeroku.Data;
using FinalProjectHeroku.Models;

namespace FinalProject.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly FinalProjectHeroku.Data.FinalContext _context;

        public IndexModel(FinalProjectHeroku.Data.FinalContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.ToListAsync();
        }
    }
}

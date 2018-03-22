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
    public class DetailsModel : PageModel
    {
        private readonly FinalProjectHeroku.Data.FinalContext _context;

        public DetailsModel(FinalProjectHeroku.Data.FinalContext context)
        {
            _context = context;
        }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);

            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}

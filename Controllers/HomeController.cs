using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using System.Web;



namespace FinalProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
 [HttpGet]
        public ActionResult Chat()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Chat(string answer, int a)
        {
            
            
          //  if (a == 88)
           // {
           //     a = 0;
           //     Session["q"] = a;
           // }
           // else
           // {
           //     a = (int)Session["q"] + 1;
           //     Session["q"] = a;
           // }


            string[] questions =
            {
                "Hey! This is your bot! I will conduct your test!",
                "Whats Your Name?",
                "How Old are you?",
                "Whats your qualification?",
                "Ok! That was all for today!"
            };


            return Json(new { message = questions[(int)a] });
        
        
    }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

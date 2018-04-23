using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using FinalProjectRenewed;
using FinalProject.Context;

namespace FinalProjectRenewed.Controllers
{
    public class HomeController : Controller
    {
        private FinalContext db = new FinalContext();
        Dictionary<int, string> chatQuestions = new Dictionary<int, string>();
        Dictionary<int, string> Answers = new Dictionary<int, string>();

       
           
        
        public ActionResult Index()
        {
          
            return View();
        }
        [HttpGet]
        public ActionResult Chat()
        {
            if( Session["type"]==null || Session["type"].ToString()!="user")

            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
               
            }

            return View();
            
        }
        [HttpPost]
        public ActionResult Chat(string answer, int a)
        {
            chatQuestions.Add(0, "Ok! That was all for today!");
            chatQuestions.Add(1, "Hey!This is your bot!I will conduct your test!");
            chatQuestions.Add(2, "Whats your qualification?");
            chatQuestions.Add(3, "Ok! That was all for today!");
            if (answer != null && answer != "" && a != 1)
            {
                Answers.Add(a, answer);
                a++;
            }

            if (a == 1)
            {
                return Json(new { message = chatQuestions[a], key = 2 });
            }
            if (!chatQuestions.Keys.Contains(a))
            {
                RedirectToAction("Index");
            }
            return Json(new { message = chatQuestions[a], key = a });


        }
        public ActionResult PsyList()
        {
            var psychologists = db.Psychologists.Include( p => p.psyType);
            return View(psychologists.ToList());
            
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
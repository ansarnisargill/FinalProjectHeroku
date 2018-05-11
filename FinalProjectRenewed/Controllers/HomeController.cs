using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using FinalProjectRenewed;
using FinalProject.Context;
using FinalProject.Models;
using FinalProject.Models.ViewModels;

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
            //if( Session["type"]==null || Session["type"].ToString()!="user")

            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
               
            //}

            return View();
            
        }
        [HttpPost]
        public ActionResult Chat(string answer, int a)
        {
            chatQuestions.Add(0, "Hey! This is your DoctorBB! I am going to conduct your Depression Analysis Survey,<br> This would take only about 5 minutes! Are We ready to go?");

            chatQuestions.Add(1, "Ok! First Of all, tell me about your home environment?");
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
        //These two methods control the login process of the user. Here user means client, for psychologist, there is another controller

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login( User match)
        {
            var data = db.Users.Where(m => m.Email == match.Email && m.Password == match.Password).FirstOrDefault();
            if (data != null)
            {
                Session["name"] = match.Name;
                Session["type"] = "user";
                Session["id"] = match.ID;
                return RedirectToAction("Chat", "Home");
            }
            else
            {
                ViewBag.alert = "Email Or Password Is Wrong!!";
                return View();
            }
        }
        //These two methods are responsible for adding client, they are basically creeate methods

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(UserSignUpViewModel userv )
        {
            try
            {
                userv.user.JoinDate = DateTime.Now;
                db.Users.Add(userv.user);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(userv.user);

            }
            
           
            

            
        }
    }
}
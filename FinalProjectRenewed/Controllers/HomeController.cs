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
using System.IO;

namespace FinalProjectRenewed.Controllers
{
    public class HomeController : Controller
    {
        private FinalContext db = new FinalContext();
        Dictionary<int, string> chatQuestions = new Dictionary<int, string>();
        Dictionary<int, string> Answers = new Dictionary<int, string>();

       
        public bool IsUser()
        {
            if((string)Session["Type"]!= null && (string)Session["Type"]=="user")
            {
                return true;
            }
            return false;
        }   
        
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
            match.Password = match.Password.GetHashCode().ToString();
            var data = db.Users.Where(m => m.Email == match.Email && m.Password == match.Password).FirstOrDefault();
            if (data != null)
            {
                
                Session["Type"] = "user";
                Session["User"] = data;
                return RedirectToAction("ChatInit", "Home");
            }
            else
            {
                ViewBag.alert = "Email Or Password Is Wrong!!";
                return View();
            }
        }
        //These two methods are responsible for adding client, they are basically creeate methods
        
        [HttpGet]
        public ActionResult Signup()
        {
            User user = new User();
            if ((User)TempData["user"] != null)
            {
                user = (User)TempData["user"];
            }
           

                ViewBag.user = user;
            

            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserSignUpViewModel userv )
        {
            try
            {
                User newUser = userv.user;
                newUser.Password = newUser.Password.GetHashCode().ToString();
                string fileName = Path.GetFileNameWithoutExtension(userv.image.FileName);
                string extension = Path.GetExtension(userv.image.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string directory = "~/Images/";
                string imageUrl = directory + fileName;
                newUser.ImageUrl = imageUrl;
                fileName = Path.Combine(Server.MapPath(directory), fileName);
                userv.image.SaveAs(fileName);
                newUser.JoinDate = DateTime.Now;
                
                db.Users.Add(newUser);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
              
                return View(userv.user);

            }
            
           
            

            
        }
        public ActionResult test()
        {
            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:52673/Home/test2");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"user\":\"test\"," +
                              "\"password\":\"bla\"}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
             string result;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return Content(result);
        }
        public ActionResult test2()
        {
            HttpStatusCodeResult ht = new HttpStatusCodeResult(200);
            return ht ;
        }
        public ActionResult IsLoggedIn(int? fbid,string email, string name)
        {
            var user = db.Users.Where(c => c.fbid == fbid).FirstOrDefault();
            if (user != null)
            {
               
                Session["Type"] = "user";
                Session["User"] = user;
               return RedirectToAction("ChatInit");
            }
            else
            {
                User newuser = new User();
                newuser.fbid = fbid;
                newuser.Email = email;
                newuser.Name = name;

                TempData["user"]=newuser;
                return RedirectToAction("Signup");
            }
            
        }
        public ActionResult ChatInit()
        {
            return View();
        }
    }
}
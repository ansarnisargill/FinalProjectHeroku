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


        int total_score = new int();
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
            if (IsUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("NotAuthorized");
            }
           
            
        }
        [HttpPost]
        public ActionResult Chat(string answer, int a)
        {
            ChatSystem c = new ChatSystem();
            if (answer != null && answer != "" && a != 1)
            {
               
               c.Answers.Add(a, answer);
                a++;
            }

            if (a == 1)
            {
                return Json(new { message = c.chatQuestions[a], key = 2 });
            }
            if (!c.chatQuestions.Keys.Contains(a))
            {
                RedirectToAction("Index");
            }
            return Json(new { message = c.chatQuestions[a], key = a });


        }
        
        public ActionResult BDIChat()
        {
            if (IsUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("NotAuthorized");
            }


        }
        [HttpPost]
        public ActionResult BDIChat(string answer, int a)
        {
            ChatSystem c = new ChatSystem();

            if (answer != null && answer != "" && a != 0)
            {
                
                int score = new int();
                answer = answer.ToLower();
                if(answer.Contains("first")|| answer.Contains("1st")|| answer.Contains("1")|| answer.Contains("blue") || answer.Contains("blu"))
                {
                    score = 0;
                }
                else if (answer.Contains("2") || answer.Contains("second") || answer.Contains("2nd")  || answer.Contains("brown") )
                {
                    score = 1;
                }
                else if(answer.Contains("3") || answer.Contains("third") || answer.Contains("3rd")  || answer.Contains("purple") || answer.Contains("purpel") )
                {
                    score = 2;
                }
                else if (answer.Contains("4") || answer.Contains("four") || answer.Contains("4th") || answer.Contains("green") || answer.Contains("parrot")||answer.Contains("teal"))
                {
                    score = 3;
                }
                else
                {

                }
                total_score = total_score + score;
                c.BDIAnswers.Add(a, score);
                a++;
            }

            if (a == 0)
            {
                return Json(new { message = c.BDIQuesstions[a], key = 1 });
            }
            if (!c.BDIQuesstions.Keys.Contains(a))
            {
                RedirectToAction("Index");
            }
            var messages = c.BDIQuesstions[a];
            return Json(new { message = messages[0], key = a,option1=messages[1], option2 = messages[2], option3 = messages[3], option4 = messages[4] });


        }
        public ActionResult PsyList()
        {
            if (IsUser())
            {
                var psychologists = db.Psychologists.Include(p => p.psyType);
                return View(psychologists.ToList());
            }
            else
            {
                return RedirectToAction("NotAuthorized");
            }

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
            if (IsUser())
            {
                return View();
            }
            else
            {
                return RedirectToAction("NotAuthorized");
            }

        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult NotAuthorized()
        {
            return View();
        }
    }
}
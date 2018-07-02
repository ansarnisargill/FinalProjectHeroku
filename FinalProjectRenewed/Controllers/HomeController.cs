using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using FinalProjectRenewed;
using System.Net.Http;
using FinalProject.Context;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using System.IO;
using System.Web.Helpers;
using Newtonsoft.Json;

namespace FinalProjectRenewed.Controllers
{
    public class HomeController : Controller
    {
        private FinalContext db = new FinalContext();
        private static readonly WebClient client = new WebClient();

        public static int total_score = new int();
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
                Result rs = new Result();
                Session["Result"] = rs;
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
            if (a == 11)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (answer != null && answer != "" )
            {
                bool val = SaveChatAnswers(answer, a);
                if (a == 0&& answer=="first")
                {
                    return Json(new { message = c.chatQuestions[a], key = 0 });
                }
                else if (val)
                {
                    a++;
                    return Json(new { message = c.chatQuestions[a], key = a });

                }
                else
                {
                    return Json(new { message = "I am sorry! I can't understand your answer,<br> so read question again and answer!<br>" + c.chatQuestions[a], key = a });
                }
            }
            return Json(new { message = "Sorry! You sent an empty answer! reply again!", key = a });

            //if (a == 0)
            //{
            //    return Json(new { message = c.chatQuestions[a], key = 1 });
            //}




        }
        public bool SaveChatAnswers( string answer, int key)
        {
            Result rs = (Result)Session["Result"];
            if (key == 0)
            {
                return true;
            }
            else if (key == 1)
            {
                int val=CountHelper(answer);
                if (val == 0)
                {
                    return false;
                }
                else
                {
                    rs.HomeEnvironmentStrictness = val;
                    Session["Result"] = rs;
                    return true;
                }
                
            }
            else if (key == 2)
            {
                
                if (answer=="")
                {
                    return false;
                }
                else
                {
                    rs.Education = answer;
                    Session["Result"] = rs;
                    return true;
                }

            }
            else if (key == 3)
            {
                bool val = YesNoHelper(answer);
                rs.SexuallyActive = val;
                Session["Result"] = rs;
                return true;
                

            }
            else if (key == 4)
            {
                bool val = YesNoHelper(answer);
                rs.IsReligios = val;
                Session["Result"] = rs;
                return true;
                

            }
            else if (key == 5)
            {
                bool val = YesNoHelper(answer);
                
             
                rs.EmploymentStatus = val;
                Session["Result"] = rs;
                return true;
               

            }
            else if (key == 6)
            {
                int val = CountHelper(answer);
                if (val == 0)
                {
                    return false;
                }
                else
                {
                    rs.JobSatisfaction = val;
                    Session["Result"] = rs;
                    return true;
                }

            }
            else if (key == 7)
            {
                int val = CountHelper(answer);
                if (val == 0)
                {
                    return false;
                }
                else
                {
                    rs.Siblings = val;
                    Session["Result"] = rs;
                    return true;
                }

            }
            else if (key == 8)
            {
                int val = CountHelper(answer);
                if (val == 0)
                {
                    return false;
                }
                else
                {
                    rs.BirthOrder = val;
                    Session["Result"] = rs;
                    return true;
                }

            }
            else if (key == 9)
            {
                    bool val = YesNoHelper(answer);
                    rs.Insomnia = val;
                    Session["Result"] = rs;
                    return true;
                

            }
            else if (key == 10)
            {
                if (answer == "")
                {
                    return false;
                }
                else
                {
                    rs.Illness = answer;
                    Session["Result"] = rs;
                    return true;
                }
               
                   
                

            }
            else
            {
                return false;
            }
        }
        public int CountHelper(string s)
        {
            s = s.ToLower();
            int toReturn = new int();
            if(int.TryParse(s,out toReturn))
            {
                return toReturn;
            }
            else if (s.Contains("1") || s.Contains("1st") || s.Contains("first") || s.Contains("one"))
            {
                return 1;
            }
            else if (s.Contains("2") || s.Contains("2nd") || s.Contains("second") || s.Contains("two"))
            {
                return 2;
            }
            else if (s.Contains("3") || s.Contains("3rd") || s.Contains("third") || s.Contains("three"))
            {
                return 3;
            }
            else if(s.Contains("4") || s.Contains("4th") || s.Contains("fourth") || s.Contains("four"))
            {
                return 4;
            }
            else if(s.Contains("5") || s.Contains("5th") || s.Contains("fifth") || s.Contains("five"))
            {
                return 5;
            }
            else if(s.Contains("6") || s.Contains("6th") || s.Contains("sixth") || s.Contains("six"))
            {
                return 6;
            }
            else if(s.Contains("7") || s.Contains("7th") || s.Contains("seventh") || s.Contains("seven"))
            {
                return 7;
            }
            else if(s.Contains("8") || s.Contains("8th") || s.Contains("eighth") || s.Contains("eight"))
            {
                return 8;
            }
            else if(s.Contains("9") || s.Contains("9th") || s.Contains("ninth") || s.Contains("nine"))
            {
                return 9;
            }
            else if(s.Contains("10") || s.Contains("10th") || s.Contains("tenth") || s.Contains("ten"))
            {
                return 10;
            }
            else
            {
                return 0;
            }
        }
        public bool YesNoHelper(string s)
        {
            s = s.ToLower();
            if (s.Contains("yeah") || s.Contains("yes") || s.Contains("ok") || s.Contains("sure") || s.Contains("go"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult BDIChat( bool? FirstPartDone)
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
                if(int.TryParse(answer,out score))
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

            if (a == 22)
            {
                User u1 = (User)Session["User"];
                Result rs = (Result)Session["Result"];
                rs.DateOfTest = DateTime.Now;
                rs.BDIScore = total_score;
                rs.UserID = u1.ID;
                db.Results.Add(rs);
                db.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                
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
                var tocheck = db.Users.Where(c => c.Email == userv.user.Email).FirstOrDefault();
                if (tocheck != null)
                {
                    throw new Exception("The email address you entered already exists!");

                }
                User newUser = userv.user;
                newUser.Password = newUser.Password.GetHashCode().ToString();
                string fileName = Path.GetFileNameWithoutExtension(userv.image.FileName);
                string extension = Path.GetExtension(userv.image.FileName);
                if(extension==".jpg" || extension==".png"||extension==".jpeg" || extension == ".gif")
                {
                    fileName = DateTime.Now.ToString("yymmssfff") + extension;
                    string directory = "~/Images/";
                    string imageUrl = directory + fileName;
                    newUser.ImageUrl = imageUrl;
                    fileName = Path.Combine(Server.MapPath(directory), fileName);
                    userv.image.SaveAs(fileName);
                }
                else
                {
                    throw new Exception("Picture you have selected is not in supported formate");
                }
               
                newUser.JoinDate = DateTime.Now;
                newUser.IsPhotoSignup = false;
                db.Users.Add(newUser);
                db.SaveChanges();
                Session["User"] = newUser;
                return RedirectToAction("PicSignup", "Home");

            }
            catch(Exception ex)
            {

                return Content(ex.ToString());

            }
            
          
            

            
        }
        public ActionResult PicSignup()
        {
            return View();
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
        public ActionResult ChatInit(string first)
        {
            if (IsUser())
            {
                if (first != null && first != "" && first=="yes")
                {
                    ViewBag.done = "fdone";
                }
                else
                {
                    ViewBag.done = "not";
                }
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
        public ActionResult Show_Sessions(int id)
        {
            
            DateTime today = DateTime.Now.Date;
            var sessions = db.Sessions.Where(c => c.PsychologistID == id && c.Active==true && c.SessionDate >= today);
            return View( sessions);
        }
        public ActionResult Confirm_Request(int id)
        {
            var session_details = db.Sessions.Where(c => c.ID == id).Include(c => c.psychologist).First();


            return View(session_details);
        }
        [ActionName("Confirmed")]
        public ActionResult Confirm_Request(int psychologist_id,int session_id)
        {
            Request rq = new Request();
            User u1 = (User)Session["User"];
            var result_id = db.Results.Where(c => c.UserID == u1.ID).Select(c=>c.ID).Max();
            rq.UserID = u1.ID;
            rq.ResultID = result_id;
            rq.PsychologistID = psychologist_id;
            rq.SessionID = session_id;
            db.Requests.Add(rq);
            db.SaveChanges();
            


            return Content("Ok Your Request has been sent!");
        }
        public ActionResult TwitterAnalysis()
        {
            return View();
        }
        [HttpPost]
       
        public ActionResult TwitterAnalysis(string username)
        {
            if (username != "")
            {
                WebClient tw = new WebClient();
                tw.Headers[HttpRequestHeader.ContentType] = "application/json";
                tw.Headers[HttpRequestHeader.Accept] = "application/json";
                tw.BaseAddress = "http://localhost:8080/twitter";
                var data = new { username= username};
                var dataSer = JsonConvert.SerializeObject(data,Formatting.Indented);
                var res = tw.UploadString(tw.BaseAddress, "POST",dataSer );
                return Json(new {res });

            }
            return View();
        }
        public ActionResult PhotoSignup()
        {
           

            return View();
        }
        public ActionResult Notification(string type, int id)
        {
            if(type== "reqacc")
            {


                TempData["notif"] = id;
                return RedirectToAction("RequestAcc");
            }
            else
            {
                var rq = db.Requests.Where(c => c.ID == id).FirstOrDefault();
                TempData["notif"] = rq;
                return RedirectToAction("RequestRej");
            }
            
        }
        public ActionResult RequestAcc()
        {
            int id = (int)TempData["notif"];
           Appointment ap = db.Appointments.Where(c => c.ID == id).Include(c => c.psychologist).Include(c => c.session).FirstOrDefault();

            return View(ap);
        }
        public ActionResult RequestRej()
        {
            int  id = (int)TempData["notif"];
            var rq = db.Requests.Where(c => c.ID == id).Include(c => c.psychologist).Include(c => c.session).FirstOrDefault();

            return View(rq);
        }
        public ActionResult PhotoLogin()
        {
            return View();
        }
        public ActionResult LoginByPic(int id)
        {
            return View();
        }
    }
}
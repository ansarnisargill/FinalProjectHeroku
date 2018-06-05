using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.Context;
using FinalProject.Models;
using FinalProject.Models.ViewModels;
using System.IO;


namespace FinalProjectRenewed.Controllers
{
    public class PsychologistsController : Controller
    {
        private FinalContext db = new FinalContext();

        // GET: Psychologists


        public bool IsPsychologist()
        {
            if (Session["Type"] != null && Session["Type"].ToString() == "Psy")
            {
                return true;
            }
            return false;
        }
        public ActionResult Index()
        {
            if (IsPsychologist())
            {
                var psychologists = db.Psychologists.Include(p => p.psyType);
            return View(psychologists.ToList());
            }
            else
            {
                return View("NotAuthorize");
             }

}

        // GET: Psychologists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Psychologist psychologist = db.Psychologists.Find(id);
            if (psychologist == null)
            {
                return HttpNotFound();
            }
            return View(psychologist);
        }

        // GET: Psychologists/Create
        public ActionResult Create()
        {
            ViewBag.PsyTypeID = new SelectList(db.PsyTypes, "ID", "Name");
            return View();
        }

        // POST: Psychologists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PsyViewModel psyViewModel)
        {
            Psychologist newPsy = psyViewModel.psychologist;
            newPsy.Password = newPsy.Password.GetHashCode().ToString();
            string fileName = Path.GetFileNameWithoutExtension(psyViewModel.image.FileName);
            string extension = Path.GetExtension(psyViewModel.image.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string directory = "~/Images/";
            string imageUrl = directory + fileName;
            newPsy.ImageAddress = imageUrl;
            fileName = Path.Combine(Server.MapPath(directory), fileName);
            psyViewModel.image.SaveAs(fileName);
            newPsy.JoinDate = DateTime.Now;

            db.Psychologists.Add(newPsy);
            db.SaveChanges();
            return RedirectToAction("Login");

        }

        // GET: Psychologists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var psychologist = (Psychologist)db.Psychologists.Find(id);
            if (psychologist == null)
            {
                return HttpNotFound();
            }
            ViewBag.PsyTypeID = new SelectList(db.PsyTypes, "ID", "Name", psychologist.PsyTypeID);
            return View(psychologist);
        }

        // POST: Psychologists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Username,Password,Name,PsyTypeID,Education,Experience,RegistrationNo,CNIC,MobileNo,Sex,JoinDate")] Psychologist psychologist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(psychologist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PsyTypeID = new SelectList(db.PsyTypes, "ID", "Name", psychologist.PsyTypeID);
            return View(psychologist);
        }

        // GET: Psychologists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Psychologist psychologist = db.Psychologists.Find(id);
            if (psychologist == null)
            {
                return HttpNotFound();
            }
            return View(psychologist);
        }

        // POST: Psychologists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Psychologist psychologist = db.Psychologists.Find(id);
            db.Psychologists.Remove(psychologist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult psycard()
        {
            var list = db.Psychologists.ToList();
            return View(list);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Psychologist match)
        {
            var pass = match.Password.GetHashCode().ToString();
            Psychologist data = db.Psychologists.Where(m => m.Email == match.Email && m.Password == pass).FirstOrDefault();
            if (data != null)
            {
                Session["Psy"] = data;
                Session["Type"] = "Psy";
                return RedirectToAction("Index", "Psychologists");
            }
            else
            {
                ViewBag.alert = "Email Or Password Is Wrong!!";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public ActionResult Appointment()
        {
            var data = db.Appointments.ToList();
            return View(data);
        }
        public ActionResult History()
        {
            var data = db.Histories.ToList();
            return View(data);
        }
        public ActionResult Requests()
        {
            var data = db.Requests.ToList();
            return View(data);
        }
        public ActionResult Schedual()
        {
            if (IsPsychologist())
            {
                Psychologist ps = (Psychologist)Session["Psy"];
                var dateOfDay = DateTime.Now.Date;

                int numberOfDays = 3; //i will set it as setting parameter
                int durationOfSession = 240;

                for (int i = 0; i < numberOfDays; i++)
                {
                    DateTime forWhere = dateOfDay.AddDays(i);


                    var checkDayEXIST = db.Sessions.Where(c => c.SessionDate == forWhere &&  c.PsychologistID==ps.ID).FirstOrDefault();
                    if(checkDayEXIST == null)
                    {
                        DateTime today = new DateTime();
                        var numberOfSesssions = 1440 / durationOfSession;
                        int valueOfTimeToAdd = new int();
                        for (int j = 0; j < numberOfSesssions; j++)
                        {
                            var start = today.AddMinutes(valueOfTimeToAdd).TimeOfDay;
                            var end = today.AddMinutes(valueOfTimeToAdd + durationOfSession).TimeOfDay;
                            valueOfTimeToAdd = valueOfTimeToAdd + durationOfSession;
                            Session s1 = new Session();
                            s1.SessionDate = dateOfDay.AddDays(i).Date;
                            s1.StartingTime = start;
                            s1.EndingTime = end;
                            s1.Status = false;
                            s1.Active = false;
                           
                            s1.PsychologistID = ps.ID;
                            db.Sessions.Add(s1);
                            db.SaveChanges();
                        }
                    }
                   


                }
                var sessionData = db.Sessions.Where(c => c.SessionDate >= dateOfDay && c.PsychologistID==ps.ID);
                ViewBag.sessions = sessionData;
                return View();
            }
            return View("NotAuthorize");

        }
    
        [HttpPost]
        public ActionResult Schedual(int id)
        {
            Session s = db.Sessions.Where(c => c.ID == id).Single();
            bool toSend = s.Active;
            if (s.Active == false)
            {
                s.Active = true;
            }
            else
            {
                s.Active = false;
            }
           
            db.SaveChanges();

            return Json(new { message = toSend.ToString()});
        }
        public ActionResult NotAuthorize()
        {
            return View();
        }
    }
}

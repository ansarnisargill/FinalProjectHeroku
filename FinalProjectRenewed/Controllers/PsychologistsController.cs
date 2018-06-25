﻿using System;
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
            Psychologist ps = (Psychologist)Session["Psy"];
            DateTime dt = DateTime.Now.Date;
            var data = db.Appointments
                .Where(a=> a.Status==false && a.session.SessionDate>= dt && a.PsychologistID==ps.ID && a.Missed==false)
                .Include(a=>a.session)
                .Include(a=>a.user)
                .Include(a=>a.result);
            return View(data);
        }
        public ActionResult History()
        {
            var data = db.Histories.ToList();
            return View(data);
        }
        public ActionResult Requests()
        {
            Psychologist ps = (Psychologist)Session["Psy"];

            var data = db.Requests.Where(c=>c.PsychologistID==ps.ID&& c.Accepted==null).Include(c=>c.session).Include(c=>c.user).ToList();
            return View(data);
        }
        public ActionResult AcceptRequest(int id)
        {
            if (IsPsychologist())
            {
                Appointment ap = new Appointment();
                Request rq = db.Requests.Where(c => c.ID == id).Include(c => c.session).SingleOrDefault();
                ap.PsychologistID = rq.PsychologistID;
                ap.Rating = 0;
                ap.ResultID = rq.ResultID;
                ap.SessionID = rq.SessionID;
                ap.Status = false;
                ap.Missed = false;
                ap.UserID = rq.UserID;
                rq.Accepted = true;
                db.Appointments.Add(ap);
                db.SaveChanges();
                Notification nt = new Notification();
                nt.Link = Url.Content("~") + "Home/Notification?type=reqacc&id=" + ap.ID;
                nt.Type = "User";
                nt.Text = "Your Appointment request has been Accepted";
                nt.UserID = (int)rq.UserID;
                nt.Status = false;
                db.Notifications.Add(nt);
                db.SaveChanges();
                TimeSpan ts = rq.session.StartingTime;
                List<Request> rqlist = db.Requests.Where(c => c.PsychologistID == ap.PsychologistID && c.session.StartingTime == ts && c.Accepted==null
                ).ToList();
                foreach (Request r in rqlist)
                {

                    RejectRequestHelper(r.ID);
                }


                return View();
            }
            else
            {
                return RedirectToAction("NotAuthorize");
            }
            
        }
        public ActionResult RejectRequest(int id)
        {

            RejectRequestHelper(id);
            return View();
        }
        private bool RejectRequestHelper(int id)
        {
            Request rq = db.Requests.Where(c => c.ID == id).SingleOrDefault();
            rq.Accepted = false;
            
            Notification nt = new Notification();
            nt.Link = Url.Content("~") + "Home/Notification?type=reqrej&id="+rq.ID;
            nt.Type = "User";
            nt.UserID = (int)rq.UserID;
            nt.Status = false;
            nt.Text = "Your Appointment Request has been rejected";
            db.Notifications.Add(nt);
            db.SaveChanges();
            return true;
        }
        public ActionResult Schedual()
        {
            if (IsPsychologist())
            {
                Psychologist ps = (Psychologist)Session["Psy"];
                var dateOfDay = DateTime.Now.Date;

                int numberOfDays = 3; //i will set it as setting parameter
                int durationOfSession = 60;
                TimeSpan dayStart = new TimeSpan(00, 00, 00);
                var settings = db.PsySettings.Where(c => c.PsychologistID == ps.ID).FirstOrDefault();
                if (settings != null)
                {
                    numberOfDays = settings.NumberOfDaysToShow;
                    durationOfSession = settings.DurationOfAppointment;
                    dayStart = settings.StartOfDay;
                }
                for (int i = 0; i < numberOfDays; i++)
                {
                    DateTime forWhere = dateOfDay.AddDays(i+1);


                    var checkDayEXIST = db.Sessions.Where(c => c.SessionDate == forWhere &&  c.PsychologistID==ps.ID).FirstOrDefault();
                    if(checkDayEXIST == null)
                    {
                        DateTime today = new DateTime();
                        var numberOfSesssions = 1440 / durationOfSession;
                        int valueOfTimeToAdd = dayStart.Minutes;
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
                ViewBag.NoOfDays = numberOfDays;
                return View();
            }
            return View("NotAuthorize");

        }
        public ActionResult AppointmentDetail(int id)
        {
            var appointment = db.Appointments.Where(c => c.ID == id).Include(c=>c.session).Include(c=>c.result).Include(c=>c.user).FirstOrDefault();
            return View(appointment);
        }
        public ActionResult RequestDetail(int id)
        {
            var rq = db.Requests.Where(c => c.ID == id).Include(c => c.session).Include(c => c.result).Include(c => c.user).FirstOrDefault();
            return View(rq);
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
        public ActionResult PerformAppointment(int id)
        {
            var ap = db.Appointments.Where(c => c.ID == id).SingleOrDefault();
         
            db.SaveChanges();
            return View(ap);
        }
        [HttpPost]
        public ActionResult PerformAppointment(int apid,int userid,string description)
        {
            Psychologist ps = (Psychologist)Session["Psy"];
            History hs = new History();
            hs.PsychologitsID = ps.ID;
            hs.UserID = userid;
            hs.AppointmentID = apid;
            hs.Description = description;
            db.Histories.Add(hs);
            var ap = db.Appointments.Where(c => c.ID == apid).SingleOrDefault();
            ap.Missed = false;
            ap.Status = true;
            Notification nt = new Notification();
            nt.Link = Url.Content("~") + "Home/Notification?type=apdone&id=" + ap.ID;
            nt.Type = "User";
            nt.Text = "Your Appointment has been conducted successfully!";
            nt.UserID =(int) ap.UserID;
            nt.Status = false;
            db.Notifications.Add(nt);
            db.SaveChanges();
            return Json( new { message = "sent" });
            
        }
        public ActionResult MissedAppointment(int id)
        {
            var ap = db.Appointments.Where(c => c.ID == id).SingleOrDefault();
            ap.Missed = true;
            Notification nt = new Notification();
            nt.Link = Url.Content("~") + "Home/Notification?type=apmissed&id=" + ap.ID;
            nt.Type = "User";
            nt.Text = "Your Missed your Appointment!";
            nt.UserID = (int)ap.UserID;
            nt.Status = false;
            db.Notifications.Add(nt);
            db.SaveChanges();
            return View();
        }
        public ActionResult DescriptionSaved()
        {
            return View();
        }
        public ActionResult PsychologistSettings()
        {
            return View();
        }
    }
}

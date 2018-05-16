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

namespace FinalProjectRenewed.Controllers
{
    public class PsychologistsController : Controller
    {
        private FinalContext db = new FinalContext();

        // GET: Psychologists


        public bool IsPsychologist()
        {
            if ( Session["Type"]!=null && Session["Type"].ToString() == "Psy")
            {
                return true;
            }
            return false;
        }
        public ActionResult Index()
        {
            //if (IsPsychologist())
            //{
                var psychologists = db.Psychologists.Include(p => p.psyType);
                return View(psychologists.ToList());
            //}
            //else
            //{
              //  string a = HttpStatusCode.Forbidden.ToString();
                //return Content( a );
            //}
           
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
        public ActionResult Create([Bind(Include = "ID,Username,Password,Name,PsyTypeID,Education,Experience,RegistrationNo,CNIC,MobileNo,Sex,JoinDate,Email")] Psychologist psychologist)
        {
            if (ModelState.IsValid)
            {
                db.Psychologists.Add(psychologist);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.PsyTypeID = new SelectList(db.PsyTypes, "ID", "Name", psychologist.PsyTypeID);
            return View(psychologist);
        }

        // GET: Psychologists/Edit/5
        public ActionResult Edit(int? id)
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
            var data = db.Psychologists.Where(m => m.Email == match.Email && m.Password == match.Password).FirstOrDefault();
            if (data != null)
            {
                Session["name"] = data.Name;
                Session["Type"] = "Psy";
                Session["pic"] = data.ImageAddress;
                return RedirectToAction("Index", "Psychologists", Session["name"]);
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

                

                return View();

            }
            return Content( HttpStatusCode.Forbidden.ToString());
        }
    }
}

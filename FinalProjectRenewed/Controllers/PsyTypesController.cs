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
    public class PsyTypesController : Controller
    {
        private FinalContext db = new FinalContext();

        // GET: PsyTypes
        public ActionResult Index()
        {
            return View(db.PsyTypes.ToList());
        }

        // GET: PsyTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PsyType psyType = db.PsyTypes.Find(id);
            if (psyType == null)
            {
                return HttpNotFound();
            }
            return View(psyType);
        }

        // GET: PsyTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PsyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Rating")] PsyType psyType)
        {
            if (ModelState.IsValid)
            {
                db.PsyTypes.Add(psyType);
                db.SaveChanges();
                return View();
            }

            return View(psyType);
        }

        // GET: PsyTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PsyType psyType = db.PsyTypes.Find(id);
            if (psyType == null)
            {
                return HttpNotFound();
            }
            return View(psyType);
        }

        // POST: PsyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Rating")] PsyType psyType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(psyType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(psyType);
        }

        // GET: PsyTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PsyType psyType = db.PsyTypes.Find(id);
            if (psyType == null)
            {
                return HttpNotFound();
            }
            return View(psyType);
        }

        // POST: PsyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PsyType psyType = db.PsyTypes.Find(id);
            db.PsyTypes.Remove(psyType);
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
    }
}

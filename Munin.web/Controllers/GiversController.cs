using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Munin.DAL.Models;

namespace Munin.web.Controllers
{
    public class GiversController : Controller
    {
        private ILABNew2Entities db = new ILABNew2Entities();

        // GET: Givers
        public ActionResult Index()
        {
            var giver = db.Giver.Include(g => g.Journaler);
            return View(giver.ToList());
        }

        // GET: Givers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giver giver = db.Giver.Find(id);
            if (giver == null)
            {
                return HttpNotFound();
            }
            return View(giver);
        }

        // GET: Givers/Create
        public ActionResult Create()
        {
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb");
            return View();
        }

        // POST: Givers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GiverID,Journal,Navn,Att,Adresse,Postnr,By,AflevDato,Registrator,Note,JournalID")] Giver giver)
        {
            if (ModelState.IsValid)
            {
                db.Giver.Add(giver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", giver.JournalID);
            return View(giver);
        }

        // GET: Givers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giver giver = db.Giver.Find(id);
            if (giver == null)
            {
                return HttpNotFound();
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", giver.JournalID);
            return View(giver);
        }

        // POST: Givers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GiverID,Journal,Navn,Att,Adresse,Postnr,By,AflevDato,Registrator,Note,JournalID")] Giver giver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", giver.JournalID);
            return View(giver);
        }

        // GET: Givers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Giver giver = db.Giver.Find(id);
            if (giver == null)
            {
                return HttpNotFound();
            }
            return View(giver);
        }

        // POST: Givers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Giver giver = db.Giver.Find(id);
            db.Giver.Remove(giver);
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

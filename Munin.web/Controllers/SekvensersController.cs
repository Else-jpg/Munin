using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Munin.web.Models;

namespace Munin.web.Controllers
{
    public class SekvensersController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: Sekvensers
        public ActionResult Index()
        {
            var sekvenser = db.Sekvenser.Include(s => s.Journaler);
            return View(sekvenser.ToList());
        }

        // GET: Sekvensers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sekvenser sekvenser = db.Sekvenser.Find(id);
            if (sekvenser == null)
            {
                return HttpNotFound();
            }
            return View(sekvenser);
        }

        // GET: Sekvensers/Create
        public ActionResult Create()
        {
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb");
            return View();
        }

        // POST: Sekvensers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SekvensID,SekvensNr,Journal,Titel,DateringDag,DateringMrd,DateringAar,Kilde,Interviewer,Klausul,Type,Indlevering,Note,JournalID")] Sekvenser sekvenser)
        {
            if (ModelState.IsValid)
            {
                db.Sekvenser.Add(sekvenser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", sekvenser.JournalID);
            return View(sekvenser);
        }

        // GET: Sekvensers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sekvenser sekvenser = db.Sekvenser.Find(id);
            if (sekvenser == null)
            {
                return HttpNotFound();
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", sekvenser.JournalID);
            return View(sekvenser);
        }

        // POST: Sekvensers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SekvensID,SekvensNr,Journal,Titel,DateringDag,DateringMrd,DateringAar,Kilde,Interviewer,Klausul,Type,Indlevering,Note,JournalID")] Sekvenser sekvenser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sekvenser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", sekvenser.JournalID);
            return View(sekvenser);
        }

        // GET: Sekvensers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sekvenser sekvenser = db.Sekvenser.Find(id);
            if (sekvenser == null)
            {
                return HttpNotFound();
            }
            return View(sekvenser);
        }

        // POST: Sekvensers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sekvenser sekvenser = db.Sekvenser.Find(id);
            db.Sekvenser.Remove(sekvenser);
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

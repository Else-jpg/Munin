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
    public class KortsController : Controller
    {
        private ILABNew2Entities db = new ILABNew2Entities();

        // GET: Korts
        public ActionResult Index()
        {
            var kort = db.Kort.Include(k => k.Journaler);
            return View(kort.ToList());
        }

        // GET: Korts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kort kort = db.Kort.Find(id);
            if (kort == null)
            {
                return HttpNotFound();
            }
            return View(kort);
        }

        // GET: Korts/Create
        public ActionResult Create()
        {
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb");
            return View();
        }

        // POST: Korts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KortID,Kortnr,Journal,Navn,KortType,Propertion,Udgiver,DateringAar,RevAar,Omraade,Depot,Format,Materiale,Indlevering,Note,JournalID")] Kort kort)
        {
            if (ModelState.IsValid)
            {
                db.Kort.Add(kort);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", kort.JournalID);
            return View(kort);
        }

        // GET: Korts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kort kort = db.Kort.Find(id);
            if (kort == null)
            {
                return HttpNotFound();
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", kort.JournalID);
            return View(kort);
        }

        // POST: Korts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KortID,Kortnr,Journal,Navn,KortType,Propertion,Udgiver,DateringAar,RevAar,Omraade,Depot,Format,Materiale,Indlevering,Note,JournalID")] Kort kort)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kort).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", kort.JournalID);
            return View(kort);
        }

        // GET: Korts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kort kort = db.Kort.Find(id);
            if (kort == null)
            {
                return HttpNotFound();
            }
            return View(kort);
        }

        // POST: Korts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kort kort = db.Kort.Find(id);
            db.Kort.Remove(kort);
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

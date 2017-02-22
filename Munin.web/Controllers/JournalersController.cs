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
    public class JournalersController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: Journalers
        public ActionResult Index()
        {
            return View(db.Journaler.ToList());
        }

        // GET: Journalers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journaler journaler = db.Journaler.Find(id);
            if (journaler == null)
            {
                return HttpNotFound();
            }
            return View(journaler);
        }

        // GET: Journalers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Journalers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JournalID,JournalNb,Afleveret,MedieArt,Antal,Regs,Note")] Journaler journaler)
        {
            if (ModelState.IsValid)
            {
                db.Journaler.Add(journaler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(journaler);
        }

        // GET: Journalers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journaler journaler = db.Journaler.Find(id);
            if (journaler == null)
            {
                return HttpNotFound();
            }
            return View(journaler);
        }

        // POST: Journalers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JournalID,JournalNb,Afleveret,MedieArt,Antal,Regs,Note")] Journaler journaler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journaler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(journaler);
        }

        // GET: Journalers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journaler journaler = db.Journaler.Find(id);
            if (journaler == null)
            {
                return HttpNotFound();
            }
            return View(journaler);
        }

        // POST: Journalers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Journaler journaler = db.Journaler.Find(id);
            db.Journaler.Remove(journaler);
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

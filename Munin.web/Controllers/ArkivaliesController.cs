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
    public class ArkivaliesController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: Arkivalies
        public ActionResult Index()
        {
            var arkivalie = db.Arkivalie.Include(a => a.ArkivFond).Include(a => a.Journaler);
            return View(arkivalie.ToList());
        }

        // GET: Arkivalies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arkivalie arkivalie = db.Arkivalie.Find(id);
            if (arkivalie == null)
            {
                return HttpNotFound();
            }
            return View(arkivalie);
        }

        // GET: Arkivalies/Create
        public ActionResult Create()
        {
            ViewBag.ArkivfondID = new SelectList(db.ArkivFond, "ArkivfondID", "ArkivNr");
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb");
            return View();
        }

        // POST: Arkivalies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArkivalieID,Journal,Arkivfondnr,Art,KasseNr,Signatur,Klausul,YderAarStart,YderAarSlut,Omfang,AntKort,Indlevering,Note,JournalID,ArkivfondID")] Arkivalie arkivalie)
        {
            if (ModelState.IsValid)
            {
                db.Arkivalie.Add(arkivalie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArkivfondID = new SelectList(db.ArkivFond, "ArkivfondID", "ArkivNr", arkivalie.ArkivfondID);
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", arkivalie.JournalID);
            return View(arkivalie);
        }

        // GET: Arkivalies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arkivalie arkivalie = db.Arkivalie.Find(id);
            if (arkivalie == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArkivfondID = new SelectList(db.ArkivFond, "ArkivfondID", "ArkivNr", arkivalie.ArkivfondID);
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", arkivalie.JournalID);
            return View(arkivalie);
        }

        // POST: Arkivalies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArkivalieID,Journal,Arkivfondnr,Art,KasseNr,Signatur,Klausul,YderAarStart,YderAarSlut,Omfang,AntKort,Indlevering,Note,JournalID,ArkivfondID")] Arkivalie arkivalie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arkivalie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArkivfondID = new SelectList(db.ArkivFond, "ArkivfondID", "ArkivNr", arkivalie.ArkivfondID);
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", arkivalie.JournalID);
            return View(arkivalie);
        }

        // GET: Arkivalies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arkivalie arkivalie = db.Arkivalie.Find(id);
            if (arkivalie == null)
            {
                return HttpNotFound();
            }
            return View(arkivalie);
        }

        // POST: Arkivalies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Arkivalie arkivalie = db.Arkivalie.Find(id);
            db.Arkivalie.Remove(arkivalie);
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

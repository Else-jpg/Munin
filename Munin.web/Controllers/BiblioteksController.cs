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
    public class BiblioteksController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: Biblioteks
        public ActionResult Index()
        {
            var bibliotek = db.Bibliotek.Include(b => b.Journaler);
            return View(bibliotek.ToList());
        }

        // GET: Biblioteks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotek bibliotek = db.Bibliotek.Find(id);
            if (bibliotek == null)
            {
                return HttpNotFound();
            }
            return View(bibliotek);
        }

        // GET: Biblioteks/Create
        public ActionResult Create()
        {
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb");
            return View();
        }

        // POST: Biblioteks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BogID,Bogkode,Journal,DK5,Ordningsord,Titel,Undertitel,Redaktor,Forfatter,Udgivet,Forlag,Samlemappe,Indlevering,Note,JournalID")] Bibliotek bibliotek)
        {
            if (ModelState.IsValid)
            {
                db.Bibliotek.Add(bibliotek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", bibliotek.JournalID);
            return View(bibliotek);
        }

        // GET: Biblioteks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotek bibliotek = db.Bibliotek.Find(id);
            if (bibliotek == null)
            {
                return HttpNotFound();
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", bibliotek.JournalID);
            return View(bibliotek);
        }

        // POST: Biblioteks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BogID,Bogkode,Journal,DK5,Ordningsord,Titel,Undertitel,Redaktor,Forfatter,Udgivet,Forlag,Samlemappe,Indlevering,Note,JournalID")] Bibliotek bibliotek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bibliotek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", bibliotek.JournalID);
            return View(bibliotek);
        }

        // GET: Biblioteks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bibliotek bibliotek = db.Bibliotek.Find(id);
            if (bibliotek == null)
            {
                return HttpNotFound();
            }
            return View(bibliotek);
        }

        // POST: Biblioteks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bibliotek bibliotek = db.Bibliotek.Find(id);
            db.Bibliotek.Remove(bibliotek);
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

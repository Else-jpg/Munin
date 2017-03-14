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
    public class KirkebogsController : Controller
    {
        private ILABNew2Entities db = new ILABNew2Entities();

        // GET: Kirkebogs
        public ActionResult Index()
        {
            return View(db.Kirkebog.ToList());
        }

        // GET: Kirkebogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirkebog kirkebog = db.Kirkebog.Find(id);
            if (kirkebog == null)
            {
                return HttpNotFound();
            }
            return View(kirkebog);
        }

        // GET: Kirkebogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kirkebogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PersonID,Fornavn,Efternavn,Ind_dato,Kirkehandling")] Kirkebog kirkebog)
        {
            if (ModelState.IsValid)
            {
                db.Kirkebog.Add(kirkebog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kirkebog);
        }

        // GET: Kirkebogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirkebog kirkebog = db.Kirkebog.Find(id);
            if (kirkebog == null)
            {
                return HttpNotFound();
            }
            return View(kirkebog);
        }

        // POST: Kirkebogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonID,Fornavn,Efternavn,Ind_dato,Kirkehandling")] Kirkebog kirkebog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kirkebog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kirkebog);
        }

        // GET: Kirkebogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kirkebog kirkebog = db.Kirkebog.Find(id);
            if (kirkebog == null)
            {
                return HttpNotFound();
            }
            return View(kirkebog);
        }

        // POST: Kirkebogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kirkebog kirkebog = db.Kirkebog.Find(id);
            db.Kirkebog.Remove(kirkebog);
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

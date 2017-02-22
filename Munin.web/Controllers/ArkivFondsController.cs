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
    public class ArkivFondsController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: ArkivFonds
        public ActionResult Index()
        {
            return View(db.ArkivFond.ToList());
        }

        // GET: ArkivFonds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArkivFond arkivFond = db.ArkivFond.Find(id);
            if (arkivFond == null)
            {
                return HttpNotFound();
            }
            return View(arkivFond);
        }

        // GET: ArkivFonds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArkivFonds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArkivfondID,ArkivNr,ArkivNavn,ArkivDef,ArkivType,StiftetDay,StiftetMonth,StiftetYear,AfsluttetDay,AfsluttetMonth,AfsluttetYear,Adresse,Postnr,By,Note")] ArkivFond arkivFond)
        {
            if (ModelState.IsValid)
            {
                db.ArkivFond.Add(arkivFond);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arkivFond);
        }

        // GET: ArkivFonds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArkivFond arkivFond = db.ArkivFond.Find(id);
            if (arkivFond == null)
            {
                return HttpNotFound();
            }
            return View(arkivFond);
        }

        // POST: ArkivFonds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArkivfondID,ArkivNr,ArkivNavn,ArkivDef,ArkivType,StiftetDay,StiftetMonth,StiftetYear,AfsluttetDay,AfsluttetMonth,AfsluttetYear,Adresse,Postnr,By,Note")] ArkivFond arkivFond)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arkivFond).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arkivFond);
        }

        // GET: ArkivFonds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArkivFond arkivFond = db.ArkivFond.Find(id);
            if (arkivFond == null)
            {
                return HttpNotFound();
            }
            return View(arkivFond);
        }

        // POST: ArkivFonds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArkivFond arkivFond = db.ArkivFond.Find(id);
            db.ArkivFond.Remove(arkivFond);
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

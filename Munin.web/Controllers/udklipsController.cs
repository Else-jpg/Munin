using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Munin.web;
using Munin.web.Models;
using System.Threading.Tasks;
using Munin.web.ViewModels;
using Newtonsoft.Json;
using System.Reflection;

namespace Munin.web.Controllers
{
    public class udklipsController : Controller
    {
        private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: udklips
        public ActionResult Index()
        {
            return View();
        }

 
        [HttpPost]
        public async Task<ActionResult> Udklipslist(ListQuery query)
        {
            try
            {
                using (var dbmunin = new ILABNewEntities2())
                {

                    int flicks = query.P * query.Size;

                    var l = await dbmunin.udklip.ToListAsync();

                    if (!string.IsNullOrEmpty(query.Q))
                    {
                        string[] sQuery = query.Q.Split(' ');
                        for (int i = 0; i < sQuery.Length; i++)
                        {
                            l = l.Where(x => x.Overskrift.ToLower().Contains(sQuery[i].ToLower()) || 
                            (x.Note != null && x.Note.ToLower().Contains(sQuery[i].ToLower())) ||
                            (x.Mappe != null && x.Mappe.ToLower().Contains(sQuery[i].ToLower()))).ToList();
                        }
                    }

                    var column = typeof (udklip).GetProperty(query.S,
                        BindingFlags.SetProperty | BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);


                    if (column != null)
                    {
                        if (query.O.ToUpper() == "DESC")
                        {
                            l = l.OrderByDescending(x => column.GetValue(x, null)).ToList();
                        }
                        else
                        {
                            l = l.OrderBy(x => column.GetValue(x, null)).ToList();
                        }
                    }

                    var pageResult = l.Select(x=> new UdklipDto()
                    {
                        Id = x.UdklipsID,
                        Datering = x.Datering,
                        Overskrift = x.Overskrift,
                        Mappe = x.Mappe
                        }).Skip(1).Take(query.Size);

                    if (l.Count > flicks)
                    {
                        pageResult = l.Select(x => new UdklipDto()
                        {
                            Id = x.UdklipsID,
                            Datering = x.Datering,
                            Overskrift = x.Overskrift,
                            Mappe = x.Mappe
                        }).Skip(flicks).Take(query.Size);
                    }

                    //1971.12.09
                    Regex regx = new Regex(@"^([0-9]{4})[- \/.](0[1-9]|1[012])[- \/.](0[1-9]|[12][0-9]|3[01])$");

                    foreach (var o in pageResult)
                    {
                        if (o.Datering == null)
                            continue;
                        
                        if (!regx.IsMatch(o.Datering))
                            o.ErrorCode = 1;
                    }

                    var listResult = new UdklipViewModel()
                    {
                        Count = l.Count,
                        Pages = l.Count / query.Size,
                        Data = pageResult.ToList()
                    };

                    var result = JsonConvert.SerializeObject(listResult, Utils.JsonSettings());

                    return Content(result);
                }
            }
            catch (Exception ex)
            {
                return Content("Fejl:" + ex.Message);
            }
        }

        // GET: udklips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            udklip udklip = db.udklip.Find(id);
            if (udklip == null)
            {
                return HttpNotFound();
            }
            return View(udklip);
        }

        // GET: udklips/Create
        public ActionResult Create()
        {
            return View();
        }


        

        // POST: udklips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UdklipsID,Mappe,Overskrift,Datering,Aviskode,Note")] udklip udklip)
        {
            if (ModelState.IsValid)
            {
                db.udklip.Add(udklip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(udklip);
        }

        // GET: udklips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            udklip udklip = db.udklip.Find(id);
            if (udklip == null)
            {
                return HttpNotFound();
            }
            return View(udklip);
        }

        // POST: udklips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UdklipsID,Mappe,Overskrift,Datering,Aviskode,Note")] udklip udklip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(udklip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(udklip);
        }

        // GET: udklips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            udklip udklip = db.udklip.Find(id);
            if (udklip == null)
            {
                return HttpNotFound();
            }
            return View(udklip);
        }

        // POST: udklips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            udklip udklip = db.udklip.Find(id);
            db.udklip.Remove(udklip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

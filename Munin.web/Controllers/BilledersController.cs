using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using Munin.web.Models;
using Munin.web.ViewModels;
using Newtonsoft.Json;

namespace Munin.web.Controllers
{
    public class BilledersController : Controller
    {
        //private ILABNewEntities2 db = new ILABNewEntities2();

        // GET: Billeders
        public ActionResult Index()
        {
            using (var db = new ILABNewEntities2())
            {
                var billeder = db.Billeder.ToList();
                return View(billeder);
            }
        }

        public async Task<ActionResult> BilledeList(ListQuery query)
        {
            try
            {
                using (var dbmunin = new ILABNewEntities2())
                {

                    int flicks = query.P*query.Size;

                    var l = await dbmunin.Billeder.ToListAsync();

                    if (!string.IsNullOrEmpty(query.Q))
                    {
                        string[] sQuery = query.Q.Split(' ');
                        for (int i = 0; i < sQuery.Length; i++)
                        {
                            l =
                                l.Where(
                                    x => (x.Note != null && x.Billedindex.ToLower().Contains(sQuery[i].ToLower())) ||
                                         (x.Note != null && x.Note.ToLower().Contains(sQuery[i].ToLower())) ||
                                         (x.Numordning != null && x.Numordning.ToLower().Contains(sQuery[i].ToLower())) ||
                                         (x.Placering != null && x.Placering.ToLower().Contains(sQuery[i].ToLower())))
                                    .ToList();
                        }
                    }

                    var column = typeof (Billeder).GetProperty(query.S,
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

                    var pageResult = l.Select(x => new BillederDto()
                    {
                        Id = x.BilledID,
                        Datering = x.Datering.ToString(),
                        Note = x.Note,
                        BilledIndex = x.Billedindex,
                        TicksDatering = x.Datering.Ticks
                    }).Skip(1).Take(query.Size);

                    if (l.Count > flicks)
                    {
                        pageResult = l.Select(x => new BillederDto()
                        {
                            Id = x.BilledID,
                            Datering = x.Datering.ToString("dd-MM-yyyy"),
                            Note = (x.Note.Length > 100) ? x.Note.Substring(0, 100) : x.Note,
                            BilledIndex = x.Billedindex,
                            TicksDatering = (x.Datering == null) ? 0 : x.Datering.Ticks

                        }).Skip(flicks).Take(query.Size);
                    }

                    var listResult = new BilledeListViewModel()
                    {
                        Count = l.Count,
                        Pages = l.Count/query.Size,
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


        public async Task<ActionResult> Load(int id = 0)
        {
            var vm = new BilledeVm();

            try
            {
                using (ILABNewEntities2 db = new ILABNewEntities2())
                {
                    vm.JournalList =
                        db.Journaler.Select(x => new UISelectItem() {Value = x.JournalID, Text = x.JournalNb}).ToList();

                    vm.MaterialeList = Utils.SelectListOf<BilledeMateriale>();
                    vm.Model = new Billeder();


                    if (id > 0)
                    {
                        var billede = db.Billeder.FirstOrDefault(x => x.BilledID == id);
                        vm.Model = new Billeder()
                        {
                            JournalID = 0,
                            Materiale = 0
                        };

                        if (billede != null)
                        {
                            vm.Model = new Billeder()
                            {
                                BilledID = billede.BilledID,
                                Billedindex = billede.Billedindex,
                                CDnr = billede.CDnr,
                                Datering = billede.Datering.Date,
                                Format = billede.Format,
                                Note = billede.Note,
                                Indlevering = billede.Indlevering,
                                Fotograf = billede.Fotograf,
                                Journal = billede.Journal,
                                JournalID = (billede.JournalID == null) ? 0 : billede.JournalID.Value,
                                Klausul = billede.Klausul,
                                Materiale = billede.Materiale,
                                Ophavsret = billede.Ophavsret,
                                Numordning = billede.Numordning,
                                Ordning = billede.Ordning,
                                Placering = billede.Placering
                            };
                        }
                    }
                    else
                    {
                        var billedindex = db.Billeder.OrderByDescending(x => x.Billedindex).First().Billedindex;
                        int bindex = Int32.Parse(billedindex.Split('.')[1]);
                        bindex++;
                        vm.Model.Billedindex = "B." + bindex.ToString().PadLeft(4, '0');

                    }
                }

                var result = JsonConvert.SerializeObject(vm, Utils.JsonSettings());
                return Content(result);
            }
            catch (Exception ex)
            {
                var result = JsonConvert.SerializeObject(ex.Message, Utils.JsonSettings());
                return Content(result);

            }
        }


        [HttpPost]
        public async Task<ActionResult> Save(Billeder model)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    success = false,
                    message = ModelState.Keys.SelectMany(k => ModelState[k].Errors).First().ErrorMessage
                });

            try
            {
                using (var db = new ILABNewEntities2())
                {
                    var dbModel = new Billeder()
                    {
                        Datering = model.Datering,
                        Billedindex = model.Billedindex,
                        CDnr = model.CDnr,
                        Format = model.Format,
                        Fotograf = model.Fotograf,
                        Journal = model.Journal,
                        JournalID = model.JournalID,
                        Klausul = model.Klausul,
                        Materiale = model.Materiale,
                        Note = model.Note,
                        Numordning = model.Numordning,
                        Ophavsret = model.Ophavsret,
                        Ordning = model.Ordning,
                        Placering = model.Placering
                    };

                    db.Billeder.Add(dbModel);

                    await db.SaveChangesAsync();
                    return Json(new {success = true, message = ""});
                }
            }
            catch (Exception ex)
            {
                //brug extensible funktion
                string err_message = ex.Message;

                if (ex.InnerException != null)
                {
                    err_message = ex.InnerException.Message;
                    if (ex.InnerException.InnerException != null)
                        err_message = ex.InnerException.InnerException.Message;
                }

                return Json(new
                {
                    success = false,
                    message = err_message
                });
            }

        }

        // GET: Billeders/Details/5
        public ActionResult Details(int? id)
        {
            using (var db = new ILABNewEntities2())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Billeder billeder = db.Billeder.Find(id);
                if (billeder == null)
                {
                    return HttpNotFound();
                }
                return View(billeder);
            }

        }

        // GET: Billeders/Create
        public async Task<ActionResult> Create()
        {
            var vm = new BilledeVm();
            vm.Model = new Billeder();
            vm.Model.BilledID = 0;           
            
            return View();
        }

        // POST: Billeders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BilledID,Journal,Billedindex,Numordning,Ordning,CDnr,Fotograf,Format,Materiale,Placering,Ophavsret,Klausul,Datering,Indlevering,Note,JournalID")] Billeder billeder)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ILABNewEntities2())
                {
                    db.Billeder.Add(billeder);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            //ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", billeder.JournalID);
            return View(billeder);
        }

        // GET: Billeders/Edit/5
        public ActionResult Edit(int? id)
        {
            using (var db = new ILABNewEntities2())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Billeder billeder = db.Billeder.Find(id);
                if (billeder == null)
                {
                    return HttpNotFound();
                }
                ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", billeder.JournalID);
                return View(billeder);
            }
        }

        // POST: Billeders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BilledID,Journal,Billedindex,Numordning,Ordning,CDnr,Fotograf,Format,Materiale,Placering,Ophavsret,Klausul,Datering,Indlevering,Note,JournalID")] Billeder billeder)
        {
            using (var db = new ILABNewEntities2())
            {

                if (ModelState.IsValid)
                {
                    db.Entry(billeder).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.JournalID = new SelectList(db.Journaler, "JournalID", "JournalNb", billeder.JournalID);
                return View(billeder);
            }
        }

        // GET: Billeders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (var db = new ILABNewEntities2())
            {
                Billeder billeder = db.Billeder.Find(id);
                if (billeder == null)
                {
                    return HttpNotFound();
                }
                return View(billeder);
            }
        }

        // POST: Billeders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new ILABNewEntities2())
            {
                Billeder billeder = db.Billeder.Find(id);
                db.Billeder.Remove(billeder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            using (var db = new ILABNewEntities2())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }
    }
}

using coursework_db_mvc_cf.Models;
using coursework_db_mvc_cf.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace coursework_db_mvc_cf.Controllers
{
    public class ОтельController : Controller
    {
        TourAgencyModel db = new TourAgencyModel();

        // GET: Отель
        public ActionResult Index()
        {
            return View(db.Отель.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View(new ТурНочёвка());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Отель отель)
        {
            if (ModelState.IsValid)
            {
                db.Отель.Add(отель);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    var errorMessages = e.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
                    Trace.WriteLine(exceptionMessage + ":" + e.EntityValidationErrors);
                }

                return RedirectToAction("Index");
            }

            
            return View(отель);
        }

        // GET: Отель/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отель отель = db.Отель.Find(id);
            if (отель == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Страна", отель.ИД_Адреса);
            return View(отель);
        }

        // POST: Отель/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,ИД_Адреса,Название_отели,Рейтинг")] Отель отель)
        {
            if (ModelState.IsValid)
            {
                db.Entry(отель).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Страна", отель.ИД_Адреса);
            return View(отель);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Отель отель = db.Отель.Find(id);
            if (отель == null)
            {
                return HttpNotFound();
            }
            return View(отель);
        }

        // POST: Отель/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Отель отель = db.Отель.Find(id);
            db.Отель.Remove(отель);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
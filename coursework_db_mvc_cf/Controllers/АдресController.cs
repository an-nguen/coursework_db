using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using coursework_db_mvc_cf.Models.DB;

namespace coursework_db_mvc_cf.Controllers
{
    public class АдресController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Адрес
        public ActionResult Index()
        {
            return View(db.Адрес.ToList());
        }

        // GET: Адрес/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Адрес/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,Страна,Город")] Адрес адрес)
        {
            if (ModelState.IsValid)
            {
                db.Адрес.Add(адрес);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(адрес);
        }

        // GET: Адрес/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Адрес адрес = db.Адрес.Find(id);
            if (адрес == null)
            {
                return HttpNotFound();
            }
            return View(адрес);
        }

        // POST: Адрес/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,Страна,Город")] Адрес адрес)
        {
            if (ModelState.IsValid)
            {
                db.Entry(адрес).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(адрес);
        }

        // GET: Адрес/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Адрес адрес = db.Адрес.Find(id);
            if (адрес == null)
            {
                return HttpNotFound();
            }
            return View(адрес);
        }

        // POST: Адрес/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Адрес адрес = db.Адрес.Find(id);
            db.Адрес.Remove(адрес);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

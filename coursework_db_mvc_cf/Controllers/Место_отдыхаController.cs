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
    public class Место_отдыхаController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Место_отдыха
        public ActionResult Index()
        {
            var место_отдыха = db.Место_отдыха.Include(м => м.Адрес);
            return View(место_отдыха.ToList());
        }

        // GET: Место_отдыха/Create
        public ActionResult Create()
        {
            ViewBag.ИД_адреса = new SelectList(db.Адрес, "ИД", "Город"); 

            return View();
        }

        // POST: Место_отдыха/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,ИД_адреса,Название")] Место_отдыха место_отдыха)
        {
            if (ModelState.IsValid)
            {
                db.Место_отдыха.Add(место_отдыха);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИД_адреса = new SelectList(db.Адрес, "ИД", "Город", место_отдыха.ИД_адреса);
            return View(место_отдыха);
        }

        // GET: Место_отдыха/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Место_отдыха место_отдыха = db.Место_отдыха.Find(id);
            if (место_отдыха == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_адреса = new SelectList(db.Адрес, "ИД", "Город", место_отдыха.ИД_адреса);
            return View(место_отдыха);
        }

        // POST: Место_отдыха/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,ИД_адреса,Название")] Место_отдыха место_отдыха)
        {
            if (ModelState.IsValid)
            {
                db.Entry(место_отдыха).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИД_адреса = new SelectList(db.Адрес, "ИД", "Город", место_отдыха.ИД_адреса);
            return View(место_отдыха);
        }

        // GET: Место_отдыха/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Место_отдыха место_отдыха = db.Место_отдыха.Find(id);
            if (место_отдыха == null)
            {
                return HttpNotFound();
            }
            return View(место_отдыха);
        }

        // POST: Место_отдыха/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Место_отдыха место_отдыха = db.Место_отдыха.Find(id);
            db.Место_отдыха.Remove(место_отдыха);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

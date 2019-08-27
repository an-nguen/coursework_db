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
    public class ОтельController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Отель
        public ActionResult Index()
        {
            var отель = db.Отель.Include(о => о.Адрес);
            return View(отель.ToList());
        }

        // GET: Отель/Create
        public ActionResult Create()
        {
            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Город");
            return View();
        }

        // POST: Отель/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,ИД_Адреса,Название_отели,Рейтинг,Индекс")] Отель отель)
        {
            if (ModelState.IsValid)
            {
                db.Отель.Add(отель);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Город", отель.ИД_Адреса);
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
            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Город", отель.ИД_Адреса);
            return View(отель);
        }

        // POST: Отель/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,ИД_Адреса,Название_отели,Рейтинг,Индекс")] Отель отель)
        {
            if (ModelState.IsValid)
            {
                db.Entry(отель).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИД_Адреса = new SelectList(db.Адрес, "ИД", "Город", отель.ИД_Адреса);
            return View(отель);
        }

        // GET: Отель/Delete/5
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

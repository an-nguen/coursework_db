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
    public class НочёвкаController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Ночёвка
        public ActionResult Index()
        {
            var ночёвка = db.Ночёвка.Include(н => н.Отель);
            return View(ночёвка.ToList());
        }

        // GET: Ночёвка/Create
        public ActionResult Create()
        {
            ViewBag.ИД_отели = new SelectList(db.Отель, "ИД", "Название_отели");
            return View();
        }

        // POST: Ночёвка/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,Цена_за_ночь,Тип_номера,Количество_ночей,ИД_отели")] Ночёвка ночёвка)
        {
            if (ModelState.IsValid)
            {
                db.Ночёвка.Add(ночёвка);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИД_отели = new SelectList(db.Отель, "ИД", "Название_отели", ночёвка.ИД_отели);
            return View(ночёвка);
        }

        // GET: Ночёвка/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ночёвка ночёвка = db.Ночёвка.Find(id);
            if (ночёвка == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_отели = new SelectList(db.Отель, "ИД", "Название_отели", ночёвка.ИД_отели);
            return View(ночёвка);
        }

        // POST: Ночёвка/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,Цена_за_ночь,Тип_номера,Количество_ночей,ИД_отели")] Ночёвка ночёвка)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ночёвка).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИД_отели = new SelectList(db.Отель, "ИД", "Название_отели", ночёвка.ИД_отели);
            return View(ночёвка);
        }

        // GET: Ночёвка/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ночёвка ночёвка = db.Ночёвка.Find(id);
            if (ночёвка == null)
            {
                return HttpNotFound();
            }
            return View(ночёвка);
        }

        // POST: Ночёвка/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ночёвка ночёвка = db.Ночёвка.Find(id);
            db.Ночёвка.Remove(ночёвка);
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

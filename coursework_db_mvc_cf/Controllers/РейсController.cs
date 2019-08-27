using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace coursework_db_mvc_cf.Models.DB
{
    public class РейсController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Рейс
        public ActionResult Index()
        {
            return View(db.Рейс.ToList());
        }

        // GET: Рейс/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Рейс/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,НомерБилета,ТипТранспорта,СтранаОтправления,СтранаПрибытия,ДатаОтправления,ДатаПрибытия,СтоимостьПоездки")] Рейс рейс)
        {
            if (ModelState.IsValid)
            {
                db.Рейс.Add(рейс);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(рейс);
        }

        // GET: Рейс/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
        }

        // POST: Рейс/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,НомерБилета,ТипТранспорта,СтранаОтправления,СтранаПрибытия,ДатаОтправления,ДатаПрибытия,СтоимостьПоездки")] Рейс рейс)
        {
            if (ModelState.IsValid)
            {
                db.Entry(рейс).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(рейс);
        }

        // GET: Рейс/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Рейс рейс = db.Рейс.Find(id);
            if (рейс == null)
            {
                return HttpNotFound();
            }
            return View(рейс);
        }

        // POST: Рейс/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Рейс рейс = db.Рейс.Find(id);
            db.Рейс.Remove(рейс);
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

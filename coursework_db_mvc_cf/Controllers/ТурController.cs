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
    public class ТурController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Тур
        public ActionResult Index()
        {
            var тур = db.Тур.Include(т => т.Место_отдыха)
                .Include(т => т.Ночёвка)
                .Include(т => т.Рейс)
                .Include(т => т.Рейс1)
                .Include(т => т.Клиент)
                .ToList();
            return View(тур.ToList());
        }

        // GET: Тур/Create
        public ActionResult Create()
        {
            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название");
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "ИД");
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета");
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета");
            ViewBag.Клиент = new SelectList(db.Клиент, "ИД", "Клиент");
            return View();
        }

        // POST: Тур/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,Вкл_гида,Вкл_питание,Вкл_ночёвка,Вкл_поездка,Общая_стоимость,Длительность_отдыха_в_днях,ИД_рейса_в_место_отдыха,ИД_рейса_из_места_отдыха,ИД_место_отдыха,ИД_ночёвки")] Тур тур)
        {
            if (ModelState.IsValid)
            {
                db.Тур.Add(тур);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название", тур.ИД_место_отдыха);
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "ИД", тур.ИД_ночёвки);
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_из_места_отдыха);
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_в_место_отдыха);
            ViewBag.Клиент = new SelectList(db.Клиент, "ИД", "Почта", тур.Клиент);
            return View(тур);
        }

        // GET: Тур/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Тур тур = db.Тур.Find(id);
            if (тур == null)
            {
                return HttpNotFound();
            }
            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название", тур.ИД_место_отдыха);
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "Тип_номера", тур.ИД_ночёвки);
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_из_места_отдыха);
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_в_место_отдыха);
            return View(тур);
        }

        // POST: Тур/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ИД,Вкл_гида,Вкл_питание,Вкл_ночёвка,Вкл_поездка,Общая_стоимость,Длительность_отдыха_в_днях,ИД_рейса_в_место_отдыха,ИД_рейса_из_места_отдыха,ИД_место_отдыха,ИД_ночёвки")] Тур тур)
        {
            if (ModelState.IsValid)
            {
                db.Entry(тур).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название", тур.ИД_место_отдыха);
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "Тип_номера", тур.ИД_ночёвки);
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_из_места_отдыха);
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_в_место_отдыха);
            return View(тур);
        }

        // GET: Тур/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Тур тур = db.Тур.Find(id);
            if (тур == null)
            {
                return HttpNotFound();
            }
            return View(тур);
        }

        // POST: Тур/Delete/5
        [HttpPost, ActionName("Удалить")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Тур тур = db.Тур.Find(id);
            db.Тур.Remove(тур);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

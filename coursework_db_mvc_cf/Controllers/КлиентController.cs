﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using coursework_db_mvc_cf.Models.DB;
using coursework_db_mvc_cf.Models;

namespace coursework_db_mvc_cf.Controllers
{
    public class КлиентController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        // GET: Клиент
        public ActionResult Index()
        {
            return View(db.Клиент.ToList());
        }

        // GET: Клиент/Create
        public ActionResult Create()
        {
            var client = new Клиент();
            var result = (from t in db.Тур select t);
            client.checkBoxList = new List<CheckBoxViewModel>();

            foreach (var tour in result)
            {
                client.checkBoxList.Add(new CheckBoxViewModel
                {
                    id = tour.ИД,
                    name = tour.ИД + " - " + tour.Место_отдыха.Название + " - " + tour.Общая_стоимость,
                    Checked = false
                });
            }
            return View(client);
        }

        // POST: Клиент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Клиент клиент)
        {
            if (ModelState.IsValid)
            {
                db.Клиент.Add(клиент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(клиент);
        }

        // GET: Клиент/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);

            var tours = (from t in db.Тур select t);

            клиент.checkBoxList = new List<CheckBoxViewModel>();

            foreach (var tour in tours)
            {
                клиент.checkBoxList.Add(new CheckBoxViewModel
                {
                    id = tour.ИД,
                    name = tour.ИД + " - " + tour.Место_отдыха.Название + " - " + tour.Общая_стоимость,
                    Checked = tour.isOwnedByClient(клиент)
                });
            }

            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // POST: Клиент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Клиент клиент)
        {
            if (ModelState.IsValid)
            {
                db.Entry(клиент).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(клиент);
        }

        // GET: Клиент/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Клиент клиент = db.Клиент.Find(id);
            if (клиент == null)
            {
                return HttpNotFound();
            }
            return View(клиент);
        }

        // POST: Клиент/Delete/5
        [HttpPost, ActionName("Удалить")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Клиент клиент = db.Клиент.Find(id);
            db.Клиент.Remove(клиент);
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

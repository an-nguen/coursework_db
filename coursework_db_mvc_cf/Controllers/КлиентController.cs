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
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace coursework_db_mvc_cf.Controllers
{
    public class КлиентController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        public void deleteRelationship(long tourID, long clientID)
        {
            var tour = db.Тур.FirstOrDefault(t => t.ИД == tourID);
            var client = db.Клиент.FirstOrDefault(c => c.ИД == clientID);

            tour.Клиент.Remove(client);
        }

        // GET: Клиент
        public ActionResult Index()
        {
            var clients = new List<КлиентViewModel>();
            foreach (var c in db.Клиент) {
                var clientView = new КлиентViewModel();
                clientView.клиент = c;
                db.Entry(c).Collection(p => p.Тур).Load();
                clientView.checkBoxList = new List<CheckBoxViewModel>();

                foreach (var tour in c.Тур)
                {
                    clientView.checkBoxList.Add(new CheckBoxViewModel
                    {
                        id = tour.ИД,
                        name = tour.ИД + " - " + tour.Место_отдыха.Название,
                        Checked = false
                    });
                }

                clients.Add(clientView);
            }

            
            return View(clients);
        }

     
        // GET: Клиент/Create
        public ActionResult Create()
        {
            var clientView = new КлиентViewModel();
            var result = (from t in db.Тур select t);
            clientView.checkBoxList = new List<CheckBoxViewModel>();

            foreach (var tour in result)
            {
                clientView.checkBoxList.Add(new CheckBoxViewModel
                {
                    id = tour.ИД,
                    name = tour.ИД + " - " + tour.Место_отдыха.Название + " - " + tour.Общая_стоимость,
                    Checked = false
                });
            }

            return View(clientView);
        }

        // POST: Клиент/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(КлиентViewModel клиентView)
        {
            if (ModelState.IsValid)
            {
                foreach (var checkBox in клиентView.checkBoxList)
                {
                    if (checkBox.Checked)
                    {
                        клиентView.клиент.Тур.Add(db.Тур.Find(checkBox.id));
                    }
                }
                db.Клиент.Add(клиентView.клиент);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(клиентView);
        }

        // GET: Клиент/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            КлиентViewModel клиентView = new КлиентViewModel();

            Клиент клиент = db.Клиент.Find(id);

            var tours = (from t in db.Тур select t);

            var checkBoxList = new List<CheckBoxViewModel>();

            foreach (var tour in tours)
            {
                checkBoxList.Add(new CheckBoxViewModel
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
            клиентView.клиент = клиент;
            клиентView.checkBoxList = checkBoxList;

            return View(клиентView);
        }

        // POST: Клиент/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(КлиентViewModel клиентView)
        {
            if (ModelState.IsValid)
            {
                var tours = (from t in db.Тур select t);

                var modifiedData = клиентView.клиент;
                var client = db.Клиент.Find(клиентView.клиент.ИД);

                db.Entry(client).Collection(p => p.Тур).Load();

                client.Фамилия = modifiedData.Фамилия;
                client.Имя = modifiedData.Имя;
                client.Отчество = modifiedData.Отчество;
                client.Дата_рождения = modifiedData.Дата_рождения;
                client.Почта = modifiedData.Почта;
                client.Серия = modifiedData.Серия;
                client.Номер = modifiedData.Номер;
                client.ДействителенДо = modifiedData.ДействителенДо;
                client.ДатаВыдачиПасспорта = modifiedData.ДатаВыдачиПасспорта;

                foreach (var tour in client.Тур)
                {
                    deleteRelationship(tour.ИД, client.ИД);
                }

                foreach (var selTour in клиентView.checkBoxList)
                {
                    if (selTour.Checked)
                    {
                        client.Тур.Add(db.Тур.Find(selTour.id));
                    }
                }
                

                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(клиентView);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Клиент клиент = db.Клиент.Find(id);
            db.Entry(клиент).Collection(p => p.Тур).Load();

            foreach (var tour in клиент.Тур)
            {
                deleteRelationship(tour.ИД, клиент.ИД);
            }

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

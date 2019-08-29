using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using coursework_db_mvc_cf.Models;
using coursework_db_mvc_cf.Models.DB;

namespace coursework_db_mvc_cf.Controllers
{
    public class ТурController : Controller
    {
        private TourAgencyModel db = new TourAgencyModel();

        public void deleteRelationship(long tourID, long clientID)
        {
            var tour = db.Тур.FirstOrDefault(t => t.ИД == tourID);
            var client = db.Клиент.FirstOrDefault(c => c.ИД == clientID);

            tour.Клиент.Remove(client);
        }

        public List<Тур> selectExpiredTour()
        {
            return db.Тур.SqlQuery("[dbo].[SelectExpiredTours]").ToList();
        }

        // GET: Тур
        [HttpGet]
        public ActionResult Index()
        {
            var тур = db.Тур.Include(т => т.Место_отдыха)
                .Include(т => т.Ночёвка)
                .Include(т => т.Рейс)
                .Include(т => т.Рейс1)
                .ToList();

            var турView = new ТурFindModel();
            турView.tours = тур;
            турView.findByMinCost = 0;
            турView.findByMaxCost = 1000000;

            return View(турView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ТурFindModel турModel)
        {
            var тур = db.Тур.Include(т => т.Место_отдыха)
                .Include(т => т.Ночёвка)
                .Include(т => т.Рейс)
                .Include(т => т.Рейс1)
                .ToList();
            if (!String.IsNullOrEmpty(турModel.findByCity))
            {
                тур = тур.Where(s => s.Место_отдыха.Адрес.Город.Contains(турModel.findByCity)).ToList();
            }
            if (!String.IsNullOrEmpty(турModel.findByCountry))
            {
                тур = тур.Where(s => s.Место_отдыха.Адрес.Страна.Contains(турModel.findByCountry)).ToList();
            }
            тур = тур.Where(s => s.Общая_стоимость > турModel.findByMinCost
                            && s.Общая_стоимость < турModel.findByMaxCost)
                        .ToList();
            турModel.tours = тур;

            турModel.findByCity = турModel.findByCountry = "";
            турModel.findByMinCost = 0;
            турModel.findByMaxCost = 1000000;

            return View(турModel);
        }

        // GET: Тур
        public ActionResult ExpiredTours()
        {

            var тур = selectExpiredTour();
            return View(тур.ToList());
        }

        [HttpGet]
        public ActionResult Details()
        {
            var тур = db.Тур.Include(т => т.Место_отдыха)
                .Include(т => т.Ночёвка)
                .Include(т => т.Рейс)
                .Include(т => т.Рейс1)
                .ToList();

            var турView = new ТурFindModel();
            турView.tours = тур;
            турView.findByMinCost = 0;
            турView.findByMaxCost = 1000000;

            return View(турView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ТурFindModel турModel)
        {
            var тур = db.Тур.Include(т => т.Место_отдыха)
                .Include(т => т.Ночёвка)
                .Include(т => т.Рейс)
                .Include(т => т.Рейс1)
                .ToList();
            if (!String.IsNullOrEmpty(турModel.findByCity))
            {
                тур = тур.Where(s => s.Место_отдыха.Адрес.Город.Contains(турModel.findByCity)).ToList();
            }
            if (!String.IsNullOrEmpty(турModel.findByCountry))
            {
                тур = тур.Where(s => s.Место_отдыха.Адрес.Страна.Contains(турModel.findByCountry)).ToList();
            }
            тур = тур.Where(s => s.Общая_стоимость > турModel.findByMinCost 
                            && s.Общая_стоимость < турModel.findByMaxCost)
                        .ToList();
            турModel.tours = тур;

            турModel.findByCity = турModel.findByCountry = "";
            турModel.findByMinCost = 0;
            турModel.findByMaxCost = 1000000;

            return View(турModel);
        }

        // GET: Тур/Create
        public ActionResult Create()
        {
            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название");
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "ИД");
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета");
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета");
            return View();
        }

        // POST: Тур/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ИД,Вкл_гида,Вкл_питание,Вкл_ночёвка,Вкл_поездка,Общая_стоимость,Длительность_отдыха_в_днях,ИД_рейса_в_место_отдыха,ИД_рейса_из_места_отдыха,ИД_место_отдыха,ИД_ночёвки")] Тур тур)
        {
            if (ModelState.IsValid)
            {
                if (тур.Вкл_поездка == false)
                {
                    тур.ИД_рейса_в_место_отдыха = тур.ИД_рейса_из_места_отдыха = null;
                }
                if (тур.Вкл_ночёвка == false)
                {
                    тур.ИД_ночёвки = null;
                }

                db.Тур.Add(тур);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ИД_место_отдыха = new SelectList(db.Место_отдыха, "ИД", "Название", тур.ИД_место_отдыха);
            ViewBag.ИД_ночёвки = new SelectList(db.Ночёвка, "ИД", "ИД", тур.ИД_ночёвки);
            ViewBag.ИД_рейса_из_места_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_из_места_отдыха);
            ViewBag.ИД_рейса_в_место_отдыха = new SelectList(db.Рейс, "ИД", "НомерБилета", тур.ИД_рейса_в_место_отдыха);
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
                if (тур.Вкл_поездка == false) {
                    тур.ИД_рейса_в_место_отдыха = тур.ИД_рейса_из_места_отдыха = null;
                }
                if (тур.Вкл_ночёвка == false)
                {
                    тур.ИД_ночёвки = null;
                }

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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Тур тур = db.Тур.Find(id);
            db.Entry(тур).Collection(p => p.Клиент).Load();

            foreach (var client in тур.Клиент.ToList())
            {
                deleteRelationship(тур.ИД, client.ИД);
            }

            db.Тур.Remove(тур);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

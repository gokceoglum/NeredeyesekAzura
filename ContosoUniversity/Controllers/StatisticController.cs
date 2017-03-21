using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;

namespace ContosoUniversity.Controllers
{
    public class StatisticController : Controller
    {
        private ProjectContext db = new ProjectContext();

        public ActionResult createStatisticsTable()
        {
            foreach (var p in db.Statistics)
            {
                db.Statistics.Remove(p);
            }
            db.SaveChanges();
            int PointId = 1;
            int totalPoint = 0;
            foreach (var restaurant in db.Restaurants)
            {
                var drafts = db.Points.Where(d => d.RestaurantID == restaurant.ID).ToList();
                int point = 0;
                foreach (var resPoint in drafts)
                {
                    point += resPoint.GivenPoint;
                }
                totalPoint += point;
                Statistic statistic = new Statistic();
                statistic.ID = PointId;
                statistic.RestaurantID = restaurant.ID;
                statistic.DaysLeft = point;
                statistic.DaysToGo = point;
                db.Statistics.Add(statistic);
                PointId++;
            }
            db.SaveChanges();
            foreach (var s in db.Statistics)
            {
                int exactDay = s.DaysToGo * 20 / totalPoint;
                s.DaysToGo = exactDay;
                s.DaysLeft = exactDay;
               
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Statistic");
        }
        // GET: Statistic
        public ActionResult Index()
        {
            return View(db.Statistics.ToList());
        }

        // GET: Statistic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // GET: Statistic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Statistic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,GroupID,RestaurantID,DaysToGo,DaysLeft")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                db.Statistics.Add(statistic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(statistic);
        }

        // GET: Statistic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // POST: Statistic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,GroupID,RestaurantID,DaysToGo,DaysLeft")] Statistic statistic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(statistic);
        }

        // GET: Statistic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistic statistic = db.Statistics.Find(id);
            if (statistic == null)
            {
                return HttpNotFound();
            }
            return View(statistic);
        }

        // POST: Statistic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistic statistic = db.Statistics.Find(id);
            db.Statistics.Remove(statistic);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.Models;
using FestivalCMS.DAL;

namespace FestivalCMS.Controllers
{
    public class SeminarsController : Controller
    {
        private FestivalDBContext db = new FestivalDBContext();

        // GET: /Seminars/
        public ActionResult Index()
        {
            return View(db.Seminars.OrderByDescending(s => s.StartOn).ToList());
        }

        // GET: /Seminars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // GET: /Seminars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Seminars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Headline,StartOn,Text,ExtLink,IsActive,Duration")] Seminar seminar)
        {
            if (ModelState.IsValid)
            {
                seminar.OrderNum = db.Seminars.Count() + 1;
                seminar.IsActive = true;
                db.Seminars.Add(seminar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(seminar);
        }

        // GET: /Seminars/Edit/5
        public ActionResult Edit(int id)
        {
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            return View(seminar);
        }

        // POST: /Seminars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Headline,StartOn,Text,ExtLink,IsActive,Duration")] Seminar seminar)
        {
            if (!ModelState.IsValid) return View(seminar);
            
            Seminar sem = db.Seminars.Find(seminar.ID);
            if (sem == null) return HttpNotFound();
            
            sem.Duration = seminar.Duration;
            sem.ExtLink = seminar.ExtLink;
            sem.Headline = seminar.Headline;
            sem.StartOn = seminar.StartOn;
            sem.Text = seminar.Text;
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Seminars/Delete/5
        public ActionResult DeleteSeminar(int id)
        {
            Seminar seminar = db.Seminars.Find(id);
            if (seminar == null)
            {
                return HttpNotFound();
            }
            db.Seminars.Remove(seminar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ActivateSeminar(int sID, bool toActivate)
        {
            Seminar seminar = db.Seminars.Find(sID);
            if (seminar == null) return HttpNotFound();
            seminar.IsActive = toActivate;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSeminarsOrder(IEnumerable<Seminar> seminars)
        {
            if (seminars == null) return RedirectToAction("Index");
            int currentNum = 0;
            foreach (var item in seminars.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Seminars.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
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

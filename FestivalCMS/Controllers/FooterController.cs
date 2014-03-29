using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.Models;
using FestivalCMS.Repositories;
using FestivalCMS.DAL;

namespace FestivalCMS.Controllers
{
    [Authorize]
    public class FooterController : Controller
    {
        protected IDataRepository footerRepo;
        public FooterController()
        {
            footerRepo = new DataRepository(new FestivalDBContext());
        }
        public FooterController(IDataRepository ifooterRepo)
        {
            this.footerRepo = ifooterRepo;
        }


        public ActionResult Index()
        {
            Footer f = footerRepo.GetFooter();

            return View(f ?? new Footer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Footer f)
        {
            if (ModelState.IsValid)
            {
                footerRepo.UpdateFooter(f);
                return RedirectToAction("Index", "Home");
            }
            return View(f);
        }

        public ActionResult GetMetatag()
        {
            Metatag m = footerRepo.GetMetatag();
            return View(m ?? new Metatag());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetMetatag(Metatag m)
        {
            if (ModelState.IsValid)
            {
                footerRepo.UpdateMetatag(m);
                return RedirectToAction("Index", "Home");
            }
            return View(m);
        }
        protected override void Dispose(bool disposing)
        {
            footerRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
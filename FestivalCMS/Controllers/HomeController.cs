using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.Repositories;
using FestivalCMS.DAL;
using System.Threading;

namespace FestivalCMS.Controllers
{
    public class HomeController : FestivalCMS.App_Code.BaseController<IFrontSideRepo>
    {
        public HomeController()
            : base(new FrontSideRepo(new FestivalDBContext()))
        {

        }
        //
        // GET: /Home/
        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("no");
            return View(controllerRepo.GetDataForHomePage());
        }
    }
}
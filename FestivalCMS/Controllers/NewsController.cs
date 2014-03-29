using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.App_Code;
using FestivalCMS.Models;
using FestivalCMS.DAL;
using FestivalCMS.Repositories;

namespace FestivalCMS.Controllers
{
    public class NewsController : BaseController<IFrontSideRepo>
    {
        public NewsController()
            : base(new FrontSideRepo(new FestivalDBContext()))
        {

        }
        //
        // GET: /News/
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("no");
            return View(controllerRepo.GetDataForNewsPage((int)id));
        }
    }
}
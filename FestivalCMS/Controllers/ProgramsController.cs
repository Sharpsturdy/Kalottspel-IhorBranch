using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.Repositories;
using FestivalCMS.DAL;
using FestivalCMS.App_Code;
using System.Threading;


namespace FestivalCMS.Controllers
{
    public class ProgramsController : BaseController<IFrontSideRepo>
    {
        public ProgramsController()
            : base(new FrontSideRepo(new FestivalDBContext()))
        {

        }
        //
        // GET: /Programs/
        public ActionResult Index()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("no");
            return View(controllerRepo.GetProgram());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.App_Code;
using FestivalCMS.DAL;
using FestivalCMS.Models;
using FestivalCMS.Repositories;

namespace FestivalCMS.Controllers
{
    public class FrontLayoutController : BaseController<ILayoutRepo>
    {

        public FrontLayoutController()
            : base(new LayoutRepo(new FestivalDBContext()))
        {

        }

        public ActionResult GetSponsors()
        {
            return PartialView(controllerRepo.GetSponsors());
        }

        public ActionResult GetPartners()
        {
            return PartialView(controllerRepo.GetPartners());
        }
    }
}
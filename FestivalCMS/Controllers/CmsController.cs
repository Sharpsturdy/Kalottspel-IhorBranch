using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalCMS.Controllers
{
    [Authorize]
    public class CmsController : Controller
    {
        //
        // GET: /Cms/
        public ActionResult Index()
        {
            return View();
        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FestivalCMS.App_Code
{
    public class BaseController<T> :Controller where T:IDisposable
    {
        protected T controllerRepo;
        protected BaseController(T repository)
        {
            controllerRepo = repository;
        }

        protected override void Dispose(bool disposing)
        {
            controllerRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.Repositories;
using FestivalCMS.Models;
using FestivalCMS.DAL;

namespace FestivalCMS.Controllers
{
    [Authorize]
    public class ArticlesController : Controller
    {
        IDataRepository articlesRepo;
        public ArticlesController()
        {
            articlesRepo = new DataRepository(new FestivalDBContext());
        }

        public ArticlesController(IDataRepository iartRepo)
        {
            articlesRepo = iartRepo;
        }
        //
        // GET: /Articles/
        public ActionResult Index(int catID = 0)
        {
            if (Request.IsAjaxRequest())
            {
                return View("IndexAjax", articlesRepo.GetArticlesInCategory(catID));
            }
            return View(articlesRepo.GetArticlesInCategory(catID));
        }

        public ActionResult CreateArticle(int cID)
        {
            if (cID != 0)
            {
                return View(new ArticleModel() { MediaTypes = articlesRepo.GetMediaTypesSelectList(), Article = new Article() { CategoryID = cID } });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(ArticleModel art)
        {
            if (ModelState.IsValid)
            {
                articlesRepo.CreateArticle(art);
                return RedirectToAction("Index", new { catID=art.Article.CategoryID});
            }
            art.MediaTypes = articlesRepo.GetMediaTypesSelectList();
            return View(art);
        }

        public ActionResult DeleteArticle(int aID, int cID)
        {
            articlesRepo.DeleteArticle(aID);
            return RedirectToAction("Index", new { catID = cID });
        }

        public ActionResult ActivateArticle(int aID, bool toActivate, int cID)
        {
            articlesRepo.UpdateArticleStatus(aID, toActivate);
            return RedirectToAction("Index", new { catID = cID });
        }

        [HttpPost]
        public ActionResult UpdateOrder(IEnumerable<Article> articles, int cID)
        {
            articlesRepo.UpdateArticleOrderNum(articles);
            return RedirectToAction("Index", new { catID = cID });
        }


        public ActionResult EditArticle(int aID)
        {

            ArticleModel model = articlesRepo.GetArticleForEdit(aID);
            if (model != null) return View(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(ArticleModel model)
        {
            if (ModelState.IsValid)
            {
                articlesRepo.UpdateArticle(model);
                return RedirectToAction("Index", new { catID = model.Article.CategoryID });
            }
            return View(model);
        }

        public ActionResult GetPartners()
        {
            return View("Index",articlesRepo.GetPartners());
        }

        public ActionResult GetSponsors()
        {
            return View("Index", articlesRepo.GetSponsors());
        }
        protected override void Dispose(bool disposing)
        {
            articlesRepo.Dispose();
            base.Dispose(disposing);
        }
    }
}
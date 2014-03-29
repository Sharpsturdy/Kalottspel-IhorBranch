using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FestivalCMS.DAL;
using FestivalCMS.Models;
using FestivalCMS.Repositories;

namespace FestivalCMS.Controllers
{
    [Authorize]
    public class ProgrammController : Controller
    {
        IDataRepository repo;
        public ProgrammController()
        {
            this.repo = new DataRepository(new FestivalDBContext());
        }

        public ProgrammController(IDataRepository idr)
        {
            this.repo = idr;
        }

        protected override void Dispose(bool disposing)
        {
            repo.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return RedirectToAction("GetAllEvents");
        }

        public ActionResult GetAllArtists()
        {

            if (Request.IsAjaxRequest())
            {
                return View("GetAllArtistsAjax", repo.GetAllArtists().ToList());
            }
            return View(repo.GetAllArtists().ToList());
        }

        public ActionResult CreateArtist()
        {
            ArtistModel model = new ArtistModel();
            model.MediaTypes = repo.GetMediaTypesSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArtist(ArtistModel model)
        {
            if (ModelState.IsValid)
            {
                repo.CreateArtist(model);
                return RedirectToAction("GetAllArtists");
            }
            return View(model);
        }

        public ActionResult EditArtist(int aID)
        {

            return View(repo.GetArtist(aID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArtist(ArtistModel model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateArtist(model);
                return RedirectToAction("GetAllArtists");
            }
            return View(model);
        }

        public ActionResult UpdateArtistOrderNum(IEnumerable<Artist> artists, int fId = 0)
        {
            repo.UpdateArtistsOrderNum(artists);
            if (fId != 0)
            {
                return RedirectToAction("GetArtistsInFestival", new { fID = fId });
            }
            return RedirectToAction("GetAllArtists");
        }

        public ActionResult UpdateEventOrderNum(IEnumerable<Event> evnts)
        {
            repo.UpdateEventOrdernum(evnts);
            return RedirectToAction("GetAllEvents");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEventInFestOrderNum(EventsInFestivalModel model)
        {
            repo.UpdateEventOrdernum(model.Events);
            return RedirectToAction("GetFestival", new { fID = model.Festival.ID });

        }

        public ActionResult ActivateArtist(int aID, bool toActivate)
        {
            repo.UpdateArtistStatus(aID, toActivate);
            return RedirectToAction("GetAllArtists");
        }
        public ActionResult DeleteArtist(int aID)
        {
            repo.DeleteArtist(aID);
            return RedirectToAction("GetAllArtists");

        }

        public ActionResult GetAllEvents()
        {
            if (Request.IsAjaxRequest())
            {
                return View("GetAllEventsAjax", repo.GetAllEvents().ToList());
            }
            return View(repo.GetAllEvents().ToList());
        }

        public ActionResult CreateEvent()
        {

            EventModel model = new EventModel()
            {
                Artists = repo.GetAllArtistsInSelectedList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                repo.CreateEvent(model);
                return RedirectToAction("GetAllEvents");
            }
            model.Artists = repo.GetAllArtistsInSelectedList();
            return View("CreateEventNoAjax", model);
        }

        public ActionResult EditEvent(int eID)
        {
            EventModel model = new EventModel();
            model.Artists = repo.GetAllArtistsInEventSelectedList(eID);
            model.Event = repo.GetEvent(eID);
            return View(model);
        }

        public ActionResult EditEventInFestival(int eID, int fId)
        {
            EventModel model = new EventModel();
            model.Artists = repo.GetAllArtistsInEventSelectedList(eID, fId);
            model.Event = repo.GetEvent(eID);
            return View("EditEvent", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEvent(EventModel model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateEvent(model);
                return RedirectToAction("GetAllEvents");
            }
            model.Artists = repo.GetAllArtistsInSelectedList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEventInFestival(EventModel model)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateEvent(model);
                return RedirectToAction("GetFestival", new { fID = model.Event.FestivalID });
            }
            model.Artists = repo.GetAllArtistsInFestivalSelectedList((int)model.Event.FestivalID);
            return View("EditEvent", model);
        }
        public ActionResult ActivateEvent(int eID, bool toActivate, int fId = 0)
        {
            repo.ActivateEvent(eID, toActivate);
            if (fId != 0)
            {
                return RedirectToAction("GetFestival", new { fID = fId });
            }
            return RedirectToAction("GetAllEvents");
        }

        public ActionResult GetAllFestivals()
        {
            if(Request.IsAjaxRequest())
            {
                return PartialView("GetAllFestivalsAjax", repo.GetAllFestivals().ToList());
            }
            return View(repo.GetAllFestivals().ToList());
        }

        public ActionResult CreateFestival()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFeastival(Festival fest)
        {
            if (ModelState.IsValid)
            {
                int newFestID = repo.CreateFestival(fest);
                return RedirectToAction("GetAllFestivals");
            }
            return View(fest);
        }

        public ActionResult GetFestival(int fID)
        {
            return View(repo.GetEventsInFestivals(fID));
        }

        public ActionResult CreateEventInFestival(int fID)
        {
            EventModel model = new EventModel();
            model.Artists = repo.GetAllArtistsInFestivalSelectedList(fID);
            model.Event.FestivalID = fID;
            return View("CreateEvent", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEventInFestival(EventModel model)
        {
            if (ModelState.IsValid)
            {
                repo.CreateEvent(model);
                return RedirectToAction("GetFestival", new { fID = model.Event.FestivalID });
            }
            model.Artists = repo.GetAllArtistsInFestivalSelectedList((int)model.Event.FestivalID);
            return View("CreateEventNoAjax", model);
        }

        public ActionResult GetAllFestivalsForSideMenu()
        {
            IEnumerable<Festival> fests = repo.GetAllFestivals();
            return PartialView(fests);
        }

        public ActionResult GetArtistsInFestival(int fID)
        {
            ArtistsInFestivalModel model = new ArtistsInFestivalModel()
            {
                Artists = repo.GetAllArtistsInFestival(fID).ToList(),
                Festival = repo.GetFestival(fID)
            };
            return View(model);
        }

        public ActionResult CreateArtistInFestival(int fID)
        {
            ArtistModel model = new ArtistModel();
            model.Festival = repo.GetFestival(fID);
            model.MediaTypes = repo.GetMediaTypesSelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArtistInFestival(ArtistModel model)
        {
            ModelState.Remove("Festival.Name");
            if (ModelState.IsValid)
            {
                repo.CreateArtistInFestival(model);
                return RedirectToAction("GetArtistsInFestival", new { fID = model.Festival.ID });
            }
            return View(model);
        }

        public ActionResult DeleteArtistInFestival(int aID, int fId)
        {
            repo.DeleteArtist(aID, fId);
            return RedirectToAction("GetArtistsInFestival", new { fID = fId });
        }
        public ActionResult DeleteEvent(int eID)
        {
            repo.DeleteEvent(eID);
            return RedirectToAction("GetAllEvents");
        }

        public ActionResult DeleteEventInFestival(int eID, int fId)
        {
            repo.DeleteEvent(eID);
            return RedirectToAction("GetFestival", new { fID = fId });
        }

        public ActionResult DeleteFestival(int fID)
        {
            repo.DeleteFestival(fID);
            return RedirectToAction("GetAllFestivals");
        }

        public ActionResult EditFestival(int fID)
        {
            return View(repo.GetFestival(fID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFestival(Festival fest)
        {
            repo.UpdateFestival(fest);
            return RedirectToAction("GetAllFestivals");
        }

        public ActionResult ActivateFestival(int fID, bool toActive)
        {
            repo.ToActivateFestival(fID, toActive);
            return RedirectToAction("GetAllFestivals");
        }
        public ActionResult UpdateFestivalsOrder(IEnumerable<Festival> fests)
        {
            repo.UpdateFestivalsOrderNums(fests);
            return RedirectToAction("GetAllFestivals");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FestivalCMS.Models;
using FestivalCMS.DAL;
using System.Web.Mvc;
using System.Data.Entity;

namespace FestivalCMS.Repositories
{
    public class DataRepository : ContentRepositoryBase, IDataRepository
    {
        public DataRepository(FestivalDBContext db)
            : base(db)
        {

        }
        public void CreateArticle(ArticleModel article)
        {
            if (article != null)
            {
                //article.Article.MediaTypeID = article.MediatypeID;  //fixes in articles
                if (article.Article.MediaTypeID == 0) article.Article.MediaTypeID = 4;
                article.Article.CreatedOn = DateTime.Now;
                article.Article.OrderNum = db.Articles.Where(a => a.CategoryID == article.Article.CategoryID).Count() + 1;
                Article newArt = db.Articles.Add(article.Article);
                db.SaveChanges();
                newArt.ContentID = AddContent(article.Content, newArt.MediaTypeID);
                db.SaveChanges();
            }
        }

        public void DeleteArticle(int aID)
        {
            Article art = db.Articles.Find(aID);
            if (art != null)
            {
                if (art.ContentID == 0)
                {
                    db.Articles.Remove(art);
                    db.SaveChanges();
                }
                else
                {
                    db.Articles.Remove(art);
                    DeleteContent(art.MediaTypeID, art.ContentID);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateArticleStatus(int aID, bool isActive)
        {
            Article a = db.Articles.Find(aID);
            if (a != null)
            {
                a.IsActive = isActive;
                db.SaveChanges();
            }
        }

        public void UpdateArticleOrderNum(IEnumerable<Models.Article> articles)
        {
            if (articles == null) return;
            int totalProducts = articles.Count();
            int currentNum = 0;
            foreach (var item in articles.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Articles.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return db.Articles.Where(a => !a.Category.IsSponsor && !a.Category.IsPartner).OrderBy(a => a.OrderNum);
        }

        public GetArticlesModel GetArticlesInCategory(int cID = 0)
        {
            GetArticlesModel m = new GetArticlesModel();

            if (cID != 0)
            {
                m.Articles = db.Articles.Where(a => a.CategoryID == cID).OrderBy(a => a.OrderNum).ToList();
                m.Category = db.Categories.Find(cID);
                return m;
            }
            m.Category = db.Categories.FirstOrDefault();
            m.Articles = db.Articles.Where(a => a.CategoryID == m.Category.ID).OrderBy(a => a.OrderNum).ToList();
            return m;
        }

        public IEnumerable<Models.MediaType> GetMediaTypes()
        {
            return db.MediaTypes;
        }

        public List<SelectListItem> GetMediaTypesSelectList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var mt in GetMediaTypes())
            {
                if (mt.ID == 5) continue;
                list.Add(new SelectListItem()
                {
                    Text = mt.Type,
                    Value = mt.ID.ToString(),
                    Selected = mt.ID == 4 ? true : false
                });
            }
            return list;
        }

        public ArticleModel GetArticleForEdit(int aID)
        {
            ArticleModel model = new ArticleModel();
            Article article = db.Articles.FirstOrDefault(v => v.ID == aID);
            if (article == null) return null;
            model.Article = article;
            model.Content = GetContent(article.MediaTypeID, article.ContentID);
            return model;
        }

        public void UpdateArticle(ArticleModel article)
        {
            Article updArt = db.Articles.Find(article.Article.ID);
            if (updArt == null) return;
            updArt.Headline = article.Article.Headline;
            updArt.ExtLink = article.Article.ExtLink;
            updArt.Ingress = article.Article.Ingress;
            updArt.Body = article.Article.Body;
            db.SaveChanges();
            article.Content.MediaID = article.Article.MediaTypeID;
            updArt.ContentID = UpdateMedia(updArt.ContentID, article.Content);
            db.SaveChanges();
        }

        //Categories
        public void CreateCategory(CategoryModel cat)
        {
            if (cat != null)
            {
                if (cat.Category.MediaTypeID == 0) cat.Category.MediaTypeID = 4;
                Category newCat = db.Categories.Add(cat.Category);
                db.SaveChanges();
                cat.Content.MediaID = cat.Category.MediaTypeID;
                newCat.ContentID = AddContent(cat.Content, newCat.MediaTypeID);
                db.SaveChanges();
            }
        }

        public void UpdateCategoryName(Category cat)
        {
            if (cat != null)
            {
                Category c = db.Categories.Find(cat.ID);
                if (c != null)
                {
                    c.Name = cat.Name;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteCategory(int cID)
        {
            Category cat = db.Categories.Find(cID);
            if (cat != null)
            {
                if (cat.Articles.Count == 0)
                {
                    if (cat.ContentID == 0)
                    {
                        db.Categories.Remove(cat);
                        db.SaveChanges();
                    }
                    else
                    {
                        db.Categories.Remove(cat);
                        DeleteContent(cat.MediaTypeID, cat.ContentID);
                        db.SaveChanges();
                    }

                }
            }
        }

        public void UpdateCategoryStatus(int cID, bool isActive)
        {
            Category c = db.Categories.Find(cID);
            if (c != null)
            {
                c.IsActive = isActive;
                db.SaveChanges();
            }
        }

        public void UpdateCategoryOrderNum(IEnumerable<Category> categories)
        {

            if (categories == null) return;
            int totalProducts = categories.Count();
            int currentNum = 0;
            foreach (var item in categories.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Categories.FirstOrDefault(c => c.ID == item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();

        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.Categories.Where(c => !c.IsPartner && !c.IsSponsor).OrderBy(c => c.OrderNum);
        }

        public CategoryModel GetCategoryForEdit(int categoryID)
        {
            CategoryModel model = new CategoryModel();
            Category category = db.Categories.Include(c => c.MediaType).FirstOrDefault(v => v.ID == categoryID);
            if (category == null) return null;
            model.Category = category;
            model.Content = GetContent(model.Category.MediaTypeID, model.Category.ContentID);
            return model;

        }

        public void UpdateCategory(CategoryModel category)
        {
            Category updcat = db.Categories.Find(category.Category.ID);
            if (updcat == null) return;
            updcat.Name = category.Category.Name;
            db.SaveChanges();
            category.Content.MediaID = category.Category.MediaTypeID;
            updcat.ContentID = UpdateMedia(updcat.ContentID, category.Content);
            db.SaveChanges();
        }

        //Footer
        public Footer GetFooter()
        {
            return db.Footer.FirstOrDefault();
        }
        public Metatag GetMetatag()
        {
            return db.Metatag.FirstOrDefault();
        }
        public void UpdateMetatag(Metatag meta)
        {
            if (meta != null)
            {
                Metatag m = db.Metatag.FirstOrDefault();
                if (m == null)
                {
                    db.Metatag.Add(meta);
                }
                else
                {
                    m.Text = meta.Text;
                }
                db.SaveChanges();
            }
        }
        public void UpdateFooter(Footer footer)
        {
            if (footer != null)
            {
                Footer f = db.Footer.FirstOrDefault();
                if (f == null)
                {
                    db.Footer.Add(footer);
                }
                else
                {
                    f.Address = footer.Address;
                    f.PostAddress = footer.PostAddress;
                    f.LegalAddress = footer.LegalAddress;
                    f.Phone = footer.Phone;
                    f.COntactPhone = footer.COntactPhone;
                    f.CellPhone = footer.CellPhone;
                    f.Fax = footer.Fax;
                    f.Email = footer.Email;
                    f.SupportEmail = footer.SupportEmail;
                    f.GoogleMapIFrameLink = footer.GoogleMapIFrameLink;
                    f.FacebookLink = footer.FacebookLink;
                    f.ExternalLink = footer.ExternalLink;
                    f.SocialLink = footer.SocialLink;
                    f.CompanyName = footer.CompanyName;
                }
                db.SaveChanges();
            }
        }

        //User
        public void CreateUser(CreateUserProfileModel user)
        {
            if (user != null)
            {
                db.UserProfiles.Add(new UserProfile()
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IsActive = true
                });
                db.SaveChanges();

            }
        }
        public void UpdateUser(UserProfile user)
        {
            if (user != null)
            {
                UserProfile edituser = db.UserProfiles.FirstOrDefault(u => u.UserId == user.UserId);
                if (edituser != null)
                {
                    edituser.UserName = user.UserName;
                    edituser.FirstName = user.FirstName;
                    edituser.LastName = user.LastName;
                    edituser.Email = user.Email;
                    db.SaveChanges();
                }
            }
        }
        public void DeleteUser(int userID)
        {
            UserProfile deluser = db.UserProfiles.Find(userID);
            if (deluser != null)
            {
                db.UserProfiles.Remove(deluser);
                Membership mu = db.Memberships.Find(userID);
                if (mu != null)
                {
                    db.Memberships.Remove(mu);
                }
                db.SaveChanges();
            }
        }
        public UserProfile GetUserByID(int userID)
        {
            UserProfile user = db.UserProfiles.Find(userID);
            return user;
        }
        public IEnumerable<UserProfile> GetAllUsers()
        {
            return db.UserProfiles;
        }
        public void UpdateUserStatus(int userID, bool isActive)
        {
            UserProfile user = db.UserProfiles.Find(userID);
            if (user == null) return;
            user.IsActive = isActive;
            db.SaveChanges();
        }
        public bool IsUserActive(string userName)
        {
            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName == userName);
            if (user == null) return false;
            return user.IsActive;
        }
        public bool IsUserNameExist(string userName)
        {
            return db.UserProfiles.Any(n => n.UserName == userName);
        }
        public bool IsUserEmailExist(string email)
        {
            return db.UserProfiles.Any(u => u.Email == email);
        }
        public string GetUserNamebyEmail(string email)
        {
            UserProfile user = db.UserProfiles.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user.UserName;
        }


        //Event
        public IEnumerable<Event> GetAllEvents()
        {
            return db.Events.Where(e => e.FestivalID == null).OrderBy(e => e.OrderNum);
        }

        public Event GetEvent(int eID)
        {
            return db.Events.FirstOrDefault(ed => ed.ID == eID);
        }

        public void UpdateEvent(EventModel evnt)
        {
            Event e = db.Events.Find(evnt.Event.ID);
            if (e == null) return;
            e.Stage = evnt.Event.Stage;
            e.Description = evnt.Event.Description;
            e.Duration = evnt.Event.Duration;
            e.ExtLink = evnt.Event.ExtLink;
            e.StartOn = evnt.Event.StartOn;
            e.TicketCode = evnt.Event.TicketCode;
            e.Title = evnt.Event.Title;
            foreach (var artist in db.ArtistsOnEvents.Where(a => a.EventID == e.ID))
            {
                db.ArtistsOnEvents.Remove(artist);
            }
            foreach (var artistID in evnt.SelectedArtists)
            {
                db.ArtistsOnEvents.Add(new ArtistOnEvent()
                    {
                        ArtistID = artistID,
                        EventID = e.ID
                    });
            }
            db.SaveChanges();
        }

        public void CreateEvent(EventModel evnt)
        {
            if (evnt == null && evnt.Event != null) return;
            if (evnt.Event.FestivalID == null)
            {
                evnt.Event.OrderNum = db.Events.Where(ev => ev.FestivalID == null).Count() + 1;
            }
            else
            {
                evnt.Event.OrderNum = db.Events.Where(en => en.FestivalID == evnt.Event.FestivalID).Count() + 1;
            }
            Event e = db.Events.Add(evnt.Event);
            db.SaveChanges();
            foreach (var artistID in evnt.SelectedArtists)
            {
                db.ArtistsOnEvents.Add(new ArtistOnEvent()
                    {
                        ArtistID = artistID,
                        EventID = e.ID
                    });
            }
            db.SaveChanges();

        }

        public void DeleteEvent(int eID, int fID = 0)
        {
            foreach (var item in db.ArtistsOnEvents.Where(e => e.EventID == eID).ToList())
            {
                db.ArtistsOnEvents.Remove(item);
            }
            db.SaveChanges();
            Event ev = db.Events.Find(eID);
            if (ev != null)
            {
                db.Events.Remove(ev);
                db.SaveChanges();
            }
        }
        public void CreateEventInFestival(EventModel evnt)
        {
            if (evnt == null && evnt.Event != null) return;
            evnt.Event.OrderNum = db.Events.Where(en => en.FestivalID == evnt.Event.FestivalID).Count() + 1;
            Event e = db.Events.Add(evnt.Event);
            db.SaveChanges();
            foreach (var artistID in evnt.SelectedArtists)
            {
                db.ArtistsOnEvents.Add(new ArtistOnEvent()
                {
                    ArtistID = artistID,
                    EventID = e.ID
                });
            }
            db.SaveChanges();
        }
        public void UpdateEventOrdernum(IEnumerable<Event> evnts)
        {
            if (evnts == null) return;
            int currentNum = 0;
            foreach (var item in evnts.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Events.Find(item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }

        public void ActivateEvent(int eID, bool toActivate)
        {
            Event c = db.Events.Find(eID);
            if (c != null)
            {
                c.isActive = toActivate;
                db.SaveChanges();
            }
        }

        //Artist
        public void CreateArtist(ArtistModel artistmodel)
        {
            if (artistmodel == null || artistmodel.Artist == null) return;
            artistmodel.Artist.OrderNum = db.Artists.Except(db.ArtistsOnFestivals.Select(ar => ar.Artist)).Count() + 1;
            Artist a = db.Artists.Add(artistmodel.Artist);
            db.SaveChanges();
            artistmodel.Content.MediaID = artistmodel.Artist.MediaTypeID;
            a.ContentID = AddContent(artistmodel.Content, a.MediaTypeID);
            db.SaveChanges();
        }

        public void UpdateArtist(ArtistModel artistmodel)
        {
            if (artistmodel == null || artistmodel.Artist == null) return;
            Artist newArtist = db.Artists.Find(artistmodel.Artist.ID);
            if (newArtist == null) return;
            newArtist.Name = artistmodel.Artist.Name;
            newArtist.Description = artistmodel.Artist.Description;
            newArtist.ExtLink = artistmodel.Artist.ExtLink;
            db.SaveChanges();
            artistmodel.Content.MediaID = newArtist.MediaTypeID;
            newArtist.ContentID = UpdateMedia(newArtist.ContentID, artistmodel.Content);
            db.SaveChanges();
        }

        public void DeleteArtist(int artistID, int fID = 0)
        {
            Artist a = db.Artists.Find(artistID);
            if (a == null) return;
            if (db.ArtistsOnEvents.Any(ae => ae.ArtistID == artistID)) return;
            if (fID != 0)
            {
                ArtistOnFestival artist = db.ArtistsOnFestivals.FirstOrDefault(af => af.ArtistID == artistID);
                if (artist != null)
                {
                    db.ArtistsOnFestivals.Remove(artist);
                    db.SaveChanges();
                }
            }
            if (a.ContentID == 0)
            {
                db.Artists.Remove(a);
            }
            else
            {
                DeleteContent(a.MediaTypeID, a.ContentID);
                db.Artists.Remove(a);
            }
            db.SaveChanges();
        }

        public IEnumerable<Artist> GetAllArtists()
        {
            return db.Artists.Except(db.ArtistsOnFestivals.Include(a => a.Artist).Select(a => a.Artist)).OrderBy(a => a.OrderNum);
        }

        public ArtistModel GetArtist(int artistID)
        {

            Artist a = db.Artists.Find(artistID);
            if (a == null) return new ArtistModel();
            ArtistModel aModel = new ArtistModel();
            aModel.Artist = a;
            aModel.Content = GetContent(a.MediaTypeID, a.ContentID);
            return aModel;
        }

        public IEnumerable<SelectListItem> GetAllArtistsInSelectedList()
        {
            List<SelectListItem> artists = new List<SelectListItem>();
            foreach (var a in db.Artists.Except(db.ArtistsOnFestivals.Include(af => af.Artist).Select(af => af.Artist)))
            {
                artists.Add(new SelectListItem()
                {
                    Value = a.ID.ToString(),
                    Text = a.Name
                });
            }
            return artists;
        }

        public void UpdateArtistsOrderNum(IEnumerable<Artist> artists)
        {
            if (artists == null) return;
            int currentNum = 0;
            foreach (var item in artists.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Artists.Find(item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }
        public void UpdateArtistStatus(int aID, bool toActive)
        {
            Artist c = db.Artists.Find(aID);
            if (c != null)
            {
                c.isActive = toActive;
                db.SaveChanges();
            }
        }
        public void CreateArtistInFestival(ArtistModel artistmodel)
        {
            if (artistmodel == null || artistmodel.Artist == null) return;
            artistmodel.Artist.OrderNum = db.ArtistsOnFestivals.Where(af => af.FestivalID == artistmodel.Festival.ID).Count() + 1;
            Artist a = db.Artists.Add(artistmodel.Artist);
            db.SaveChanges();
            artistmodel.Content.MediaID = artistmodel.Artist.MediaTypeID;
            a.ContentID = AddContent(artistmodel.Content, a.MediaTypeID);
            db.SaveChanges();
            db.ArtistsOnFestivals.Add(new ArtistOnFestival()
            {
                ArtistID = a.ID,
                FestivalID = artistmodel.Festival.ID
            });
            db.SaveChanges();
        }

        public IEnumerable<Artist> GetAllArtistsInFestival(int fID)
        {

            return db.ArtistsOnFestivals.Where(a => a.FestivalID == fID).Include(a => a.Artist).Select(a => a.Artist).OrderBy(a => a.OrderNum);
        }

        public IEnumerable<SelectListItem> GetAllArtistsInFestivalSelectedList(int fID)
        {
            List<SelectListItem> artists = new List<SelectListItem>();
            foreach (var a in db.ArtistsOnFestivals.Include(a => a.Artist).Where(a => a.FestivalID == fID))
            {
                artists.Add(new SelectListItem()
                {
                    Value = a.ArtistID.ToString(),
                    Text = a.Artist.Name
                });
            }
            return artists;
        }
        public IEnumerable<SelectListItem> GetAllArtistsInEventSelectedList(int eID, int fID = 0)
        {
            List<SelectListItem> artists = new List<SelectListItem>();
            if (fID == 0)
            {
                foreach (var artist in db.Artists.Except(db.ArtistsOnFestivals.Include(a => a.Artist).Select(a => a.Artist)).ToList())
                {
                    artists.Add(new SelectListItem()
                        {
                            Value = artist.ID.ToString(),
                            Text = artist.Name,
                            Selected = db.ArtistsOnEvents.Where(e => e.EventID == eID).Any(a => a.ArtistID == artist.ID) ? true : false
                        });
                }
                return artists;
            }
            foreach (var artist in db.ArtistsOnFestivals.Where(f => f.FestivalID == fID).Include(a => a.Artist).Select(a => a.Artist).ToList())
            {
                artists.Add(new SelectListItem()
                {
                    Value = artist.ID.ToString(),
                    Text = artist.Name,
                    Selected = db.ArtistsOnEvents.Where(e => e.EventID == eID).Any(a => a.ArtistID == artist.ID) ? true : false
                });
            }
            return artists;
        }

        //Festival
        public int CreateFestival(Festival fest)
        {
            if (fest == null) return 0;
            fest.isActive = true;
            fest.OrderNum = db.Festivals.Count() + 1;
            Festival f = db.Festivals.Add(fest);
            db.SaveChanges();
            return f.ID;
        }

        public void UpdateFestival(Festival fest)
        {
            Festival f = db.Festivals.Find(fest.ID);
            if (f == null) return;
            f.From = fest.From;
            f.Name = fest.Name;
            f.Untill = fest.Untill;
            f.Description = fest.Description;
            db.SaveChanges();
        }

        public void DeleteFestival(int festID)
        {

            if (db.ArtistsOnFestivals.Any(fst => fst.FestivalID == festID)) return;
            if (db.Events.Where(e => e.FestivalID == festID).Count() > 0) return;
            Festival f = db.Festivals.Find(festID);
            if (f != null)
            {
                db.Festivals.Remove(f);
                db.SaveChanges();
            }
        }

        public IEnumerable<Festival> GetAllFestivals()
        {
            return db.Festivals.OrderBy(f => f.OrderNum);
        }

        public Festival GetFestival(int festID)
        {
            return db.Festivals.Find(festID);
        }

        public EventsInFestivalModel GetEventsInFestivals(int fID)
        {
            EventsInFestivalModel model = new EventsInFestivalModel();
            model.Events = db.Events.Where(e => e.FestivalID == fID).OrderBy(e => e.OrderNum).ToList();
            model.Festival = db.Festivals.Find(fID);
            return model;
        }

        public void ToActivateFestival(int fID, bool toActive)
        {
            Festival fest = db.Festivals.Find(fID);
            if (fest != null)
            {
                fest.isActive = toActive;
                db.SaveChanges();
            }
        }
        public void UpdateFestivalsOrderNums(IEnumerable<Festival> fests)
        {
            if (fests == null) return;
            int currentNum = 0;
            foreach (var item in fests.OrderBy(p => p.OrderNum))
            {
                currentNum++;
                db.Festivals.Find(item.ID).OrderNum = currentNum;
            }
            db.SaveChanges();
        }

        public GetArticlesModel GetPartners()
        {

            GetArticlesModel partners = new GetArticlesModel();
            partners.Articles = db.Articles.Where(a => a.Category.IsPartner).OrderBy(a => a.OrderNum).ToList();
            partners.Category = db.Categories.FirstOrDefault(c => c.IsPartner);
            return partners;
        }

        public GetArticlesModel GetSponsors()
        {
            GetArticlesModel sponsors = new GetArticlesModel();
            sponsors.Articles = db.Articles.Where(a => a.Category.IsSponsor).OrderBy(a => a.OrderNum).ToList();
            sponsors.Category = db.Categories.FirstOrDefault(c => c.IsSponsor);
            return sponsors;
        }






    }
}
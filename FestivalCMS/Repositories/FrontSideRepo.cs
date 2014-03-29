using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FestivalCMS.DAL;
using FestivalCMS.Models;
using System.Data.Entity;

namespace FestivalCMS.Repositories
{
    public class FrontSideRepo : ContentRepositoryBase, IFrontSideRepo
    {
        public FrontSideRepo(FestivalDBContext db)
            : base(db)
        {

        }

        public HomePageModel GetDataForHomePage()
        {
            HomePageModel homepage = new HomePageModel();
            Category catNews = db.Categories.FirstOrDefault(c => c.Name.Equals("News", StringComparison.InvariantCultureIgnoreCase));
            if (catNews != null)
            {
                homepage.News = db.Articles.Where(a => a.CategoryID == catNews.ID && a.IsActive).OrderBy(a => a.OrderNum).ToList();
            }
            foreach (var item in homepage.News)
            {
                Photo photo = db.Photos.Find(item.ContentID);
                string fileName = photo == null ? null : photo.FileName;
                homepage.NewsPhotosLinks.Add(item.ID, fileName);
            }
            Festival fest = db.Festivals.Where(f => f.isActive).FirstOrDefault();
            if (fest != null)
            {
                homepage.Festivals.Add(fest);
                homepage.FestivalEvents = db.Events.Include(f => f.EventArtists).Where(f => f.FestivalID == fest.ID && f.isActive).ToList();
            }
            homepage.ProgrammEvents = db.Events.Include(f => f.EventArtists).Where(f => f.FestivalID == null && f.isActive).ToList();
            foreach (var item in homepage.FestivalEvents)
            {
                foreach (var artist in item.EventArtists)
                {
                    Artist art = db.ArtistsOnFestivals.Include(a => a.Artist).Select(a => a.Artist).FirstOrDefault(a => a.ID == artist.ID);
                    if (art == null) continue;
                    artist.Artist = art;
                    Photo photo = db.Photos.Find(art.ContentID);
                    string fileName = photo == null ? null : photo.FileName;
                    if (homepage.ArtistsPhotos.Where(a => a.Key == art.ID).Count() == 0)
                    {
                        homepage.ArtistsPhotos.Add(artist.ID, fileName);
                    }
                }
            }
            foreach (var prog in homepage.ProgrammEvents)
            {
                foreach (var artist in prog.EventArtists)
                {
                    Artist art = db.Artists.FirstOrDefault(a => a.ID == artist.ArtistID);
                    if (art == null) continue;
                    artist.Artist = art;
                    Photo photo = db.Photos.Find(art.ContentID);
                    string fileName = photo == null ? null : photo.FileName;
                    if (homepage.ArtistsPhotos.Where(a => a.Key == art.ID).Count() == 0)
                    {
                        homepage.ArtistsPhotos.Add(art.ID, fileName);
                    }
                }

            }
            return homepage;
        }

        public NewsPageModel GetDataForNewsPage(int id)
        {
            NewsPageModel newspage = new NewsPageModel();
            newspage.News = db.Articles.Find(id);
            if (newspage.News == null) newspage.News = new Article();
            Photo photo = db.Photos.Find(newspage.News.ContentID);
            string fileName = photo == null ? null : photo.FileName;
            newspage.NewsPhotosLink = fileName;
            Festival fest = db.Festivals.Where(f => f.isActive).FirstOrDefault();
            if (fest != null)
            {
                newspage.Festivals.Add(fest);
                newspage.FestivalEvents = db.Events.Include(f => f.EventArtists).Where(f => f.FestivalID == fest.ID && f.isActive).ToList();
            }
            newspage.ProgrammEvents = db.Events.Include(f => f.EventArtists).Where(f => f.FestivalID == null && f.isActive).ToList();
            foreach (var item in newspage.FestivalEvents)
            {
                foreach (var artist in item.EventArtists)
                {
                    Artist art = db.ArtistsOnFestivals.Include(a => a.Artist).Select(a => a.Artist).FirstOrDefault(a => a.ID == artist.ID);
                    if (art == null) continue;
                    artist.Artist = art;
                    Photo artistPhoto = db.Photos.Find(art.ContentID);
                    string photoFileName = photo == null ? null : photo.FileName;
                    if (newspage.ArtistsPhotos.Where(a => a.Key == art.ID).Count() == 0)
                    {
                        newspage.ArtistsPhotos.Add(artist.ID, photoFileName);
                    }
                }
            }
            foreach (var prog in newspage.ProgrammEvents)
            {
                foreach (var artist in prog.EventArtists)
                {
                    Artist art = db.Artists.FirstOrDefault(a => a.ID == artist.ArtistID);
                    if (art == null) continue;
                    artist.Artist = art;
                    Photo artistPhoto = db.Photos.Find(art.ContentID);
                    string photoFileName = artistPhoto == null ? null : artistPhoto.FileName;
                    if (newspage.ArtistsPhotos.Where(a => a.Key == art.ID).Count() == 0)
                    {
                        newspage.ArtistsPhotos.Add(art.ID, photoFileName);
                    }
                }

            }
            return newspage;
        }

        public ProgramPageModel GetProgram()
        {
            ProgramPageModel programmModel = new ProgramPageModel();
            programmModel.ProgrammEvents = db.Events.Include(f => f.EventArtists).Where(f => f.FestivalID == null && f.isActive).ToList();
            foreach (var prog in programmModel.ProgrammEvents)
            {
                foreach (var artist in prog.EventArtists)
                {
                    Artist art = db.Artists.FirstOrDefault(a => a.ID == artist.ArtistID);
                    if (art == null) continue;
                    artist.Artist = art;
                    Photo photo = db.Photos.Find(art.ContentID);
                    string fileName = photo == null ? null : photo.FileName;
                    if (programmModel.ArtistsPhotos.Where(a => a.Key == art.ID).Count() == 0)
                    {
                        programmModel.ArtistsPhotos.Add(art.ID, fileName);
                    }
                }
            }
            return programmModel;
        }

    }
}
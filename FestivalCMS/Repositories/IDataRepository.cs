using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FestivalCMS.Models;

namespace FestivalCMS.Repositories
{
    public interface IDataRepository : IDisposable
    {
        //Article methods
        void CreateArticle(ArticleModel cat);
        void DeleteArticle(int aID);
        void UpdateArticleStatus(int aID, bool isActive);
        void UpdateArticleOrderNum(IEnumerable<Article> categories);
        IEnumerable<Article> GetAllArticles();
        GetArticlesModel GetArticlesInCategory(int cID = 0);

        ArticleModel GetArticleForEdit(int aID);
        void UpdateArticle(ArticleModel article);

        //Categories methods
        void CreateCategory(CategoryModel cat);
        void UpdateCategoryName(Category cat);
        void DeleteCategory(int cID);
        void UpdateCategoryStatus(int cID, bool isActive);
        void UpdateCategoryOrderNum(IEnumerable<Category> categories);
        IEnumerable<Category> GetAllCategories();
        CategoryModel GetCategoryForEdit(int categoryID);
        void UpdateCategory(CategoryModel category);

        //Mediatypes methods
        IEnumerable<MediaType> GetMediaTypes();
        List<SelectListItem> GetMediaTypesSelectList();


        //Footer methods
        void UpdateFooter(Footer footer);
        Footer GetFooter();
        Metatag GetMetatag();
        void UpdateMetatag(Metatag meta);

        //User methods
        void CreateUser(CreateUserProfileModel user);
        void UpdateUser(UserProfile user);
        void DeleteUser(int userID);
        UserProfile GetUserByID(int userID);
        IEnumerable<UserProfile> GetAllUsers();
        void UpdateUserStatus(int userID, bool isActive);
        bool IsUserActive(string userName);
        bool IsUserNameExist(string userName);
        bool IsUserEmailExist(string email);
        string GetUserNamebyEmail(string email);


        //Artist
        void CreateArtist(ArtistModel artistmodel);
        void CreateArtistInFestival(ArtistModel artistmodel);
        void UpdateArtist(ArtistModel artistmodel);
        void DeleteArtist(int artistID, int fID=0);
        IEnumerable<Artist> GetAllArtists();
        IEnumerable<Artist> GetAllArtistsInFestival(int fID);
        ArtistModel GetArtist(int artistID);
        IEnumerable<SelectListItem> GetAllArtistsInSelectedList();
        IEnumerable<SelectListItem> GetAllArtistsInFestivalSelectedList(int fID);
        IEnumerable<SelectListItem> GetAllArtistsInEventSelectedList(int eID, int fID = 0);

        void UpdateArtistsOrderNum(IEnumerable<Artist> artists);
        void UpdateArtistStatus(int aID, bool toActive);

        //Events
        IEnumerable<Event> GetAllEvents();
        Event GetEvent(int eID);
        void UpdateEvent(EventModel evnt);
        void CreateEvent(EventModel evnt);
        void CreateEventInFestival(EventModel evnt);
        void DeleteEvent(int eID, int fID=0);
        void UpdateEventOrdernum(IEnumerable<Event> evnts);
        void ActivateEvent(int eID, bool toActivate);
        
        //Festival
        int CreateFestival(Festival fest);
        void UpdateFestival(Festival fest);
        void DeleteFestival(int festID);
        IEnumerable<Festival> GetAllFestivals();
        Festival GetFestival(int festID);
        EventsInFestivalModel GetEventsInFestivals(int fID);
        void ToActivateFestival(int fID, bool toActive);
        void UpdateFestivalsOrderNums(IEnumerable<Festival> fests);

        //Partners
        GetArticlesModel GetPartners();

        //Sponsors
        GetArticlesModel GetSponsors();
    }
}

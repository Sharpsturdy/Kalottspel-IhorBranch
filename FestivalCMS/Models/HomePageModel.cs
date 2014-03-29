using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class HomePageModel
    {
        public HomePageModel()
        {
            News = new List<Article>();
            NewsPhotosLinks = new Dictionary<int, string>();
            FestivalEvents = new List<Event>();
            ProgrammEvents = new List<Event>();
            Festivals = new List<Festival>();
            ArtistsPhotos = new Dictionary<int, string>();
        }
        public List<Article> News { get; set; }
        public Dictionary<int, string> NewsPhotosLinks { get; set; }
        public List<Event> FestivalEvents { get; set; }
        public List<Event> ProgrammEvents { get; set; }
        public List<Festival> Festivals { get; set; }
        public Dictionary<int, string> ArtistsPhotos { get; set; }
    }
}
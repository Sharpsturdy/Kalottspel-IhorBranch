using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class NewsPageModel
    {
        public NewsPageModel()
        {
            News = new Article();
            FestivalEvents = new List<Event>();
            ProgrammEvents = new List<Event>();
            Festivals = new List<Festival>();
            ArtistsPhotos = new Dictionary<int, string>();
        }
        public Article News { get; set; }
        public string NewsPhotosLink { get; set; }
        public List<Event> FestivalEvents { get; set; }
        public List<Event> ProgrammEvents { get; set; }
        public List<Festival> Festivals { get; set; }
        public Dictionary<int, string> ArtistsPhotos { get; set; }
    }
}
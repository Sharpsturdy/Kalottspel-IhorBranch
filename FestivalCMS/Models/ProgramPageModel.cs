using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class ProgramPageModel
    {
        public ProgramPageModel()
        {
            ProgrammEvents = new List<Event>();
            ArtistsPhotos = new Dictionary<int, string>();
        }
        public List<Event> ProgrammEvents { get; set; }
        public Dictionary<int, string> ArtistsPhotos { get; set; }
    }
}
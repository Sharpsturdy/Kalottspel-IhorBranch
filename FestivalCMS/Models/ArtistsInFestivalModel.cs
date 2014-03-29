using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class ArtistsInFestivalModel
    {

        public ArtistsInFestivalModel()
        {
            Artists = new List<Artist>();
        }

        public List<Artist> Artists { get; set; }
        public Festival Festival { get; set; }
    }
}
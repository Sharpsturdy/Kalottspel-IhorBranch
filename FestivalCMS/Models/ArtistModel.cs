using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FestivalCMS.Models;

namespace FestivalCMS.Models
{
    public class ArtistModel
    {
        public ArtistModel()
        {
            Artist = new Artist();
            Content = new ContentModel();
            Festival = new Festival();
        }
        public Artist Artist { get; set; }
        public ContentModel Content { get; set;}
        public List<SelectListItem> MediaTypes { get; set; }
        public Festival Festival { get; set; }
    }
}
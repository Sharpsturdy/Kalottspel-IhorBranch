using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class ContentModel
    {
        public int MediaID { get; set; }
        public PhotoMediaModel Photo { get; set; }
        public PhotoGallereyModel Gallery { get; set; }
        public VideoLinkModel VideoLink { get; set; }
    }
}
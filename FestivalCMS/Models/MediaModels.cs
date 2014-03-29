using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class PhotoMediaModel
    {
        public string FileName { get; set; }
        public HttpPostedFileBase PhotoFile { get; set; }
    }

    public class PhotoGallereyModel
    {
        public PhotoGallereyModel()
        {
            PhotoFiles = new List<PhotoMediaModel>();
        }
        public List<PhotoMediaModel> PhotoFiles { get; set; } 
    }

    public class VideoLinkModel
    {
        public string Link { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using FestivalCMS.App_Code;

namespace FestivalCMS.Models
{
    public class ArticleModel
    {
        public ArticleModel()
        {
            Article = new Article();
            Content = new ContentModel();
        }

        public Article Article { get; set; }
        public IEnumerable<SelectListItem> MediaTypes { get; set; }
        public ContentModel Content { get; set; }


    }
}
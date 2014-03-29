using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalCMS.Models
{
    public class ArticlesFrontModel
    {
        public ArticlesFrontModel()
        {
            Articles = new List<Article>();
            Photos = new Dictionary<int, string>();
        }
        public List<Article> Articles { get; set; }
        public Dictionary<int, string> Photos { get; set; }
    }
}
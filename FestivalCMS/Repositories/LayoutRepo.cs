using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FestivalCMS.Models;
using FestivalCMS.DAL;

namespace FestivalCMS.Repositories
{
    public class LayoutRepo : ContentRepositoryBase, ILayoutRepo
    {
        public LayoutRepo(FestivalDBContext db)
            : base(db)
        {

        }

        public ArticlesFrontModel GetSponsors()
        {
            ArticlesFrontModel sponsors = new ArticlesFrontModel();
            Category categorySponsors = db.Categories.FirstOrDefault(c => c.IsSponsor);
            if (categorySponsors == null) return sponsors;
            sponsors.Articles = db.Articles.Where(a => a.CategoryID == categorySponsors.ID && a.IsActive).OrderBy(a => a.OrderNum).ToList();
            foreach (var sponsor in sponsors.Articles)
            {
                string fileName = db.Photos.Find(sponsor.ContentID) == null ? null : db.Photos.Find(sponsor.ContentID).FileName;
                sponsors.Photos.Add(sponsor.ID, fileName);
            }
            return sponsors;
        }

        public ArticlesFrontModel GetPartners()
        {
            ArticlesFrontModel partners = new ArticlesFrontModel();
            Category categoryPartners = db.Categories.FirstOrDefault(c => c.IsPartner);
            if (categoryPartners == null) return partners;
            partners.Articles = db.Articles.Where(a => a.CategoryID == categoryPartners.ID && a.IsActive).OrderBy(a => a.OrderNum).ToList();
            foreach (var partner in partners.Articles)
            {
                string fileName = db.Photos.Find(partner.ContentID) == null ? null : db.Photos.Find(partner.ContentID).FileName;
                partners.Photos.Add(partner.ID, fileName);
            }
            return partners;
        }
    }
}
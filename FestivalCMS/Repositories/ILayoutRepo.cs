using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestivalCMS.Models;

namespace FestivalCMS.Repositories
{
    public interface ILayoutRepo:IDisposable
    {
        ArticlesFrontModel GetSponsors();
        ArticlesFrontModel GetPartners();
    }
}

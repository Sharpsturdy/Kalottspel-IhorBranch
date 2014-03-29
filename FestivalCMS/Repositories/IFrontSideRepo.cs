using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestivalCMS.Models;

namespace FestivalCMS.Repositories
{
    public interface IFrontSideRepo:IDisposable
    {
        HomePageModel GetDataForHomePage();
        NewsPageModel GetDataForNewsPage(int id);
        ProgramPageModel GetProgram();
    }
}

using Negev2.DataContext.Infrastructure;
using Negev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negev2.DataContext.Repositories
{
    public class SiteByYearRepository : RepositoryBase<SiteByYear>, ISiteByYearRepository
    {

        public SiteByYearRepository()
        {

        }
    }

    public interface ISiteByYearRepository : IRepository<SiteByYear>
    {

    }
}
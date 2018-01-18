using Negev2.DataContext.Infrastructure;
using Negev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negev2.DataContext.Repositories
{
    public class SiteRepository : RepositoryBase<Site>, ISiteRepository
    {

        public SiteRepository()
        {

        }
    }

    public interface ISiteRepository : IRepository<Site>
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negev2.DataContext.Infrastructure
{
    public static class DatabaseFactory
    { 

        public static CropsDb Get()
        {
            return CropsDb.Instance;
        }

        public static void Dispose()
        {
            CropsDb.Instance.Dispose();
        }

        public static void Save()
        {
            CropsDb.Instance.SaveChanges();
        }
    }
}
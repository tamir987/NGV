using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Device.Location;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using Negev2.Models;
using Microsoft.SqlServer.Types;

namespace Negev2.Models
{
    public class Site
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public String Name { get; set; }

        public int Dunam { get; set; }

        public String Region { get; set; }

        //public virtual ICollection<Coordinatez> Shape { get; set; }

        public SqlGeometry Shape { get; set; }

        public virtual ICollection<SiteByYear> SitesByYear { get; set; }
    }
}
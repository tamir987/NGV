using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Device.Location;
using System.Linq;
using System.Web;

namespace Negev2.Models
{
    public class Coordinatez
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public double Longtitude { get; set; }

        public double Llatitude { get; set; }

        public int CurrentSiteId { get; set; }

        public virtual Site CurrentSite { get; set; }

    }
}
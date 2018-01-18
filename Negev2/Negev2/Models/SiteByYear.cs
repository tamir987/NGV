using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Negev2.Models
{
    public class SiteByYear
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CurrentLayerId { get; set; }
        
        public virtual Layer CurrentLayer { get; set; }

        public int CurrentCropId { get; set; }

        public virtual Crops CurrentCrop { get; set; }

        public int CurrentSiteId { get; set; }

        public virtual Site CurrentSite { get; set; }
    }
}
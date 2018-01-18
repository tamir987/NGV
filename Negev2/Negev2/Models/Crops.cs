using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Negev2.Models
{
    public class Crops
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; } = "No further description available.";

        public int Quantity { get; set; } = 0;

        public virtual ICollection<SiteByYear> SitesByYear { get; set; }

        //public Crops DeepClone()
        //{
        //    return this.MemberwiseClone() as Crops;
        //}
    }
}
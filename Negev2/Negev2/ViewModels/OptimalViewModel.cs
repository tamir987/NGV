using Negev2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negev2.ViewModels
{
    public class OptimalViewModel
    {
        public IEnumerable<Crops> Crops { get; set; } //crops that not entered yet to the proccess
        public IEnumerable<Crops> CurCrops { get; set; } //crops that entered to the proccess

        public int Id { get; set; }
        public String Name { get; set; }
        public int Quantity { get; set; }


        public OptimalViewModel()
        {
        }

        public OptimalViewModel(Crops crop, int quantity)
        {
            Quantity = quantity;
            Id = crop.Id;
            Name = crop.Name;
        }
    }
}
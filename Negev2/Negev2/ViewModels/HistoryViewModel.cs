using Negev2.HelpingModels;
using Negev2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Negev2.ViewModels
{
    public class HistoryViewModel
    {

        public IEnumerable<IdToName> LayersIdToName { get; set; }
        public IEnumerable<Layer> LayersCollection { get; set; }

        public HistoryViewModel()
        {
           
        }
    }
}
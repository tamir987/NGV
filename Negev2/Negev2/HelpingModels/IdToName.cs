using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Negev2.HelpingModels
{
    public struct IdToName
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public IdToName(int IdTo, String NameTo)
        {
            Id = IdTo;
            Name = NameTo;
        }
    }
}
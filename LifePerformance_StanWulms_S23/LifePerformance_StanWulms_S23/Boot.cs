using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Boot
    {
        public String Naam { get; set; }
        public String Type { get; set; }
        public String Beschrijving { get; set; }

        public Boot()
        {

        }

        public Boot(string naam, string type, string beschrijving)
        {
            this.Naam = naam;
            this.Type = type;
            this.Beschrijving = beschrijving;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

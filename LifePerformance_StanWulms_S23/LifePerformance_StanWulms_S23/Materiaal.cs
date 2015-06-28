using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Materiaal
    {
        public String Naam { get; set; }
        public String Beschrijving { get; set; }
        
        public Materiaal()
        {

        }

        public Materiaal(string naam, string beschrijving)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

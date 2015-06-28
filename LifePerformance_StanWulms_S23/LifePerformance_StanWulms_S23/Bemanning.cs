using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Bemanning
    {
        public String Naam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public String Functie { get; set; }

        List<Missie> missies = new List<Missie>();
        
        public Bemanning()
        {
                
        }

        public Bemanning(string naam, DateTime geboortedatum, string functie)
        {
            this.Naam = naam;
            this.Geboortedatum = geboortedatum;
            this.Functie = functie;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

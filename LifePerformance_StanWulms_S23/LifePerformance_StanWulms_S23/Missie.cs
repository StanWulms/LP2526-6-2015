using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Missie
    {
        public int ID { get; set; }
        public String Beschrijving { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        List<Bemanning> bemaningsleden = new List<Bemanning>();
        List<Materiaal> materialen = new List<Materiaal>();
        Boot boot = new Boot();

        public Missie()
        {

        }

        public Missie(int id, string beschrijving, int x, int y)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
        }
    }
}

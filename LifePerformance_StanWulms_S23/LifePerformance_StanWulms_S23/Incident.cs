using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Incident
    {
        public int ID { get; set; }
        public String Beschrijving { get; set; }

        public Incident()
        {

        }

        public Incident(int id, string beschrijving)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }
}

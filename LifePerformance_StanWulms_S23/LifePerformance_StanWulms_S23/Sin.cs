using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Sin : Missie
    {
        public DateTime Aankomsttijd { get; set; }
        public String Incidentbeschrijving { get; set; }

        List<Incident> incidenten = new List<Incident>();

        public Sin(int id, string beschrijving, int x, int y, DateTime aankomsttijd, string incidentbeschrijving)
                    : base(id, beschrijving,x,y)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
            this.Aankomsttijd = aankomsttijd;
            this.Incidentbeschrijving = incidentbeschrijving;
        }
    }
}

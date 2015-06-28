using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Hope : Missie
    {
        public DateTime Aankomsttijd { get; set; }
        public DateTime Vertrektijd { get; set; }

        Database db;
        List<Meting> metingen = new List<Meting>();

        public Hope()
        {

        }

        //De constructor erft over van Missie
        public Hope(int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
                    : base(id, beschrijving,x,y)
        {

            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
            this.Aankomsttijd = aankomsttijd;
            this.Vertrektijd = vertrektijd;
        }
        
        //Invoer zijn de parameters voor de methode MaakHMissie (komt overeen met de attributen in de database)
        public void HopeToevoegen(int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
        {
            db = new Database();
            db.MaakHMissies(id, beschrijving, x, y, aankomsttijd, vertrektijd);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

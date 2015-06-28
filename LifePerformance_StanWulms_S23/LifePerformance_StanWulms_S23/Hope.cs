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
        public List<Hope> hmissies { get; set; }

        Database db;
        List<Meting> metingen = new List<Meting>();

        public Hope()
        {
            hmissies = new List<Hope>();
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
            hmissies = new List<Hope>();
        }

        //Geef alle Hope-missies terug.
        public void GeefHopeMissies()
        {
            hmissies = new List<Hope>();
            db = new Database();
            hmissies = db.GetHopeMissies();
        }
        
        //Invoer zijn de parameters voor de methode MaakHMissie (komt overeen met de attributen in de database)
        public void HopeToevoegen(int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
        {
            db = new Database();
            db.MaakHMissies(id, beschrijving, x, y, aankomsttijd, vertrektijd);
            hmissies = new List<Hope>();
        }

        public void HopeUpdaten(int missieID, int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
        {
            db = new Database();
            db.UpdateHMissies(missieID, id, beschrijving, x, y, aankomsttijd, vertrektijd);
        }

        public void HopeDelete(int hopeID)
        {
            db = new Database();
            db.DeleteHope(hopeID);
        }

        public override string ToString()
        {
            return "ID: " + ID + " - " + "Beschrijving: " + Beschrijving + " - " + "Aankomst: " + Aankomsttijd + " - " + "Vertrek: " + Vertrektijd;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Meting
    {
        public int ID { get; set; }
        public String Beschrijving { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public DateTime Tijdstip { get; set; }

        public List<Meting> metingen { get; set; }

        Database db;
        public Meting()
        {
            this.metingen = new List<Meting>();
        }

        public Meting(int id, string beschrijving, int x, int y, DateTime tijdstip)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
            this.Tijdstip = tijdstip;
            this.metingen = new List<Meting>();
        }

        //Haal de waardes op uit de database.
        public void GeefMetingen()
        {
            metingen = new List<Meting>();
            db = new Database();
            metingen = db.GetMeting();
        }

        //Toevoegen van een nieuwe meting. De parameters komen overeen met de attributen in de database.
        public void MetingToevoegen(int id, string beschrijving, int x, int y, DateTime tijdstip)
        {
            db = new Database();
            db.MaakMeting(id, beschrijving, x, y, tijdstip);
        }

        public override string ToString()
        {
            return "ID: " + ID + " - " + "Beschrijving: " + Beschrijving;
        }
    }
}

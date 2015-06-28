using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifePerformance_StanWulms_S23
{
    class Sin : Missie
    {
        public DateTime Vertrektijd { get; set; }
        public int IncidentID { get; set; }
        public int BootID { get; set; }
        public List<Sin> smissies { get; set; }

        List<Incident> incidenten = new List<Incident>();

        Database db;

        public Sin()
        {

        }

        public Sin(int id, string beschrijving, int x, int y, DateTime vertrektijd, int bootID)
            : base(id, beschrijving, x, y)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
            this.Vertrektijd = vertrektijd;
            this.BootID = bootID;
            smissies = new List<Sin>();
        }

        public Sin(int id, string beschrijving, int x, int y, DateTime vertrektijd, int incidentID, int bootID)
                    : base(id, beschrijving,x,y)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.X = x;
            this.Y = y;
            this.Vertrektijd = vertrektijd;
            this.IncidentID = incidentID;
            this.BootID = bootID;
            smissies = new List<Sin>();
        }

        public void GeefSinMissies()
        {
            smissies = new List<Sin>();
            db = new Database();
            smissies = db.GetSinMissies();
        }

        public void SinToevoegen(string beschrijving, int x, int y, DateTime vertrektijd)
        {
            db = new Database();
            db.MaakSMissies(beschrijving, x, y, vertrektijd);
            smissies = new List<Sin>();
        }

        public void SinWijzigen(int missieID, string beschrijving, int x, int y, DateTime vertrektijd)
        {
            db = new Database();
            db.UpdateSMissies(missieID, beschrijving, x, y, vertrektijd);
        }

        public void SinDelete(int sinID)
        {
            db = new Database();
            db.DeleteSin(sinID);
        }

        public override string ToString()
        {
            return "ID: " + ID + " - " + "BootID: " + BootID + " - " + "Beschrijving: " + Beschrijving + " - " + "Vertrektijd: " + Vertrektijd;
        }
    }
}

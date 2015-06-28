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
        public int MissieID { get; set; }
        public String Beschrijving { get; set; }
        public List<Incident> Incidenten { get; set; }

        Database db;

        public Incident()
        {
            this.Incidenten = new List<Incident>();
        }

        public Incident(int id, string beschrijving)
        {
            this.ID = id;
            this.Beschrijving = beschrijving;
            this.Incidenten = new List<Incident>();
        }

        public Incident(int id, int missieID, string beschrijving)
        {
            this.ID = id;
            this.MissieID = missieID;
            this.Beschrijving = beschrijving;
            this.Incidenten = new List<Incident>();
        }

        public void GeefIncidenten()
        {
            Incidenten = new List<Incident>();
            db = new Database();
            Incidenten = db.GetIncident();
        }

        public void IncidentToevoegen(int missieID, string beschrijving)
        {
            db = new Database();
            db.MaakIncident(missieID, beschrijving);
        }

        public void IncidentUpdaten(int incidentID, int missieID, string beschrijving)
        {
            db = new Database();
            db.UpdateIncident(incidentID, missieID, beschrijving);
        }

        public void IncidentVerwijderen(int id)
        {
            db = new Database();
            db.DeleteMeting(id);
        }

        public override string ToString()
        {
            return "ID: " + ID + " - " + "MissieID: " + MissieID + " - " + "Beschrijving: " + Beschrijving;
        }

    }
}

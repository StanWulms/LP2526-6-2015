using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace LifePerformance_StanWulms_S23
{
    class Database
    {
        /// <summary>
        /// Maakt een database connectie
        /// </summary>
        /// <returns></returns>
        public OracleConnection Connection()
        {
            OracleConnection conn;

            conn = new OracleConnection();
            //Gegevens van de FHICT-Oracle Database server.
  //        //  string user = "dbi323305";
            //string pw = "Rt1YxNAcSR";
 //          // string pw = "PSupOz8Wh7";
            string user = "system";
            string pw = "Wirdoxtaf8";
            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "localhost/xe" + ";";
  //         // conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();
            return conn;
        }

        /// <summary>
        /// Het selecteren van alle HOPE-missies.
        /// </summary>
        /// <returns></returns>
        public List<Hope> GetHopeMissies()
        {
            List<Hope> hopemissies = new List<Hope>();
            try
            {
                OracleConnection conn = Connection();
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT h.ID, m.beschrijving, m.x, m.y, h.aankomsttijd, h.vertrektijd FROM missie m, hope h WHERE m.ID = h.ID";

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Hope h;
                    h = new Hope(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetDateTime(4), dr.GetDateTime(5));
                    hopemissies.Add(h);
                }
                return hopemissies;
            }
            catch (NullReferenceException)
            {
                System.Windows.Forms.MessageBox.Show("Er is geen data gevonden voor Hope-Missies.");
                return hopemissies;
            }
            catch (OracleException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
                return hopemissies;
            }
        }

        /// <summary>
        /// Maken  van  een nieuwe HOPE-missie
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="aankomsttijd"></param>
        /// <param name="vertrektijd"></param>
        public void MaakHMissies(int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
        {
            int maxid = -1;
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                //Insert in de Missie tabel
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO MISSIE (boot_id, beschrijving, x, y, soort) VALUES (" + id + ", '" + beschrijving + "' , " + x + ", " + y + " , 'HOPE')";
                cmd.ExecuteNonQuery();
                otn.Commit();

                try
                {
                    //Hoogste ID ophalen uit de missie tabel (deze is zojuist geinsert.)
                    OracleCommand cmd2 = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                    cmd2.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                    cmd2.CommandText = "SELECT MAX(id) FROM MISSIE";

                    OracleDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                    {
                        maxid = dr.GetInt32(0);
                    }
                    
                }
                catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
                // Met hetzelfde ID (als missie) een nieuwe HOPE-missie aanmaken.
                OracleTransaction otn2 = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO HOPE (id, vertrektijd, aankomsttijd) VALUES (" + maxid + ",'" + vertrektijd.ToString("dd-MMM-yyyy") + "', '" + aankomsttijd.ToString("dd-MMM-yyyy") + "')";
                cmd.ExecuteNonQuery();
                otn2.Commit();
                System.Windows.Forms.MessageBox.Show("HOPE-missie is toegevoegd!");
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }

        }

        /// <summary>
        /// Updaten van een HOPE-missie.
        /// </summary>
        /// <param name="missieID"></param>
        /// <param name="id"></param>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="aankomsttijd"></param>
        /// <param name="vertrektijd"></param>
        public void UpdateHMissies(int missieID, int id, string beschrijving, int x, int y, DateTime aankomsttijd, DateTime vertrektijd)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE MISSIE SET boot_id =" + id + ", beschrijving = '" + beschrijving + "', x = " + x + ", y = " + y + " WHERE id =" + missieID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
            //Update Hope tabel.
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE HOPE SET vertrektijd ='" + vertrektijd.ToString("dd-MMM-yyyy") + "', aankomsttijd = '" + vertrektijd.ToString("dd-MMM-yyyy") + "' WHERE id =" + missieID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Deleten van een HOPE-missie
        /// </summary>
        /// <param name="hopeID"></param>
        public void DeleteHope(int hopeID)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM HOPE WHERE id =" + hopeID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Geeft een lijst terug met metingen (uit de database)
        /// </summary>
        /// <returns></returns>
        public List<Meting> GetMeting()
        {
            List<Meting> metingen = new List<Meting>();
            try
            {
                OracleConnection conn = Connection();
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT id, hope_id, beschrijving, x, y, tijdstip FROM METING";

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Meting met;
                    met = new Meting(dr.GetInt32(0),dr.GetString(2),dr.GetInt32(3), dr.GetInt32(4), dr.GetDateTime(5));
                    metingen.Add(met);
                }
                return metingen;
            }
            catch (NullReferenceException)
            {
                System.Windows.Forms.MessageBox.Show("Er is geen data gevonden voor Metingen.");
                return metingen;
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
            return metingen;
            }
        }

        /// <summary>
        /// Maken van een nieuwe meting, de parameters zijn de attirubuten uit de database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tijdstip"></param>
        public void MaakMeting(int id, string beschrijving, int x, int y, DateTime tijdstip)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO METING (hope_id, beschrijving, x, y, tijdstip) VALUES (" + id + ", '" + beschrijving + "' , " + x + ", " + y + " ,'" + tijdstip.ToString("dd-MMM-yyyy") + "')";
                cmd.ExecuteNonQuery();
                otn.Commit();
                
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Update een rij uit de database bij het ingevoerde metingsID met als waarden de andere parameters.
        /// </summary>
        /// <param name="metingID"></param>
        /// <param name="id"></param>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tijdstip"></param>
        public void UpdateMeting(int metingID, int id, string beschrijving, int x, int y, DateTime tijdstip)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE METING SET hope_id =" + id + ", beschrijving = '" + beschrijving + "', x = " + x + ", y = " + y + ", tijdstip = '" + tijdstip.ToString("dd-MMM-yyyy") + "'" + " WHERE id =" + metingID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Delete een rij uit de database bij de ingegeven parameter.
        /// </summary>
        /// <param name="metingID"></param>
        public void DeleteMeting(int incidentID)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM INCIDENT WHERE id =" + incidentID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Uitlezen van alle SIN-missies.
        /// </summary>
        /// <returns></returns>
        public List<Sin> GetSinMissies()
        {
            List<Sin> sinmissies = new List<Sin>();
            try
            {
                OracleConnection conn = Connection();
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT s.ID, m.beschrijving, m.x, m.y, s.vertrektijd, s.incident_id, m.boot_id FROM missie m, sin s WHERE m.ID = s.ID";

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    Sin h;
                    if (dr.IsDBNull(5))
                    {
                        h = new Sin(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetDateTime(4), dr.GetInt32(6));
                        sinmissies.Add(h);
                    }
                    else
                    {
                        h = new Sin(dr.GetInt32(0), dr.GetString(1), dr.GetInt32(2), dr.GetInt32(3), dr.GetDateTime(4), dr.GetInt32(5), dr.GetInt32(6));
                        sinmissies.Add(h);
                    }
                }
                return sinmissies;
            }
            catch (NullReferenceException)
            {
                System.Windows.Forms.MessageBox.Show("Er is geen data gevonden voor Sin-Missies.");
                return sinmissies;
            }
            catch (OracleException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
                return sinmissies;
            }
        }

        /// <summary>
        /// Met behulp van  Pythagoras het berekenen van de kleinste afstand.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int BerekenKleinsteAfstand(int x, int y)
        {
            int bootID = -1;
            int xBoot = -1;
            int yBoot = -1;
            int a;
            int b;
            double c;
            int kleinsteAfstand = 999999999; // Grootst mogelijke waarde
            //Berekenen van de dichtstbijzijnde boot.
            try
            {
                OracleConnection conn = Connection();
                //Hoogste ID ophalen uit de missie tabel (deze is zojuist geinsert.)
                OracleCommand cmd0 = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd0.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd0.CommandText = "SELECT id, x, y FROM BOOT";

                OracleDataReader dr = cmd0.ExecuteReader();
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    xBoot = dr.GetInt32(1);
                    yBoot = dr.GetInt32(2);

                    //Pythagoras.
                    a = xBoot - x;
                    b = yBoot - y;

                    c = Math.Sqrt(Convert.ToDouble(Math.Pow(a, 2)) + Convert.ToDouble(Math.Pow(b, 2)));
                    if (Convert.ToInt32(c) < kleinsteAfstand)
                    {
                        kleinsteAfstand = Convert.ToInt32(c);
                        bootID = dr.GetInt32(0);
                    }
                }
                return bootID;

            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); return -1; }
        }

        /// <summary>
        /// Het maken van een nieuwe SIN-missie.
        /// </summary>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vertrektijd"></param>
        public void MaakSMissies(string beschrijving, int x, int y, DateTime vertrektijd)
        {
            int maxid = -1;
            int bootID = -1;
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            
            // Geven van dichtstbijzijnde Boot.
            bootID = BerekenKleinsteAfstand(x,y);
            try
            {
                //Insert in de Missie tabel
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO MISSIE (boot_id, beschrijving, x, y, soort) VALUES (" + bootID + ", '" + beschrijving + "' , " + x + ", " + y + " , 'SIN')";
                cmd.ExecuteNonQuery();
                otn.Commit();

                try
                {
                    //Hoogste ID ophalen uit de missie tabel (deze is zojuist geinsert.)
                    OracleCommand cmd2 = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                    cmd2.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                    cmd2.CommandText = "SELECT MAX(id) FROM MISSIE";

                    OracleDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                    {
                        maxid = dr.GetInt32(0);
                    }

                }
                catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
                // Met hetzelfde ID (als missie) een nieuwe SIN-missie aanmaken.
                OracleTransaction otn2 = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO SIN (id, vertrektijd, incident_id) VALUES (" + maxid + ",'" + vertrektijd.ToString("dd-MMM-yyyy") + "', '" + null + "')";
                cmd.ExecuteNonQuery();
                otn2.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }

        }

        /// <summary>
        /// Updaten van een geselecteerde SIN-missie.
        /// </summary>
        /// <param name="missieID"></param>
        /// <param name="beschrijving"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vertrektijd"></param>
        public void UpdateSMissies(int missieID, string beschrijving, int x, int y, DateTime vertrektijd)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE MISSIE SET beschrijving = '" + beschrijving + "', x = " + x + ", y = " + y + " WHERE id =" + missieID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
            //Update Sin tabel.
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE SIN SET vertrektijd ='" + vertrektijd.ToString("dd-MMM-yyyy") + "' WHERE id =" + missieID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Verwijderen van een SIN-missie.
        /// </summary>
        /// <param name="sinID"></param>
        public void DeleteSin(int sinID)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM SIN WHERE id =" + sinID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Uitlezen van alle Incidenten.
        /// </summary>
        /// <returns></returns>
        public List<Incident> GetIncident()
        {
            List<Incident> incidenten = new List<Incident>();
            try
            {
                OracleConnection conn = Connection();
                OracleCommand cmd = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                cmd.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                cmd.CommandText = "SELECT i.id, s.id, i.beschrijving FROM INCIDENT i LEFT OUTER JOIN SIN s ON i.id = s.incident_id";

                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                {
                    if (dr.IsDBNull(1))
                    {
                        Incident inc;
                        inc = new Incident(dr.GetInt32(0), dr.GetString(2));
                        incidenten.Add(inc);

                    }
                    else
                    {
                        Incident inc;
                        inc = new Incident(dr.GetInt32(0), dr.GetInt32(1), dr.GetString(2));
                        incidenten.Add(inc);
                    }
                }
                return incidenten;
            }
            catch (NullReferenceException)
            {
                System.Windows.Forms.MessageBox.Show("Er is geen data gevonden voor Incidenten.");
                return incidenten;
            }
            catch (OracleException ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message);
                return incidenten;
            }
        }

        /// <summary>
        /// Het maken van een nieuw Incident.
        /// </summary>
        /// <param name="missieID"></param>
        /// <param name="beschrijving"></param>
        public void MaakIncident(int missieID, string beschrijving)
        {
            int maxid = -1;
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            OracleCommand cmd3 = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO INCIDENT (beschrijving) VALUES ('" + beschrijving + "')";
                cmd.ExecuteNonQuery();
                otn.Commit();

                try
                {
                    //Hoogste ID ophalen uit de missie tabel (deze is zojuist geinsert.)
                    OracleCommand cmd2 = conn.CreateCommand(); //oraclecommand opstellen, eerste waarde in de haakjes is je SQL string en de 2de is je connectie
                    cmd2.CommandType = CommandType.Text; //commandtype instellen, dit is meestal text
                    cmd2.CommandText = "SELECT MAX(id) FROM INCIDENT";

                    OracleDataReader dr = cmd2.ExecuteReader();
                    while (dr.Read()) //leest het OracleDatareader en daarmee het command dat je eraan linkt.
                    {
                        maxid = dr.GetInt32(0);
                    }

                }
                catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }

                OracleTransaction otn2 = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "UPDATE SIN SET incident_id =" + maxid + " WHERE id = " + missieID;
                cmd3.ExecuteNonQuery();
                otn2.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
            catch { System.Windows.Forms.MessageBox.Show("erorororororroor"); }
        }

        /// <summary>
        /// Het updaten van een Incident.
        /// </summary>
        /// <param name="incidentID"></param>
        /// <param name="missieid"></param>
        /// <param name="beschrijving"></param>
        public void UpdateIncident(int incidentID, int missieid, string beschrijving)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE INCIDENT SET beschrijving = '" + beschrijving + "'" + " WHERE id =" + incidentID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
            //Update SIN
            try
            {
                OracleTransaction otn2 = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE SIN SET incident_id =" + incidentID + " WHERE id = " + missieid;
                cmd.ExecuteNonQuery();
                otn2.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Het verwijderen van een Incident.
        /// </summary>
        /// <param name="incidentID"></param>
        public void DeleteIncident(int incidentID)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM INCIDENT WHERE id =" + incidentID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }
    }
}

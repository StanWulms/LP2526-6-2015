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
            string user = "dbi323305";
            string pw = "Rt1YxNAcSR";
            conn.ConnectionString = "User id=" + user + ";Password=" + pw + ";Data Source=" + "//192.168.15.50/fhictora" + ";";
            conn.Open();
            return conn;
        }


        /// <summary>
        /// Maken  van  een nieuwe missie
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
                    Meting meting = new Meting();
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
                System.Windows.Forms.MessageBox.Show("Meting is toegevoegd!");
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
                cmd.CommandText = "UPDATE METING SET hope_id =" + id + ",'" + beschrijving + "', " + x + ", " + y + ", '" + tijdstip + "'" + " WHERE id =" + metingID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }

        /// <summary>
        /// Delete een rij uit de database bij de ingegeven parameter.
        /// </summary>
        /// <param name="metingID"></param>
        public void DeleteMeting(int metingID)
        {
            OracleConnection conn = Connection();
            OracleCommand cmd = conn.CreateCommand();
            try
            {
                OracleTransaction otn = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM METING WHERE id =" + metingID;
                cmd.ExecuteNonQuery();
                otn.Commit();
            }
            catch (OracleException ex) { System.Windows.Forms.MessageBox.Show("Error: " + ex.Message); }
        }
    }
}

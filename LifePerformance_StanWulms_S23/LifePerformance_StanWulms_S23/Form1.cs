using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifePerformance_StanWulms_S23
{
    //Door Stan Wulms S23
    public partial class Form1 : Form
    {
        Hope h;
        Meting met;
        Sin s;
        Incident inc;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            lbHOPEmissies.Items.Clear();
            lbSINmissies.Items.Clear();
            lbMetingen.Items.Clear();
            lbIncidenten.Items.Clear();

            //Toevoegen van HOPE-missies in de listbox.
            h = new Hope();
            h.GeefHopeMissies();
            foreach (Hope hope in h.hmissies)
            {
                lbHOPEmissies.Items.Add(hope.ToString());
            }

            //Toevoegen van Metingen in de listbox.
            met = new Meting();
            met.GeefMetingen();
            foreach (Meting meting in met.metingen)
            {
                lbMetingen.Items.Add(meting.ToString());
            }

            //Toevoegen van SIN-missies in de listbox.
            s = new Sin();
            s.GeefSinMissies();
            foreach (Sin sin in s.smissies)
            {
                lbSINmissies.Items.Add(sin.ToString());
            }

            //Toevoegen van Incidenten in de listbox.
            inc = new Incident();
            inc.GeefIncidenten();
            foreach (Incident incident in inc.Incidenten)
            {
                lbIncidenten.Items.Add(incident.ToString());
            }

        }

        //Toevoegen van een nieuwe HOPE-missie.
        private void btnHopeToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                h = new Hope();
                h.HopeToevoegen(Convert.ToInt32(tbBootID.Text), tbHopeBeschrijving.Text, Convert.ToInt32(tbHopeX.Text), Convert.ToInt32(tbHopeY.Text), dtpHopeAankomst.Value.Date, dtpHopeVertrek.Value.Date);
            }
            catch
            {
                MessageBox.Show("Verkeerde invoer");
            }
        }

        //Wijzigen van een HOPE-missie.
        private void btnHopeWijzigen_Click(object sender, EventArgs e)
        {
            try
            {
                h = new Hope();
                h.HopeUpdaten(Convert.ToInt32(lbHOPEmissies.SelectedItem.ToString().Substring(3, lbHOPEmissies.SelectedItem.ToString().IndexOf("-") - 3)), Convert.ToInt32(tbBootID.Text), tbHopeBeschrijving.Text, Convert.ToInt32(tbHopeX.Text), Convert.ToInt32(tbHopeY.Text), dtpHopeVertrek.Value.Date, dtpHopeAankomst.Value.Date);
                System.Windows.Forms.MessageBox.Show("Hope nr *" + lbHOPEmissies.SelectedItem.ToString().Substring(3, lbHOPEmissies.SelectedItem.ToString().IndexOf("-") - 3) + "* is gewijzigd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Hope-missie uit de lijst en vul alle waardes in.");
            }
        }

        //Verwijderen van een HOPE-missie.
        private void btnHopeVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                h = new Hope(
                    +33636.....................................................................................999999999999999999996666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666666...666.666666666666666666666666666666666666666666.66.66.........................................................................................................6...............................................................................................................................................6.6.6.6..................................................6.);
                h.HopeDelete(Convert.ToInt32(lbHOPEmissies.SelectedItem.ToString().Substring(3, lbHOPEmissies.SelectedItem.ToString().IndexOf("-") - 3)));
                System.Windows.Forms.MessageBox.Show("Meting nr *" + lbHOPEmissies.SelectedItem.ToString().Substring(3, lbHOPEmissies.SelectedItem.ToString().IndexOf("-") - 3) + "* is verwijderd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een HOPE-missie uit de lijst.");
            }
        }

        //Toevoegen van een nieuwe Meting.
        private void btnMetingToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                met = new Meting();
                met.MetingToevoegen(Convert.ToInt32(tbHopeID.Text), tbMetingBeschrijving.Text, Convert.ToInt32(tbMetingX.Text), Convert.ToInt32(tbMetingY.Text), dtpMetingTijdstip.Value.Date);
                System.Windows.Forms.MessageBox.Show("Meting is toegevoegd!");
            }
            catch
            {
                MessageBox.Show("Verkeerde invoer");
            }
        }

        //Het wijzigen van een Meting.
        private void btnMetingWijzigen_Click(object sender, EventArgs e)
        {
            try
            {
                met = new Meting();
                met.MetingUpdaten(Convert.ToInt32(lbMetingen.SelectedItem.ToString().Substring(3, lbMetingen.SelectedItem.ToString().IndexOf("-") - 3)), Convert.ToInt32(tbHopeID.Text), tbMetingBeschrijving.Text, Convert.ToInt32(tbMetingX.Text), Convert.ToInt32(tbMetingY.Text), dtpMetingTijdstip.Value.Date);
                System.Windows.Forms.MessageBox.Show("Meting nr *" + lbMetingen.SelectedItem.ToString().Substring(3, lbMetingen.SelectedItem.ToString().IndexOf("-") - 3) + "* is gewijzigd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Meting uit de lijst en vul alle waardes in.");
            }
        }

        //Het verwijderen van een Meting.
        private void btnMetingVerwijderen_Click_1(object sender, EventArgs e)
        {
            try
            {
                met = new Meting();
                met.MetingVerwijderen(Convert.ToInt32(lbMetingen.SelectedItem.ToString().Substring(3, lbMetingen.SelectedItem.ToString().IndexOf("-") - 3)));
                System.Windows.Forms.MessageBox.Show("Meting nr *" + lbMetingen.SelectedItem.ToString().Substring(3, lbMetingen.SelectedItem.ToString().IndexOf("-") - 3) + "* is verwijderd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Meting uit de lijst.");
            }
        }

        //Toevoegen van een nieuwe SIN-missie.
        private void btnSinToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                s = new Sin();
                s.SinToevoegen(tbSinBeschrijving.Text, Convert.ToInt32(tbSinX.Text), Convert.ToInt32(tbSinY.Text), dtpSinVertrektijd.Value.Date);
                System.Windows.Forms.MessageBox.Show("Sin-missie is toegevoegd.");
            }
            catch
            {
                MessageBox.Show("Verkeerde invoer");
            }
        }

        //Het wijzigen van een SIN-missie.
        private void btnSinWijzigen_Click(object sender, EventArgs e)
        {
            try
            {
                s = new Sin();
                s.SinWijzigen(Convert.ToInt32(lbSINmissies.SelectedItem.ToString().Substring(3, lbSINmissies.SelectedItem.ToString().IndexOf("-") - 3)), tbSinBeschrijving.Text, Convert.ToInt32(tbSinX.Text), Convert.ToInt32(tbSinY.Text), dtpSinVertrektijd.Value.Date);
                System.Windows.Forms.MessageBox.Show("Sin nr *" + lbSINmissies.SelectedItem.ToString().Substring(3, lbSINmissies.SelectedItem.ToString().IndexOf("-") - 3) + "* is gewijzigd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Sin-missie uit de lijst en vul alle waardes in.");
            }
        }

        //Het verwijderen  van een SIN-missie.
        private void btnSinVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                s = new Sin();
                s.SinDelete(Convert.ToInt32(lbSINmissies.SelectedItem.ToString().Substring(3, lbSINmissies.SelectedItem.ToString().IndexOf("-") - 3)));
                System.Windows.Forms.MessageBox.Show("Meting nr *" + lbSINmissies.SelectedItem.ToString().Substring(3, lbSINmissies.SelectedItem.ToString().IndexOf("-") - 3) + "* is verwijderd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een SIN-missie uit de lijst.");
            }
        }

        //Het toevoegen van een nieuw Incident.
        private void btnIncidentToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                inc = new Incident();
                inc.IncidentToevoegen(Convert.ToInt32(tbMissieID.Text), rtbIncidentBeschrijving.Text);
                System.Windows.Forms.MessageBox.Show("Incident is toegevoegd.");
            }
            catch
            {
                MessageBox.Show("Verkeerde invoer");
            }
        }

        //Het wijzigen  van een Incident.
        private void btnIncidentWijzigen_Click(object sender, EventArgs e)
        {
            try
            {
                inc = new Incident();
                inc.IncidentUpdaten(Convert.ToInt32(lbIncidenten.SelectedItem.ToString().Substring(3, lbIncidenten.SelectedItem.ToString().IndexOf("-") - 3)), Convert.ToInt32(tbMissieID.Text), rtbIncidentBeschrijving.Text);
                System.Windows.Forms.MessageBox.Show("Incident nr *" + lbIncidenten.SelectedItem.ToString().Substring(3, lbIncidenten.SelectedItem.ToString().IndexOf("-") - 3) + "* is gewijzigd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Incident uit de lijst en vul alle gegevens in.");
            }
        }

        //Het verwijderen van een Incident.
        private void btnIncidentVerwijderen_Click(object sender, EventArgs e)
        {
            try
            {
                inc = new Incident();
                inc.IncidentVerwijderen(Convert.ToInt32(lbIncidenten.SelectedItem.ToString().Substring(3, lbIncidenten.SelectedItem.ToString().IndexOf("-") - 3)));
                System.Windows.Forms.MessageBox.Show("Incident nr *" + lbIncidenten.SelectedItem.ToString().Substring(3, lbIncidenten.SelectedItem.ToString().IndexOf("-") - 3) + "* is verwijderd!");
            }
            catch
            {
                MessageBox.Show("Selecteer een Incident uit de lijst.");
            }
        }
    }
}

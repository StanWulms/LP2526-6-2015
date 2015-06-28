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
        public Form1()
        {
         /*   met = new Meting();
            
            met.GeefMetingen();
            foreach (Meting meting in met.metingen)
            {
                lbMetingen.Items.Add(meting);
            }*/
            InitializeComponent();
        }

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

        private void btnMetingToevoegen_Click(object sender, EventArgs e)
        {
            try
            {
                met = new Meting();
                met.MetingToevoegen(Convert.ToInt32(tbHopeID.Text), tbMetingBeschrijving.Text, Convert.ToInt32(tbMetingX.Text), Convert.ToInt32(tbMetingY.Text), dtpMetingTijdstip.Value.Date);
            }
            catch
            {
                MessageBox.Show("Verkeerde invoer");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class accueil : Form
    {
        private app mainForm = null;
        public accueil(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            string month = DateTime.Now.Month.ToString();
            label3.Text = "Dans le mois " + month;
            SqlConnection connect = mainForm.connector();
            string sql = "Select SUM (PrixTotal) as prix from Transact where MONTH(Date_tra) =" + month;
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    label2.Text =reader["prix"].ToString() + " DH";
                }
                reader.Close();
                req.Dispose();
                sql = "Select SUM(PrixTotal) as prix from Transact where CAST(Date_tra as date) between CAST(DATEADD(dd, -7, GETDATE()) as date) and CAST(GETDATE() AS DATE)";
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    label5.Text = reader["prix"].ToString() + " DH";
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

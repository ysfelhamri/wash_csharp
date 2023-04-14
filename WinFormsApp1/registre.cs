using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1;

namespace AppLavage
{
    public partial class registre : Form
    {
        private app mainForm = null;
        public registre(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from log_client";
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["ID_C"].ToString(), reader["nom_prenom"].ToString(), reader["num_voiture"].ToString(), reader["date_del"].ToString());
                }
                reader.Close();
                req.Dispose();
                sql = "Select * from log_employe";
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView2.Rows.Add(reader["ID_E"].ToString(), reader["nom_prenom"].ToString(), reader["date_sor"].ToString());
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            label1.Cursor = System.Windows.Forms.Cursors.Hand;
            label2.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            panel2.Visible= true;
            panel3.Visible= false;
            dataGridView1.Visible= true;
            dataGridView2.Visible= false;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

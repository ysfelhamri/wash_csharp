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
    public partial class modifierclient : Form
    {
        private app mainForm = null;
        private string id;
        public modifierclient(Form mainfrm,string id)
        {
            mainForm = mainfrm as app;
            this.id = id;
            InitializeComponent();
            mainForm.sethandcursor(button2);
            this.Text = "Modifier les informations de client " + id;
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Client where ID ="+id;
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Nom"].ToString();
                    textBox2.Text = reader["Prenom"].ToString();
                    textBox3.Text = reader["Num_voiture"].ToString();
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.Message.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.openform(new clients(mainForm));
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                label4.Visible = true;
                label4.Text = "Saisir tout les champs";
            }
            else
            {

                SqlConnection connect = mainForm.connector();
                string sql = "Update Client set Prenom = '"+ textBox1.Text + "', Nom = '"+ textBox2.Text + "' , Num_voiture ='"+ textBox3.Text + "' where ID = "+id;
                SqlCommand req;
                SqlDataReader reader;
                try
                {
                    connect.Open();
                    req = new SqlCommand(sql, connect);
                    reader = req.ExecuteReader();
                    reader.Close();
                    req.Dispose();
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! " + ex.Message.ToString());
                }
                this.Close();
            }
        }
    }
}

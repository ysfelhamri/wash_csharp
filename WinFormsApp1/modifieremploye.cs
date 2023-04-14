using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1;

namespace AppLavage
{

    public partial class modifieremploye : Form
    {
        private app mainForm = null;
        private string id;
        public modifieremploye(Form mainfrm,string id)
        {
            mainForm = mainfrm as app;
            this.id = id;
            InitializeComponent();
            mainForm.sethandcursor(button2);
            this.Text = "Modifier les infomations d'employe " + id;
            dateTimePicker1.MaxDate = DateTime.Now;
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Employe where ID =" + id;
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
                    textBox3.Text = reader["Adresse"].ToString();
                    dateTimePicker1.Value = DateTime.Parse(reader["Date_naiss"].ToString());
                    textBox5.Text = reader["Fonction"].ToString();
                    textBox4.Text = reader["Salaire"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            int age = 0;
            try
            {
                age = new DateTime(DateTime.Today.Subtract(dateTimePicker1.Value).Ticks).Year - 1;
            }
            catch (Exception ex)
            {
            }

            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty || textBox4.Text == string.Empty || textBox5.Text == string.Empty)
            {
                label7.Visible = true;
                label7.Text = "Saisir tout les champs";
            }
            else if (age < 18)
            {
                label7.Visible = true;
                label7.Text = "Age invalide(inferieur a 18ans)";
            }
            else
            {
                SqlConnection connect = mainForm.connector();
                string sql = "Update Employe set Prenom ='" + textBox1.Text + "' ,Nom = '" + textBox2.Text + "',Adresse = '" + textBox3.Text + "',Date_naiss = '" + dateTimePicker1.Value.ToShortDateString() + "',Fonction = '" + textBox5.Text + "',Salaire = '" + textBox4.Text + "' where ID = "+id;
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
                mainForm.openform(new employes(mainForm));
                this.Close();
            }
            
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}

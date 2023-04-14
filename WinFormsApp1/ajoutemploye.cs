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
    public partial class ajoutemploye : Form
    {
        private app mainForm = null;
        public ajoutemploye(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            mainForm.sethandcursor(button2, button3);
            dateTimePicker1.MaxDate= DateTime.Now;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.openform(new employes(mainForm));
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
            
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty ||textBox4.Text == string.Empty || textBox5.Text == string.Empty)
            {
                label7.Visible = true;
                label7.Text = "Saisir tout les champs";
            }else if(age < 18)
            {
                label7.Visible = true;
                label7.Text = "Age invalide(inferieur a 18ans)";
            }
            else
            {
                SqlConnection connect = mainForm.connector();
                string sql = "Insert into Employe(Prenom,Nom,Adresse,Date_naiss,Fonction,Salaire,Date_rec) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','"+ dateTimePicker1.Value.ToShortDateString()+ "','"+ textBox5.Text + "','"+ textBox4.Text + "',GETDATE())";
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}

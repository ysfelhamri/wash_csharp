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
    public partial class modifierservice : Form
    {
        private app mainForm = null;
        private string id;
        public modifierservice(Form mainfrm, string id)
        {
            mainForm = mainfrm as app;
            this.id = id;
            InitializeComponent();
            mainForm.sethandcursor(button2);
            this.Text = "Modifier la service " + id;
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Service where ID ="+id;
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Nom_ser"].ToString();
                    textBox2.Text = reader["Prix"].ToString();
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " +ex.Message.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string txt1 = textBox1.Text;
            if (txt1 == string.Empty)
            {
                label4.Visible = true;
                label4.Text = "Nom de service invalide";
            }
            else if (textBox2.Text == string.Empty || float.Parse(textBox2.Text) < 0f)
            {
                label4.Visible = true;
                label4.Text = "Prix invalide";
            }
            else
            {
                SqlConnection connect = mainForm.connector();
                string sql = "Update Service set Nom_ser ='"+ txt1 + "',Prix = "+ float.Parse(textBox2.Text) + "where ID ="+id;
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
                mainForm.openform(new services(mainForm));
                this.Close();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

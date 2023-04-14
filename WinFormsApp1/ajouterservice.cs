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
    public partial class ajouterservice : Form
    {
        private app mainForm = null;
        public ajouterservice(Form mainfrm)
        {
            mainForm = mainfrm as app;   
            InitializeComponent();
            mainForm.sethandcursor(button2, button3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.openform(new services(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string txt1 = textBox1.Text;
            if (txt1 == string.Empty)
            {
                label4.Visible= true;
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
                string sql = "Insert into Service (Nom_ser,Prix) values ('"+ txt1 + "',"+ float.Parse(textBox2.Text) + ")";
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

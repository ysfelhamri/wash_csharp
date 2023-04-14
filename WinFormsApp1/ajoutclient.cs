using AppLavage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class ajoutclient : Form
    {
        private app mainForm = null;
        private bool dis;
        public ajoutclient(Form mainfrm,bool dis)
        {
            mainForm = mainfrm as app;
            this.dis = dis;
            InitializeComponent();

            if (dis) {
                button3.Visible = true;
                button3.Cursor = System.Windows.Forms.Cursors.Hand;
            }
            button2.Cursor = System.Windows.Forms.Cursors.Hand;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                label4.Visible = true;
                label4.Text = "Saisir tout les champs";
            }
            else
            {
                SqlConnection connect = mainForm.connector();
                string sql = "Insert into Client(Prenom,Nom,Num_voiture,Date_pr_vis) values ('"+ textBox1.Text + "','"+ textBox2.Text + "','"+ textBox3.Text + "',GETDATE())";
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
                    MessageBox.Show("Can not open connection ! "+ex.Message.ToString());
                }
                if (dis) mainForm.openform(new clients(mainForm));
                else this.Close();
            }     
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dis)
            {
                mainForm.openform(new clients(mainForm));
            }
        }
    }
}

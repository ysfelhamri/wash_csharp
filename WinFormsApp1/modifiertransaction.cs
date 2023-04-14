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
    public partial class modifiertransaction : Form
    {
        private app mainForm = null;
        private string id;
        HashSet<CheckBox> chks = new HashSet<CheckBox>();

        public modifiertransaction(Form mainfrm,string id)
        {
            mainForm = mainfrm as app;
            this.id = id;
            InitializeComponent();
            mainForm.sethandcursor(button1,button2,button3);
            this.Text = "La transaction " + id;
            label3.Text = "La transaction "+id;
        }
        private void load()
        {
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Service";
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID"].ToString() != "1")
                    {
                        CheckBox chk = new CheckBox();
                        chk.Height = 50;
                        chk.Width = 200;
                        chk.TextAlign = ContentAlignment.MiddleCenter;
                        chk.Tag = new Service(reader["ID"].ToString(), reader["Nom_ser"].ToString(), reader["Prix"].ToString()); ;
                        chk.Text = reader["Nom_ser"].ToString() + " - " + reader["Prix"].ToString() + " DH";
                        chk.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
                        chk.ForeColor = Color.White;
                        chk.CheckedChanged += new EventHandler(checkboxs);
                        chk.Dock = DockStyle.Top;
                        panel3.Controls.Add(chk);
                    }

                }
                reader.Close();
                req.Dispose();
                sql = "Select * from Client";
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {

                    comboBox1.Items.Add(new Client(reader["ID"].ToString(), reader["Nom"].ToString(), reader["Prenom"].ToString(), reader["Num_voiture"].ToString()));
                }
                reader.Close();
                req.Dispose();
                sql = "Select * from Employe";
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["ID"].ToString() != "1")
                        comboBox2.Items.Add(new Employe(reader["ID"].ToString(), reader["Nom"].ToString(), reader["Prenom"].ToString()));
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

        private void checkboxs(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Checked)
            {

                chks.Add(chk);
            }
            else
            {
                chks.Remove(chk);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.openform(new transactions(mainForm));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form clientfrm = new ajoutclient(mainForm, false);
            clientfrm.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

using AppLavage;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{

    public partial class ajouttransaction : Form
    {
        private app mainForm = null;
        HashSet<CheckBox> chks = new HashSet<CheckBox>();
        public ajouttransaction(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            mainForm.sethandcursor(button1, button2, button3);
            load();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

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
                        chk.Text = reader["Nom_ser"].ToString() +" - "+ reader["Prix"].ToString() + " DH";
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
                    if(reader["ID"].ToString() !="1")      
                        comboBox2.Items.Add(new Employe(reader["ID"].ToString(), reader["Nom"].ToString(), reader["Prenom"].ToString()));
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! "+ex.Message.ToString());
            }
        }

        private void checkboxs(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (chk.Checked)
            {
                
                chks.Add(chk);
            }else
            {
                chks.Remove(chk);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form clientfrm = new ajoutclient(mainForm,false);
            clientfrm.ShowDialog();  
            try
            {
                SqlConnection connect = new SqlConnection("Server= localhost; Database= lavageapp;Integrated Security = SSPI; "); ;
                string sql = "Select * from Client";
                SqlCommand req;
                SqlDataReader reader;
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(new Client(reader["ID"].ToString(), reader["Nom"].ToString(), reader["Prenom"].ToString(), reader["Num_voiture"].ToString()));
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch
            {
                return;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainForm.openform(new transactions(mainForm));
        }

        private void button1_Click(object sender, EventArgs e)
        {       
            string traID = string.Empty;
            if(chks.Count == 0)
            {
                label4.Visible = true;
                label4.Text = "Selectionner une service";
            }
            else if (comboBox1.SelectedIndex <= -1)
            {
                label4.Visible = true;
                label4.Text = "Selectionner un client";
            }
            else if (comboBox2.SelectedIndex <= -1)
            {
                label4.Visible = true;
                label4.Text = "Selectionner un employe";
            }
            else
            {
                Client cl = (Client)comboBox1.SelectedItem;
                Employe em = (Employe)comboBox2.SelectedItem;
                float totalp = 0;
                foreach (var c in chks)
                {
                    Service ser = (Service)c.Tag;
                    totalp += float.Parse(ser.prix);
                }
                SqlConnection connect = mainForm.connector();
                string sql = "Insert into Transact(ID_cl,PrixTotal,Date_tra) values ('"+ cl.id +"',"+ totalp + ",GETDATE()); SELECT SCOPE_IDENTITY() as traID;";
                SqlCommand req;
                SqlDataReader reader;
                try
                {
                    connect.Open();
                    req = new SqlCommand(sql, connect);
                    reader = req.ExecuteReader();
                    while (reader.Read())
                    {
                        traID = reader["traID"].ToString();
                    }
                    reader.Close();
                    req.Dispose();
                    foreach(var c in chks)
                    {
                        Service ser = (Service)c.Tag;
                        sql = "Insert into Servit(ID_tra,ID_em,ID_ser) values("+ traID + ","+em.id+","+ser.id+")";
                        req = new SqlCommand(sql, connect);
                        reader = req.ExecuteReader();
                        reader.Close();
                        req.Dispose();
                    }
                    connect.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! "+ex.Message.ToString());
                }
                mainForm.openform(new transactions(mainForm));
            }
            
        }
    }
}

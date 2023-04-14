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

namespace WinFormsApp1
{
    public partial class detailstransaction : Form
    {
        string idtra = string.Empty;
        public detailstransaction(string id)
        {
            idtra = id;
            InitializeComponent();
            label1.Text = "Details de la transaction "+id;
            this.Text = label1.Text;
            SqlConnection connect = new SqlConnection("Server= localhost; Database= lavageapp;Integrated Security = SSPI; ");
            string sql = "Select Nom_ser,Prix,Cl.Nom as Clnom,Cl.Prenom as Clprenom,Em.Nom as Emnom,Em.Prenom as Emprenom from Transact tr,Client Cl,Employe Em,Servit sert,Service ser where tr.ID_cl = Cl.ID and sert.ID_tra = tr.ID and sert.ID_em = Em.ID and sert.ID_ser = ser.ID and tr.ID ="+id;
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["Nom_ser"].ToString(), reader["Prix"].ToString(), reader["Clnom"].ToString()+" "+ reader["Clprenom"].ToString(), reader["Emnom"].ToString() + " " + reader["Emprenom"].ToString());
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

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}

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

    public partial class clients : Form
    {
        int index = -1;
        private app mainForm = null;
        
        public clients(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            mainForm.sethandcursor(button1, button2, button3);
            dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            load();
        }
        private void load()
        {
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Client";
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["ID"].ToString(), reader["Nom"].ToString(), reader["Prenom"].ToString(), reader["Num_voiture"].ToString(), reader["Date_pr_vis"].ToString());
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

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.openform(new ajoutclient(mainForm,true));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "êtes-vous sûr de supprimer ce client?";
            string title = "Supprimer le client";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string cID = string.Empty;
                if (index != -1)
                    cID =dataGridView1.Rows[index].Cells[0].Value.ToString();
                else
                {
                    try
                    {
                        cID = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }
                SqlConnection connect = mainForm.connector();
                string sql = "Delete from Client where ID = "+cID;
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
                    MessageBox.Show("Can not open connection ! ");
                }
                dataGridView1.Rows.Clear();
                load();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (index != -1)
                new modifierclient(mainForm, dataGridView1.Rows[index].Cells[0].Value.ToString()).ShowDialog();
            else
            {
                try
                {
                    new modifierclient(mainForm, dataGridView1.Rows[0].Cells[0].Value.ToString()).ShowDialog();
                }
                catch (Exception ex)
                {
                    return;
                }
                    
            }
            dataGridView1.Rows.Clear();
            load();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) index = e.RowIndex;
        }
    }
}

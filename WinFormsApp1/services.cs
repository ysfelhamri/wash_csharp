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
    public partial class services : Form
    {
        int index = -2;
        private app mainForm = null;
        public services(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            mainForm.sethandcursor(button1, button2, button3);
            loadServices();
            dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "êtes-vous sûr de supprimer cette service?";
            string title = "Supprimer la service";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string sID = string.Empty;
                if (index != -2)
                    sID = dataGridView1.Rows[index].Cells[0].Value.ToString();

                else
                {
                    try
                    {
                        sID = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }
                SqlConnection connect = mainForm.connector();
                string sql = "Delete from Service where ID = " + sID;
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
                loadServices();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.openform(new ajouterservice(mainForm));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index != -2)
                new modifierservice(mainForm, dataGridView1.Rows[index].Cells[0].Value.ToString()).ShowDialog();
            else
            {
                try
                {
                    new modifierservice(mainForm, dataGridView1.Rows[0].Cells[0].Value.ToString()).ShowDialog();
                }
                catch (Exception ex)
                {

                }

            }
        }
        private void loadServices()
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
                        dataGridView1.Rows.Add(reader["ID"].ToString(), reader["Nom_ser"].ToString(), reader["Prix"].ToString());
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) index = e.RowIndex;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1;

namespace AppLavage
{
    public partial class employes : Form
    {
        int index = -1;
        private app mainForm = null;
        public employes(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            mainForm.sethandcursor(button1, button2, button3);
            dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            load();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "êtes-vous sûr de supprimer cet employe?";
            string title = "Supprimer l'employe";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string emID = string.Empty;
                if (index != -1)
                    emID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                else
                {
                    try
                    {
                       emID = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                }
                SqlConnection connect = mainForm.connector();
                string sql = "Delete from Employe where ID = " + emID;
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
        private void load()
        {
            SqlConnection connect = mainForm.connector();
            string sql = "Select * from Employe";
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
                        dataGridView1.Rows.Add(reader["ID"].ToString(), reader["Prenom"].ToString(), reader["Nom"].ToString(), reader["Adresse"].ToString(), reader["Date_naiss"].ToString(), reader["Fonction"].ToString(), reader["Salaire"].ToString(), reader["Date_rec"].ToString());
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
        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            mainForm.openform(new ajoutemploye(mainForm));
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (index != -1)
                new modifieremploye(mainForm, dataGridView1.Rows[index].Cells[0].Value.ToString()).ShowDialog();
            else
            {
                try
                {
                    new modifieremploye(mainForm, dataGridView1.Rows[0].Cells[0].Value.ToString()).ShowDialog();
                }
                catch (Exception ex)
                {

                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) index = e.RowIndex;
        }
    }
}

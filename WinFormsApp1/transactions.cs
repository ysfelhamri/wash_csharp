using AppLavage;
using DocumentFormat.OpenXml.Office2010.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class transactions : Form
    {

        int index = -1;
        private app mainForm = null;
        public transactions(Form mainfrm)
        {
            mainForm = mainfrm as app;
            InitializeComponent();
            load();
            dataGridView1.Cursor = System.Windows.Forms.Cursors.Hand;
            mainForm.sethandcursor(button1, button2,button3,button5);
            if (index == -1)
            {
                try
                {
                    label2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    label3.Text = dataGridView1.Rows[0].Cells[1].Value.ToString() + " DH";
                }
                catch(Exception e)
                {
                    
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) { 
                index = e.RowIndex;
                label2.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
                label3.Text = dataGridView1.Rows[index].Cells[1].Value.ToString() +" DH";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (index !=-1)
                new detailstransaction(dataGridView1.Rows[index].Cells[0].Value.ToString()).ShowDialog();
            else
            {
                try
                {
                    new detailstransaction(dataGridView1.Rows[0].Cells[0].Value.ToString()).ShowDialog();
                }
                catch (Exception ex)
                {

                }
            }               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainForm.openform(new ajouttransaction(mainForm));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string message = "êtes-vous sûr de supprimer cette transaction?";
            string title = "Supprimer la transaction";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string traID = string.Empty;
                if (index != -1)
                    traID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                else
                {
                    try
                    {
                        traID = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        return;
                    }
                }
                SqlConnection connect = mainForm.connector();
                string sql = "Delete from Transact where ID = "+traID;
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
                dataGridView1.Rows.Clear();
                load();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (index != -1)
                mainForm.openform(new modifiertransaction(mainForm,dataGridView1.Rows[index].Cells[0].Value.ToString()));
            else
            {
                try
                {
                    mainForm.openform(new modifiertransaction(mainForm, dataGridView1.Rows[0].Cells[0].Value.ToString()));
                }
                catch(Exception ex) {
                    
                }
                  
            }
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            string traID = string.Empty;
            string ptota = string.Empty;
            string client = string.Empty;
            string date = string.Empty;
            if (index != -1)
            {
                traID = dataGridView1.Rows[index].Cells[0].Value.ToString();
                ptota = dataGridView1.Rows[index].Cells[1].Value.ToString();
                client = dataGridView1.Rows[index].Cells[2].Value.ToString();
                date = dataGridView1.Rows[index].Cells[3].Value.ToString();
            }   
            else
            {
                try
                {
                    traID = dataGridView1.Rows[0].Cells[0].Value.ToString();
                    ptota = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    client = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    date = dataGridView1.Rows[0].Cells[3].Value.ToString();
                }
                catch (Exception ex)
                {

                }
            }
            
            var pgSize = new iTextSharp.text.Rectangle(250, 450);
            Document pdfDocument = new Document(pgSize, 0f, 0f, 20f, 20f);
            string filename = "recucache" + DateTime.Now.Ticks + ".PDF";
            PdfWriter.GetInstance(pdfDocument, new FileStream(filename, FileMode.Create));
            pdfDocument.Open();
            Paragraph p = new Paragraph("*************************\n" + DateTime.Parse(date).ToShortDateString() + "\nLa transaction " + traID + "\n*************************\nClient : " + client + "\n*************************\n");
            p.Alignment=Element.ALIGN_CENTER;
            pdfDocument.Add(p);
            
            SqlConnection connect = mainForm.connector();
            string sql = "Select Nom_ser,Prix from Transact tr,Servit sert,Service ser where sert.ID_tra = tr.ID and sert.ID_ser = ser.ID and tr.ID =" + traID;
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    Paragraph p2 = new Paragraph(reader["Nom_ser"].ToString() + " --- " + reader["Prix"].ToString() + " DH");
                    p2.Alignment=Element.ALIGN_CENTER;
                    pdfDocument.Add(p2);
                }
                reader.Close();
                req.Dispose();
                connect.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
            }
            Paragraph p3 = new Paragraph("*************************\nPrix Total: " + ptota + " DH");
            p3.Alignment = Element.ALIGN_CENTER;
            pdfDocument.Add(p3);
            pdfDocument.Close();
            new recu(new MemoryStream(File.ReadAllBytes(filename))).ShowDialog();
            
        }
        private void load()
        {
            SqlConnection connect = mainForm.connector();
            string sql = "Select tr.ID as trID,PrixTotal,Date_tra,Cl.Nom as Clnom,Cl.Prenom as Clprenom,Num_voiture from Transact tr,Client Cl where tr.ID_cl = Cl.ID";
            SqlCommand req;
            SqlDataReader reader;
            try
            {
                connect.Open();
                req = new SqlCommand(sql, connect);
                reader = req.ExecuteReader();
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["trID"].ToString(), reader["PrixTotal"].ToString(), reader["Clnom"].ToString() + " " + reader["Clprenom"].ToString() +" - "+ reader["Num_voiture"].ToString(), reader["Date_tra"].ToString());
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
        
        private void transactions_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        
    }
}

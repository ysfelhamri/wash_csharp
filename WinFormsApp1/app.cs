using AppLavage;
using System.Data.SqlClient;

namespace WinFormsApp1
{
    public partial class app : Form
    {
        public app()
        {
            InitializeComponent();
            sethandcursor(button1, button2, button3, button4,button5,button6);
            openform(new accueil(this));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openform(new accueil(this));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openform(new transactions(this));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            openform(new clients(this));
        }
        private void button4_Click(object sender, EventArgs e)
        {
            openform(new employes(this));
        }
        private void button5_Click(object sender, EventArgs e)
        {
            openform(new registre(this));
        }
        private void button6_Click(object sender, EventArgs e)
        {
            openform(new services(this));
        }
        public void sethandcursor(params Button[] btns)
        {
            foreach (Button b in btns)
            {
                b.Cursor = System.Windows.Forms.Cursors.Hand;
            }
        }

        public Form currentform = null;
        public void openform(Form form)
        {
            if(currentform != null) {
                currentform.Close();
            }
            currentform= form;
            form.TopLevel= false;
            form.FormBorderStyle= FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            formdispanel.Controls.Add(form);
            form.Show();
        }
        public SqlConnection connector()
        {
            return new SqlConnection("Server= localhost; Database= lavageapp;Integrated Security = SSPI; ");
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
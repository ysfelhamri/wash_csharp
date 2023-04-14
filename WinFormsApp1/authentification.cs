using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Authentification : Form
    {
        public Authentification()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin1234") {
                Form form = new app();
                form.Show();
                this.Hide();
            }
            else if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Saisir le mot de passe");
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            textBox1.Focus();
            textBox1.PasswordChar = '\0';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Visible = true;
            button3.Visible = false;
            textBox1.Focus();
            textBox1.PasswordChar = '•';
        }
    }
}

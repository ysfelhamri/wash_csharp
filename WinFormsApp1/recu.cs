using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppLavage
{
    public partial class recu : Form
    {
        public recu(Stream st)
        {
            InitializeComponent();
            pdfViewer1.Document = PdfDocument.Load(st);
        }

        private void recu_Load(object sender, EventArgs e)
        {

        }
    }
}

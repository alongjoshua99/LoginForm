using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginForm
{

    public partial class RegForm : Form
    {
        public RegForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
        }

        private void RegForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cboGender.Items.Add("Male");
            cboGender.Items.Add("Female");
            cboSec.Items.Add("BSIT1-1");
            cboSec.Items.Add("BSIT1-2");
            cboSec.Items.Add("BSIT2-1");
            cboSec.Items.Add("BSENT1-1");
            cboSec.Items.Add("BSENT2-1");
            cboSec.Items.Add("BSENT3-1");
            cboSec.Items.Add("BBTLED1-1");
            cboSec.Items.Add("BBTLED2-1");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void RegButt_Click(object sender, EventArgs e)
        {
            this.Hide();
            ComlabLogin ComlabLogin = new ComlabLogin();
            ComlabLogin.ShowDialog();
        }
    }
}

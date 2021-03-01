using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowLoginForm();
        }
        public void ShowLoginForm()
        {
            Login login = new Login(this);
            login.MdiParent = this;
            login.Show();
        }
        public void ShowDataForm()
        {
            DataEditHub dataEditHub = new DataEditHub();
            dataEditHub.MdiParent = this;
            dataEditHub.Show();
        }
    }
}

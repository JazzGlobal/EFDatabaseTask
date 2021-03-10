using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class MainForm : Form
    {
        System.Windows.Forms.Timer timer;

        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        private void CheckForEvents()
        {
            Console.WriteLine("Checking for calendar events!");
            // TODO: Query the list of meetings for the logged in user for meetings that will occur within 15 minutes.
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                CheckForEvents();
            }).Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 60000;
            timer.Start();
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

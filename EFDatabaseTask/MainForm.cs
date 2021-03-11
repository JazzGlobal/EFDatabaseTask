using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
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
        List<CalendarEvent> calendarEvents;
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        private U07lyXEntities dbcontext {
            get { return new U07lyXEntities(); } // Return a new entity object for every get request. This is a must because otherwise the CheckForEvents() function will not have the latest and greatest appointment info.
        }
        private void CheckForEvents()
        {
            calendarEvents = new List<CalendarEvent>(); 
            Console.WriteLine("Checking for calendar events!");
            // TODO: Query the list of meetings for the logged in user for meetings that will occur within 15 minutes.
            var a = from app in dbcontext.appointments // Get appointments that will occur today. 
                    where app.start.Month == DateTime.Now.Month && app.start.Day == DateTime.Now.Day && app.start.Hour <= DateTime.Now.Hour
                    select app;
            
            foreach(appointment app in a)
            {
                TimeSpan ts = DateTime.Now - app.start;
                Console.WriteLine(ts.TotalMinutes);
                if(ts.TotalMinutes > -15.10 && ts.TotalMinutes < -14)
                {
                    Console.WriteLine(" {0} Meeting in 15 minutes.", app.title);
                }

            }
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
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 60000;
            timer.Start();

            DataEditHub dataEditHub = new DataEditHub();
            dataEditHub.MdiParent = this;
            dataEditHub.Show();
        }
    }
}

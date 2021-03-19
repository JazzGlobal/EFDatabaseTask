using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class MainForm : Form
    {
        // Set business hours for error checking comparisons. Users cannot currently configure this.
        // public static DateTime StartBusinessHours = new DateTime(2021, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        // public static DateTime EndBusinessHours = new DateTime(2021, 1, 1, 21, 0, 0, DateTimeKind.Utc);

        public static DateTime StartBusinessHours = new DateTime(2021, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        public static DateTime EndBusinessHours = new DateTime(2021, 1, 1, 23, 0, 0, DateTimeKind.Utc);

        System.Windows.Forms.Timer timer;
        List<CalendarEvent> calendarEvents;
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }
        private U07lyXEntities dbcontext
        {
            get { return new U07lyXEntities(); }
            /**
             * Return a new entity object for every get request. 
             * This is a must, otherwise the CheckForEvents() function will 
             * not have the latest and greatest appointment info when checking for alerts.
             */
        }
        private void CheckForEvents()
        {
            calendarEvents = new List<CalendarEvent>();
            var a = from app in dbcontext.appointments // Get appointments that will occur today and have not yet occurred. 
                    where app.start.Month == DateTime.Now.Month && app.start.Day == DateTime.Now.Day
                    select app;

            foreach (appointment app in a)
            {
                if (app.start >= DateTime.Now)
                {
                    Console.WriteLine(app.start.Hour >= DateTime.Now.Hour);
                    TimeSpan ts = app.start - DateTime.Now;
                    Console.WriteLine(ts.TotalMinutes);
                    if (ts.TotalMinutes < 15.10 && ts.TotalMinutes > 14)
                    {
                        string messageText = $"{app.title}: in 15 minutes.";
                        Console.WriteLine(messageText);
                        MessageBox.Show(messageText, "Meeting Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
            /* timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = 60000;
            timer.Start(); */
            DataEditHub dataEditHub = new DataEditHub();
            dataEditHub.MdiParent = this;
            dataEditHub.Show();
        }
    }
}

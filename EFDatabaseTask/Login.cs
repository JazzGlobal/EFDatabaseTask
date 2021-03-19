using EFDatabaseTask.DataFormExceptions;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class Login : Form
    {
        MainForm mainForm;
        public static Model.user CurrentLoggedInUser;
        public Login(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void loginButton_Click(object sender, EventArgs e)
        {
            login();
        }

        public static void SwitchLanguage(string s)
        {
            if (s.Split('-')[0].Contains("fr"))
            {
                Console.WriteLine("Changing Culture to fr-FR");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                Console.WriteLine("Switched To French");
            }
        }

        private void CheckForEvents()
        {
            List<CalendarEvent> calendarEvents = new List<CalendarEvent>();
            var appointments = from app in dbcontext.appointments // Get appointments that will occur today and have not yet occurred and belong to the logged in user.
                    where app.start.Month == DateTime.Now.Month && app.start.Day == DateTime.Now.Day && app.user.userName == Login.CurrentLoggedInUser.userName
                    select app;

            foreach (appointment app in appointments)
            {
                if (app.start >= DateTime.Now)
                {
                    TimeSpan ts = app.start.ToLocalTime() - DateTime.Now;
                    if (ts.TotalMinutes <= 15 && ts.TotalMinutes >= 0)
                    {
                        Console.WriteLine(ts.TotalMinutes);
                        string messageText = $"{app.title}: in {decimal.Round((decimal) ts.TotalMinutes)} minutes.";
                        Console.WriteLine(messageText);
                        MessageBox.Show(messageText, "Meeting Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void login()
        {
            string attemptedUsername = usernameTextBox.Text;
            string attemptedPassword = passwordTextBox.Text;
            try
            {

                dbcontext.users.Load();
                var query = from user in dbcontext.users.Local
                            where user.userName == attemptedUsername
                            select user;

                // Handle UserNotFound case. 
                if (query.Count() == 0)
                {
                    throw new UserNotFoundException(attemptedUsername);
                }

                foreach (var user in query)
                {
                    if (user.password == attemptedPassword)
                    {
                        // Fire Succuessful Login Event.
                        string success = Properties.Resources.LoginSuccess;
                        MessageBox.Show(success);
                        Logger.Log.LogEvent("Login_Log.txt", $"{success} for {attemptedUsername}");
                        CurrentLoggedInUser = user;
                        Close();
                        CheckForEvents();
                        mainForm.ShowDataForm();
                        return;
                    }
                }
                // Handle Credential Mismatch case.
                throw new MismatchingCredentialsException(Properties.Resources.MistmatchingCredentialsExceptionMessage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Log.LogEvent("Login_Log.txt", e.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SwitchLanguage(Thread.CurrentThread.CurrentCulture.Name);
            usernameLabel.Text = Properties.Resources.LoginUsernameLabel;
            passwordLabel.Text = Properties.Resources.LoginPasswordLabel;
            this.Text = Properties.Resources.LoginTitle;
            loginButton.Text = Properties.Resources.LoginButtonText;
        }
    }

}

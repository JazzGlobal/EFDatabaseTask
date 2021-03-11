using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Globalization;
using System.Threading;
using System.Resources;
using System.Collections;

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

        public static void SwitchExceptionLanguage(string s)
        {
            if (s.Split('-')[0].Contains("fr"))
            {
                Console.WriteLine("Changing Culture to fr-FR");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            }
        }

        private void login()
        {
            SwitchExceptionLanguage(Thread.CurrentThread.CurrentCulture.Name);
            string attemptedUsername = usernameTextBox.Text;
            string attemptedPassword = passwordTextBox.Text;
            try
            {

                dbcontext.users.Load();
                var query = from user in dbcontext.users.Local
                            where user.userName == attemptedUsername
                            select user;

                // Handle UserNotFound case. 
                if(query.Count() == 0)
                {
                    throw new UserNotFoundException(attemptedUsername);
                }

                foreach(var user in query)
                {
                    if(user.password == attemptedPassword)
                    {
                        // Fire Succuessful Login Event.
                        string success = "Login Successful";
                        MessageBox.Show(success);
                        Logger.Log.LogEvent("Login_Log.txt", $"{success} for {attemptedUsername}");
                        CurrentLoggedInUser = user;
                        Close();
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
    }
    [Serializable]
    class MismatchingCredentialsException : Exception
    {
        public MismatchingCredentialsException()
        {

        }
        public MismatchingCredentialsException(string message) : base(message)
        {

        }
    }
    [Serializable]
    class UserNotFoundException : Exception
    {
        public UserNotFoundException(string userName) : base($" {Properties.Resources.UserNotFoundExceptionMessage} {userName}")
        {

        }
    }
}

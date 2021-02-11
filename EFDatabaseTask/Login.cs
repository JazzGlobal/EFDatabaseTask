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

namespace EFDatabaseTask
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void loginButton_Click(object sender, EventArgs e)
        {
            login(); 
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
                if(query.Count() == 0)
                {
                    throw new UserNotFoundException(attemptedUsername);
                }

                foreach(var user in query)
                {
                    if(user.password == attemptedPassword)
                    {
                        // Fire Succuessful Login Event.
                        MessageBox.Show("Login Successful!");
                        DataEditHub dataEditHub = new DataEditHub();
                        dataEditHub.ShowDialog();
                        return;
                    }
                } 
                // Handle Credential Mismatch case.
                throw new MismatchingCredentialsException("Login Failed! Make sure your Username and Password match!");
            }
            catch (Exception e)
            {
                // var culture = new System.Threading.Thread(() => { }).CurrentCulture;
                // var culture = new CultureInfo(20);
                // Console.WriteLine(culture.Name);
                // MessageBox.Show(e.Message.ToString(culture));
                MessageBox.Show(e.Message);
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
        public UserNotFoundException(string userName) : base($"Could Not Find User: {userName}")
        {

        }
    }
}

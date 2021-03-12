using Model;
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
    public partial class Reports : Form
    {
        ReportData reportData;
        string seperator = "\n=========================================\n";
        public Reports()
        {
            InitializeComponent();
            reportData = new ReportData();
        }
        private U07lyXEntities dbcontext
        {
            get { return new U07lyXEntities(); }
        }
        private string GenerateUserSchedule()
        {
            var users = from user in dbcontext.users
                        select user;

            string returnString = "";

            foreach(user user in users)
            {
                string userFormattedString = seperator;
                List<appointment> apps = reportData.GetUserAppointments(user);
                userFormattedString += $"Appointments for User: {user.userName}";
                foreach (appointment app in apps)
                {
                    userFormattedString += $"\n{app.start} - {app.end}\n{app.title}: {app.description}\n";
                }
                returnString += userFormattedString;
            }
            return returnString;
        }
        private string GenerateUniqueAppointmentType()
        {
            // return $"Unique Report Types: {reportData.GetUniqueAppointmentTypes()}";
            string appointmentFormattedString = seperator;
            foreach(var item in reportData.GetUniqueAppointmentTypes())
            {
                appointmentFormattedString += $"Unique Types For { new DateTime(item.Item3, item.Item2, 1, 0, 0, 0).ToString("yyyy MMMM")}: {item.Item1}";
                appointmentFormattedString += seperator;
            }
            return appointmentFormattedString;
        }

        private void Reports_Load(object sender, EventArgs e)
        {
            reportTypeComboBox.Items.Add("User Schedules");
            reportTypeComboBox.Items.Add("Unique Appointments");
        }

        private void generateReportButton_Click(object sender, EventArgs e)
        {
            reportOutputRichEditTextbox.Text = "";

            switch (reportTypeComboBox.SelectedIndex)
            {
                case 0:
                    reportOutputRichEditTextbox.Text = GenerateUserSchedule();
                    break;
                case 1:
                    reportOutputRichEditTextbox.Text = GenerateUniqueAppointmentType();
                    break;
            }
        }
    }
}

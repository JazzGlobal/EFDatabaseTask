using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
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

            foreach (user user in users)
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
            string appointmentFormattedString = $"Showing Appointments For {dateTimePickerUniqueTypes.Value.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)}" + seperator;
            foreach (var item in reportData.GetAppointmentTypes(dateTimePickerUniqueTypes.Value))
            {
                //appointmentFormattedString += $"Unique Types For { new DateTime(item.Item3, item.Item2, 1, 0, 0, 0).ToString("yyyy MMMM")}: {item.Item1}";
                appointmentFormattedString += $"Type: {item.Item1}\n# Of: {item.Item2}";
                appointmentFormattedString += seperator;
            }
            return appointmentFormattedString;
        }
        private string GenerateActiveCustomers()
        {
            List<customer> customerList = reportData.GetAllActiveCustomers();
            string customerFormattedString = "Showing Active Customers:" + seperator;

            foreach(customer customer in customerList)
            {
                customerFormattedString += $"{customer.customerName}\n";
            }
            return customerFormattedString;
        }
        
        private string GenerateActivePets()
        {
            List<PET> petList = reportData.GetAllActivePets();
            string petFormattedString = "Showing Active Pets: " + seperator;

            foreach(PET pet in petList)
            {
                petFormattedString += $"{pet.PET_NAME}\n";
            }
            return petFormattedString;
        }
        
        private void Reports_Load(object sender, EventArgs e)
        {
            reportTypeComboBox.Items.Add("User Schedules");
            reportTypeComboBox.Items.Add("Unique Appointments");
            reportTypeComboBox.Items.Add("Active Customers");
            reportTypeComboBox.Items.Add("Active Pets");
            reportTypeComboBox.SelectedIndex = 0;
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
                case 2:
                    reportOutputRichEditTextbox.Text = GenerateActiveCustomers();
                    break;
                case 3:
                    reportOutputRichEditTextbox.Text = GenerateActivePets();
                    break;
            }
        }

        private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(reportTypeComboBox.SelectedIndex == 1)
            {
                // display control
                dateTimePickerUniqueTypes.Visible = true;
            } else
            {
                dateTimePickerUniqueTypes.Visible = false;
            }
        }
    }
}

using EFDatabaseTask.DataFormExceptions;
using Model;
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

namespace EFDatabaseTask
{
    public partial class AppointmentEdit : Form
    {
        public AppointmentEdit()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();
        private void AppointmentEdit_Load(object sender, EventArgs e)
        {
            dbcontext.appointments.OrderBy(appointment => appointment.appointmentId).Load();
            dbcontext.customers.OrderBy(customer => customer.customerId).Load();
            appointmentBindingSource.DataSource = dbcontext.appointments.Local;
            customerBindingSource1.DataSource = dbcontext.customers.Local;
        }

        private void appointmentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAppointmentChanges();
            }
            catch (InvalidDatabaseItemsException invDBEx)
            {
                MessageBox.Show(invDBEx.Message, "Incorrect Database Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveAppointmentChanges()
        {
            try
            {
                dbcontext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    string invalidProperties = "";
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        invalidProperties += $"\n{validationError.PropertyName}";
                    }
                    throw new InvalidDatabaseItemsException(invalidProperties); // Throw new error with the invalid properties as an argument. 
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void appointmentDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // TODO: Add validation logic to prevent appointments being made outside of business hours. 
            // TODO: Add validation logic to prevent appointments from overlapping eachother.

            string columnName = "";
            switch(e.ColumnIndex)
            {
                case 0:
                    columnName = "appointmentId";
                    break;
                case 1:
                    columnName = "customerId";
                    
                    // Lambda expression below, used to make querying the customer database more efficient. We only need one customer.The customer is decided
                    // by which customerID the user attempts to enter.
                    var list = dbcontext.customers.Where(customer => customer.customerId.ToString() == e.FormattedValue.ToString()).AsEnumerable();
                    try
                    {
                        // Attempt to assign that customer to a new Model.customer object. 
                        Model.customer customerName = list.ElementAt(0);
                        appointmentDataGridView.Rows[e.RowIndex].Cells[16].Value = customerName; // Set the customerName field to the name of the found customer.
                        appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value = e.FormattedValue; // Set the customerId field to the id of the found customer.
                    }
                    catch (ArgumentOutOfRangeException argoutofrange_ex)
                    {
                        // Exception thrown if the ID does not exist in the database. Print the exception to the console, log the error, let the user know that the customer does not exist 
                        // using a MessageBox, then default the customerId (and consequently, the customerName) to the first known customer.
                        Console.WriteLine(argoutofrange_ex);
                        string formattedErrorMessage = $"CustomerID: {e.FormattedValue} Does not exist. Defaulting to next existing customer.";
                        Logger.Log.LogEvent("Error_Log.txt", formattedErrorMessage);
                        MessageBox.Show(formattedErrorMessage, "Validation Error" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var existing_customers = dbcontext.customers.OrderBy(customer => customer.customerId).AsEnumerable();
                        Model.customer customerName = existing_customers.ElementAt(0);
                        appointmentDataGridView.Rows[e.RowIndex].Cells[16].Value = customerName;
                        appointmentDataGridView.Rows[e.RowIndex].Cells[1].Value = customerName.customerId;
                    }
                    catch (Exception general_ex)
                    {
                        Console.WriteLine(general_ex);
                        Logger.Log.LogEvent("Error_Log.txt", $" Error Occurred During Validation of Appointment (CustomerId) :{general_ex}");
                    }
                    break;
                case 9:
                    columnName = "start";
                    try
                    {
                        DateTime appointmentTime = DateTime.Parse(e.FormattedValue.ToString()).ToUniversalTime();
                        if (appointmentTime.Hour < MainForm.StartBusinessHours.Hour || appointmentTime.Hour >= (MainForm.EndBusinessHours.Hour - 1))
                        {
                            throw new ScheduledAppointmentOutsideOfBusinessHoursException();
                        }
                    } catch (ScheduledAppointmentOutsideOfBusinessHoursException outsideHoursEx)
                    {
                        string errorMessage = $"Cannot schedule start time of appointment outside of business hours \n Business Hours: " +
                            $"{MainForm.StartBusinessHours.ToLocalTime().Hour} - {MainForm.EndBusinessHours.ToLocalTime().Hour}";
                        Logger.Log.LogEvent("Error_Log.txt", outsideHoursEx + errorMessage);   
                        MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                    catch (Exception general_ex)
                    {
                        Console.WriteLine(general_ex);
                        Logger.Log.LogEvent("Error_Log.txt", $"Error Occurred During Validation of Appointment (start) :{general_ex.Message}");
                    }
                    break;
            }
            Console.WriteLine($"Validation logic running for {appointmentDataGridView.Rows[e.RowIndex]} ... {columnName}");
        }
    }
}

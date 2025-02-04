﻿using EFDatabaseTask.DataFormExceptions;
using Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
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
            RefreshDataGridData();
        }

        private void appointmentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
               /* appointmentDataGridView.EndEdit();
                DataGridViewCell currentCell = appointmentDataGridView.CurrentCell;
                appointmentDataGridView.CurrentCell = null;
                appointmentDataGridView.CurrentCell = currentCell; */
                SaveAppointmentChanges();
                MessageBox.Show("Records have been updated successfully.", "Database Change Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                for(int i = 0; i < appointmentDataGridView.Rows.Count; i++)
                {
                    if(appointmentDataGridView.Rows[i].Cells[9].Value != null)
                    {
                        DateTime toUTC = TimeZoneInfo.ConvertTimeToUtc((DateTime)appointmentDataGridView.Rows[i].Cells[9].Value, TimeZoneInfo.Local);
                        Console.WriteLine(appointmentDataGridView.Rows[i].Cells[9].Value);
                        appointmentDataGridView.Rows[i].Cells[9].Value = toUTC;
                    }
                    if (appointmentDataGridView.Rows[i].Cells[10].Value != null)
                    {
                        DateTime toUTC = TimeZoneInfo.ConvertTimeToUtc((DateTime)appointmentDataGridView.Rows[i].Cells[10].Value, TimeZoneInfo.Local);
                        Console.WriteLine(appointmentDataGridView.Rows[i].Cells[10].Value);
                        appointmentDataGridView.Rows[i].Cells[10].Value = toUTC;
                    }
                }
                dbcontext.SaveChanges();
                RefreshDataGridData();
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void appointmentDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = "";
            switch (e.ColumnIndex)
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
                        MessageBox.Show(formattedErrorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // ValidateStartTimes(e);
                    // ValidateTimes(e);
                    ValidateTimes(e);
                    break;
                case 10:
                    columnName = "end";
                    // ValidateEndTimes(e);
                    ValidateTimes(e);
                    break;

            }
            Console.WriteLine($"Validation logic running for {appointmentDataGridView.Rows[e.RowIndex]} ... {columnName}");
        }
        private void ValidateTimes(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DateTime newAppTime = DateTime.Parse(e.FormattedValue.ToString());
                if (newAppTime.Hour < MainForm.StartBusinessHours.ToLocalTime().Hour || newAppTime.Hour >= (MainForm.EndBusinessHours.ToLocalTime().Hour - 1))
                {
                    throw new ScheduledAppointmentOutsideOfBusinessHoursException();
                }

                var user = (user)appointmentDataGridView.Rows[e.RowIndex].Cells[15].Value;
                var appointments = from app in dbcontext.appointments // Query appointments that are the same month and day of the user entered appointment.
                                   where app.start.Month == newAppTime.Month
                                   && app.start.Day == newAppTime.Day
                                   && app.start.Year == newAppTime.Year
                                   select app;

                foreach (appointment app in appointments)
                {
                    if (app.appointmentId.ToString() != appointmentDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())

                    {
                        Console.WriteLine($"Compare 1:  {app.appointmentId}");
                        Console.WriteLine(app.start.ToLocalTime().TimeOfDay <= newAppTime.TimeOfDay && newAppTime.TimeOfDay <= app.end.ToLocalTime().TimeOfDay);
                        if (app.start.ToLocalTime().TimeOfDay <= newAppTime.TimeOfDay && newAppTime.TimeOfDay <= app.end.ToLocalTime().TimeOfDay)
                        
                        {
                            throw new ScheduledAppointmentDuringAnotherAppointment();
                        }
                    }
                } 
                if(RowTimesFilled(e))
                {
                    DateTime newAppStart;
                    DateTime newAppEnd;
                    if(e.ColumnIndex == 9)
                    {
                        newAppStart = DateTime.Parse(e.FormattedValue.ToString());
                        newAppEnd = DateTime.Parse(appointmentDataGridView.Rows[e.RowIndex].Cells[10].FormattedValue.ToString());
                    } else
                    {
                        newAppStart = DateTime.Parse(appointmentDataGridView.Rows[e.RowIndex].Cells[9].FormattedValue.ToString());
                        newAppEnd = DateTime.Parse(e.FormattedValue.ToString());
                    }
                    foreach (appointment app in appointments)
                    {
                        if (app.appointmentId.ToString() != appointmentDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                        /*
                         * Do not check the app if the ID matches the validating rows ID 
                         * (Checking same row will always result in an error.)
                         */
                        {
                            Console.WriteLine($"Compare 2:  {app.appointmentId}");
                            Console.WriteLine("Comparing: {0} <= {1} && {2} <= {3}", newAppStart, app.end.ToLocalTime(), app.start.ToLocalTime(), newAppEnd);
                            Console.WriteLine(newAppStart <= app.end.ToLocalTime() && app.start.ToLocalTime() <= newAppEnd);

                                if (newAppStart <= app.end.ToLocalTime() && app.start.ToLocalTime() <= newAppEnd) 
                            /*
                             * Check if the validating row's start time falls within the comparison app's start and end time. If so, throw an error.
                             */
                            {
                                throw new ScheduledAppointmentDuringAnotherAppointment();
                            }
                        }
                    }
                    try
                    {
                        if (newAppStart.ToUniversalTime().TimeOfDay > newAppEnd.ToUniversalTime().TimeOfDay)
                        {
                            throw new StartTimeMustBeBeforeEndTimeException();
                        }
                    }
                    catch (StartTimeMustBeBeforeEndTimeException ex)
                    {
                        ValidateFailed(e.RowIndex, e.ColumnIndex);
                        string errorMessage = "Start time must be before end time.";
                        Logger.Log.LogEvent("Error_Log.txt", ex + errorMessage);
                        MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (ScheduledAppointmentOutsideOfBusinessHoursException outsideHoursEx)
            {
                ValidateFailed(e.RowIndex, e.ColumnIndex);
                DateTime localStart = TimeZoneInfo.ConvertTimeFromUtc(MainForm.StartBusinessHours,TimeZoneInfo.Local);
                DateTime localEnd = TimeZoneInfo.ConvertTimeFromUtc(MainForm.EndBusinessHours, TimeZoneInfo.Local);

                localEnd = localEnd.AddHours(-1);

                string errorMessage = $"Cannot schedule start or end time of appointment outside of business hours \n Business Hours (Local Time): " +
                    $"{localStart.ToString("hh:mm tt", CultureInfo.InvariantCulture)} - {localEnd.ToString("hh:mm tt", CultureInfo.InvariantCulture)}";
                Logger.Log.LogEvent("Error_Log.txt", outsideHoursEx + errorMessage);
                MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            catch (ScheduledAppointmentDuringAnotherAppointment overlapEx)
            {
                ValidateFailed(e.RowIndex, e.ColumnIndex);
                string errorMessage = "Entered date / time falls within the duration of another appointment!";
                Logger.Log.LogEvent("Error_Log.txt", $"{errorMessage}\n{overlapEx}");
                MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception general_ex)
            {
                Console.WriteLine(general_ex);
                Logger.Log.LogEvent("Error_Log.txt", $"Error Occurred During Validation of Appointment (start) :{general_ex.Message}");
            }
        }

        private bool RowTimesFilled(DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DateTime result1;
                DateTime result2;
                if (DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString(), out result1) && DateTime.TryParse(appointmentDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString(),out result2))
                {
                    Console.WriteLine("Both times are filled!");
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }

        private void ValidateTimes(int rowIndex, int columnIndex)
        {
            try
            {
                DateTime newAppTime = DateTime.Parse(appointmentDataGridView.Rows[rowIndex].Cells[columnIndex].FormattedValue.ToString()).ToUniversalTime();
                if (newAppTime.Hour < MainForm.StartBusinessHours.Hour || newAppTime.Hour >= (MainForm.EndBusinessHours.Hour - 1))
                {
                    throw new ScheduledAppointmentOutsideOfBusinessHoursException();
                }

                var appointments = from app in dbcontext.appointments // Query appointments that are the same month and day of the user entered appointment.
                                   where app.user.userName == Login.CurrentLoggedInUser.userName
                                   && app.start.Month == newAppTime.Month
                                   && app.start.Day == newAppTime.Day
                                   select app;

                foreach (appointment app in appointments)
                {
                    if (app.appointmentId.ToString() != appointmentDataGridView.Rows[rowIndex].Cells[0].Value.ToString())
                    /*
                     * Do not check the app if the ID matches the validating rows ID 
                     * (Checking same row will always result in an error.)
                     */
                    {
                        if (app.start.ToLocalTime().TimeOfDay <= newAppTime.TimeOfDay && newAppTime.TimeOfDay <= app.end.ToLocalTime().TimeOfDay)
                        /*
                         * Check if the validating row's start time falls within the comparison app's start and end time. If so, throw an error.
                         */
                        {
                            throw new ScheduledAppointmentDuringAnotherAppointment();
                        }
                    }
                }
            }
            catch (ScheduledAppointmentOutsideOfBusinessHoursException outsideHoursEx)
            {
                ValidateFailed(rowIndex, columnIndex);
                string errorMessage = $"Cannot schedule start or end time of appointment outside of business hours \n Business Hours: " +
                    $"{MainForm.StartBusinessHours.ToLocalTime().Hour}:00 - {MainForm.EndBusinessHours.ToLocalTime().Hour - 1}:00";
                Logger.Log.LogEvent("Error_Log.txt", outsideHoursEx + errorMessage);
                MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ScheduledAppointmentDuringAnotherAppointment overlapEx)
            {
                ValidateFailed(rowIndex, columnIndex);
                string errorMessage = "Entered date / time falls within the duration of another appointment!";
                Logger.Log.LogEvent("Error_Log.txt", $"{errorMessage}\n{overlapEx}");
                MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception general_ex)
            {
                Console.WriteLine(general_ex);
                Logger.Log.LogEvent("Error_Log.txt", $"Error Occurred During Validation of Appointment (start) :{general_ex.Message}");
            }
        }

        private void appointmentDataGridView_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 9 || e.ColumnIndex == 10)
            {
                bool bothFieldsValidated = false;
                DateTime validatingStartTime = new DateTime();
                DateTime validatingEndTime = new DateTime();
                try
                {
                    validatingStartTime = DateTime.Parse(appointmentDataGridView.Rows[e.RowIndex].Cells[9].FormattedValue.ToString());
                    validatingEndTime = DateTime.Parse(appointmentDataGridView.Rows[e.RowIndex].Cells[10].FormattedValue.ToString());
                    bothFieldsValidated = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    bothFieldsValidated = false;
                }

                if (bothFieldsValidated)
                {
                    try
                    {
                        if (validatingStartTime.ToUniversalTime().TimeOfDay > validatingEndTime.ToUniversalTime().TimeOfDay)
                        {
                            throw new StartTimeMustBeBeforeEndTimeException();
                        }
                    }
                    catch (StartTimeMustBeBeforeEndTimeException ex)
                    {
                        ValidateFailed(e.RowIndex, e.ColumnIndex);
                        string errorMessage = "Start time must be before end time.";
                        Logger.Log.LogEvent("Error_Log.txt", ex + errorMessage);
                        MessageBox.Show(errorMessage, "Scheduling Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ValidateTimes(e.RowIndex, e.ColumnIndex);

                }
            }
        }
        private void ValidateFailed(int rowIndex, int columnIndex)
        {
            appointmentDataGridView.RefreshEdit();
        }

        private void appointmentDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.ColumnIndex == 9 || e.ColumnIndex == 10)
            {
                try
                {
                    if (e.Value != null)
                    {
                        /*Console.WriteLine(e.Value);
                        DateTime dtValue;
                        DateTime.TryParse(e.Value.ToString(), out dtValue);
                        Console.WriteLine(dtValue);
                        DateTime dtToLocal = dtValue.ToLocalTime();
                        e.Value = dtToLocal;
                        Console.WriteLine(e.Value);*/
                    }

                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }
        }
        private void RefreshDataGridData()
        {
            var appointmentList = dbcontext.appointments.OrderBy(appointment => appointment.appointmentId);
            foreach (var app in appointmentList)
            {
                app.start = app.start.ToLocalTime();
                app.end = app.end.ToLocalTime();
            }
            appointmentList.Load();
            dbcontext.customers.OrderBy(customer => customer.customerId).Load();
            appointmentBindingSource.DataSource = dbcontext.appointments.Local;
            customerBindingSource1.DataSource = dbcontext.customers.Local;
        }
    }
}

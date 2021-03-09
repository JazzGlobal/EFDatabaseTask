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
        }

        private void appointmentDataGridView_Validating(object sender, CancelEventArgs e)
        {

            foreach(DataGridViewRow row in appointmentDataGridView.Rows)
            {
                if (row.Cells[16] == null)
                {
                    Console.WriteLine("Customer was null");
                }
            }
        }

        private void appointmentDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string columnName = "";
            switch(e.ColumnIndex)
            {
                case 0:
                    columnName = "appointmentId";
                    break;
                case 1:
                    columnName = "customerId";
                    // set customerName to customerBindingSource -> Customer of CustomerId = customerId. 
                    break;
            }
            Console.WriteLine($"Validation logic running for {appointmentDataGridView.Rows[e.RowIndex]} ... {columnName}");
        }
    }
}

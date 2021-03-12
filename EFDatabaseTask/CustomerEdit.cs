using EFDatabaseTask.DataFormExceptions;
using Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class CustomerEdit : Form
    {
        public CustomerEdit()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void CustomerEdit_Load(object sender, EventArgs e)
        {
            dbcontext.customers.OrderBy(customer => customer.customerId).Load();
            customerBindingSource.DataSource = dbcontext.customers.Local;
        }

        private void customerBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveCustomerChanges();
            }
            catch (InvalidDatabaseItemsException invDBEx)
            {
                MessageBox.Show(invDBEx.Message, "Incorrect Database Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveCustomerChanges()
        {   // TODO: On Save, compare results to appointment database. If there are appointments for customerIds that no longer exist, purge them from the appointment database.
            // TODO: On Save, validate customerIds to ensure no duplicates.
            try
            {
                dbcontext.SaveChanges();
            }
            catch(DbUpdateException dbUpdateEx)
            {
                Console.WriteLine(dbUpdateEx);
                string errorMessage = $"Could not delete customer record, it is associated with other records (appointments, etc)";
                MessageBox.Show(errorMessage,"Database Update Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Logger.Log.LogEvent("Error_Log.txt", errorMessage);
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
    }

}

using EFDatabaseTask.DataFormExceptions;
using Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class AddressEdit : Form
    {
        public AddressEdit()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void AddressEdit_Load(object sender, EventArgs e)
        {
            dbcontext.addresses.OrderBy(address => address.addressId).Load();
            addressBindingSource.DataSource = dbcontext.addresses.Local;
        }

        private void addressBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAddressChanges();
            }
            catch (InvalidDatabaseItemsException invDBEx)
            {
                MessageBox.Show(invDBEx.Message, "Incorrect Database Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveAddressChanges()
        {
            try
            {
                dbcontext.SaveChanges();
                MessageBox.Show("Records have been updated successfully.", "Database Change Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

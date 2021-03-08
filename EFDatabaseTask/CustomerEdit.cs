using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using System.Data.Entity; 

namespace EFDatabaseTask
{
    public partial class CustomerEdit : Form
    {
        public CustomerEdit()
        {
            InitializeComponent();
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
            } catch (InvalidDatabaseItemsException invDBEx)
            {            
               MessageBox.Show(invDBEx.Message,"Incorrect Database Items",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveCustomerChanges()
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
                        // raise a new exception nesting
                        // the current instance as InnerException
                        
                    }
                    throw new InvalidDatabaseItemsException(invalidProperties);
                }
            }
        }
    }
    [Serializable]
    class InvalidDatabaseItemsException : Exception
    {
        public InvalidDatabaseItemsException()
        {

        }
        public InvalidDatabaseItemsException(string message) : base($"The following items were invalid: {message}")
        {

        }
    }
}

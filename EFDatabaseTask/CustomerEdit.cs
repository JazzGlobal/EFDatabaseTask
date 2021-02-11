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
    }
}

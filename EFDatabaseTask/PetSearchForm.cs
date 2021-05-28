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

namespace EFDatabaseTask
{
    public partial class PetSearchForm : Form
    {
        public PetSearchForm()
        {
            InitializeComponent();
        }

        private U07lyXEntities dbcontext
        {
            get { return new U07lyXEntities(); }
        }
        private void RefreshDataGridView()
        {
            var tempContext = dbcontext;
            tempContext.PETs.OrderBy(pet => pet.PET_ID).Load();
            pETBindingSource.DataSource = tempContext.PETs.Local;
        }
        private void PetSearchForm_Load(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
        private void filterResultsButton_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
            string filter = filterResultsTextBox.Text;

            var tempContext = dbcontext;
            tempContext.PETs.OrderBy(pet => pet.PET_NAME).Where(pet => pet.PET_NAME.Contains(filter)).Load();
            pETBindingSource.DataSource = tempContext.PETs.Local;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }
}

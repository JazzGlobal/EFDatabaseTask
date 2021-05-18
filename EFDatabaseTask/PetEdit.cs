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
    public partial class PetEdit : Form
    {
        public PetEdit()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();
        private void PetEdit_Load(object sender, EventArgs e)
        {
            dbcontext.PETs.OrderBy(pet => pet.PET_ID).Load();
            pETBindingSource.DataSource = dbcontext.PETs.Local;
        }
        private void SavePetChanges()
        {

        }
        private void PetGridValidate()
        {

        }

        private void pETBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            SavePetChanges();
        }
    }
}

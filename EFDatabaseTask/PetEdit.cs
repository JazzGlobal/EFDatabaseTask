using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EFDatabaseTask.DataFormExceptions;
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
        private List<int> currentCustomerIds = new List<int>();
        private void PetEdit_Load(object sender, EventArgs e)
        {
            dbcontext.PETs.OrderBy(pet => pet.PET_ID).Load();
            pETBindingSource.DataSource = dbcontext.PETs.Local;

            dbcontext.customers.OrderBy(customer => customer.customerId).Load();
            foreach (var customer in dbcontext.customers.Local.ToList())
            {
                currentCustomerIds.Add(customer.customerId);
            }
        }
        private void SavePetChanges()
        {
            try
            {
                dbcontext.SaveChanges();
            } catch (DbEntityValidationException ex)
            {
                Console.WriteLine(ex);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException dbUpdateEx)
            {
                var checkExForFKeyConstraint = dbUpdateEx.InnerException.InnerException.Message;
                if(dbUpdateEx.InnerException.InnerException.Message.Contains("foreign key constraint fails"))
                {
                    MessageBox.Show(checkExForFKeyConstraint, "CustomerID Not Valid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void pETBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            pETDataGridView.RefreshEdit();
            SavePetChanges();
        }

        private void pETDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch(e.ColumnIndex)
            {
                case 1: // PET Name
                    Console.WriteLine("Validating PET NAME!");
                    try
                    {
                        if (e.FormattedValue.ToString().Trim() == "")
                        {
                            throw new PetNameCannotBeNullException();
                        }
                    } catch (PetNameCannotBeNullException petNameNullEx)
                    {
                        MessageBox.Show(petNameNullEx.Message);
                        pETDataGridView.RefreshEdit();
                    }

                    break;
                case 2: // Active
                    try
                    {
                        bool parsedValue = bool.Parse(e.FormattedValue.ToString());
                    }
                    catch (FormatException formatEx)
                    {
                        MessageBox.Show("Failed to validate Column: Active.\n Value must be True or False.");
                        Console.WriteLine(formatEx);
                        pETDataGridView.RefreshEdit();
                    }
                    break;
                case 3: // Customer ID
                    try
                    {
                        int parsedValue = Int32.Parse(e.FormattedValue.ToString());
                        bool idMatch = false;
                        foreach (var customerId in currentCustomerIds)
                        {
                            if (parsedValue == customerId)
                            {
                                idMatch = true;
                                break;
                            }
                        }
                        if(idMatch == false)
                        {
                            throw new CustomerIDNotFoundException(parsedValue);
                        }
                    } catch (FormatException formatEx)
                    {
                        MessageBox.Show("Failed to validate column: CustomerID.\n Value must be a number.");
                        pETDataGridView.RefreshEdit();
                    }
                    catch (CustomerIDNotFoundException customerIDNotFoundEx)
                    {
                        MessageBox.Show(customerIDNotFoundEx.Message);
                        pETDataGridView.RefreshEdit();
                    }
                    break;
            }
        }
    }
}

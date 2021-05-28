using System;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class DataEditHub : Form
    {
        public DataEditHub()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void editCustomersButton_Click(object sender, EventArgs e)
        {
            CustomerEdit c_edit = new CustomerEdit();
            c_edit.ShowDialog();
        }

        private void editAddressesButton_Click(object sender, EventArgs e)
        {
            AddressEdit a_edit = new AddressEdit();
            a_edit.ShowDialog();
        }

        private void editAppointmentsButton_Click(object sender, EventArgs e)
        {
            AppointmentEdit a_edit = new AppointmentEdit();
            try
            {
                a_edit.ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Unable to perform this action. Closing Appointment Window");
                Console.WriteLine(ex);
            }
        }

        private void viewCalendarButton_Click(object sender, EventArgs e)
        {
            Calendar cal = new Calendar();
            try
            {
                cal.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void viewReportsButton_Click(object sender, EventArgs e)
        {
            // Show reporting selection form.
            Reports r_form = new Reports();
            r_form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PetEdit p_edit = new PetEdit();
            try
            {
                p_edit.ShowDialog();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Unable to perform this action. Closing Pet Window");
                Console.WriteLine(ex);
            }
        }

        private void searchPetsButton_Click(object sender, EventArgs e)
        {
            PetSearchForm p_search = new PetSearchForm();
            try
            {
                p_search.ShowDialog();
            } catch (InvalidOperationException ex)
            {
                MessageBox.Show("Unable to perform this action. Closing Pet Window");
                Console.WriteLine(ex);
            }
        }
    }
}

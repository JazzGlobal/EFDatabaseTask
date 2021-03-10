using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EFDatabaseTask
{
    public partial class Calendar : Form
    {
        public Calendar()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities();

        private void Calendar_Load(object sender, EventArgs e)
        {
            // 
            Console.WriteLine("Getting Appointments For User: " + Login.CurrentLoggedInUser.userName);
        }
    }
}

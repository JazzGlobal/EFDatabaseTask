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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private U07lyXEntities dbcontext = new U07lyXEntities(); 
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}

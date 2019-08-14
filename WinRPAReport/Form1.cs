using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinRPAReport.Resource;

namespace WinRPAReport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryManager regman = new RegistryManager();

                regman.WriteRegistry();

                lblClicks.Text = regman.ReadRegistry();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryManager regman = new RegistryManager();

            lblClicks.Text = regman.ReadRegistry();
        }
    }
}

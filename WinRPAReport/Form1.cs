using System;
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryManager regman = new RegistryManager();

            lblClicks.Text = regman.ReadRegistry();

        }
    }
}

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
            RegistryManager regman = new RegistryManager();

            try
            {
                regman.WriteRegistryClicks(this);

                lblClicks.Text = regman.ReadRegistryClicks(this);
            }
            catch (Exception ex)
            {
               // RegistryManager regman = new RegistryManager();
                regman.SetAlarm(ex.Message.ToString(), this);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryManager regman = new RegistryManager();

            lblClicks.Text = regman.ReadRegistryClicks(this);

        }
    }
}

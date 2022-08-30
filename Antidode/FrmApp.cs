using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Antidode
{
    public partial class FrmApp : Form
    {

        private static string EXT = "NYSM";
        private static string[] EXCLUDE_DIRS = new string[] { @"C:\Windows" };
        public FrmApp()
        {
            InitializeComponent();
        }

        private void FrmApp_Load(object sender, EventArgs e)
        {

        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            Worker.Instance(EXT).Run(
                Encoding.ASCII.GetString(Utils.HexToBytes(this.tBoxAntidodeKey.Text)), 
                LOGGER, 
                EXCLUDE_DIRS);
        }
        private void LOGGER(string LOG)
        {
            if (this.tBoxLog.InvokeRequired)
            {
                this.tBoxLog.Invoke(new MethodInvoker(() => { this.tBoxLog.Text += $"\r\n{LOG}"; }));
            }
            else { this.tBoxLog.Text += $"\r\n{LOG}"; }
        }

        private void FrmApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.Disappear();
        }
        
    }
}

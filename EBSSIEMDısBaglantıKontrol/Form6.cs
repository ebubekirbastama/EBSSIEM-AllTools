using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        Thread th;
        private void ihlalleriListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            th = new Thread(gtr);th.Start();
        }

        private void gtr()
        {
            ebscontrol.ReadAndLogFailedLogins(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}

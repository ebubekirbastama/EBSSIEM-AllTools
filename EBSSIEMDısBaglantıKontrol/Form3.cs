using System;
using System.Timers;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        System.Timers.Timer timer;
        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("CounterName", "Counter Name");
            dataGridView1.Columns.Add("Value", "Value");
            timer = new System.Timers.Timer();
            timer.Interval = 2000; // 20 saniye
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }
        public void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)sveri);
        }

        private void sveri()
        {
            try
            {
                dataGridView1.Rows.Clear();
                string performanceData = ebscontrol.GetPerformanceData();
                string[] lines = performanceData.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in lines)
                {
                    dataGridView1.Rows.Add(item.Split(':')[0].ToString(), item.Split(':')[1].ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void verileriGetirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sveri();
        }
    }
}

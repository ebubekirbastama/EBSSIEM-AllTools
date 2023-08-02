using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Verileri temizle
            resultDataGridView.Rows.Clear();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    // İşlem bağlantıları al
                    IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                    TcpConnectionInformation[] connections = ipProperties.GetActiveTcpConnections();


                    // İşlem bağlantılarını analiz et
                    foreach (TcpConnectionInformation connection in connections)
                    {
                        if (CloseWait.Checked == true)
                        {
                            if (connection.State.ToString() == "CloseWait")
                            {
                                int rowIndex = resultDataGridView.Rows.Add();
                                DataGridViewRow row = resultDataGridView.Rows[rowIndex];
                                row.Cells[0].Value = process.ProcessName;
                                row.Cells[1].Value = process.Id;
                                row.Cells[2].Value = connection.LocalEndPoint;
                                row.Cells[3].Value = connection.RemoteEndPoint;
                                row.Cells[4].Value = connection.State;
                            }
                        }
                        else if (Established.Checked == true)
                        {
                            if (connection.State.ToString() == "Established")
                            {
                                int rowIndex = resultDataGridView.Rows.Add();
                                DataGridViewRow row = resultDataGridView.Rows[rowIndex];
                                row.Cells[0].Value = process.ProcessName;
                                row.Cells[1].Value = process.Id;
                                row.Cells[2].Value = connection.LocalEndPoint;
                                row.Cells[3].Value = connection.RemoteEndPoint;
                                row.Cells[4].Value = connection.State;
                            }
                        }
                        else
                        {
                            int rowIndex = resultDataGridView.Rows.Add();
                            DataGridViewRow row = resultDataGridView.Rows[rowIndex];
                            row.Cells[0].Value = process.ProcessName;
                            row.Cells[1].Value = process.Id;
                            row.Cells[2].Value = connection.LocalEndPoint;
                            row.Cells[3].Value = connection.RemoteEndPoint;
                            row.Cells[4].Value = connection.State;
                        }

                        //if (connection.State == TcpState.Established && connection.LocalEndPoint.Port == 80) // Bağlantıları filtrelemek için istediğiniz kriterleri burada belirtebilirsiniz
                        //{
                        //    int rowIndex = resultDataGridView.Rows.Add();
                        //    DataGridViewRow row = resultDataGridView.Rows[rowIndex];
                        //    row.Cells[0].Value = process.ProcessName;
                        //    row.Cells[1].Value = process.Id;
                        //    row.Cells[2].Value = connection.LocalEndPoint;
                        //    row.Cells[3].Value = connection.RemoteEndPoint;
                        //}
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }

        }
        private bool IsProcessRunning(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.Length > 0;
        }

        private bool CanConnectToRemoteHost(string host, int port)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(host, port);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resultDataGridView.ColumnCount = 5;
            resultDataGridView.Columns[0].HeaderText = "Process İsmi";
            resultDataGridView.Columns[1].HeaderText = "ID";
            resultDataGridView.Columns[2].HeaderText = "Local Endpoint";
            resultDataGridView.Columns[3].HeaderText = "Remote Endpoint";
            resultDataGridView.Columns[4].HeaderText = "Durumu";

            runningProcessesDataGridView.ColumnCount = 2;
            runningProcessesDataGridView.Columns[0].HeaderText = "ID";
            runningProcessesDataGridView.Columns[1].HeaderText = "Process Name";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Running Processes DataGridView'i temizle
            runningProcessesDataGridView.Rows.Clear();

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {
                    // Running Processes DataGridView'e işlem adını ve ID'sini ekleyin
                    int runningRowIndex = runningProcessesDataGridView.Rows.Add();
                    DataGridViewRow runningRow = runningProcessesDataGridView.Rows[runningRowIndex];
                    runningRow.Cells[0].Value = process.Id;
                    runningRow.Cells[1].Value = process.ProcessName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }
    }
}

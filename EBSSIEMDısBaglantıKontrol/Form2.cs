using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        #region Form Load
        private void Form2_Load(object sender, EventArgs e)
        {
            TCPDatagrid.ColumnCount = 7;
            TCPDatagrid.Columns[0].HeaderText = "Process İsmi";
            TCPDatagrid.Columns[1].HeaderText = "Process ID";
            TCPDatagrid.Columns[2].HeaderText = "Local Endpoint";
            TCPDatagrid.Columns[3].HeaderText = "Local Port";
            TCPDatagrid.Columns[4].HeaderText = "Remote Endpoint";
            TCPDatagrid.Columns[5].HeaderText = "Remote Port";
            TCPDatagrid.Columns[6].HeaderText = "Durumu";


            Icmpdatagridview.ColumnCount = 4;
            Icmpdatagridview.Columns[0].HeaderText = "İp Adresi";
            Icmpdatagridview.Columns[1].HeaderText = "İndex";
            Icmpdatagridview.Columns[2].HeaderText = "Mac Adresi";
            Icmpdatagridview.Columns[3].HeaderText = "Mesaj Türü";

            udpdatagrid.ColumnCount = 4;
            udpdatagrid.Columns[0].HeaderText = "İp Adresi";
            udpdatagrid.Columns[1].HeaderText = "Process ID";
            udpdatagrid.Columns[2].HeaderText = "Process Name";
            udpdatagrid.Columns[3].HeaderText = "Local Port";
        }
        #endregion
        #region TCP Listing
        [StructLayout(LayoutKind.Sequential)]
        private struct MIB_TCPROW_OWNER_PID
        {
            public uint state;
            public uint localAddr;
            public uint localPort;
            public uint remoteAddr;
            public uint remotePort;
            public int owningPid;
        }
        [DllImport("iphlpapi.dll")]
        private static extern int GetExtendedTcpTable(IntPtr pTcpTable, ref int pdwSize, bool bOrder, int ulAf, TcpTableClass tableClass, uint reserved);
        private enum TcpTableClass
        {
            TCP_TABLE_BASIC_LISTENER,
            TCP_TABLE_BASIC_CONNECTIONS,
            TCP_TABLE_BASIC_ALL,
            TCP_TABLE_OWNER_PID_LISTENER,
            TCP_TABLE_OWNER_PID_CONNECTIONS,
            TCP_TABLE_OWNER_PID_ALL,
            TCP_TABLE_OWNER_MODULE_LISTENER,
            TCP_TABLE_OWNER_MODULE_CONNECTIONS,
            TCP_TABLE_OWNER_MODULE_ALL
        }
        private void ShowProcessTcpConnections()
        {

            TCPDatagrid.Rows.Clear();
            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes)
            {
                try
                {

                    int bufferSize = 0;
                    GetExtendedTcpTable(IntPtr.Zero, ref bufferSize, false, 2, TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0);

                    IntPtr tcpTablePtr = Marshal.AllocHGlobal(bufferSize);

                    if (GetExtendedTcpTable(tcpTablePtr, ref bufferSize, false, 2, TcpTableClass.TCP_TABLE_OWNER_PID_ALL, 0) == 0)
                    {
                        int rowCount = Marshal.ReadInt32(tcpTablePtr);
                        IntPtr rowPtr = tcpTablePtr + 4;
                      
                        for (int i = 0; i < rowCount; i++)
                        {
                            MIB_TCPROW_OWNER_PID row = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));

                            if (row.owningPid == process.Id)
                            {
                                int rowIndex = TCPDatagrid.Rows.Add();
                                DataGridViewRow rowii = TCPDatagrid.Rows[rowIndex];
                                rowii.Cells[0].Value = process.ProcessName;
                                rowii.Cells[1].Value = process.Id;
                                rowii.Cells[2].Value = ebscontrol.ConvertToIpAddress(row.localAddr);
                                rowii.Cells[3].Value = ebscontrol.ConvertToPort(row.localPort);
                                rowii.Cells[4].Value = ebscontrol.ConvertToIpAddress(row.remoteAddr);
                                rowii.Cells[5].Value = row.remotePort;
                                rowii.Cells[6].Value = ebscontrol.GetTcpConnectionStatus(Convert.ToInt32(row.state));

                            }

                            rowPtr += Marshal.SizeOf<MIB_TCPROW_OWNER_PID>();
                        }
                    }

                    Marshal.FreeHGlobal(tcpTablePtr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }
        #endregion
        #region UDP Verileri
        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern uint GetExtendedUdpTable(IntPtr pUdpTable, ref int pdwSize, bool bOrder, int ulAf, UdpTableClass tableClass, uint reserved);
        [StructLayout(LayoutKind.Sequential)]
        private struct MIB_UDPROW_OWNER_PID
        {
            public uint localAddr;
            public uint localPort;
            public int owningPid;
        }
        private enum UdpTableClass
        {
            UDP_TABLE_OWNER_PID,
            UDP_TABLE_OWNER_MODULE
        }
        public void GetUdpConnectedIPAddresses()
        {
            Process[] processes = Process.GetProcesses();
            int bufferSize = 0;

            GetExtendedUdpTable(IntPtr.Zero, ref bufferSize, false, 2, UdpTableClass.UDP_TABLE_OWNER_PID, 0);
            IntPtr udpTablePtr = Marshal.AllocHGlobal(bufferSize);

            if (GetExtendedUdpTable(udpTablePtr, ref bufferSize, false, 2, UdpTableClass.UDP_TABLE_OWNER_PID, 0) == 0)
            {
                IntPtr rowPtr = udpTablePtr + 4; // Skip the first 4 bytes, which represent the row count.
                MIB_UDPROW_OWNER_PID udpRow;
                int rowCount = Marshal.ReadInt32(udpTablePtr); // Read the row count.
                udpdatagrid.Rows.Clear();

                for (int i = 0; i < rowCount; i++)
                {
                    udpRow = Marshal.PtrToStructure<MIB_UDPROW_OWNER_PID>(rowPtr);

                    uint localAddr = udpRow.localAddr;
                    string ipAddress = ebscontrol.ConvertToIpAddress(localAddr);
                    string processID = udpRow.owningPid.ToString();
                    string processName = ebscontrol.GetProcessNameByProcessId(udpRow.owningPid);
                    string Localport = ebscontrol.ConvertToPort(udpRow.localPort).ToString();


                    int rowIndex = udpdatagrid.Rows.Add();
                    DataGridViewRow rowii = udpdatagrid.Rows[rowIndex];
                    rowii.Cells[0].Value = ipAddress;
                    rowii.Cells[1].Value = processID;
                    rowii.Cells[2].Value = processName;
                    rowii.Cells[3].Value = Localport;

                    // Move to the next row in memory.
                    rowPtr += Marshal.SizeOf(udpRow);
                }
            }

            Marshal.FreeHGlobal(udpTablePtr);

        }
        #endregion
        #region ICMP Verileri
        /// <summary>
        /// Aşağıdaki kodlar ile ICMP Verileri Listeliyoruz..
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        [DllImport("iphlpapi.dll", SetLastError = true)]
        private static extern uint GetIpNetTable(IntPtr pIpNetTable, ref int pdwSize, bool bOrder);
        [StructLayout(LayoutKind.Sequential)]
        private struct MIB_IPNETROW
        {
            public int Index;
            public uint PhysAddrLen;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] PhysAddr;
            public uint Addr;
            public uint Type;
        }
        public void GetIcmpConnectedIPAddresses()
        {

            int bufferSize = 0;

            GetIpNetTable(IntPtr.Zero, ref bufferSize, false);
            IntPtr icmpTablePtr = Marshal.AllocHGlobal(bufferSize);

            if (GetIpNetTable(icmpTablePtr, ref bufferSize, false) == 0)
            {
                IntPtr rowPtr = icmpTablePtr + 4; // Skip the first 4 bytes, which represent the row count.
                MIB_IPNETROW icmpRow;
                int rowCount = Marshal.ReadInt32(icmpTablePtr); // Read the row count.

                Icmpdatagridview.Rows.Clear();

                for (int i = 0; i < rowCount; i++)
                {
                    icmpRow = Marshal.PtrToStructure<MIB_IPNETROW>(rowPtr);

                    // Now you can access the individual fields in tcpRow and display them as needed.

                    uint ipAddress = icmpRow.Addr;
                    string ipAddressStr = ebscontrol.ConvertToIpAddress(ipAddress);
                    int Index = icmpRow.Index;
                    string PhysAddr = BitConverter.ToString(icmpRow.PhysAddr);
                    uint PhysAddrLen = icmpRow.PhysAddrLen;
                    string Type = ebscontrol.GetIcmpMessageType(int.Parse(icmpRow.Type.ToString()));

                    Icmpdatagridview.Rows.Add(ipAddressStr, Index, PhysAddr, Type);




                    // Move to the next row in memory.
                    rowPtr += Marshal.SizeOf(icmpRow);
                }
            }

            Marshal.FreeHGlobal(icmpTablePtr);

        }
        #endregion
        #region Butonlar
        private void ıCMPListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetIcmpConnectedIPAddresses();
        }
        private void tCPListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowProcessTcpConnections();
        }
        private void uDPListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetUdpConnectedIPAddresses();
        }
        #endregion

    }
}

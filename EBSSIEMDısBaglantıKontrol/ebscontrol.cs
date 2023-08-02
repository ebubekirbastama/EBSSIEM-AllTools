using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public class ebscontrol
    {
        public static EventLog eventLog;
        public static EventLogEntry lastEntry;
        public static string GetProcessNameByProcessId(int processId)
        {
            try
            {

                Process process = Process.GetProcessById(processId);
                return process.ProcessName;
            }
            catch (ArgumentException)
            {
                // Eğer process ID'si bulunamazsa veya geçersizse ArgumentException hatası oluşur.
                // Bu durumda boş bir string döndürüyoruz.
                return "";
            }
        }
        public static string GetTcpConnectionStatus(int state)
        {
            switch ((TcpState)state)
            {
                case TcpState.Closed:
                    return "Bağlantı kapalı";
                case TcpState.Listen:
                    return "Bağlantı dinleme modunda";
                case TcpState.SynSent:
                    return "Bağlantı istemci tarafından SYN gönderildi, ama henüz onaylanmadı";
                case TcpState.SynReceived:
                    return "Bağlantı sunucu tarafından SYN alındı, ve onay için yanıt gönderildi";
                case TcpState.Established:
                    return "Bağlantı başarıyla kuruldu, veri alışverişi yapılabilir durumda";
                case TcpState.FinWait1:
                    return "Bağlantı FIN bekliyor";
                case TcpState.FinWait2:
                    return "Bağlantı FIN bekliyor ve karşı tarafın FIN gönderdiği doğrulandı";
                case TcpState.CloseWait:
                    return "Uzak taraf bağlantıyı kapattı, ancak yerel taraf henüz kapatmadı";
                case TcpState.Closing:
                    return "Hem yerel hem de uzak taraf FIN gönderdi, ancak henüz kapatmadılar";
                case TcpState.LastAck:
                    return "Yerel taraf FIN gönderdi, ve uzak taraf kapatma onayını aldı";
                case TcpState.TimeWait:
                    return "Bağlantının sona erdiği zaman bekleniyor";
                case TcpState.DeleteTcb:
                    return "Bağlantı silindi (Tcb: Transmission Control Block)";
                default:
                    return "Bilinmeyen durum";
            }
        }
        public static string GetIcmpMessageType(int type)
        {
            switch (type)
            {
                case 0:
                    return "Echo Yanıtı";
                case 3:
                    return "Hedef Ulaşılamaz";
                case 8:
                    return "Echo İsteği";
                case 11:
                    return "Zaman Aşımı";
                case 13:
                    return "Zaman Damgası İsteği";
                case 14:
                    return "Zaman Damgası Yanıtı";
                // Diğer ICMP Tür değerleri için eklemeler yapılabilir
                default:
                    return "Bilinmeyen ICMP Türü";
            }
        }
        public static string ConvertToIpAddress(uint ipAddress)
        {
            return new IPAddress(BitConverter.GetBytes(ipAddress)).ToString();
        }
        public static int ConvertToPort(uint port)
        {
            return (int)port;
        }
        public static string Donustur(long byteDeger)
        {
            const float birMegabyte = 1024 * 1024;
            const float birGigabyte = 1024 * birMegabyte;

            if (byteDeger >= birGigabyte)
            {
                float gbDeger = byteDeger / birGigabyte;
                return $"{gbDeger:F2} GB";
            }
            else if (byteDeger >= birMegabyte)
            {
                float mbDeger = byteDeger / birMegabyte;
                return $"{mbDeger:F2} MB";
            }
            else
            {
                return $"{byteDeger} bytes";
            }
        }
        public static string GetPerformanceData()
        {

            StringBuilder data = new StringBuilder();

            // Disk İstatistikleri
            PerformanceCounter diskReadCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
            float diskReadRate = diskReadCounter.NextValue() / (1024 * 1024); // Bayt/sn'yi MB/sn'ye dönüştürme
            data.AppendLine("Disk Okuma Hızı: " + diskReadRate.ToString("F2") + " MB/sn");

            PerformanceCounter diskWriteCounter = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
            float diskWriteRate = diskWriteCounter.NextValue() / (1024 * 1024); // Bayt/sn'yi MB/sn'ye dönüştürme
            data.AppendLine("Disk Yazma Hızı: " + diskWriteRate.ToString("F2") + " MB/sn");

            PerformanceCounter diskQueueCounter = new PerformanceCounter("PhysicalDisk", "Current Disk Queue Length", "_Total");
            float diskQueueLength = diskQueueCounter.NextValue();
            data.AppendLine("Disk Kuyruğu Uzunluğu: " + diskQueueLength);

            // Ağ İstatistikleri (varsayılan ağ arayüzü)
            PerformanceCounter networkSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", GetDefaultNetworkInterface());
            float networkSentRate = networkSentCounter.NextValue() / (1024 * 1024); // Bayt/sn'yi MB/sn'ye dönüştürme
            data.AppendLine("Gönderilen Veri Miktarı: " + networkSentRate.ToString("F2") + " MB/sn");

            PerformanceCounter networkReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", GetDefaultNetworkInterface());
            float networkReceivedRate = networkReceivedCounter.NextValue() / (1024 * 1024); // Bayt/sn'yi MB/sn'ye dönüştürme
            data.AppendLine("Alınan Veri Miktarı: " + networkReceivedRate.ToString("F2") + " MB/sn");

            PerformanceCounter networkBandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", GetDefaultNetworkInterface());
            float networkBandwidth = networkBandwidthCounter.NextValue() / (1024 * 1024); // Bit/sn'yi Mbps'ye dönüştürme
            data.AppendLine("Ağ Hızı: " + networkBandwidth.ToString("F2") + " Mbps");

            // Bellek İstatistikleri
            PerformanceCounter memoryAvailableCounter = new PerformanceCounter("Memory", "Available Bytes");
            float memoryAvailable = memoryAvailableCounter.NextValue() / (1024 * 1024); // Bayt'ı MB'ye dönüştürme
            data.AppendLine("Toplam Fiziksel Bellek: " + memoryAvailable.ToString("F2") + " MB");


            PerformanceCounter memoryCommittedCounter = new PerformanceCounter("Memory", "Committed Bytes");
            float memoryCommitted = memoryCommittedCounter.NextValue() / (1024 * 1024); // Bayt'ı MB'ye dönüştürme
            data.AppendLine("Toplam Sanal Bellek: " + memoryCommitted.ToString("F2") + " MB");

            PerformanceCounter memoryUsageCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
            float memoryUsage = memoryUsageCounter.NextValue();
            data.AppendLine("Bellek Kullanımı Yüzdesi: " + memoryUsage.ToString("F2") + "%");

            // CPU Çekirdek İstatistikleri
            PerformanceCounter cpuCoreCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float cpuCoreUsage = cpuCoreCounter.NextValue();
            data.AppendLine("İşlemci Çekirdeği Kullanımı: " + cpuCoreUsage.ToString("F2") + "%");

            // Eklemek istediğiniz diğer performans istatistiklerini buraya ekleyebilirsiniz.

            return data.ToString();
        }
        public static string GetDefaultNetworkInterface()
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
            string[] instances = category.GetInstanceNames();
            return instances[0]; // Varsayılan ağ arayüzü
        }
        public static void ReadAndLogFailedLogins(DataGridView dg)
        {
            InitializeEventLog(dg);

                EventLogEntryCollection entries = eventLog.Entries;

                foreach (EventLogEntry entry in entries)
                {
                    if (entry.InstanceId == 4625 && !entry.Equals(lastEntry))
                    {
                        // DataGridView'e yeni girişi ekleyin
                        AddEntryToDataGridView(entry,dg);
                    }
                }

                lastEntry = entries[entries.Count - 1];

                // Belirli bir süre bekleyin (örn. 5 saniye) ve yeni girişleri kontrol edin
                System.Threading.Thread.Sleep(5000);
            
            
        }
        public static void AddEntryToDataGridView(EventLogEntry entry, DataGridView dg)
        {
            // Yeni satır ekleyin
            int rowIndex = dg.Rows.Add();

            // Giriş bilgilerini DataGridView'e yazın
            dg.Rows[rowIndex].Cells["Date"].Value = entry.TimeGenerated;
            dg.Rows[rowIndex].Cells["Description"].Value = entry.Message;
        }
        public static void InitializeEventLog(DataGridView dg)
        {
            string eventLogName = "Security";
            string eventSource = "Microsoft Windows security auditing.";

            eventLog = new EventLog(eventLogName);
            eventLog.Source = eventSource;
            lastEntry = eventLog.Entries[eventLog.Entries.Count - 1];

            // DataGridView sütun başlıklarını belirleyin
            dg.Columns.Add("Date", "Tarih/Zaman");
            dg.Columns.Add("Description", "Açıklama");
        }
    }
}

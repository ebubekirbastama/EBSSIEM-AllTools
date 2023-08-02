using System;
using System.IO;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // DataGridView sütunlarını oluşturuyoruz
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].HeaderText = "Durumu";
            dataGridView1.Columns[1].HeaderText = "Dosya Adı";
            dataGridView1.Columns[2].HeaderText = "Dosya Yolu";

        }

        // Dosya oluşturulduğunda tetiklenen olay
        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            AddFileToDataGridView(e.Name, e.ChangeType.ToString(), e.FullPath);
        }
        // Dosya değiştirildiğinde tetiklenen olay
        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            AddFileToDataGridView(e.Name, e.ChangeType.ToString(), e.FullPath);
        }
        // Dosya silindiğinde tetiklenen olay
        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            AddFileToDataGridView(e.Name, e.ChangeType.ToString(), e.FullPath);
        }
        // DataGridView'e yeni veriyi eklemek için yardımcı fonksiyon
        private void AddFileToDataGridView(string fileName, string changeType, string FullPath)
        {
             dataGridView1.Rows.Add(changeType,fileName,FullPath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // FileSystemWatcher ayarları
            FileSystemWatcher fileSystemWatcher1 = new FileSystemWatcher(textBox1.Text);
            fileSystemWatcher1.EnableRaisingEvents = true;
            fileSystemWatcher1.IncludeSubdirectories = false;

            // Olayları dinleme
            fileSystemWatcher1.Created += new FileSystemEventHandler(fileSystemWatcher1_Created);
            fileSystemWatcher1.Changed += new FileSystemEventHandler(fileSystemWatcher1_Changed);
            fileSystemWatcher1.Deleted += new FileSystemEventHandler(fileSystemWatcher1_Deleted);
        }
    }
}

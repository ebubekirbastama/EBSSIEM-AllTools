using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EBSSIEMDısBaglantıKontrol
{
    public partial class Form5 : Form
    {
        int borderRadius = 20;
        int borderSize = 2;
        Color borderColor = Color.Teal;
        public Form5()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            FormBorderStyle = FormBorderStyle.None;
            Padding = new Padding(borderSize);
            panelTitleBar.BackColor = borderColor;
            BackColor = borderColor;
        }
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- Minimize borderless form from taskbar
                return cp;
            }
        }
        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                using (Matrix transform = new Matrix())
                {
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (borderSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((borderSize + 1) / rect.Height);
                        transform.Scale(scaleX, scaleY);
                        transform.Translate(borderSize / 1.6F, borderSize / 1.6F);
                        graph.Transform = transform;
                        graph.DrawPath(penBorder, roundPath);
                    }
                }
            }
        }
        private void ControlRegionAndBorder(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }
        private void DrawPath(Rectangle rect, Graphics graph, Color color)
        {
            using (GraphicsPath roundPath = GetRoundedPath(rect, borderRadius))
            using (Pen penBorder = new Pen(color, 3))
            {
                graph.DrawPath(penBorder, roundPath);
            }
        }
        private struct FormBoundsColors
        {
            public Color TopLeftColor;
            public Color TopRightColor;
            public Color BottomLeftColor;
            public Color BottomRightColor;
        }
        private FormBoundsColors GetFormBoundsColors()
        {
            var fbColor = new FormBoundsColors();
            using (var bmp = new Bitmap(1, 1))
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectBmp = new Rectangle(0, 0, 1, 1);

                //Top Left
                rectBmp.X = this.Bounds.X - 1;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopLeftColor = bmp.GetPixel(0, 0);

                //Top Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopRightColor = bmp.GetPixel(0, 0);

                //Bottom Left
                rectBmp.X = this.Bounds.X;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomLeftColor = bmp.GetPixel(0, 0);

                //Bottom Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomRightColor = bmp.GetPixel(0, 0);
            }
            return fbColor;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //-> SMOOTH OUTER BORDER
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectForm = this.ClientRectangle;
            int mWidht = rectForm.Width / 2;
            int mHeight = rectForm.Height / 2;
            var fbColors = GetFormBoundsColors();

            //Top Left
            DrawPath(rectForm, e.Graphics, fbColors.TopLeftColor);

            //Top Right
            Rectangle rectTopRight = new Rectangle(mWidht, rectForm.Y, mWidht, mHeight);
            DrawPath(rectTopRight, e.Graphics, fbColors.TopRightColor);

            //Bottom Left
            Rectangle rectBottomLeft = new Rectangle(rectForm.X, rectForm.X + mHeight, mWidht, mHeight);
            DrawPath(rectBottomLeft, e.Graphics, fbColors.BottomLeftColor);

            //Bottom Right
            Rectangle rectBottomRight = new Rectangle(mWidht, rectForm.Y + mHeight, mWidht, mHeight);
            DrawPath(rectBottomRight, e.Graphics, fbColors.BottomRightColor);

            //-> SET ROUNDED REGION AND BORDER
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }
        private void panelContainer_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panelContainer, borderRadius - (borderSize / 2), e.Graphics, borderColor);
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void Form1_Activated(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void EnableFileSystemAuditing(string folderPath)
        {
            try
            {
                string logName = "Security";
                string query = string.Format(@"*[System[(EventID=4663) and (Data[@Name='ObjectType'] and (Data='File')) and (Data[@Name='ObjectName'] and (Data='{0}'))]]", folderPath);

                EventLogQuery eventsQuery = new EventLogQuery(logName, PathType.LogName, query);
                EventLogReader logReader = new EventLogReader(eventsQuery);

                EventRecord eventRecord;
                while ((eventRecord = logReader.ReadEvent()) != null)
                {
                    // Dosya silindiği için gerekli işlemleri yapabilirsiniz
                    // Mesela, logReader.ReadEvent()'ten dönen EventRecord nesnesini kullanarak olay bilgilerine erişebilirsiniz.

                    // Dosya adını almak için:
                    string fileName = eventRecord.Properties[5].Value.ToString();

                    // Dosyanın tam yolu için:
                    string fullPath = eventRecord.Properties[6].Value.ToString();

                    // Olayın gerçekleştiği tarih ve saat için:
                    DateTime eventTime = eventRecord.TimeCreated ?? DateTime.Now;

                    // Olayı gerçekleştiren kullanıcının adı için:
                    string userName = eventRecord.Properties[1].Value.ToString();

                    // Yukarıdaki bilgileri kullanarak istediğiniz işlemleri yapabilirsiniz.
                    // Örneğin, bunları DataGridView'e veya başka bir nesneye ekleyebilirsiniz.
                }

                logReader.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya sistemi günlüğü izlemesi yapılamadı: " + ex.Message, "Hata");
            }
        }
        private void startLogingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fileSystemWatcher1.EnableRaisingEvents = true;
        }
        void veri_getir(string log, string state)
        {
            dataGridView1.Rows.Add(log, state);
        }
        void createcolumns()
        {
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Log";
            dataGridView1.Columns[1].Name = "State";
        }
        void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                veri_getir(e.FullPath + e.Name, "Oluşturuldu");
            }
            catch (Exception)
            {

            }

        }
        void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                veri_getir(e.FullPath, "Silindi");
                EnableFileSystemAuditing(e.FullPath);
            }
            catch (Exception)
            {

            }
        }
        void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            try
            {
                veri_getir(e.FullPath, " Adı Değiştirildi");
            }
            catch (Exception)
            {
            }
        }
        void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            try
            {
                veri_getir(e.FullPath, " Değişiklik yapıldı.");
            }
            catch (Exception ex)
            {

            }
        }
        void anliklog_Load(object sender, EventArgs e)
        {
            createcolumns();
        }
        void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Process.Start(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eçk Yazılım.");
            }
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            createcolumns();
        }
    }
}

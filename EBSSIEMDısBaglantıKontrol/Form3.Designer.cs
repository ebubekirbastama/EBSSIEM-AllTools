namespace EBSSIEMDısBaglantıKontrol
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.performanceCounter1 = new System.Diagnostics.PerformanceCounter();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ebubekirBastamaYazılımToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ebubekirYazılımV01ToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.verileriGetirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(662, 383);
            this.dataGridView1.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ebubekirBastamaYazılımToolStripMenuItem,
            this.ebubekirYazılımV01ToolStripMenuItem,
            this.verileriGetirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(209, 54);
            // 
            // ebubekirBastamaYazılımToolStripMenuItem
            // 
            this.ebubekirBastamaYazılımToolStripMenuItem.Enabled = false;
            this.ebubekirBastamaYazılımToolStripMenuItem.Name = "ebubekirBastamaYazılımToolStripMenuItem";
            this.ebubekirBastamaYazılımToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ebubekirBastamaYazılımToolStripMenuItem.Text = "Ebubekir Bastama Yazılım";
            // 
            // ebubekirYazılımV01ToolStripMenuItem
            // 
            this.ebubekirYazılımV01ToolStripMenuItem.Name = "ebubekirYazılımV01ToolStripMenuItem";
            this.ebubekirYazılımV01ToolStripMenuItem.Size = new System.Drawing.Size(205, 6);
            // 
            // verileriGetirToolStripMenuItem
            // 
            this.verileriGetirToolStripMenuItem.Name = "verileriGetirToolStripMenuItem";
            this.verileriGetirToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.verileriGetirToolStripMenuItem.Text = "Verileri Getir";
            this.verileriGetirToolStripMenuItem.Click += new System.EventHandler(this.verileriGetirToolStripMenuItem_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(662, 383);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perfmon Exe";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.performanceCounter1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Diagnostics.PerformanceCounter performanceCounter1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ebubekirBastamaYazılımToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator ebubekirYazılımV01ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verileriGetirToolStripMenuItem;
    }
}
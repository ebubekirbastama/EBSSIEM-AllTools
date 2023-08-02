namespace EBSSIEMDısBaglantıKontrol
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.runningProcessesDataGridView = new System.Windows.Forms.DataGridView();
            this.CloseWait = new System.Windows.Forms.CheckBox();
            this.Established = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runningProcessesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(709, 252);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 41);
            this.button1.TabIndex = 0;
            this.button1.Text = "Analiz Et";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.Location = new System.Drawing.Point(18, 22);
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.Size = new System.Drawing.Size(816, 224);
            this.resultDataGridView.TabIndex = 1;
            // 
            // runningProcessesDataGridView
            // 
            this.runningProcessesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.runningProcessesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runningProcessesDataGridView.Location = new System.Drawing.Point(857, 22);
            this.runningProcessesDataGridView.Name = "runningProcessesDataGridView";
            this.runningProcessesDataGridView.Size = new System.Drawing.Size(330, 1037);
            this.runningProcessesDataGridView.TabIndex = 2;
            // 
            // CloseWait
            // 
            this.CloseWait.AutoSize = true;
            this.CloseWait.Location = new System.Drawing.Point(26, 259);
            this.CloseWait.Name = "CloseWait";
            this.CloseWait.Size = new System.Drawing.Size(74, 17);
            this.CloseWait.TabIndex = 3;
            this.CloseWait.Text = "CloseWait";
            this.CloseWait.UseVisualStyleBackColor = true;
            // 
            // Established
            // 
            this.Established.AutoSize = true;
            this.Established.Location = new System.Drawing.Point(112, 259);
            this.Established.Name = "Established";
            this.Established.Size = new System.Drawing.Size(80, 17);
            this.Established.TabIndex = 4;
            this.Established.Text = "Established";
            this.Established.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(709, 299);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 38);
            this.button2.TabIndex = 5;
            this.button2.Text = "Process List Getir";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1198, 1071);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Established);
            this.Controls.Add(this.CloseWait);
            this.Controls.Add(this.runningProcessesDataGridView);
            this.Controls.Add(this.resultDataGridView);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runningProcessesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.DataGridView runningProcessesDataGridView;
        private System.Windows.Forms.CheckBox CloseWait;
        private System.Windows.Forms.CheckBox Established;
        private System.Windows.Forms.Button button2;
    }
}


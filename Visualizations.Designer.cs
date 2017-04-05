namespace Player
{
    partial class Visualizations
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pbVis = new System.Windows.Forms.PictureBox();
            this.lbldBL = new System.Windows.Forms.Label();
            this.lbldBR = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prgL = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.prgR = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.visualizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumWaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPectrumDotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spectrumLinePeakToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pbVis)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 35;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pbVis
            // 
            this.pbVis.BackColor = System.Drawing.Color.Black;
            this.pbVis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbVis.Location = new System.Drawing.Point(0, 24);
            this.pbVis.Name = "pbVis";
            this.pbVis.Size = new System.Drawing.Size(700, 326);
            this.pbVis.TabIndex = 29;
            this.pbVis.TabStop = false;
            // 
            // lbldBL
            // 
            this.lbldBL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbldBL.AutoSize = true;
            this.lbldBL.Location = new System.Drawing.Point(12, 305);
            this.lbldBL.Name = "lbldBL";
            this.lbldBL.Size = new System.Drawing.Size(41, 13);
            this.lbldBL.TabIndex = 21;
            this.lbldBL.Text = "00.0dB";
            this.lbldBL.Visible = false;
            // 
            // lbldBR
            // 
            this.lbldBR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbldBR.AutoSize = true;
            this.lbldBR.Location = new System.Drawing.Point(12, 328);
            this.lbldBR.Name = "lbldBR";
            this.lbldBR.Size = new System.Drawing.Size(41, 13);
            this.lbldBR.TabIndex = 22;
            this.lbldBR.Text = "00.0dB";
            this.lbldBR.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(654, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "+6dB";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(673, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "R";
            this.label4.Visible = false;
            // 
            // prgL
            // 
            this.prgL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgL.Location = new System.Drawing.Point(55, 305);
            this.prgL.Maximum = 65535;
            this.prgL.Name = "prgL";
            this.prgL.Size = new System.Drawing.Size(612, 13);
            this.prgL.TabIndex = 23;
            this.prgL.Visible = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(673, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "L";
            this.label3.Visible = false;
            // 
            // prgR
            // 
            this.prgR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgR.Location = new System.Drawing.Point(55, 328);
            this.prgR.Maximum = 65535;
            this.prgR.Name = "prgR";
            this.prgR.Size = new System.Drawing.Size(612, 13);
            this.prgR.TabIndex = 24;
            this.prgR.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visualizationToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(700, 24);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // visualizationToolStripMenuItem
            // 
            this.visualizationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spectrumToolStripMenuItem,
            this.spectrumLineToolStripMenuItem,
            this.spectrumWaveToolStripMenuItem,
            this.spectrumTextToolStripMenuItem,
            this.sPectrumDotToolStripMenuItem,
            this.spectrumLinePeakToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.visualizationToolStripMenuItem.Name = "visualizationToolStripMenuItem";
            this.visualizationToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.visualizationToolStripMenuItem.Text = "Visualization";
            // 
            // spectrumToolStripMenuItem
            // 
            this.spectrumToolStripMenuItem.Checked = true;
            this.spectrumToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.spectrumToolStripMenuItem.Name = "spectrumToolStripMenuItem";
            this.spectrumToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spectrumToolStripMenuItem.Text = "Spectrum";
            this.spectrumToolStripMenuItem.Click += new System.EventHandler(this.spectrumToolStripMenuItem_Click_1);
            // 
            // spectrumLineToolStripMenuItem
            // 
            this.spectrumLineToolStripMenuItem.Name = "spectrumLineToolStripMenuItem";
            this.spectrumLineToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spectrumLineToolStripMenuItem.Text = "Spectrum Line";
            this.spectrumLineToolStripMenuItem.Click += new System.EventHandler(this.spectrumLineToolStripMenuItem_Click_1);
            // 
            // spectrumWaveToolStripMenuItem
            // 
            this.spectrumWaveToolStripMenuItem.Name = "spectrumWaveToolStripMenuItem";
            this.spectrumWaveToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spectrumWaveToolStripMenuItem.Text = "Spectrum Wave";
            this.spectrumWaveToolStripMenuItem.Click += new System.EventHandler(this.spectrumWaveToolStripMenuItem_Click_1);
            // 
            // spectrumTextToolStripMenuItem
            // 
            this.spectrumTextToolStripMenuItem.Name = "spectrumTextToolStripMenuItem";
            this.spectrumTextToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spectrumTextToolStripMenuItem.Text = "Spectrum Text";
            this.spectrumTextToolStripMenuItem.Click += new System.EventHandler(this.spectrumTextToolStripMenuItem_Click);
            // 
            // sPectrumDotToolStripMenuItem
            // 
            this.sPectrumDotToolStripMenuItem.Name = "sPectrumDotToolStripMenuItem";
            this.sPectrumDotToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.sPectrumDotToolStripMenuItem.Text = "Spectrum Dot";
            this.sPectrumDotToolStripMenuItem.Click += new System.EventHandler(this.sPectrumDotToolStripMenuItem_Click);
            // 
            // spectrumLinePeakToolStripMenuItem
            // 
            this.spectrumLinePeakToolStripMenuItem.Name = "spectrumLinePeakToolStripMenuItem";
            this.spectrumLinePeakToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.spectrumLinePeakToolStripMenuItem.Text = "Spectrum LinePeak";
            this.spectrumLinePeakToolStripMenuItem.Click += new System.EventHandler(this.spectrumLinePeakToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // Visualizations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 350);
            this.Controls.Add(this.pbVis);
            this.Controls.Add(this.lbldBL);
            this.Controls.Add(this.lbldBR);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.prgL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prgR);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(285, 150);
            this.Name = "Visualizations";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizations";
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.pbVis)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbldBL;
        private System.Windows.Forms.Label lbldBR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar prgL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar prgR;
        private System.Windows.Forms.ToolStripMenuItem visualizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumWaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPectrumDotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spectrumLinePeakToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem spectrumToolStripMenuItem;
        internal System.Windows.Forms.Timer timer;
        internal System.Windows.Forms.MenuStrip menuStrip1;
        internal System.Windows.Forms.PictureBox pbVis;
    }
}
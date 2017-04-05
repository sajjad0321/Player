namespace Player
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDevice = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtProxy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cd = new System.Windows.Forms.ColorDialog();
            this.txtvisColorbg = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblvisBack = new System.Windows.Forms.Label();
            this.chooseVis = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtvisColor2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtvisColor3 = new System.Windows.Forms.TextBox();
            this.btnDefault = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtvisbg = new System.Windows.Forms.TextBox();
            this.visonTop = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output Device:";
            // 
            // cmbDevice
            // 
            this.cmbDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevice.FormattingEnabled = true;
            this.cmbDevice.Location = new System.Drawing.Point(95, 6);
            this.cmbDevice.Name = "cmbDevice";
            this.cmbDevice.Size = new System.Drawing.Size(191, 21);
            this.cmbDevice.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(155, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(65, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(89, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(65, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtProxy
            // 
            this.txtProxy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProxy.Location = new System.Drawing.Point(78, 267);
            this.txtProxy.Name = "txtProxy";
            this.txtProxy.Size = new System.Drawing.Size(191, 20);
            this.txtProxy.TabIndex = 5;
            this.txtProxy.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(-5, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "user:pass@server:port";
            this.label3.Visible = false;
            // 
            // txtvisColorbg
            // 
            this.txtvisColorbg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtvisColorbg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtvisColorbg.Location = new System.Drawing.Point(95, 61);
            this.txtvisColorbg.Name = "txtvisColorbg";
            this.txtvisColorbg.ReadOnly = true;
            this.txtvisColorbg.Size = new System.Drawing.Size(191, 20);
            this.txtvisColorbg.TabIndex = 7;
            this.txtvisColorbg.Text = " ";
            this.txtvisColorbg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-5, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Proxy string";
            this.label2.Visible = false;
            // 
            // lblvisBack
            // 
            this.lblvisBack.AutoSize = true;
            this.lblvisBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.lblvisBack.Location = new System.Drawing.Point(40, 64);
            this.lblvisBack.Name = "lblvisBack";
            this.lblvisBack.Size = new System.Drawing.Size(49, 15);
            this.lblvisBack.TabIndex = 8;
            this.lblvisBack.Text = "Color 1:";
            // 
            // chooseVis
            // 
            this.chooseVis.AutoSize = true;
            this.chooseVis.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.chooseVis.Location = new System.Drawing.Point(18, 36);
            this.chooseVis.Name = "chooseVis";
            this.chooseVis.Size = new System.Drawing.Size(71, 15);
            this.chooseVis.TabIndex = 9;
            this.chooseVis.Text = "Choose Vis:";
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "BAR",
            "TEXT",
            "SIMPLE",
            "WAVE",
            "LINE",
            "LINEPEAK"});
            this.comboBox1.Location = new System.Drawing.Point(95, 33);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(191, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.label4.Location = new System.Drawing.Point(40, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Color 2:";
            // 
            // txtvisColor2
            // 
            this.txtvisColor2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtvisColor2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtvisColor2.Location = new System.Drawing.Point(95, 87);
            this.txtvisColor2.Name = "txtvisColor2";
            this.txtvisColor2.ReadOnly = true;
            this.txtvisColor2.Size = new System.Drawing.Size(191, 20);
            this.txtvisColor2.TabIndex = 11;
            this.txtvisColor2.Text = " ";
            this.txtvisColor2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtvisColor2_MouseClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.label5.Location = new System.Drawing.Point(13, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "Background:";
            // 
            // txtvisColor3
            // 
            this.txtvisColor3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtvisColor3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtvisColor3.Location = new System.Drawing.Point(95, 113);
            this.txtvisColor3.Name = "txtvisColor3";
            this.txtvisColor3.ReadOnly = true;
            this.txtvisColor3.Size = new System.Drawing.Size(191, 20);
            this.txtvisColor3.TabIndex = 13;
            this.txtvisColor3.Text = " ";
            this.txtvisColor3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtvisColor3_MouseClick);
            // 
            // btnDefault
            // 
            this.btnDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefault.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDefault.Location = new System.Drawing.Point(221, 238);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(65, 23);
            this.btnDefault.TabIndex = 15;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.95F);
            this.label6.Location = new System.Drawing.Point(40, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "Color 3:";
            // 
            // txtvisbg
            // 
            this.txtvisbg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtvisbg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtvisbg.Location = new System.Drawing.Point(95, 139);
            this.txtvisbg.Name = "txtvisbg";
            this.txtvisbg.ReadOnly = true;
            this.txtvisbg.Size = new System.Drawing.Size(191, 20);
            this.txtvisbg.TabIndex = 16;
            this.txtvisbg.Text = " ";
            this.txtvisbg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtvisbg_MouseClick);
            // 
            // visonTop
            // 
            this.visonTop.AutoSize = true;
            this.visonTop.Location = new System.Drawing.Point(21, 242);
            this.visonTop.Name = "visonTop";
            this.visonTop.Size = new System.Drawing.Size(59, 17);
            this.visonTop.TabIndex = 18;
            this.visonTop.Text = "OnTop";
            this.visonTop.UseVisualStyleBackColor = true;
            this.visonTop.CheckedChanged += new System.EventHandler(this.visonTop_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(298, 275);
            this.Controls.Add(this.visonTop);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtvisbg);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtvisColor3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtvisColor2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.chooseVis);
            this.Controls.Add(this.lblvisBack);
            this.Controls.Add(this.txtvisColorbg);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProxy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbDevice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(310, 130);
            this.Name = "SettingsForm";
            this.Opacity = 0.95D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbDevice;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtProxy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColorDialog cd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblvisBack;
        internal System.Windows.Forms.TextBox txtvisColorbg;
        private System.Windows.Forms.Label chooseVis;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox txtvisColor2;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtvisColor3;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtvisbg;
        private System.Windows.Forms.CheckBox visonTop;
    }
}
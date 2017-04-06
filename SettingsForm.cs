using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Un4seen.Bass;
using System.Runtime.InteropServices;

namespace Player
{
    public partial class SettingsForm : Form
    {

        public SettingsForm()
        {
            InitializeComponent();

            LoadDevices();

            comboBox1.SelectedItem = Properties.Settings.Default.configvis;
            txtProxy.Text = Properties.Settings.Default.Proxy;
            txtvisColor.BackColor = Properties.Settings.Default.visColor;
            txtvisColor2.BackColor = Properties.Settings.Default.visColor2;
            txtvisColor3.BackColor = Properties.Settings.Default.visColor3;
            bool top = Properties.Settings.Default.topMost;
            bool fullScreen = Properties.Settings.Default.chkFullscreen;
            //txtvisbg.BackColor = Properties.Settings.Default.visColorbg;
            if (top == true)
                visonTop.Checked = true;
            else visonTop.Checked = false;

            if (fullScreen == true)
                chkfull.Checked = true;
            else chkfull.Checked = false;
        }

        private void LoadDevices()
        {
            for (int i = 1; i < Bass.BASS_GetDeviceCount(); i++)
            {
                BASS_DEVICEINFO info = Bass.BASS_GetDeviceInfo(i);
                if (info.IsEnabled)
                    cmbDevice.Items.Add(info.name);
            }

            if (Properties.Settings.Default.Device == -1)
            {
                if (cmbDevice.Items.Count > 0)
                    cmbDevice.SelectedIndex = 0;
            }
            else
                cmbDevice.SelectedIndex = Properties.Settings.Default.Device - 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Device = cmbDevice.SelectedIndex + 1;
            Properties.Settings.Default.configvis = comboBox1.SelectedItem.ToString();
            Properties.Settings.Default.Proxy = txtProxy.Text;
            Properties.Settings.Default.visColor = txtvisColor.BackColor;
            Properties.Settings.Default.visColor2 = txtvisColor2.BackColor;
            Properties.Settings.Default.visColor3 = txtvisColor3.BackColor;
            Properties.Settings.Default.visColorbg = txtvisbg.BackColor;
            if (visonTop.Checked == true) // setting Styles 
                Properties.Settings.Default.topMost = true;
            else Properties.Settings.Default.topMost = false;
            if (chkfull.Checked == true) // setting FullScreen Visualization 
                Properties.Settings.Default.chkFullscreen = true;
            else Properties.Settings.Default.chkFullscreen = false;
            Properties.Settings.Default.Save();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtvisColor.BackColor = cd.Color;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           // MessageBox.Show(comboBox1.SelectedItem.ToString());
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Device = cmbDevice.SelectedIndex + 1;
            Properties.Settings.Default.Proxy = txtProxy.Text;
            Properties.Settings.Default.topMost = false;

            Properties.Settings.Default.visColor = Color.LimeGreen;
            Properties.Settings.Default.visColor2 = Color.HotPink;
            Properties.Settings.Default.visColor3 = Color.Red;
            Properties.Settings.Default.visColorbg = Color.Transparent;
            Properties.Settings.Default.configvis = "BAR";
            Properties.Settings.Default.chkFullscreen = false;
            Properties.Settings.Default.Save();
            MessageBox.Show("Setting Restored",Application.ProductName);
        }

        private void txtvisColor2_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtvisColor2.BackColor = cd.Color;
            }
        }

        private void txtvisColor3_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtvisColor3.BackColor = cd.Color;
            }
        }

        private void txtvisbg_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtvisbg.BackColor = cd.Color;
            }
        }

        private void visonTop_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}

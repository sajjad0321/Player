using System;
using System.Drawing;
using System.Windows.Forms;
using Un4seen.Bass;

namespace Player
{
    public partial class SettingsForm : Form
    {
        private static SettingsForm _instance;
        public static SettingsForm Instance => _instance ?? (_instance = new SettingsForm());

        internal SettingsForm()
        {
            InitializeComponent();

            LoadDevices();

            cmbvis.SelectedItem = Properties.Settings.Default.configvis;
            txtProxy.Text = Properties.Settings.Default.Proxy;
            txtvisColor.BackColor = Properties.Settings.Default.visColor;
            txtvisColor2.BackColor = Properties.Settings.Default.visColor2;
            txtvisColor3.BackColor = Properties.Settings.Default.visColor3;
            var check = Properties.Settings.Default.visColorbg;
            if (check != Color.Transparent)
            {
                txtvisbg.BackColor = Properties.Settings.Default.visColorbg;
            }

            var top = Properties.Settings.Default.topMost;
            var fullScreen = Properties.Settings.Default.chkFullscreen;
            visonTop.Checked = top;
            chkfull.Checked = fullScreen;
        }

        private void LoadDevices()
        {
            for (var i = 1; i < Bass.BASS_GetDeviceCount(); i++)
            {
                var info = Bass.BASS_GetDeviceInfo(i);
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
            Properties.Settings.Default.configvis = cmbvis.SelectedItem.ToString();
            Properties.Settings.Default.Proxy = txtProxy.Text;
            Properties.Settings.Default.visColor = txtvisColor.BackColor;
            Properties.Settings.Default.visColor2 = txtvisColor2.BackColor;
            Properties.Settings.Default.visColor3 = txtvisColor3.BackColor;
            Properties.Settings.Default.visColorbg = txtvisbg.BackColor;
            Properties.Settings.Default.topMost = visonTop.Checked;
            Properties.Settings.Default.chkFullscreen = chkfull.Checked;
            Properties.Settings.Default.Save();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                txtvisColor.BackColor = cd.Color;
                Properties.Settings.Default.visColor = cd.Color;
            }
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
            Properties.Settings.Default.configvis = "LINE";
            Properties.Settings.Default.chkFullscreen = false;
            Properties.Settings.Default.Save();
            MessageBox.Show(@"Setting Restored", Application.ProductName, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
        }

        private void txtvisColor2_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() != DialogResult.OK) return;
            txtvisColor2.BackColor = cd.Color;
            Properties.Settings.Default.visColor2 = cd.Color;
        }

        private void txtvisColor3_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() != DialogResult.OK) return;
            txtvisColor3.BackColor = cd.Color;
            Properties.Settings.Default.visColor3 = cd.Color;
        }

        private void txtvisbg_MouseClick(object sender, MouseEventArgs e)
        {
            if (cd.ShowDialog() != DialogResult.OK) return;
            txtvisbg.BackColor = cd.Color;
            Properties.Settings.Default.visColorbg = cd.Color;
        }

        private void visonTop_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
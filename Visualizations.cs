using System;
using System.Drawing;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.Misc;

namespace Player
{
    public partial class Visualizations : Form
    {
        private static Visualizations _instance;
        public static Visualizations Instance => _instance ?? (_instance = new Visualizations());

        Visuals _vis = new Visuals();
        int _selectedVis;
        Color _tc, _c2, _c3, _cbg;
        bool _top, _fullScreen;

        string _screenWidth = Screen.PrimaryScreen.Bounds.Width.ToString();
        string _screenHeight = Screen.PrimaryScreen.Bounds.Height.ToString();

        const int VisSpectrum = 0;
        const int VisSpectrumline = 1;
        const int VisSpectrumwave = 2;
        const int VisSpectrumbar = 3;
        const int VisSpectrumtext = 4;
        const int VisSpectrumlinepeak = 5;

        public Visualizations()
        {
            InitializeComponent();
            _top = Properties.Settings.Default.topMost;
            _fullScreen = Properties.Settings.Default.chkFullscreen;

            if (_top)
            {
                Location = new Point(725, 515);
                FormBorderStyle = FormBorderStyle.None;
                TopMost = true;
                menuStrip1.Visible = false;
            }
            if (!_fullScreen) return;
            menuStrip1.Visible = false;
            FormBorderStyle = FormBorderStyle.None;
            Width = Screen.PrimaryScreen.Bounds.Width;
            Height = Screen.PrimaryScreen.Bounds.Height;
        }

        public void Stop()
        {
            timer.Stop();

            lbldBL.Text = @"00.0dB";
            lbldBR.Text = @"00.0dB";

            prgL.Value = 0;
            prgR.Value = 0;

            if (pbVis.Image == null) return;
            Graphics.FromImage(pbVis.Image).Clear(Color.Black);
            pbVis.Refresh();
        }

        // calculates the level of a stereo signal between 0 and 65535
        // where 0 = silent, 32767 = 0dB and 65535 = +6dB
        public void GetLevel(int channel, out int peakL, out int peakR)
        {
            var maxL = 0f;
            var maxR = 0f;

            // length of a 20ms window in bytes
            var length20Ms = (int) Bass.BASS_ChannelSeconds2Bytes(channel, 0.02);
            // the number of 32-bit floats required (since length is in bytes!)
            var l4 = length20Ms / 4; // 32-bit = 4 bytes

            // create a data buffer as needed
            var sampleData = new float[l4];

            var length = Bass.BASS_ChannelGetData(channel, sampleData, length20Ms);

            // the number of 32-bit floats received
            // as less data might be returned by BASS_ChannelGetData as requested
            l4 = length / 4;

            for (var a = 0; a < l4; a++)
            {
                var absLevel = Math.Abs(sampleData[a]);

                // decide on L/R channel
                if (a % 2 == 0)
                {
                    // Left channel
                    if (absLevel > maxL)
                        maxL = absLevel;
                }
                else
                {
                    // Right channel
                    if (absLevel > maxR)
                        maxR = absLevel;
                }
            }

            // limit the maximum peak levels to +6bB = 65535 = 0xFFFF
            // the peak levels will be int values, where 32767 = 0dB
            // and a float value of 1.0 also represents 0db.
            peakL = (int) Math.Round(32767f * maxL) & 0xFFFF;
            peakR = (int) Math.Round(32767f * maxR) & 0xFFFF;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            GetLevel(MainForm.Stream, out var peakL, out var peakR);

            var progL = peakL;
            var progR = peakR;

            // convert the level to dB
            var dBlevelL = Utils.LevelToDB(peakL, 65535);
            var dBlevelR = Utils.LevelToDB(peakR, 65535);

            lbldBL.Text = dBlevelL.ToString("00.#dB");
            lbldBR.Text = dBlevelR.ToString("00.#dB");

            prgL.Value = progL;
            prgR.Value = progR;

            _tc = Properties.Settings.Default.visColor;
            _c2 = Properties.Settings.Default.visColor2;
            _c3 = Properties.Settings.Default.visColor3;
            _cbg = Properties.Settings.Default.visColorbg;

            //changing visualization from settings... ☺
            SettingVis();

            switch (_selectedVis)
            {
                case VisSpectrum:
                    pbVis.Image = _vis.CreateSpectrum(MainForm.Stream, pbVis.Width, pbVis.Height,
                        _tc, _c2, _cbg, false, false, false);
                    break;
                case VisSpectrumwave:
                    pbVis.Image = _vis.CreateSpectrumWave(MainForm.Stream, pbVis.Width, pbVis.Height,
                        _tc, _c2, _cbg, Convert.ToInt32(SettingsForm.Instance.numLine.Value), false, false, false);
                    break;
                case VisSpectrumbar:
                    pbVis.Image = _vis.CreateSpectrumBean(MainForm.Stream, pbVis.Width, pbVis.Height,
                        _tc, _c2, _cbg, Convert.ToInt32(SettingsForm.Instance.numLine.Value), false, false, false);
                    break;
                case VisSpectrumtext:
                    pbVis.Image = _vis.CreateSpectrumText(MainForm.Stream, pbVis.Width, pbVis.Height,
                        _tc, _c2, _cbg, "Created By SajjadJaved", false, false, true);
                    break;
                case VisSpectrumline:
                    pbVis.Image = _vis.CreateSpectrumLine(MainForm.Stream, pbVis.Width, pbVis.Height,
                        _tc, _c2, _cbg, Convert.ToInt32(SettingsForm.Instance.numLine.Value)
                        , Convert.ToInt32(SettingsForm.Instance.numDistance.Value), true, true, true);
                    break;
                case VisSpectrumlinepeak:
                    pbVis.Image = _vis.CreateSpectrumLinePeak(MainForm.Stream, pbVis.Width, pbVis.Height - 4,
                        _tc, _c2, _c3, _cbg, Convert.ToInt32(SettingsForm.Instance.numLine.Value)
                        , 4, Convert.ToInt32(SettingsForm.Instance.numDistance.Value), 20, true, true, true);
                    break;
            }
        }

        private void spectrumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrum);
        }

        private void spectrumLineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrumline);
        }

        private void spectrumWaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrumwave);
        }

        private void spectrumTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrumtext);
        }

        private void sPectrumDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrumbar);
        }

        private void spectrumLinePeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VisSpectrumlinepeak);
        }

        public void SetVisualization(int vis)
        {
            _selectedVis = vis;
            switch (vis)
            {
                case VisSpectrum:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == spectrumToolStripMenuItem;
                    break;

                case VisSpectrumline:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == spectrumLineToolStripMenuItem;
                    break;

                case VisSpectrumwave:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == spectrumWaveToolStripMenuItem;
                    break;
                case VisSpectrumbar:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == sPectrumDotToolStripMenuItem;
                    break;
                case VisSpectrumtext:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == spectrumTextToolStripMenuItem;
                    break;
                case VisSpectrumlinepeak:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        i.Checked = i == spectrumLinePeakToolStripMenuItem;
                    break;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SettingVis()
        {
            var changVis = Properties.Settings.Default.configvis;
            switch (changVis)
            {
                case "BAR":
                    SetVisualization(VisSpectrumbar);
                    break;
                case "SIMPLE":
                    SetVisualization(VisSpectrum);
                    break;
                case "WAVE":
                    SetVisualization(VisSpectrumwave);
                    break;
                case "LINE":
                    SetVisualization(VisSpectrumline);
                    break;
                case "TEXT":
                    SetVisualization(VisSpectrumtext);
                    break;
                case "LINEPEAK":
                    SetVisualization(VisSpectrumlinepeak);
                    break;
            }
        }
    }
}
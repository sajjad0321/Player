using System;
using System.Windows.Forms;
using Un4seen.Bass;
using Un4seen.Bass.Misc;
using System.Drawing;

namespace Player
{
    public partial class Visualizations : Form
    {
        Visuals vis = new Visuals();
        SettingsForm sForm = new SettingsForm();
        int selectedVis = 0;
        Color tc, c2, c3, cbg;
        bool top;

        const int VIS_SPECTRUM = 0;
        const int VIS_SPECTRUMLINE = 1;
        const int VIS_SPECTRUMWAVE = 2;
        const int VIS_SPECTRUMBAR = 3;
        const int VIS_SPECTRUMTEXT = 4;
        const int VIS_SPECTRUMLINEPEAK = 5;
        
        public Visualizations()
        {
            InitializeComponent();
            top = Properties.Settings.Default.topMost;
            if (top == true)
            {
                SetVisualization(VIS_SPECTRUMLINEPEAK);
            }
        }

        public void Stop()
        {
            timer.Stop();

            lbldBL.Text = "00.0dB";
            lbldBR.Text = "00.0dB";

            prgL.Value = 0;
            prgR.Value = 0;

            if (pbVis.Image != null) //form isn't closing
            {
                Graphics.FromImage(pbVis.Image).Clear(Color.Black);
                pbVis.Refresh();
            }
        }

        // calculates the level of a stereo signal between 0 and 65535
        // where 0 = silent, 32767 = 0dB and 65535 = +6dB
        private void GetLevel(int channel, out int peakL, out int peakR)
        {
            float maxL = 0f;
            float maxR = 0f;

            // length of a 20ms window in bytes
            int length20ms = (int)Bass.BASS_ChannelSeconds2Bytes(channel, 0.02);
            // the number of 32-bit floats required (since length is in bytes!)
            int l4 = length20ms / 4; // 32-bit = 4 bytes

            // create a data buffer as needed
            float[] sampleData = new float[l4];

            int length = Bass.BASS_ChannelGetData(channel, sampleData, length20ms);

            // the number of 32-bit floats received
            // as less data might be returned by BASS_ChannelGetData as requested
            l4 = length / 4;

            for (int a = 0; a < l4; a++)
            {
                float absLevel = Math.Abs(sampleData[a]);

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
            peakL = (int)Math.Round(32767f * maxL) & 0xFFFF;
            peakR = (int)Math.Round(32767f * maxR) & 0xFFFF;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int peakL = 0;
            int peakR = 0;
            GetLevel(MainForm.stream, out peakL, out peakR);

            int progL = peakL;
            int progR = peakR;

            // convert the level to dB
            double dBlevelL = Utils.LevelToDB(peakL, 65535);
            double dBlevelR = Utils.LevelToDB(peakR, 65535);

            lbldBL.Text = dBlevelL.ToString("00.#dB");
            lbldBR.Text = dBlevelR.ToString("00.#dB");
            
            prgL.Value = progL;
            prgR.Value = progR;

            tc = Properties.Settings.Default.visColor;
            c2 = Properties.Settings.Default.visColor2;
            c3 = Properties.Settings.Default.visColor3;
            cbg = Properties.Settings.Default.visColorbg;

            switch (selectedVis)
            {
                case VIS_SPECTRUM:
                    pbVis.Image = vis.CreateSpectrum(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, cbg, false, false, false);
                    break;
                case VIS_SPECTRUMLINE:
                    pbVis.Image = vis.CreateSpectrumLine(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, cbg, 5, 3, false, false, false);
                    break;
                case VIS_SPECTRUMWAVE:
                    pbVis.Image = vis.CreateSpectrumWave(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, cbg, 1, false, false, false);
                    break;
                case VIS_SPECTRUMBAR:
                    pbVis.Image = vis.CreateSpectrumBean(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, cbg, 1, false, false, false);
                    break;
                case VIS_SPECTRUMTEXT:
                    pbVis.Image = vis.CreateSpectrumText(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, cbg, "Created By SajjadJaved", false, false, true);
                    break;
                case VIS_SPECTRUMLINEPEAK:
                    pbVis.Image = vis.CreateSpectrumLinePeak(MainForm.stream, pbVis.Width, pbVis.Height,
                        tc, c2, c3, cbg, 6, 4, 3, 20,true,false,true);
                    break;
            }
        }

        private void spectrumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUM);
        }

        private void spectrumLineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMLINE);
        }

        private void spectrumWaveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMWAVE);
        }

        private void spectrumTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMTEXT);
        }

        private void sPectrumDotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMBAR);
        }

        private void spectrumLinePeakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetVisualization(VIS_SPECTRUMLINEPEAK);
        }
        public void SetVisualization(int vis)
        {
            selectedVis = vis;
            switch (vis)
            {
                case VIS_SPECTRUM:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;

                case VIS_SPECTRUMLINE:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumLineToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;

                case VIS_SPECTRUMWAVE:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumWaveToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;
                case VIS_SPECTRUMBAR:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != sPectrumDotToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;
                case VIS_SPECTRUMTEXT:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumTextToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;
                case VIS_SPECTRUMLINEPEAK:
                    foreach (ToolStripMenuItem i in visualizationToolStripMenuItem.DropDownItems)
                        if (i != spectrumLinePeakToolStripMenuItem) i.Checked = false;
                        else i.Checked = true;
                    break;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

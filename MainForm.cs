using CSCore.Codecs.WAV;
using CSCore.SoundIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Un4seen.Bass;
using WasapiLoopbackCapture = CSCore.SoundIn.WasapiLoopbackCapture;

namespace Player
{
    public partial class MainForm : Form
    {
        public static int Stream = 0;

        private bool _stopped = true;
        private bool _paused;
        private bool _scrubbing;
        private int _activeTrack = 0;

        public static readonly List<string> SupportedExts = new List<string>();

        private readonly List<PlaylistItem> _list = new List<PlaylistItem>();

        private Visualizations _visForm = new Visualizations();

        private readonly string[] _args = Environment.GetCommandLineArgs();

        private IntPtr _userAgentPtr;
        private IntPtr _proxyPtr;
        private WasapiLoopbackCapture _capture;
        private WaveWriter _w;
        private readonly KeyboardHook _keyHook = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();

            BuildFilter();
            BuildUserAgent();
            ProcessCmdLine();

            Bass.BASS_SetConfig(BASSConfig.BASS_CONFIG_NET_PLAYLIST, 1);
            _keyHook.KeyPressed += KeyHook_KeyPressed;
            _keyHook.RegisterHotKey(Player.ModifierKeys.Alt, Keys.P);
            
        }

        private void KeyHook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (_stopped)
                Play();
            else if (_paused)
                Resume();
            else if (!_stopped && !_paused)
                Pause();
        }

        private void BuildFilter() //build the filter string for openFileDialog
        {
            SupportedExts.AddRange(Bass.SupportedStreamExtensions.Split(';'));
            SupportedExts.AddRange(Bass.SupportedMusicExtensions.Split(';'));
            
            //foreach (KeyValuePair<int, string> item in Bass.BASS_PluginLoadDirectory("plugins"))
            //{
            //    BASS_PLUGINFORM[] pforms = Bass.BASS_PluginGetInfo(item.Key).formats;
            //    foreach (BASS_PLUGINFORM pf in pforms)
            //    {
            //        foreach (string ext in pf.exts.Split(';'))
            //            if (!supportedExts.Contains(ext)) supportedExts.Add(ext);
            //    }
            //}
            
            var filter = "All files (*.*)|*.*|";
            for (var i = 0; i < SupportedExts.Count; i++)
                filter += SupportedExts[i].Replace("*.", "") + " files (" + SupportedExts[i] + ")|" + SupportedExts[i] + ((i == SupportedExts.Count - 1) ? "" : "|");

            openFileDialog.Filter = filter;
        }

        private void BuildUserAgent() //build the user agent string for playing urls
        {
            var agent = "";
            var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (attributes.Length > 0)
            {
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                if (titleAttribute.Title != "")
                    agent += titleAttribute.Title + " ";
            }

            agent += Assembly.GetExecutingAssembly().GetName().Version + " ";

            attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length > 0)
            {
                agent += ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }

            agent = agent.Replace("©", "");
            _userAgentPtr = Marshal.StringToHGlobalAnsi(agent); //can't use StringToHGlobalUni for user agent :(

            Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_AGENT, _userAgentPtr);
        }

        private void ProcessCmdLine()
        {
            if (_args.Length < 2) return;

            if (new System.IO.FileInfo(_args[1]).Extension == ".xspf")
            {
                var loaded = XSPF.Load(_args[1]);

                if (loaded.Count != 0)
                {
                    foreach (var p in loaded)
                        AddToPlaylist(p);
                }
            }
            else
            {
                for (var i = 1; i < _args.Length; i++) //skip the first argument, it's just the exe
                    AddToPlaylist(XSPF.GetTags(_args[i]));
            }
            
            Play();
        }

        private void Play()
        {
            if (_list.Count == 0) return;

            if (_activeTrack > _list.Count - 1)
                _activeTrack = _list.Count - 1;
            switch (_list[_activeTrack].Type)
            {
                case PlaylistItem.TYPE_MUSIC:
                    Stream = Bass.BASS_MusicLoad(_list[_activeTrack].Path, 0, 0, BASSFlag.BASS_MUSIC_FLOAT | BASSFlag.BASS_MUSIC_PRESCAN, 0);
                    break;
                case PlaylistItem.TYPE_STREAM_URL:
                    Stream = Bass.BASS_StreamCreateURL(_list[_activeTrack].Path, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_STATUS, null, IntPtr.Zero);
                    //wmp.URL = "http://portal.onlineiptv.net:5210/live/fNOaPbcqCB/yttnwNGpCR/1599.ts"; 
                    break;
                default:
                    Stream = Bass.BASS_StreamCreateFile(_list[_activeTrack].Path, 0, 0, BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_PRESCAN 
                        | BASSFlag.BASS_AAC_STEREO);
                    break;
            }

            if (Stream != 0)
            {
                Bass.BASS_ChannelPlay(Stream, true);

                _stopped = false;
                _paused = false;

                if (_list[_activeTrack].Type == PlaylistItem.TYPE_STREAM_URL)
                {
                    trkPos.Enabled = false;
                    chkRepeatTrack.Enabled = chkRepeatAll.Enabled = chkRandom.Enabled = false;
                    chkRepeatTrack.Checked = chkRepeatAll.Checked = chkRandom.Checked = false;
                }
                else
                {
                    trkPos.Enabled = true;
                    chkRepeatTrack.Enabled = chkRepeatAll.Enabled = chkRandom.Enabled = true;
                    trkPos.Maximum = (int)Bass.BASS_ChannelBytes2Seconds(Stream, Bass.BASS_ChannelGetLength(Stream));
                }

                timer.Start();
                if (_list[_activeTrack].Type == PlaylistItem.TYPE_STREAM_URL) tagTimer.Start();
                btnStop.Enabled = true;

                lblTag.Text = _list[_activeTrack].Artist.Replace("&", "&&") + " - " + _list[_activeTrack].Title.Replace("&", "&&"); //escape ampersands (no keyboard shortcuts)
                if (_list[_activeTrack].Artist != null)
                {
                    Text = $@"Player |  {_list[_activeTrack].Artist} - + {_list[_activeTrack].Title}";
                }

                if (_visForm.Visible && !_visForm.IsDisposed)
                    _visForm.timer.Start();

                btnPlay.Image = Properties.Resources.control_pause_blue;
            } 
            else
            {
                MessageBox.Show($@"Stream Error: {Bass.BASS_ErrorGetCode()}", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pause()
        {
            Bass.BASS_ChannelPause(Stream);
            if (tagTimer.Enabled) tagTimer.Stop();
            timer.Stop();
            if (_visForm.Visible && !_visForm.IsDisposed)
                _visForm.Stop();
            _paused = true;
            btnPlay.Image = Properties.Resources.control_play_blue;
        }

        private void Resume()
        {
            Bass.BASS_ChannelPlay(Stream, false);
            timer.Start();
            if (_list[_activeTrack].Type == PlaylistItem.TYPE_STREAM_URL) tagTimer.Start();
            if (_visForm.Visible && !_visForm.IsDisposed)
                _visForm.timer.Start();
            _paused = false;
            btnPlay.Image = Properties.Resources.control_pause_blue;
        }

        private void Stop()
        {
            _stopped = true;

            Bass.BASS_ChannelStop(Stream);

            if (tagTimer.Enabled) tagTimer.Stop();
            timer.Stop();
            btnStop.Enabled = false;
            trkPos.Enabled = false;
            trkPos.Value = 0;

            lblTag.Text = @"Idle";
            Text = @"Player";
            lblPos.Text = "-/-";

            if (_visForm.Visible && !_visForm.IsDisposed)
                _visForm.Stop();

            btnPlay.Image = Properties.Resources.control_play_blue;
        }

        private void GetTags()
        {
            var item = XSPF.GetTags(_list[_activeTrack].Path);

            _list[_activeTrack].ListViewItem.SubItems[1].Text = item.Title;
            _list[_activeTrack].ListViewItem.SubItems[2].Text = item.Artist;
            _list[_activeTrack].ListViewItem.SubItems[3].Text = item.Album;

            lblTag.Text = item.Artist.Replace("&", "&&") + " - " + item.Title.Replace("&", "&&"); //escape ampersands (no keyboard shortcuts)
            if (item.Artist != null || item.Album != null)
            {
                Text = $@"Player | {item.Artist} - {item.Title}";
            }
            else
            {
                Text = @"Player ";
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //StartRecording("google.wav");
            if (_stopped)
                Play();
            else if (_paused)
                Resume();
            else if (!_stopped && !_paused)
                Pause();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_stopped) return;

            Stop();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (_stopped || _list.Count == 0) return;
            
            Stop();

            if (_activeTrack == 0)
                _activeTrack = _list.Count - 1;
            else
                _activeTrack--;

            Play();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_stopped || _list.Count == 0) return;
            
            Stop();

            if (_activeTrack == _list.Count - 1)
                _activeTrack = 0;
            else
                _activeTrack++;

            Play();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            Marshal.FreeHGlobal(_userAgentPtr);
            Marshal.FreeHGlobal(_proxyPtr);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_activeTrack > _list.Count - 1)
                _activeTrack = _list.Count - 1;
            
            if (Bass.BASS_ChannelIsActive(Stream) == BASSActive.BASS_ACTIVE_STOPPED && _list[_activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
                ShouldPlayNext();
            
            string length = null;
            double lSecsD = -1;

            if (_list[_activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
            {
                lSecsD = Bass.BASS_ChannelBytes2Seconds(Stream, Bass.BASS_ChannelGetLength(Stream));

                var lHrs = (int)Math.Floor(lSecsD / 3600);
                var lMins = (int)Math.Floor(lSecsD % 3600 / 60);
                var lSecs = (int)lSecsD % 60;

                length = $"{(lHrs == 0 ? "" : lHrs + ":")}{lMins:00}:{lSecs:00}";
            }
            
            var pSecsD = Bass.BASS_ChannelBytes2Seconds(Stream, Bass.BASS_ChannelGetPosition(Stream));

            if (!_scrubbing && _list[_activeTrack].Type != PlaylistItem.TYPE_STREAM_URL)
            {
                if (_list[_activeTrack].Type == PlaylistItem.TYPE_MUSIC && pSecsD >= lSecsD)
                    ShouldPlayNext();
                else
                    trkPos.Value = (int)pSecsD;
            }

            var pHrs = (int)Math.Floor(pSecsD / 3600);
            var pMins = (int)Math.Floor(pSecsD % 3600 / 60);
            var pSecs = (int)pSecsD % 60;

            var pos = $"{(pHrs == 0 ? "" : pHrs + ":")}{pMins:00}:{pSecs:00}";

            lblPos.Text = _list[_activeTrack].Type == PlaylistItem.TYPE_STREAM_URL ? pos : $@"{pos}/{length}";
        }

        private void ShouldPlayNext()
        {
            if (chkRepeatTrack.Checked)
            {
                Bass.BASS_ChannelPlay(Stream, true);
            }
            else if (chkRandom.Checked)
            {
                Stop();
                _activeTrack = new Random().Next(_list.Count - 1);
                Play();
            }
            else if (_activeTrack == _list.Count - 1)
            {
                if (chkRepeatAll.Checked)
                {
                    Stop();
                    _activeTrack = 0;
                    Play();
                }
                else
                    Stop();
            }
            else
            {
                Stop();
                _activeTrack++;
                Play();
            }
        }

        private void tagTimer_Tick(object sender, EventArgs e)
        {
            GetTags();
        }

        private void trkVol_Scroll(object sender, EventArgs e)
        {
            if (_list.Count == 0) return;
            Bass.BASS_SetConfig(
                _list[_activeTrack].Type == PlaylistItem.TYPE_MUSIC
                    ? BASSConfig.BASS_CONFIG_GVOL_MUSIC
                    : BASSConfig.BASS_CONFIG_GVOL_STREAM, trkVol.Value);
        }

        private void trkPos_MouseDown(object sender, MouseEventArgs e)
        {
            _scrubbing = true;
        }

        private void trkPos_MouseUp(object sender, MouseEventArgs e)
        {
            _scrubbing = false;
        }

        private void trkPos_Scroll(object sender, EventArgs e)
        {
            if (!_scrubbing) return;
            Bass.BASS_ChannelSetPosition(Stream, (double)trkPos.Value);
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            foreach (var f in openFileDialog.FileNames)
                AddToPlaylist(XSPF.GetTags(f));

            if (_stopped)
                Play();
        }

        private void addUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AddUrlForm();
            
            if (form.ShowDialog() != DialogResult.OK) return;

            if (!_stopped) Stop();

            AddToPlaylist(XSPF.GetTags(form.path));

            if (_stopped) Play();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Player plays audio files using bass.net lib. \nWritten in VB.Net. Created By SajjadJaved.\nfor Updates: https://github.com/sajjad0321/Player", Application.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void toggleVisualsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_visForm.Visible)
            {
                _visForm.Stop();
                _visForm.Close();
            }
            else if (_visForm.IsDisposed)
            {
                _visForm = new Visualizations();
                _visForm.Show();
                _visForm.timer.Start();
            }
            else
            {
                _visForm.Show();
                _visForm.timer.Start();
            }
        }

        private void openPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPlaylistDialog.ShowDialog() != DialogResult.OK)
                return;

            var loaded = XSPF.Load(openPlaylistDialog.FileName);

            if (loaded.Count == 0) return;
            
            _list.Clear();
            lstPlaylist.Items.Clear();
            
            foreach (var p in loaded)
                AddToPlaylist(p);
        }

        private void savePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_list.Count == 0 || savePlaylistDialog.ShowDialog() != DialogResult.OK)
                return;

            XSPF.Save(savePlaylistDialog.FileName, _list);
        }

        private void AddToPlaylist(PlaylistItem p)
        {
            if (p == null)
                return; //unsupported file type
            
            p.ListViewItem = new ListViewItem(p.Track);

            p.ListViewItem.SubItems.Add(p.Title);
            p.ListViewItem.SubItems.Add(p.Artist);
            p.ListViewItem.SubItems.Add(p.Album);
            p.ListViewItem.SubItems.Add(new Uri(p.Path).LocalPath);

            lstPlaylist.Items.Add(p.ListViewItem);

            _list.Add(p);
        }

        private void RemoveFromPlaylist(PlaylistItem p)
        {
            lstPlaylist.Items.Remove(p.ListViewItem);
            _list.Remove(p);
            if (_activeTrack > _list.Count - 1)
                _activeTrack = _list.Count - 1;
        }

        private PlaylistItem GetPlaylistItem(ListViewItem i)
        {
            return _list.FirstOrDefault(p => p.ListViewItem == i);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count == 0) return;

            if (lstPlaylist.SelectedItems.Contains(_list[_activeTrack].ListViewItem))
                Stop();

            foreach (ListViewItem i in lstPlaylist.SelectedItems)
            {
                var item = GetPlaylistItem(i);
                if (item != null)
                    RemoveFromPlaylist(item);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_stopped)
                Stop();
            
            lstPlaylist.Items.Clear();
            _list.Clear();

            _activeTrack = 0;
        }

        private void lstPlaylist_DoubleClick(object sender, EventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count != 1) return;

            if (!_stopped)
                Stop();
            _activeTrack = lstPlaylist.SelectedIndices[0];
            Play();
        }

        private void lstPlaylist_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void lstPlaylist_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            foreach (var file in files)
                AddToPlaylist(XSPF.GetTags(file));
        }

        private void lstPlaylist_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void lstPlaylist_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstPlaylist.SelectedItems.Count == 0)
                return;

            lstPlaylist.DoDragDrop(lstPlaylist.SelectedItems[0], DragDropEffects.Move);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var prevDevice = Properties.Settings.Default.Device;
            var ss = SettingsForm.Instance.IsDisposed;
            if (ss)
            {
                new SettingsForm().Show();
            }
            else
            {
                SettingsForm.Instance.Show();
            }
//            if (new SettingsForm().ShowDialog() != DialogResult.OK)
//                return;

            _proxyPtr = Marshal.StringToHGlobalAnsi(Properties.Settings.Default.Proxy);

            Bass.BASS_SetConfigPtr(BASSConfig.BASS_CONFIG_NET_PROXY, _proxyPtr);

            if (prevDevice == Properties.Settings.Default.Device) return;
            Stop();
            Program.init(); //reinitialize BASS on the new device
            Play();
        }

        private void lstPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void lstPlaylist_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            notifyIcon1.Visible = true;
            Hide();
        }

        private void CaptureAudio(string name = "test.wav")
        {
            _capture = new WasapiLoopbackCapture();
            _capture.Initialize();
            _w = new WaveWriter(name, _capture.WaveFormat);
            _capture.DataAvailable += CaptureOnDataAvailable;
            _capture.Start();
        }

        private void CaptureOnDataAvailable(object sender, DataAvailableEventArgs dataAvailableEventArgs)
        {
            _w.Write(dataAvailableEventArgs.Data, dataAvailableEventArgs.Offset, dataAvailableEventArgs.ByteCount);
        }

        private static void CaptureG(int index)
        {
            var name = $"dump-{index}.wav";

            //using (WasapiCapture capture = new WasapiLoopbackCapture())
            //{
            //    capture.Initialize();
            //    using (var w = new WaveWriter(Name, capture.WaveFormat))
            //    {
            //        capture.DataAvailable += (s, capData) => w.Write(capData.Data, capData.Offset, capData.ByteCount);
            //        capture.Start();

            //        Thread.Sleep(10000);

            //        capture.Stop();
            //    }
            //}
        }

        public void StartRecording(string name)
        {
            new Thread(delegate () { CaptureAudio(name); }).Start();
        }

        public void StopCapture()
        {
            //capture.Stop();
            //capture.Dispose();
            //w.Dispose();
        }

        private void loopbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loopbackToolStripMenuItem.Checked)
            {
                CaptureAudio();
            }
            else
            {
                _capture.Stop();
            }
//            var wavein = new WasapiCapture();
//            if (loopbackToolStripMenuItem.Checked == true)
//            {
//                var provider = new BufferedWaveProvider(wavein.WaveFormat);
//                var vpro = new VolumeSampleProvider(provider.ToSampleProvider());
//                var wout = new WasapiOut(AudioClientShareMode.Shared, 0);
//                var filter = BiQuadFilter.HighPassFilter(44000, 200, 1);
//
//                wout.Init(vpro);
//                wout.Play();
//                wavein.StartRecording();
//
//                wavein.DataAvailable += delegate (object send, WaveInEventArgs ee)
//                {
//                    for (var i = 0; i < ee.BytesRecorded; i += 4)
//                    {
//                        var trans = BitConverter.GetBytes(filter.Transform(BitConverter.ToSingle(ee.Buffer, i)));
//                        Buffer.BlockCopy(trans, 0, ee.Buffer, i, 4);
//                    }
//                    provider.AddSamples(ee.Buffer, 0, ee.BytesRecorded);
//
//                    //    //vpro.Volume = .8f * ReverbIntensity;
//                    };
//                } else
//            {
//                wavein.StopRecording();
//            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;
            Show();
        }
    }
}

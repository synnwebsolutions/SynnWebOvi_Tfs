using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicHelper
{
    public partial class SynnMPlayer : UserControl
    {
       SynnMusicPlayer player;
        Font font;
        Color color;
        bool scroling;
        int minut;
        int second;
        int tried;

        public List<MusicItem> lstPlaylist { get; set; }

        public void InitList(List<MusicItem> l)
        {
            lstPlaylist = l;
            player = new SynnMusicPlayer(lstPlaylist.Select(x => x.FullFileName).ToList());
        }

        public SynnMPlayer()
        {
            InitializeComponent();
            InitList(new List<MusicItem>());
        }

        private void trbVolume_Scroll(object sender, EventArgs e)
        {
            player.SetVolume(trbVolume.Value);
        }

        private void chkMute_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMute.Checked)
            {
                trbVolume.Enabled = false;
                trbBalance.Enabled = false;
                player.SetVolume(0);
            }
            else
            {
                trbVolume.Enabled = true;
                trbBalance.Enabled = true;
                player.SetVolume(trbVolume.Value);
            }
        }

        private void trbBalance_Scroll(object sender, EventArgs e)
        {
            player.SetBalance(trbBalance.Value);
        }

        private void trackBar_MouseUp(object sender, MouseEventArgs e)
        {
            scroling = false;
            player.SetPosition(trackBar.Value);
        }

        private void trackBar_MouseDown(object sender, MouseEventArgs e)
        {
            scroling = true;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            tried = 0;
            if (player.Paused)
            {
                player.Pause();
                SetEnviroment();
            }
            else
            {
                //for (int i = 0; i < lstPlaylist.Count; i++)
                //{
                //    if (lstPlaylist.Items[i].Selected == true)
                //    {
                //        play(i);
                //        break;
                //    }
                //}
                if (lstPlaylist.Count != 0 && !player.IsPlaying())
                {
                    play(0);
                }
            }
        }

        private bool play(int track)
        {
            player.Stop();
            player.Paused = false;
            if (player.Play(track))
            {
                tried = 0;
                trackBar.Maximum = player.GetSongLenght();
                timer.Enabled = true;
                //lstPlaylist.Items[player.NowPlaying].Selected = true;
                SetEnviroment();
                return true;
            }
            else
            {
                tried++;
                return false;
            }
        }

        public void SetEnviroment()
        {
            if (player.IsPlaying())
            {
                //Text = lstPlaylist.Items[player.NowPlaying].SubItems[2].Text + " - " + lstPlaylist.Items[player.NowPlaying].SubItems[4].Text;
                //lblPlaying.Text = lstPlaylist.Items[player.NowPlaying].SubItems[6].Text + " | " + GetTimeMinutsAndSeconds(player.GetSongLenght()) + " | " + lstPlaylist.Items[player.NowPlaying].SubItems[0].Text;
            }
            else
            {
                //lblPlaying.Text = "";
                //Text = "MCIplayer";
            }

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            player.Pause();
            //if (player.Paused)
            //{
            //    lblPlaying.Text = "Paused";
            //    Text = "MCIplayer";
            //}
            //else
                SetEnviroment();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            player.Stop();
            tried = 0;
            timer.Enabled = false;
            txtTime.Text = "00:00 / 00:00";
            trackBar.Value = 0;
            SetEnviroment();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int track;
            player.Stop();
            if (lstPlaylist.Count > 0)
            {
                // Not the best solution for this problem, guess i have to change 
                // the whole GetSong method to make it work 
                track = player.GetSong(true);
                while (play(track) != true && tried < lstPlaylist.Count)
                {
                    if (player.Shuffle)
                        track = player.GetSong(true);
                    else
                    {
                        if (track != 0)
                            play(track--);
                        else
                        {
                            track = lstPlaylist.Count - 1;
                            play(track);
                        }
                    }

                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int track;
            player.Stop();
            if (lstPlaylist.Count > 0)
            {
                // Not the best solution for this problem, guess i have to change 
                // the whole GetSong method to make it work 
                // Here the same problem, I could make a method for this part 
                // because I'm using the same code, but this problem must be solved 
                // somehow different
                track = player.GetSong(false);
                while (play(track) != true && tried < lstPlaylist.Count)
                {
                    if (player.Shuffle)
                        track = player.GetSong(true);
                    else
                    {
                        if (track != lstPlaylist.Count - 1)
                            play(track++);
                        else
                        {
                            track = 0;
                            play(track);
                        }
                    }
                }
            }
        }

        private void SynnMPlayer_Load(object sender, EventArgs e)
        {
            player = new SynnMusicPlayer(new List<string> { });
            scroling = false;
            minut = 0;
            second = 0;
            timer.Enabled = false;
            tried = 0;

            //if (player.Loop == true)
            //    btnLoop.Image = Properties.Resources.selected;
            //if (player.Shuffle == true)
            //    btnShuffle.Image = Properties.Resources.selected;

            CheckColumnSize();
        }

        internal void InitListAndPlay(List<MusicItem> list)
        {
            InitList(list);
            play(0);
        }

        public void CheckColumnSize()
        {
            //if (lstPlaylist.Columns[0].Width > 0)
            //    btnFileName.Image = Properties.Resources.selected;
            //else
            //    btnFileName.Image = null;

            //if (lstPlaylist.Columns[1].Width > 0)
            //    btnPath.Image = Properties.Resources.selected;
            //else
            //    btnPath.Image = null;

            //if (lstPlaylist.Columns[2].Width > 0)
            //    btnArtist.Image = Properties.Resources.selected;
            //else
            //    btnArtist.Image = null;

            //if (lstPlaylist.Columns[3].Width > 0)
            //    btnTitle.Image = Properties.Resources.selected;
            //else
            //    btnTitle.Image = null;

            //if (lstPlaylist.Columns[4].Width > 0)
            //    btnAlbum.Image = Properties.Resources.selected;
            //else
            //    btnAlbum.Image = null;

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!player.IsStopped() && player.IsPlaying())
            {
                if (player.IsPlaying() || player.Paused)
                {
                    try
                    {
                        txtTime.Text = GetTimeMinutsAndSeconds(player.GetCurentMilisecond()) + " / " + GetTimeMinutsAndSeconds(player.GetSongLenght());
                        if (scroling != true)
                            trackBar.Value = player.GetCurentMilisecond();
                    }
                    catch (Exception ex)
                    {
                        player.Stop();
                    }
                }
                else if (!player.Paused)
                {
                    txtTime.Text = "00:00 / 00:00";
                    trackBar.Value = 0;
                }
            }
            else if (!player.Paused)
                btnNext_Click(sender, e);
        }
        public string GetTimeMinutsAndSeconds(int miliseconds)
        {
            second = miliseconds / 1000;
            minut = second / 60;
            return String.Format("{0:00}", (float)minut) + ":" + String.Format("{0:00}", (float)(second % 60));
        }

    }
}

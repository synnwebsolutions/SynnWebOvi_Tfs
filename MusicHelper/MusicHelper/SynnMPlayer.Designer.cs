namespace MusicHelper
{
    partial class SynnMPlayer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trbBalance = new System.Windows.Forms.TrackBar();
            this.chkMute = new System.Windows.Forms.CheckBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.trbVolume = new System.Windows.Forms.TrackBar();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.trackBar = new System.Windows.Forms.TrackBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.chkShuffle = new System.Windows.Forms.CheckBox();
            this.txCurrentPlaying = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // trbBalance
            // 
            this.trbBalance.AutoSize = false;
            this.trbBalance.Location = new System.Drawing.Point(571, 27);
            this.trbBalance.Maximum = 1000;
            this.trbBalance.Name = "trbBalance";
            this.trbBalance.Size = new System.Drawing.Size(104, 30);
            this.trbBalance.TabIndex = 42;
            this.trbBalance.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbBalance.Value = 500;
            this.trbBalance.Scroll += new System.EventHandler(this.trbBalance_Scroll);
            // 
            // chkMute
            // 
            this.chkMute.AutoSize = true;
            this.chkMute.Location = new System.Drawing.Point(391, 3);
            this.chkMute.Name = "chkMute";
            this.chkMute.Size = new System.Drawing.Size(50, 17);
            this.chkMute.TabIndex = 43;
            this.chkMute.Text = "Mute";
            this.chkMute.UseVisualStyleBackColor = true;
            this.chkMute.CheckedChanged += new System.EventHandler(this.chkMute_CheckedChanged);
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(516, 34);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(43, 13);
            this.lblBalance.TabIndex = 40;
            this.lblBalance.Text = "Tempo:";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(520, 10);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(45, 13);
            this.lblVolume.TabIndex = 41;
            this.lblVolume.Text = "Volume:";
            // 
            // trbVolume
            // 
            this.trbVolume.AutoSize = false;
            this.trbVolume.Location = new System.Drawing.Point(571, 3);
            this.trbVolume.Maximum = 1000;
            this.trbVolume.Name = "trbVolume";
            this.trbVolume.Size = new System.Drawing.Size(104, 25);
            this.trbVolume.TabIndex = 39;
            this.trbVolume.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbVolume.Value = 800;
            this.trbVolume.Scroll += new System.EventHandler(this.trbVolume_Scroll);
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTime.Location = new System.Drawing.Point(232, 3);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(98, 26);
            this.txtTime.TabIndex = 38;
            this.txtTime.Text = "00:00 / 00:00";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // trackBar
            // 
            this.trackBar.AutoSize = false;
            this.trackBar.LargeChange = 3;
            this.trackBar.Location = new System.Drawing.Point(12, 34);
            this.trackBar.Name = "trackBar";
            this.trackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar.Size = new System.Drawing.Size(318, 22);
            this.trackBar.TabIndex = 37;
            this.trackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseDown);
            this.trackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_MouseUp);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = global::MusicHelper.Properties.Resources.play_on;
            this.btnPlay.Location = new System.Drawing.Point(24, 3);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(25, 25);
            this.btnPlay.TabIndex = 32;
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::MusicHelper.Properties.Resources.pause_on;
            this.btnPause.Location = new System.Drawing.Point(55, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(25, 25);
            this.btnPause.TabIndex = 33;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::MusicHelper.Properties.Resources.stop_on;
            this.btnStop.Location = new System.Drawing.Point(86, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(25, 25);
            this.btnStop.TabIndex = 34;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::MusicHelper.Properties.Resources.next_on;
            this.btnNext.Location = new System.Drawing.Point(148, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 25);
            this.btnNext.TabIndex = 36;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = global::MusicHelper.Properties.Resources.previus_on;
            this.btnPrevious.Location = new System.Drawing.Point(117, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 25);
            this.btnPrevious.TabIndex = 35;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // chkShuffle
            // 
            this.chkShuffle.AutoSize = true;
            this.chkShuffle.Location = new System.Drawing.Point(391, 26);
            this.chkShuffle.Name = "chkShuffle";
            this.chkShuffle.Size = new System.Drawing.Size(59, 17);
            this.chkShuffle.TabIndex = 44;
            this.chkShuffle.Text = "Shuffle";
            this.chkShuffle.UseVisualStyleBackColor = true;
            this.chkShuffle.CheckedChanged += new System.EventHandler(this.chkShuffle_CheckedChanged);
            // 
            // txCurrentPlaying
            // 
            this.txCurrentPlaying.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txCurrentPlaying.Enabled = false;
            this.txCurrentPlaying.ForeColor = System.Drawing.Color.Red;
            this.txCurrentPlaying.Location = new System.Drawing.Point(24, 63);
            this.txCurrentPlaying.Name = "txCurrentPlaying";
            this.txCurrentPlaying.Size = new System.Drawing.Size(651, 20);
            this.txCurrentPlaying.TabIndex = 45;
            this.txCurrentPlaying.Text = "Current Playing Track";
            this.txCurrentPlaying.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // SynnMPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txCurrentPlaying);
            this.Controls.Add(this.chkShuffle);
            this.Controls.Add(this.trbBalance);
            this.Controls.Add(this.chkMute);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.trbVolume);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.trackBar);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.Name = "SynnMPlayer";
            this.Size = new System.Drawing.Size(697, 90);
            this.Load += new System.EventHandler(this.SynnMPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trbBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trbBalance;
        private System.Windows.Forms.CheckBox chkMute;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trbVolume;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.TrackBar trackBar;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.CheckBox chkShuffle;
        private System.Windows.Forms.TextBox txCurrentPlaying;
    }
}

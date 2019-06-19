namespace MusicHelper
{
    partial class XMusicPlayerUi
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
            this.lblBalance = new System.Windows.Forms.Label();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnShufflle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trbBalance)).BeginInit();
            this.SuspendLayout();
            // 
            // trbBalance
            // 
            this.trbBalance.AutoSize = false;
            this.trbBalance.Location = new System.Drawing.Point(322, 42);
            this.trbBalance.Maximum = 500;
            this.trbBalance.Minimum = -500;
            this.trbBalance.Name = "trbBalance";
            this.trbBalance.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbBalance.Size = new System.Drawing.Size(30, 148);
            this.trbBalance.TabIndex = 56;
            this.trbBalance.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trbBalance.Scroll += new System.EventHandler(this.trbBalance_Scroll);
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(319, 10);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(43, 13);
            this.lblBalance.TabIndex = 54;
            this.lblBalance.Text = "Tempo:";
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtTime.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTime.ForeColor = System.Drawing.SystemColors.Window;
            this.txtTime.Location = new System.Drawing.Point(6, 3);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(101, 26);
            this.txtTime.TabIndex = 52;
            this.txtTime.Text = "00:00 / 00:00";
            this.txtTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 200;
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImage = global::MusicHelper.Properties.Resources.play_on;
            this.btnPlay.Location = new System.Drawing.Point(-25, 66);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(25, 25);
            this.btnPlay.TabIndex = 46;
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnPause
            // 
            this.btnPause.BackgroundImage = global::MusicHelper.Properties.Resources.pause_on;
            this.btnPause.Location = new System.Drawing.Point(114, 3);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(25, 25);
            this.btnPause.TabIndex = 47;
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackgroundImage = global::MusicHelper.Properties.Resources.stop_on;
            this.btnStop.Location = new System.Drawing.Point(145, 3);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(25, 25);
            this.btnStop.TabIndex = 48;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::MusicHelper.Properties.Resources.next_on;
            this.btnNext.Location = new System.Drawing.Point(207, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(25, 25);
            this.btnNext.TabIndex = 50;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = global::MusicHelper.Properties.Resources.previus_on;
            this.btnPrevious.Location = new System.Drawing.Point(176, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(25, 25);
            this.btnPrevious.TabIndex = 49;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 56);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(226, 134);
            this.listBox1.TabIndex = 57;
            this.listBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBox1_MouseDoubleClick);
            // 
            // btnShufflle
            // 
            this.btnShufflle.ForeColor = System.Drawing.Color.IndianRed;
            this.btnShufflle.Location = new System.Drawing.Point(247, 4);
            this.btnShufflle.Name = "btnShufflle";
            this.btnShufflle.Size = new System.Drawing.Size(45, 25);
            this.btnShufflle.TabIndex = 58;
            this.btnShufflle.Text = "Shuff";
            this.btnShufflle.UseVisualStyleBackColor = true;
            this.btnShufflle.Click += new System.EventHandler(this.btnShufflle_Click);
            // 
            // XMusicPlayerUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.btnShufflle);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.trbBalance);
            this.Controls.Add(this.lblBalance);
            this.Controls.Add(this.txtTime);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrevious);
            this.ForeColor = System.Drawing.Color.LightSlateGray;
            this.Name = "XMusicPlayerUi";
            this.Size = new System.Drawing.Size(381, 212);
            ((System.ComponentModel.ISupportInitialize)(this.trbBalance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trbBalance;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnShufflle;
    }
}

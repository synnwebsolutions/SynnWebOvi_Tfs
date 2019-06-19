namespace XTempoPlayer
{
    partial class Form1
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.dgvPlaylist = new System.Windows.Forms.DataGridView();
            this.tempoTrackBar = new System.Windows.Forms.TrackBar();
            this.btnSycDataBase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempoTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(467, 423);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(181, 43);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(47, 30);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(45, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(509, 30);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(45, 23);
            this.btnRecord.TabIndex = 2;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.Location = new System.Drawing.Point(458, 30);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(45, 23);
            this.btnShuffle.TabIndex = 3;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(231, 30);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(45, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(149, 18);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(76, 47);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(98, 30);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(45, 23);
            this.btnPrev.TabIndex = 6;
            this.btnPrev.Text = "Previous";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // dgvPlaylist
            // 
            this.dgvPlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlaylist.Location = new System.Drawing.Point(47, 71);
            this.dgvPlaylist.Name = "dgvPlaylist";
            this.dgvPlaylist.Size = new System.Drawing.Size(559, 330);
            this.dgvPlaylist.TabIndex = 7;
            // 
            // tempoTrackBar
            // 
            this.tempoTrackBar.Location = new System.Drawing.Point(623, 71);
            this.tempoTrackBar.Maximum = 20;
            this.tempoTrackBar.Name = "tempoTrackBar";
            this.tempoTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tempoTrackBar.Size = new System.Drawing.Size(45, 157);
            this.tempoTrackBar.TabIndex = 8;
            this.tempoTrackBar.Value = 10;
            this.tempoTrackBar.Scroll += new System.EventHandler(this.tempoTrackBar_Scroll);
            // 
            // btnSycDataBase
            // 
            this.btnSycDataBase.Location = new System.Drawing.Point(560, 30);
            this.btnSycDataBase.Name = "btnSycDataBase";
            this.btnSycDataBase.Size = new System.Drawing.Size(45, 23);
            this.btnSycDataBase.TabIndex = 9;
            this.btnSycDataBase.Text = "Sync";
            this.btnSycDataBase.UseVisualStyleBackColor = true;
            this.btnSycDataBase.Click += new System.EventHandler(this.btnSycDataBase_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 478);
            this.Controls.Add(this.btnSycDataBase);
            this.Controls.Add(this.tempoTrackBar);
            this.Controls.Add(this.dgvPlaylist);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnExit);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tempoTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.DataGridView dgvPlaylist;
        private System.Windows.Forms.TrackBar tempoTrackBar;
        private System.Windows.Forms.Button btnSycDataBase;
    }
}


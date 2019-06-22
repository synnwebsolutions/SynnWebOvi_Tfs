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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.dgvPlaylist = new System.Windows.Forms.DataGridView();
            this.btnSycDataBase = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPastClipBoard = new System.Windows.Forms.Button();
            this.txFilter = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txTempo = new System.Windows.Forms.TextBox();
            this.btnSetTempo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnExit.ForeColor = System.Drawing.Color.DarkGoldenrod;
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
            this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnStop.ForeColor = System.Drawing.Color.LightSlateGray;
            this.btnStop.Location = new System.Drawing.Point(47, 30);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 28);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnRecord.ForeColor = System.Drawing.Color.PaleGreen;
            this.btnRecord.Location = new System.Drawing.Point(509, 30);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(70, 23);
            this.btnRecord.TabIndex = 2;
            this.btnRecord.Text = "Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnShuffle
            // 
            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnShuffle.ForeColor = System.Drawing.Color.PaleGreen;
            this.btnShuffle.Location = new System.Drawing.Point(439, 30);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(70, 23);
            this.btnShuffle.TabIndex = 3;
            this.btnShuffle.Text = "Shuffle";
            this.btnShuffle.UseVisualStyleBackColor = true;
            this.btnShuffle.Click += new System.EventHandler(this.btnShuffle_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnNext.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.btnNext.Location = new System.Drawing.Point(223, 30);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(50, 28);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnPlay.ForeColor = System.Drawing.Color.Gold;
            this.btnPlay.Location = new System.Drawing.Point(147, 18);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(76, 47);
            this.btnPlay.TabIndex = 5;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnPrev.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.btnPrev.Location = new System.Drawing.Point(97, 30);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(50, 28);
            this.btnPrev.TabIndex = 6;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // dgvPlaylist
            // 
            this.dgvPlaylist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPlaylist.Location = new System.Drawing.Point(31, 17);
            this.dgvPlaylist.Name = "dgvPlaylist";
            this.dgvPlaylist.Size = new System.Drawing.Size(488, 227);
            this.dgvPlaylist.TabIndex = 7;
            this.dgvPlaylist.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPlaylist_CellContentClick);
            // 
            // btnSycDataBase
            // 
            this.btnSycDataBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSycDataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSycDataBase.ForeColor = System.Drawing.Color.PaleGreen;
            this.btnSycDataBase.Location = new System.Drawing.Point(579, 30);
            this.btnSycDataBase.Name = "btnSycDataBase";
            this.btnSycDataBase.Size = new System.Drawing.Size(70, 23);
            this.btnSycDataBase.TabIndex = 9;
            this.btnSycDataBase.Text = "Sync";
            this.btnSycDataBase.UseVisualStyleBackColor = true;
            this.btnSycDataBase.Click += new System.EventHandler(this.btnSycDataBase_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvPlaylist);
            this.panel1.Location = new System.Drawing.Point(47, 136);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 278);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkCyan;
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txFilter);
            this.panel2.Controls.Add(this.btnPastClipBoard);
            this.panel2.Location = new System.Drawing.Point(47, 97);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 33);
            this.panel2.TabIndex = 11;
            // 
            // btnPastClipBoard
            // 
            this.btnPastClipBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnPastClipBoard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPastClipBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnPastClipBoard.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnPastClipBoard.Location = new System.Drawing.Point(0, 0);
            this.btnPastClipBoard.Name = "btnPastClipBoard";
            this.btnPastClipBoard.Size = new System.Drawing.Size(94, 33);
            this.btnPastClipBoard.TabIndex = 0;
            this.btnPastClipBoard.Text = ">>";
            this.btnPastClipBoard.UseVisualStyleBackColor = true;
            // 
            // txFilter
            // 
            this.txFilter.Location = new System.Drawing.Point(100, 7);
            this.txFilter.Name = "txFilter";
            this.txFilter.Size = new System.Drawing.Size(266, 20);
            this.txFilter.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DarkSlateGray;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSearch.Location = new System.Drawing.Point(418, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(183, 33);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSetTempo);
            this.panel3.Controls.Add(this.txTempo);
            this.panel3.Location = new System.Drawing.Point(438, 59);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 22);
            this.panel3.TabIndex = 12;
            // 
            // txTempo
            // 
            this.txTempo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.txTempo.Location = new System.Drawing.Point(0, 0);
            this.txTempo.Name = "txTempo";
            this.txTempo.Size = new System.Drawing.Size(112, 26);
            this.txTempo.TabIndex = 0;
            this.txTempo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetTempo
            // 
            this.btnSetTempo.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSetTempo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.btnSetTempo.ForeColor = System.Drawing.Color.SaddleBrown;
            this.btnSetTempo.Location = new System.Drawing.Point(118, 0);
            this.btnSetTempo.Name = "btnSetTempo";
            this.btnSetTempo.Size = new System.Drawing.Size(92, 22);
            this.btnSetTempo.TabIndex = 1;
            this.btnSetTempo.Text = "T-X";
            this.btnSetTempo.UseVisualStyleBackColor = true;
            this.btnSetTempo.Click += new System.EventHandler(this.btnSetTempo_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(680, 478);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSycDataBase);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnShuffle);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPlaylist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnSycDataBase;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txFilter;
        private System.Windows.Forms.Button btnPastClipBoard;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSetTempo;
        private System.Windows.Forms.TextBox txTempo;
    }
}


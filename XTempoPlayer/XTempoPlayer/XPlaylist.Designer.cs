namespace XTempoPlayer
{
    partial class XPlaylist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XPlaylist));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnShuffle = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PanelSlide = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.xCurrentSongItem = new XTempoPlayer.XSongItem();
            this.xPrevSongItem = new XTempoPlayer.XSongItem();
            this.xNext1SongItem = new XTempoPlayer.XSongItem();
            this.panel1.SuspendLayout();
            this.PanelSlide.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            // 
            // btnShuffle
            // 
            this.btnShuffle.FlatAppearance.BorderSize = 0;
            this.btnShuffle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShuffle.Image = ((System.Drawing.Image)(resources.GetObject("btnShuffle.Image")));
            this.btnShuffle.Location = new System.Drawing.Point(444, 16);
            this.btnShuffle.Name = "btnShuffle";
            this.btnShuffle.Size = new System.Drawing.Size(37, 36);
            this.btnShuffle.TabIndex = 1;
            this.btnShuffle.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = ((System.Drawing.Image)(resources.GetObject("btnNext.Image")));
            this.btnNext.Location = new System.Drawing.Point(272, 16);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(37, 36);
            this.btnNext.TabIndex = 1;
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnPlay
            // 
            this.btnPlay.FlatAppearance.BorderSize = 0;
            this.btnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.Location = new System.Drawing.Point(214, 16);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(37, 36);
            this.btnPlay.TabIndex = 1;
            this.btnPlay.UseVisualStyleBackColor = true;
            // 
            // btnPrev
            // 
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Image = ((System.Drawing.Image)(resources.GetObject("btnPrev.Image")));
            this.btnPrev.Location = new System.Drawing.Point(153, 16);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(37, 36);
            this.btnPrev.TabIndex = 1;
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Skype Preview";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnShuffle);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnNext);
            this.panel1.Controls.Add(this.btnPrev);
            this.panel1.Controls.Add(this.btnPlay);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(249, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 66);
            this.panel1.TabIndex = 4;
            // 
            // PanelSlide
            // 
            this.PanelSlide.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.PanelSlide.Controls.Add(this.xNext1SongItem);
            this.PanelSlide.Controls.Add(this.xPrevSongItem);
            this.PanelSlide.Controls.Add(this.textBox1);
            this.PanelSlide.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelSlide.Location = new System.Drawing.Point(0, 0);
            this.PanelSlide.Name = "PanelSlide";
            this.PanelSlide.Size = new System.Drawing.Size(249, 313);
            this.PanelSlide.TabIndex = 6;
            this.PanelSlide.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelSlide_Paint);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.DarkGray;
            this.textBox1.Location = new System.Drawing.Point(9, 16);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(215, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Search Skype";
            // 
            // xCurrentSongItem
            // 
            this.xCurrentSongItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xCurrentSongItem.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.xCurrentSongItem.Location = new System.Drawing.Point(279, 90);
            this.xCurrentSongItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xCurrentSongItem.Name = "xCurrentSongItem";
            this.xCurrentSongItem.ProfilePic = null;
            this.xCurrentSongItem.Size = new System.Drawing.Size(451, 84);
            this.xCurrentSongItem.TabIndex = 7;
     
            // 
            // xPrevSongItem
            // 
            this.xPrevSongItem.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.xPrevSongItem.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.xPrevSongItem.Location = new System.Drawing.Point(9, 75);
            this.xPrevSongItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xPrevSongItem.Name = "xPrevSongItem";
            this.xPrevSongItem.ProfilePic = null;
            this.xPrevSongItem.Size = new System.Drawing.Size(221, 58);
            this.xPrevSongItem.TabIndex = 2;
    
            // 
            // xNext1SongItem
            // 
            this.xNext1SongItem.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xNext1SongItem.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.xNext1SongItem.Location = new System.Drawing.Point(9, 143);
            this.xNext1SongItem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xNext1SongItem.Name = "xNext1SongItem";
            this.xNext1SongItem.ProfilePic = null;
            this.xNext1SongItem.Size = new System.Drawing.Size(221, 58);
            this.xNext1SongItem.TabIndex = 3;
 
            // 
            // XPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.xCurrentSongItem);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelSlide);
            this.Name = "XPlaylist";
            this.Size = new System.Drawing.Size(754, 313);
            this.Load += new System.EventHandler(this.XPlaylist_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.PanelSlide.ResumeLayout(false);
            this.PanelSlide.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnShuffle;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PanelSlide;
        private System.Windows.Forms.TextBox textBox1;
        private XSongItem xNext1SongItem;
        private XSongItem xPrevSongItem;
        private XSongItem xCurrentSongItem;
    }
}

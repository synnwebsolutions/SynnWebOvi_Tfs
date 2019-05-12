namespace MusicHelper
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnExit = new System.Windows.Forms.Button();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnPlayPlaylist = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearUsb = new System.Windows.Forms.Button();
            this.btnSyncUsb = new System.Windows.Forms.Button();
            this.btnPlayUsbLst = new System.Windows.Forms.Button();
            this.txYoutubeUrl = new System.Windows.Forms.TextBox();
            this.btnYoutubeDownload = new System.Windows.Forms.Button();
            this.btnSync = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.pnDrives = new System.Windows.Forms.Panel();
            this.btnClip = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txSearchText = new System.Windows.Forms.TextBox();
            this.btnRefreshGrid = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuToUsb = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFromAttr = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuToPLaylist = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSearchMore = new System.Windows.Forms.ToolStripMenuItem();
            this.chkUsbs = new System.Windows.Forms.CheckedListBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblGridSummary = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.pnDrives.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(772, 474);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 33);
            this.btnExit.TabIndex = 0;
            this.btnExit.Tag = "ext";
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(18, 12);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(751, 23);
            this.progressBar2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txYoutubeUrl);
            this.panel1.Controls.Add(this.btnYoutubeDownload);
            this.panel1.Location = new System.Drawing.Point(775, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 365);
            this.panel1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPlayPlaylist);
            this.groupBox2.Location = new System.Drawing.Point(4, 262);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 100);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Play List";
            // 
            // btnPlayPlaylist
            // 
            this.btnPlayPlaylist.Location = new System.Drawing.Point(0, 19);
            this.btnPlayPlaylist.Name = "btnPlayPlaylist";
            this.btnPlayPlaylist.Size = new System.Drawing.Size(194, 23);
            this.btnPlayPlaylist.TabIndex = 5;
            this.btnPlayPlaylist.Text = "Play PlayList";
            this.btnPlayPlaylist.UseVisualStyleBackColor = true;
            this.btnPlayPlaylist.Click += new System.EventHandler(this.btnPlayPlaylist_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearUsb);
            this.groupBox1.Controls.Add(this.btnSyncUsb);
            this.groupBox1.Controls.Add(this.btnPlayUsbLst);
            this.groupBox1.Location = new System.Drawing.Point(3, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(193, 113);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "USB";
            // 
            // btnClearUsb
            // 
            this.btnClearUsb.Location = new System.Drawing.Point(0, 75);
            this.btnClearUsb.Name = "btnClearUsb";
            this.btnClearUsb.Size = new System.Drawing.Size(194, 23);
            this.btnClearUsb.TabIndex = 5;
            this.btnClearUsb.Text = "Clear USB List";
            this.btnClearUsb.UseVisualStyleBackColor = true;
            this.btnClearUsb.Click += new System.EventHandler(this.btnClearUsb_Click);
            // 
            // btnSyncUsb
            // 
            this.btnSyncUsb.Location = new System.Drawing.Point(-1, 19);
            this.btnSyncUsb.Name = "btnSyncUsb";
            this.btnSyncUsb.Size = new System.Drawing.Size(194, 23);
            this.btnSyncUsb.TabIndex = 2;
            this.btnSyncUsb.Text = "Sync USB";
            this.btnSyncUsb.UseVisualStyleBackColor = true;
            this.btnSyncUsb.Click += new System.EventHandler(this.btnSyncUsb_Click);
            // 
            // btnPlayUsbLst
            // 
            this.btnPlayUsbLst.Location = new System.Drawing.Point(-3, 46);
            this.btnPlayUsbLst.Name = "btnPlayUsbLst";
            this.btnPlayUsbLst.Size = new System.Drawing.Size(194, 23);
            this.btnPlayUsbLst.TabIndex = 4;
            this.btnPlayUsbLst.Text = "Play USB List";
            this.btnPlayUsbLst.UseVisualStyleBackColor = true;
            this.btnPlayUsbLst.Click += new System.EventHandler(this.btnPlayUsbLst_Click);
            // 
            // txYoutubeUrl
            // 
            this.txYoutubeUrl.Location = new System.Drawing.Point(4, 50);
            this.txYoutubeUrl.Name = "txYoutubeUrl";
            this.txYoutubeUrl.Size = new System.Drawing.Size(193, 20);
            this.txYoutubeUrl.TabIndex = 3;
            // 
            // btnYoutubeDownload
            // 
            this.btnYoutubeDownload.Location = new System.Drawing.Point(3, 20);
            this.btnYoutubeDownload.Name = "btnYoutubeDownload";
            this.btnYoutubeDownload.Size = new System.Drawing.Size(194, 23);
            this.btnYoutubeDownload.TabIndex = 0;
            this.btnYoutubeDownload.Text = "Youtube";
            this.btnYoutubeDownload.UseVisualStyleBackColor = true;
            this.btnYoutubeDownload.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(13, 8);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(194, 23);
            this.btnSync.TabIndex = 1;
            this.btnSync.Text = "Sync Data";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv);
            this.panel2.Location = new System.Drawing.Point(18, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(751, 365);
            this.panel2.TabIndex = 5;
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(4, 49);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(744, 300);
            this.dgv.TabIndex = 0;
            this.dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellDoubleClick);
            this.dgv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_MouseClick);
            // 
            // pnDrives
            // 
            this.pnDrives.Controls.Add(this.btnClip);
            this.pnDrives.Controls.Add(this.btnClear);
            this.pnDrives.Controls.Add(this.txSearchText);
            this.pnDrives.Controls.Add(this.btnRefreshGrid);
            this.pnDrives.Controls.Add(this.btnSync);
            this.pnDrives.Location = new System.Drawing.Point(18, 42);
            this.pnDrives.Name = "pnDrives";
            this.pnDrives.Size = new System.Drawing.Size(751, 34);
            this.pnDrives.TabIndex = 6;
            // 
            // btnClip
            // 
            this.btnClip.Location = new System.Drawing.Point(224, 9);
            this.btnClip.Name = "btnClip";
            this.btnClip.Size = new System.Drawing.Size(26, 23);
            this.btnClip.TabIndex = 1;
            this.btnClip.Text = ">>";
            this.btnClip.UseVisualStyleBackColor = true;
            this.btnClip.Click += new System.EventHandler(this.btnClip_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(665, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txSearchText
            // 
            this.txSearchText.Location = new System.Drawing.Point(256, 11);
            this.txSearchText.Name = "txSearchText";
            this.txSearchText.Size = new System.Drawing.Size(309, 20);
            this.txSearchText.TabIndex = 2;
            // 
            // btnRefreshGrid
            // 
            this.btnRefreshGrid.Location = new System.Drawing.Point(584, 9);
            this.btnRefreshGrid.Name = "btnRefreshGrid";
            this.btnRefreshGrid.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshGrid.TabIndex = 1;
            this.btnRefreshGrid.Text = "Search";
            this.btnRefreshGrid.UseVisualStyleBackColor = true;
            this.btnRefreshGrid.Click += new System.EventHandler(this.btnRefreshGrid_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuToUsb,
            this.toolStripMenuFromAttr,
            this.toolStripMenuFromFile,
            this.toolStripMenuDelete,
            this.toolStripMenuToPLaylist,
            this.toolStripMenuSearchMore});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(200, 136);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuToUsb
            // 
            this.toolStripMenuToUsb.Name = "toolStripMenuToUsb";
            this.toolStripMenuToUsb.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuToUsb.Tag = "usb";
            this.toolStripMenuToUsb.Text = "Send To Usb";
            // 
            // toolStripMenuFromAttr
            // 
            this.toolStripMenuFromAttr.Name = "toolStripMenuFromAttr";
            this.toolStripMenuFromAttr.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuFromAttr.Tag = "fratr";
            this.toolStripMenuFromAttr.Text = "Update From Attributes";
            // 
            // toolStripMenuFromFile
            // 
            this.toolStripMenuFromFile.Name = "toolStripMenuFromFile";
            this.toolStripMenuFromFile.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuFromFile.Tag = "frfl";
            this.toolStripMenuFromFile.Text = "Update From File Name";
            // 
            // toolStripMenuDelete
            // 
            this.toolStripMenuDelete.Name = "toolStripMenuDelete";
            this.toolStripMenuDelete.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuDelete.Tag = "dlt";
            this.toolStripMenuDelete.Text = "Delete File";
            // 
            // toolStripMenuToPLaylist
            // 
            this.toolStripMenuToPLaylist.Name = "toolStripMenuToPLaylist";
            this.toolStripMenuToPLaylist.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuToPLaylist.Tag = "plst";
            this.toolStripMenuToPLaylist.Text = "Add To Playlist";
            // 
            // toolStripMenuSearchMore
            // 
            this.toolStripMenuSearchMore.Name = "toolStripMenuSearchMore";
            this.toolStripMenuSearchMore.Size = new System.Drawing.Size(199, 22);
            this.toolStripMenuSearchMore.Tag = "src";
            this.toolStripMenuSearchMore.Text = "Search More ..";
            // 
            // chkUsbs
            // 
            this.chkUsbs.FormattingEnabled = true;
            this.chkUsbs.Location = new System.Drawing.Point(779, 42);
            this.chkUsbs.Name = "chkUsbs";
            this.chkUsbs.Size = new System.Drawing.Size(193, 34);
            this.chkUsbs.TabIndex = 7;
            this.chkUsbs.SelectedValueChanged += new System.EventHandler(this.chkUsbs_SelectedValueChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(195)));
            this.lblUser.Location = new System.Drawing.Point(791, 12);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(47, 15);
            this.lblUser.TabIndex = 8;
            this.lblUser.Text = "label1";
            // 
            // lblGridSummary
            // 
            this.lblGridSummary.AutoSize = true;
            this.lblGridSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(195)));
            this.lblGridSummary.Location = new System.Drawing.Point(28, 454);
            this.lblGridSummary.Name = "lblGridSummary";
            this.lblGridSummary.Size = new System.Drawing.Size(46, 17);
            this.lblGridSummary.TabIndex = 9;
            this.lblGridSummary.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 519);
            this.Controls.Add(this.lblGridSummary);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.chkUsbs);
            this.Controls.Add(this.pnDrives);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.pnDrives.ResumeLayout(false);
            this.pnDrives.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSyncUsb;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnYoutubeDownload;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnDrives;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txSearchText;
        private System.Windows.Forms.Button btnRefreshGrid;
        private System.Windows.Forms.TextBox txYoutubeUrl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuToUsb;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFromAttr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuFromFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuToPLaylist;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuSearchMore;
        private System.Windows.Forms.Button btnPlayPlaylist;
        private System.Windows.Forms.Button btnPlayUsbLst;
        private System.Windows.Forms.CheckedListBox chkUsbs;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearUsb;
        private System.Windows.Forms.Button btnClip;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblGridSummary;
    }
}


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
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbLibrary = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.btnScanFolder = new System.Windows.Forms.Button();
            this.foldersToScanDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.gbLibrary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1163, 733);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(268, 55);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbLibrary);
            this.panel1.Controls.Add(this.gbActions);
            this.panel1.Location = new System.Drawing.Point(41, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1390, 657);
            this.panel1.TabIndex = 1;
            // 
            // gbLibrary
            // 
            this.gbLibrary.Controls.Add(this.dataGridView1);
            this.gbLibrary.Location = new System.Drawing.Point(29, 49);
            this.gbLibrary.Name = "gbLibrary";
            this.gbLibrary.Size = new System.Drawing.Size(1068, 581);
            this.gbLibrary.TabIndex = 2;
            this.gbLibrary.TabStop = false;
            this.gbLibrary.Text = "Library";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1035, 445);
            this.dataGridView1.TabIndex = 0;
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.btnScanFolder);
            this.gbActions.Location = new System.Drawing.Point(1122, 49);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(247, 402);
            this.gbActions.TabIndex = 1;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Library Actions";
            // 
            // btnScanFolder
            // 
            this.btnScanFolder.Location = new System.Drawing.Point(18, 45);
            this.btnScanFolder.Name = "btnScanFolder";
            this.btnScanFolder.Size = new System.Drawing.Size(223, 34);
            this.btnScanFolder.TabIndex = 0;
            this.btnScanFolder.Text = "Scan Folder";
            this.btnScanFolder.UseVisualStyleBackColor = true;
            this.btnScanFolder.Click += new System.EventHandler(this.btnScanFolder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1457, 810);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.gbLibrary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gbLibrary;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.Button btnScanFolder;
        private System.Windows.Forms.FolderBrowserDialog foldersToScanDialog;
    }
}


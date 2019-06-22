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
            this.pnPrev = new System.Windows.Forms.Panel();
            this.pnNext = new System.Windows.Forms.Panel();
            this.pnCurrent = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnPrev.SuspendLayout();
            this.pnNext.SuspendLayout();
            this.pnCurrent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnPrev
            // 
            this.pnPrev.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnPrev.Controls.Add(this.label1);
            this.pnPrev.ForeColor = System.Drawing.Color.DarkOrange;
            this.pnPrev.Location = new System.Drawing.Point(13, 4);
            this.pnPrev.Name = "pnPrev";
            this.pnPrev.Size = new System.Drawing.Size(232, 30);
            this.pnPrev.TabIndex = 0;
            // 
            // pnNext
            // 
            this.pnNext.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnNext.Controls.Add(this.label3);
            this.pnNext.Location = new System.Drawing.Point(13, 132);
            this.pnNext.Name = "pnNext";
            this.pnNext.Size = new System.Drawing.Size(232, 26);
            this.pnNext.TabIndex = 1;
            // 
            // pnCurrent
            // 
            this.pnCurrent.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pnCurrent.Controls.Add(this.label2);
            this.pnCurrent.Location = new System.Drawing.Point(13, 58);
            this.pnCurrent.Name = "pnCurrent";
            this.pnCurrent.Size = new System.Drawing.Size(232, 39);
            this.pnCurrent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // XPlaylist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.pnCurrent);
            this.Controls.Add(this.pnNext);
            this.Controls.Add(this.pnPrev);
            this.Name = "XPlaylist";
            this.Size = new System.Drawing.Size(262, 158);
            this.pnPrev.ResumeLayout(false);
            this.pnPrev.PerformLayout();
            this.pnNext.ResumeLayout(false);
            this.pnNext.PerformLayout();
            this.pnCurrent.ResumeLayout(false);
            this.pnCurrent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnPrev;
        private System.Windows.Forms.Panel pnNext;
        private System.Windows.Forms.Panel pnCurrent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}

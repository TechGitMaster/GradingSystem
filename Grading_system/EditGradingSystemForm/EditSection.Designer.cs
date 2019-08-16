namespace EditGradingSystemForm
{
    partial class EditSection
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
            this.panelBox = new System.Windows.Forms.Panel();
            this.GradingPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.SearchBoxCalender = new System.Windows.Forms.TextBox();
            this.BttnSearchBar = new System.Windows.Forms.PictureBox();
            this.panelBox.SuspendLayout();
            this.GradingPanel.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BttnSearchBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panelBox
            // 
            this.panelBox.Controls.Add(this.GradingPanel);
            this.panelBox.Location = new System.Drawing.Point(192, 65);
            this.panelBox.Name = "panelBox";
            this.panelBox.Size = new System.Drawing.Size(779, 503);
            this.panelBox.TabIndex = 0;
            // 
            // GradingPanel
            // 
            this.GradingPanel.Controls.Add(this.panel7);
            this.GradingPanel.Controls.Add(this.panel1);
            this.GradingPanel.Location = new System.Drawing.Point(0, 0);
            this.GradingPanel.Name = "GradingPanel";
            this.GradingPanel.Size = new System.Drawing.Size(779, 503);
            this.GradingPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.panel1.Location = new System.Drawing.Point(458, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 411);
            this.panel1.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(458, 24);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(302, 46);
            this.panel7.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panel8.Controls.Add(this.SearchBoxCalender);
            this.panel8.Controls.Add(this.BttnSearchBar);
            this.panel8.Location = new System.Drawing.Point(12, 8);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(276, 30);
            this.panel8.TabIndex = 0;
            // 
            // SearchBoxCalender
            // 
            this.SearchBoxCalender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.SearchBoxCalender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchBoxCalender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBoxCalender.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.SearchBoxCalender.Location = new System.Drawing.Point(31, 4);
            this.SearchBoxCalender.Name = "SearchBoxCalender";
            this.SearchBoxCalender.Size = new System.Drawing.Size(232, 21);
            this.SearchBoxCalender.TabIndex = 1;
            // 
            // BttnSearchBar
            // 
            this.BttnSearchBar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BttnSearchBar.Location = new System.Drawing.Point(3, 3);
            this.BttnSearchBar.Name = "BttnSearchBar";
            this.BttnSearchBar.Size = new System.Drawing.Size(25, 24);
            this.BttnSearchBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BttnSearchBar.TabIndex = 0;
            this.BttnSearchBar.TabStop = false;
            // 
            // EditSection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(983, 602);
            this.Controls.Add(this.panelBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditSection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panelBox.ResumeLayout(false);
            this.GradingPanel.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BttnSearchBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelBox;
        private System.Windows.Forms.Panel GradingPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox SearchBoxCalender;
        private System.Windows.Forms.PictureBox BttnSearchBar;
    }
}


namespace FormLoginRegister
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
            this.usernamelabel1 = new System.Windows.Forms.Label();
            this.passwordlabel2 = new System.Windows.Forms.Label();
            this.loginbttn1 = new System.Windows.Forms.Button();
            this.createAccount = new System.Windows.Forms.LinkLabel();
            this.mousemove = new System.Windows.Forms.Panel();
            this.erase_login = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.mousemove.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernamelabel1
            // 
            this.usernamelabel1.AutoSize = true;
            this.usernamelabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernamelabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.usernamelabel1.Location = new System.Drawing.Point(71, 74);
            this.usernamelabel1.Name = "usernamelabel1";
            this.usernamelabel1.Size = new System.Drawing.Size(74, 16);
            this.usernamelabel1.TabIndex = 0;
            this.usernamelabel1.Text = "Username:";
            // 
            // passwordlabel2
            // 
            this.passwordlabel2.AutoSize = true;
            this.passwordlabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passwordlabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.passwordlabel2.Location = new System.Drawing.Point(71, 110);
            this.passwordlabel2.Name = "passwordlabel2";
            this.passwordlabel2.Size = new System.Drawing.Size(71, 16);
            this.passwordlabel2.TabIndex = 2;
            this.passwordlabel2.Text = "Password:";
            // 
            // loginbttn1
            // 
            this.loginbttn1.BackColor = System.Drawing.Color.Coral;
            this.loginbttn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginbttn1.FlatAppearance.BorderColor = System.Drawing.Color.Coral;
            this.loginbttn1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Coral;
            this.loginbttn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginbttn1.ForeColor = System.Drawing.Color.Black;
            this.loginbttn1.Location = new System.Drawing.Point(245, 140);
            this.loginbttn1.Name = "loginbttn1";
            this.loginbttn1.Size = new System.Drawing.Size(77, 30);
            this.loginbttn1.TabIndex = 4;
            this.loginbttn1.Text = "Log in";
            this.loginbttn1.UseVisualStyleBackColor = false;
            this.loginbttn1.Click += new System.EventHandler(this.Loginbttn1_Click);
            // 
            // createAccount
            // 
            this.createAccount.AutoSize = true;
            this.createAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createAccount.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(143)))), ((int)(((byte)(206)))));
            this.createAccount.Location = new System.Drawing.Point(71, 147);
            this.createAccount.Name = "createAccount";
            this.createAccount.Size = new System.Drawing.Size(89, 15);
            this.createAccount.TabIndex = 5;
            this.createAccount.TabStop = true;
            this.createAccount.Text = "Create Account";
            // 
            // mousemove
            // 
            this.mousemove.Controls.Add(this.erase_login);
            this.mousemove.Location = new System.Drawing.Point(52, 2);
            this.mousemove.Name = "mousemove";
            this.mousemove.Size = new System.Drawing.Size(335, 37);
            this.mousemove.TabIndex = 7;
            // 
            // erase_login
            // 
            this.erase_login.AutoSize = true;
            this.erase_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.erase_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.erase_login.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.erase_login.Location = new System.Drawing.Point(316, 7);
            this.erase_login.Name = "erase_login";
            this.erase_login.Size = new System.Drawing.Size(16, 20);
            this.erase_login.TabIndex = 8;
            this.erase_login.Text = "x";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::FormLoginRegister.Properties.Resources._024_tag_11;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(44, 61);
            this.panel1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(101)))), ((int)(((byte)(115)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.textBox1.Location = new System.Drawing.Point(148, 74);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(174, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "Username";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(101)))), ((int)(((byte)(115)))));
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.textBox2.Location = new System.Drawing.Point(148, 103);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(174, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Tag = "Password";
            this.textBox2.UseSystemPasswordChar = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(399, 221);
            this.Controls.Add(this.mousemove);
            this.Controls.Add(this.createAccount);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.loginbttn1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.passwordlabel2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.usernamelabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.mousemove.ResumeLayout(false);
            this.mousemove.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernamelabel1;
        private System.Windows.Forms.Label passwordlabel2;
        private System.Windows.Forms.Button loginbttn1;
        private System.Windows.Forms.LinkLabel createAccount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel mousemove;
        private System.Windows.Forms.Label erase_login;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}


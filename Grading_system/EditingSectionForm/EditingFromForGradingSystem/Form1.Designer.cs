namespace EditingFromForGradingSystem
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
            this.PanelBox = new System.Windows.Forms.Panel();
            this.CalendarPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.UserSchedAddPanelAdd = new System.Windows.Forms.Panel();
            this.NoAssignedAddSched = new System.Windows.Forms.Panel();
            this.NoAssSchedLabel = new System.Windows.Forms.Label();
            this.NameOfUserAddCalendar = new System.Windows.Forms.Label();
            this.PictureBoxScheduleAddCalendar = new System.Windows.Forms.PictureBox();
            this.DeleteBttnScedule = new System.Windows.Forms.Button();
            this.StartBttnSchedule = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.CheckingDataSchedule = new System.Windows.Forms.Button();
            this.TimeForDate = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMnt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxAP = new System.Windows.Forms.ComboBox();
            this.comboBoxHrs = new System.Windows.Forms.ComboBox();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CalendarTime = new System.Windows.Forms.Panel();
            this.ScheduleUserPanel1 = new System.Windows.Forms.Panel();
            this.ScheduleUserPanel2 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.SchedulePanelBarSearch = new System.Windows.Forms.Panel();
            this.LabelNoSearchCalendar = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.SearchBoxCalender = new System.Windows.Forms.TextBox();
            this.BttnSearchBar = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer();
            this.TimeCalendarLabel = new System.Windows.Forms.Label();
            this.UserNoSchedule = new System.Windows.Forms.Label();
            this.CalendarPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.UserSchedAddPanelAdd.SuspendLayout();
            this.NoAssignedAddSched.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxScheduleAddCalendar)).BeginInit();
            this.panel2.SuspendLayout();
            this.TimeForDate.SuspendLayout();
            this.panel5.SuspendLayout();
            this.CalendarTime.SuspendLayout();
            this.ScheduleUserPanel1.SuspendLayout();
            this.ScheduleUserPanel2.SuspendLayout();
            this.SchedulePanelBarSearch.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BttnSearchBar)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelBox
            // 
            this.PanelBox.Location = new System.Drawing.Point(192, 65);
            this.PanelBox.Name = "PanelBox";
            this.PanelBox.Size = new System.Drawing.Size(776, 498);
            this.PanelBox.TabIndex = 16;
            // 
            // CalendarPanel
            // 
            this.CalendarPanel.AccessibleDescription = "Calendar";
            this.CalendarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.CalendarPanel.Controls.Add(this.panel1);
            this.CalendarPanel.Location = new System.Drawing.Point(192, 79);
            this.CalendarPanel.Name = "CalendarPanel";
            this.CalendarPanel.Size = new System.Drawing.Size(779, 486);
            this.CalendarPanel.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.UserSchedAddPanelAdd);
            this.panel1.Controls.Add(this.DeleteBttnScedule);
            this.panel1.Controls.Add(this.StartBttnSchedule);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.TimeForDate);
            this.panel1.Controls.Add(this.monthCalendar1);
            this.panel1.Location = new System.Drawing.Point(0, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(779, 486);
            this.panel1.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.label8.Location = new System.Drawing.Point(410, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(257, 24);
            this.label8.TabIndex = 28;
            this.label8.Text = "Set a Schedule For This User";
            // 
            // UserSchedAddPanelAdd
            // 
            this.UserSchedAddPanelAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.UserSchedAddPanelAdd.Controls.Add(this.NoAssignedAddSched);
            this.UserSchedAddPanelAdd.Controls.Add(this.NameOfUserAddCalendar);
            this.UserSchedAddPanelAdd.Controls.Add(this.PictureBoxScheduleAddCalendar);
            this.UserSchedAddPanelAdd.Location = new System.Drawing.Point(299, 63);
            this.UserSchedAddPanelAdd.Name = "UserSchedAddPanelAdd";
            this.UserSchedAddPanelAdd.Size = new System.Drawing.Size(464, 414);
            this.UserSchedAddPanelAdd.TabIndex = 27;
            // 
            // NoAssignedAddSched
            // 
            this.NoAssignedAddSched.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.NoAssignedAddSched.Controls.Add(this.NoAssSchedLabel);
            this.NoAssignedAddSched.Location = new System.Drawing.Point(12, 136);
            this.NoAssignedAddSched.Name = "NoAssignedAddSched";
            this.NoAssignedAddSched.Size = new System.Drawing.Size(439, 266);
            this.NoAssignedAddSched.TabIndex = 2;
            // 
            // NoAssSchedLabel
            // 
            this.NoAssSchedLabel.AutoSize = true;
            this.NoAssSchedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoAssSchedLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.NoAssSchedLabel.Location = new System.Drawing.Point(162, 116);
            this.NoAssSchedLabel.Name = "NoAssSchedLabel";
            this.NoAssSchedLabel.Size = new System.Drawing.Size(149, 16);
            this.NoAssSchedLabel.TabIndex = 0;
            this.NoAssSchedLabel.Text = "No Schedule Assigned.";
            // 
            // NameOfUserAddCalendar
            // 
            this.NameOfUserAddCalendar.AutoSize = true;
            this.NameOfUserAddCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameOfUserAddCalendar.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.NameOfUserAddCalendar.Location = new System.Drawing.Point(186, 95);
            this.NameOfUserAddCalendar.Name = "NameOfUserAddCalendar";
            this.NameOfUserAddCalendar.Size = new System.Drawing.Size(104, 18);
            this.NameOfUserAddCalendar.TabIndex = 1;
            this.NameOfUserAddCalendar.Text = "Janina Noceto";
            // 
            // PictureBoxScheduleAddCalendar
            // 
            this.PictureBoxScheduleAddCalendar.Image = global::EditingFromForGradingSystem.Properties.Resources._8130871;
            this.PictureBoxScheduleAddCalendar.InitialImage = global::EditingFromForGradingSystem.Properties.Resources._813087;
            this.PictureBoxScheduleAddCalendar.Location = new System.Drawing.Point(189, 12);
            this.PictureBoxScheduleAddCalendar.Name = "PictureBoxScheduleAddCalendar";
            this.PictureBoxScheduleAddCalendar.Size = new System.Drawing.Size(93, 80);
            this.PictureBoxScheduleAddCalendar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxScheduleAddCalendar.TabIndex = 0;
            this.PictureBoxScheduleAddCalendar.TabStop = false;
            // 
            // DeleteBttnScedule
            // 
            this.DeleteBttnScedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.DeleteBttnScedule.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(97)))), ((int)(((byte)(85)))));
            this.DeleteBttnScedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.DeleteBttnScedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.DeleteBttnScedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteBttnScedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteBttnScedule.ForeColor = System.Drawing.Color.White;
            this.DeleteBttnScedule.Location = new System.Drawing.Point(163, 441);
            this.DeleteBttnScedule.Name = "DeleteBttnScedule";
            this.DeleteBttnScedule.Size = new System.Drawing.Size(113, 36);
            this.DeleteBttnScedule.TabIndex = 26;
            this.DeleteBttnScedule.Text = "Delete Scedule";
            this.DeleteBttnScedule.UseVisualStyleBackColor = false;
            // 
            // StartBttnSchedule
            // 
            this.StartBttnSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.StartBttnSchedule.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(97)))), ((int)(((byte)(85)))));
            this.StartBttnSchedule.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.StartBttnSchedule.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.StartBttnSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartBttnSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartBttnSchedule.ForeColor = System.Drawing.Color.White;
            this.StartBttnSchedule.Location = new System.Drawing.Point(9, 441);
            this.StartBttnSchedule.Name = "StartBttnSchedule";
            this.StartBttnSchedule.Size = new System.Drawing.Size(113, 36);
            this.StartBttnSchedule.TabIndex = 25;
            this.StartBttnSchedule.Text = "Start Scedule";
            this.StartBttnSchedule.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.CheckingDataSchedule);
            this.panel2.Location = new System.Drawing.Point(9, 326);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 100);
            this.panel2.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Coral;
            this.label4.Location = new System.Drawing.Point(62, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Selection Date Check";
            // 
            // CheckingDataSchedule
            // 
            this.CheckingDataSchedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(141)))));
            this.CheckingDataSchedule.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(141)))));
            this.CheckingDataSchedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CheckingDataSchedule.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckingDataSchedule.ForeColor = System.Drawing.Color.White;
            this.CheckingDataSchedule.Location = new System.Drawing.Point(11, 51);
            this.CheckingDataSchedule.Name = "CheckingDataSchedule";
            this.CheckingDataSchedule.Size = new System.Drawing.Size(244, 37);
            this.CheckingDataSchedule.TabIndex = 17;
            this.CheckingDataSchedule.Text = "None";
            this.CheckingDataSchedule.UseVisualStyleBackColor = false;
            // 
            // TimeForDate
            // 
            this.TimeForDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.TimeForDate.Controls.Add(this.label3);
            this.TimeForDate.Controls.Add(this.label5);
            this.TimeForDate.Controls.Add(this.comboBoxMnt);
            this.TimeForDate.Controls.Add(this.label2);
            this.TimeForDate.Controls.Add(this.label7);
            this.TimeForDate.Controls.Add(this.label1);
            this.TimeForDate.Controls.Add(this.label6);
            this.TimeForDate.Controls.Add(this.comboBoxAP);
            this.TimeForDate.Controls.Add(this.comboBoxHrs);
            this.TimeForDate.Location = new System.Drawing.Point(9, 199);
            this.TimeForDate.Name = "TimeForDate";
            this.TimeForDate.Size = new System.Drawing.Size(267, 112);
            this.TimeForDate.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Coral;
            this.label3.Location = new System.Drawing.Point(101, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Set Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(30, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Hours";
            // 
            // comboBoxMnt
            // 
            this.comboBoxMnt.AccessibleName = "MntDate";
            this.comboBoxMnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.comboBoxMnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxMnt.FormattingEnabled = true;
            this.comboBoxMnt.Location = new System.Drawing.Point(110, 70);
            this.comboBoxMnt.Name = "comboBoxMnt";
            this.comboBoxMnt.Size = new System.Drawing.Size(76, 21);
            this.comboBoxMnt.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label2.Location = new System.Drawing.Point(204, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "AM/PM";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.label7.Location = new System.Drawing.Point(93, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = ":";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.label1.Location = new System.Drawing.Point(192, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(125, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Minute";
            // 
            // comboBoxAP
            // 
            this.comboBoxAP.AccessibleName = "APDate";
            this.comboBoxAP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.comboBoxAP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxAP.FormattingEnabled = true;
            this.comboBoxAP.Location = new System.Drawing.Point(207, 70);
            this.comboBoxAP.Name = "comboBoxAP";
            this.comboBoxAP.Size = new System.Drawing.Size(49, 21);
            this.comboBoxAP.TabIndex = 14;
            // 
            // comboBoxHrs
            // 
            this.comboBoxHrs.AccessibleName = "HrsDate";
            this.comboBoxHrs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
            this.comboBoxHrs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxHrs.FormattingEnabled = true;
            this.comboBoxHrs.Location = new System.Drawing.Point(11, 70);
            this.comboBoxHrs.Name = "comboBoxHrs";
            this.comboBoxHrs.Size = new System.Drawing.Size(76, 21);
            this.comboBoxHrs.TabIndex = 9;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.BackColor = System.Drawing.SystemColors.ControlText;
            this.monthCalendar1.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.monthCalendar1.Location = new System.Drawing.Point(29, 25);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowToday = false;
            this.monthCalendar1.ShowTodayCircle = false;
            this.monthCalendar1.TabIndex = 22;
            this.monthCalendar1.TrailingForeColor = System.Drawing.SystemColors.Control;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.CalendarTime);
            this.panel5.Controls.Add(this.ScheduleUserPanel1);
            this.panel5.Controls.Add(this.SchedulePanelBarSearch);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(192, 326);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(779, 239);
            this.panel5.TabIndex = 29;
            // 
            // CalendarTime
            // 
            this.CalendarTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.CalendarTime.Controls.Add(this.TimeCalendarLabel);
            this.CalendarTime.Location = new System.Drawing.Point(9, 35);
            this.CalendarTime.Name = "CalendarTime";
            this.CalendarTime.Size = new System.Drawing.Size(213, 34);
            this.CalendarTime.TabIndex = 3;
            // 
            // ScheduleUserPanel1
            // 
            this.ScheduleUserPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.ScheduleUserPanel1.Controls.Add(this.ScheduleUserPanel2);
            this.ScheduleUserPanel1.Controls.Add(this.label12);
            this.ScheduleUserPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScheduleUserPanel1.Location = new System.Drawing.Point(9, 75);
            this.ScheduleUserPanel1.Name = "ScheduleUserPanel1";
            this.ScheduleUserPanel1.Size = new System.Drawing.Size(443, 402);
            this.ScheduleUserPanel1.TabIndex = 2;
            // 
            // ScheduleUserPanel2
            // 
            this.ScheduleUserPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.ScheduleUserPanel2.Controls.Add(this.UserNoSchedule);
            this.ScheduleUserPanel2.Location = new System.Drawing.Point(10, 45);
            this.ScheduleUserPanel2.Name = "ScheduleUserPanel2";
            this.ScheduleUserPanel2.Size = new System.Drawing.Size(421, 345);
            this.ScheduleUserPanel2.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.label12.Location = new System.Drawing.Point(7, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 18);
            this.label12.TabIndex = 0;
            this.label12.Text = "Your Schedule:";
            // 
            // SchedulePanelBarSearch
            // 
            this.SchedulePanelBarSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.SchedulePanelBarSearch.Controls.Add(this.LabelNoSearchCalendar);
            this.SchedulePanelBarSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SchedulePanelBarSearch.Location = new System.Drawing.Point(458, 75);
            this.SchedulePanelBarSearch.Name = "SchedulePanelBarSearch";
            this.SchedulePanelBarSearch.Size = new System.Drawing.Size(302, 402);
            this.SchedulePanelBarSearch.TabIndex = 1;
            // 
            // LabelNoSearchCalendar
            // 
            this.LabelNoSearchCalendar.AutoSize = true;
            this.LabelNoSearchCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelNoSearchCalendar.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.LabelNoSearchCalendar.Location = new System.Drawing.Point(111, 185);
            this.LabelNoSearchCalendar.Name = "LabelNoSearchCalendar";
            this.LabelNoSearchCalendar.Size = new System.Drawing.Size(98, 16);
            this.LabelNoSearchCalendar.TabIndex = 0;
            this.LabelNoSearchCalendar.Text = "No Search Yet!";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(40)))), ((int)(((byte)(51)))));
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Location = new System.Drawing.Point(458, 23);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(302, 46);
            this.panel6.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.panel7.Controls.Add(this.SearchBoxCalender);
            this.panel7.Controls.Add(this.BttnSearchBar);
            this.panel7.Location = new System.Drawing.Point(12, 8);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(276, 30);
            this.panel7.TabIndex = 0;
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
            this.BttnSearchBar.Image = global::EditingFromForGradingSystem.Properties.Resources.icons8_google_web_search_26;
            this.BttnSearchBar.Location = new System.Drawing.Point(3, 3);
            this.BttnSearchBar.Name = "BttnSearchBar";
            this.BttnSearchBar.Size = new System.Drawing.Size(25, 24);
            this.BttnSearchBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BttnSearchBar.TabIndex = 0;
            this.BttnSearchBar.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // TimeCalendarLabel
            // 
            this.TimeCalendarLabel.AutoSize = true;
            this.TimeCalendarLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeCalendarLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(179)))), ((int)(((byte)(182)))), ((int)(((byte)(183)))));
            this.TimeCalendarLabel.Location = new System.Drawing.Point(6, 6);
            this.TimeCalendarLabel.Name = "TimeCalendarLabel";
            this.TimeCalendarLabel.Size = new System.Drawing.Size(82, 25);
            this.TimeCalendarLabel.TabIndex = 0;
            this.TimeCalendarLabel.Text = "label13";
            // 
            // UserNoSchedule
            // 
            this.UserNoSchedule.AutoSize = true;
            this.UserNoSchedule.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.UserNoSchedule.Location = new System.Drawing.Point(162, 166);
            this.UserNoSchedule.Name = "UserNoSchedule";
            this.UserNoSchedule.Size = new System.Drawing.Size(112, 16);
            this.UserNoSchedule.TabIndex = 0;
            this.UserNoSchedule.Text = "No Schedule Yet.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(32)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(983, 577);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.CalendarPanel);
            this.Controls.Add(this.PanelBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.CalendarPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.UserSchedAddPanelAdd.ResumeLayout(false);
            this.UserSchedAddPanelAdd.PerformLayout();
            this.NoAssignedAddSched.ResumeLayout(false);
            this.NoAssignedAddSched.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxScheduleAddCalendar)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.TimeForDate.ResumeLayout(false);
            this.TimeForDate.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.CalendarTime.ResumeLayout(false);
            this.CalendarTime.PerformLayout();
            this.ScheduleUserPanel1.ResumeLayout(false);
            this.ScheduleUserPanel1.PerformLayout();
            this.ScheduleUserPanel2.ResumeLayout(false);
            this.ScheduleUserPanel2.PerformLayout();
            this.SchedulePanelBarSearch.ResumeLayout(false);
            this.SchedulePanelBarSearch.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BttnSearchBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelBox;
        private System.Windows.Forms.Panel CalendarPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button DeleteBttnScedule;
        private System.Windows.Forms.Button StartBttnSchedule;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CheckingDataSchedule;
        private System.Windows.Forms.Panel TimeForDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxAP;
        private System.Windows.Forms.ComboBox comboBoxHrs;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel UserSchedAddPanelAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox PictureBoxScheduleAddCalendar;
        private System.Windows.Forms.Label NameOfUserAddCalendar;
        private System.Windows.Forms.Panel NoAssignedAddSched;
        private System.Windows.Forms.Label NoAssSchedLabel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.PictureBox BttnSearchBar;
        private System.Windows.Forms.TextBox SearchBoxCalender;
        private System.Windows.Forms.Panel SchedulePanelBarSearch;
        private System.Windows.Forms.Label LabelNoSearchCalendar;
        private System.Windows.Forms.Panel CalendarTime;
        private System.Windows.Forms.Panel ScheduleUserPanel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel ScheduleUserPanel2;
        private System.Windows.Forms.Label TimeCalendarLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label UserNoSchedule;
    }
}


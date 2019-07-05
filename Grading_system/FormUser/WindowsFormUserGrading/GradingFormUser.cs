using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesOfFirstCome;
using ClassUserForm;
using LoadingStateMentForGradingAccount;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormUserGrading
{
    public partial class GradingFormUser : Form
    {
        protected static string[] arrComboBox = new string[] { "comboBoxHrs", "comboBoxMnt", "comboBoxAP", "TimeRangeHrs", "TimeRangeMnt" };
        protected int handlingIdInToProtected;
        protected LoadingScreen loadingOrWaiting = new LoadingScreen();
        protected Calendar calendarClass = new Calendar();
        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                return cp;
            }
        }

        public void dragingIdUserLogin(int handling_number_user)
        {
            handlingIdInToProtected = handling_number_user;
        }
        public GradingFormUser()
        {
            InitializeComponent();


            using (System.Drawing.Drawing2D.GraphicsPath pathGrap = new System.Drawing.Drawing2D.GraphicsPath()) {
                pathGrap.AddEllipse(7, 0, pictureUserBox.Width - 15, pictureUserBox.Height);
                Region region = new Region(pathGrap);
                pictureUserBox.Region = region;

                //THIS IS CALENDAR REGION IMAGE FOR ADD SCHEDULE...................................
                using (System.Drawing.Drawing2D.GraphicsPath drawCalendarImg = new GraphicsPath()) {
                    drawCalendarImg.AddEllipse(0, 0, PictureBoxScheduleAddCalendar.Width, PictureBoxScheduleAddCalendar.Height);
                    System.Drawing.Region region2Calendar = new Region(drawCalendarImg);
                    PictureBoxScheduleAddCalendar.Region = region2Calendar;
                }
            }



            //MOUSEMOVE THE FORM BOX...................................................
            Point pointHandleFirst = new Point(0, 0);
            bool conditions = false;
            panelMoveForm.MouseMove += new System.Windows.Forms.MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (conditions != false) {
                    Point setToPoint = PointToScreen(e.Location);
                    this.Location = new Point(setToPoint.X - pointHandleFirst.X, setToPoint.Y - pointHandleFirst.Y);
                }
            });
            panelMoveForm.MouseUp += new System.Windows.Forms.MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                conditions = false;
            });
            panelMoveForm.MouseDown += new System.Windows.Forms.MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                conditions = true;
                pointHandleFirst = new Point(e.X, e.Y);
            });


            //HIDE AND OUT THE PANELS IN PANELBOX and
            //Click the button for example 'Reports' the Paint Graphics will follow-
            //-Where ever the 'Reports' is there.......................
            DrawLine2Final.Paint += new System.Windows.Forms.PaintEventHandler(paintDrawLine2);
            foreach (Control buttonsClick in navigator.Controls) {
                if (buttonsClick.GetType() == typeof(Button)) {
                    buttonsClick.Click += new System.EventHandler((object sender, EventArgs e) => {
                        Button bttn = (Button)sender;
                        DrawLine2Final.Location = new Point(0, bttn.Location.Y);

                        foreach (Panel panelBoxs in PanelBox.Controls)
                        {
                            if (bttn.Name != panelBoxs.AccessibleDescription.ToString())
                            {
                                panelBoxs.Visible = false;
                            }
                            else
                            {
                                panelBoxs.Visible = true;
                            }
                        }
                    });
                }
            }



            //TIME SHOW IN CALENDAR CLASS.......................................
            string Hrs, Mmn, Sec, AP = "";
            Timer timerCalendar = new Timer();
            timerCalendar.Interval = 1000;
            timerCalendar.Start();
            timerCalendar.Tick += (object sender, EventArgs e) =>
            {
                TimeCalendarLabel.Controls.Clear();
                int hrs = System.DateTime.Now.Hour;
                int mmn = System.DateTime.Now.Minute;
                int second = System.DateTime.Now.Second;

                //HOURS......................................
                if (hrs > 11)
                {
                    if (hrs <= 12)
                    {
                        Hrs = String.Format("{0}", (hrs - 12));
                    }
                    else
                    {
                        if (hrs <= 21)
                        {
                            Hrs = String.Format("0{0}", (hrs - 12));
                        }
                        else {
                            Hrs = String.Format("{0}", (hrs - 12));
                        }
                    }

                    AP = "PM";
                }
                else
                {
                    if (hrs < 10)
                    {
                        Hrs = String.Format("0{0}", hrs);
                    }
                    else
                    {
                        Hrs = String.Format("{0}", hrs);
                    }

                    AP = "AM";
                }

                //MINUTES......................................
                if (mmn < 10)
                {
                    Mmn = String.Format("0{0}", mmn);
                }
                else
                {
                    Mmn = String.Format("{0}", mmn);
                }

                //SECOND......................................
                if (second < 10)
                {
                    Sec = "0" + second.ToString();
                }
                else
                {
                    Sec = second.ToString();
                }

                TimeCalendarLabel.Text = ":         " + Hrs + ":" + Mmn + ":" + Sec + " " + AP;

                Label label = new Label();
                label.Text = "Time:";
                label.Width = 65;
                label.Location = new System.Drawing.Point(0, 0);
                label.ForeColor = System.Drawing.Color.CornflowerBlue;
                TimeCalendarLabel.Controls.Add(label);
            };




            //Calender Controls.................................
            //Items Add the Hrs number, Minutes Number and AM PM AND DATE.................................
            comboBoxHrs.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxMnt.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxAP.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            TimeRangeHrs.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            TimeRangeMnt.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);

            NextToSeeRange.Click += new System.EventHandler(clickNextAndBack);
            BackToSeeTimeDate.Click += new System.EventHandler(clickNextAndBack);

            monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(MonthCalendar1_DateChanged);

            SearchBoxCalender.KeyUp += new System.Windows.Forms.KeyEventHandler(returnDataCalendar);

            StartBttnSchedule.Click += new System.EventHandler(AddScheduleCalendar);

            DeleteBttnScedule.Click += new System.EventHandler(DeleteScheduleCalendarForAdd);

            int[] arrComboHrs = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            int[] arrHrsRange = new int[] { 0, 01, 02, 03, 04, 05 };
            int[] arrMntRange = new int[] { 20, 30, 40, 50 };
            for (int numberCountCom = arrComboBox.Length - 1; numberCountCom >= 0; numberCountCom--)
            {
                switch (arrComboBox[numberCountCom])
                {
                    case "comboBoxHrs":
                        ComboBox comboHrs = (ComboBox)(DateTime.Controls[arrComboBox[numberCountCom]]);
                        foreach (int handleHrs in arrComboHrs)
                        {
                            comboHrs.Items.Add(handleHrs);
                        }
                        break;
                    case "comboBoxMnt":
                        ComboBox comboMNT = (ComboBox)(DateTime.Controls[arrComboBox[numberCountCom]]);
                        foreach (int handleMNT in arrMntRange)
                        {
                            comboMNT.Items.Add(handleMNT);
                        }
                        break;
                    case "comboBoxAP":
                        comboBoxAP.Items.Add("AM");
                        comboBoxAP.Items.Add("PM");
                        break;
                    case "TimeRangeHrs":
                        ComboBox comboHrsRange = (ComboBox)(rangeTimePan.Controls[arrComboBox[numberCountCom]]);
                        if ((comboHrsRange.GetType() == typeof(ComboBox))) {
                            foreach (int handleHrsArr in arrHrsRange) {
                                comboHrsRange.Items.Add(handleHrsArr);
                            }
                        }
                        break;
                    case "TimeRangeMnt":
                        ComboBox comboMntRange = (ComboBox)(rangeTimePan.Controls[arrComboBox[numberCountCom]]);
                        if ((comboMntRange.GetType() == typeof(ComboBox)))
                        {
                            foreach (int handleMntArr in arrMntRange)
                            {
                                comboMntRange.Items.Add(handleMntArr);
                            }
                        }
                        break;
                }
            }

            //For Searching Box Calender BTTN.........................................
            BttnSearchBar.Click += new System.EventHandler(SearchBttnCalenderToSched);
            this.LoadParseDataInSchedulePanelBarSearch();
        }



        //THIS IS THE DRAWLINE FOR CALENDAR, REPORTS, GRADING AND CONTROLS PAINT.......................
        public void paintDrawLine2(object sender, PaintEventArgs e)
        {
            Panel pan = (Panel)sender;
            Graphics grapDraw = e.Graphics;
            Pen penDraw = new Pen(ColorTranslator.FromHtml("#17A589"), 8);
            grapDraw.DrawLine(penDraw, 0, 0, 0, pan.Height);
            grapDraw.Dispose();
        }

        //GET ARRIVAL THE DATA WHITOUT SEARCHING IT CALENDAR..................................
        public static bool GetData = true;
        protected static Calendar classCalendar = new Calendar();
        protected List<CalendarList> DataListGetAndSearch = new List<CalendarList>();
        protected void LoadParseDataInSchedulePanelBarSearch() {
            GetData = true;
            DataListGetAndSearch = classCalendar.GetAllDataIn_searchbargradingaccounts();
            CreatingControlUserSearchAndGet();
        }

        //KEYDOWN THE SearchBoxCalender TO RETURN THE DATA VALUE.........................
        protected void returnDataCalendar(object controls, KeyEventArgs e) {
            Control textbox = (Control)controls;
            if (textbox.GetType() == typeof(TextBox) || (textbox is TextBox)) {
                if (textbox.Text.Length == 0) {
                    GetData = true;
                    DataListGetAndSearch = classCalendar.GetAllDataIn_searchbargradingaccounts();
                    CreatingControlUserSearchAndGet();
                }
            }
        }

        //This is the searching function in SEARCH CALENDAR TO USER TO SET A SCHEDULE........................
        private void SearchBttnCalenderToSched(object controls, EventArgs e) {
            GetData = false;
            DataListGetAndSearch = classCalendar.GettingSearchText(SearchBoxCalender.Text);
            CreatingControlUserSearchAndGet();
        }

        //CREATING Panel, PictureBox, Label for searching user calendar.....................
        private void CreatingControlUserSearchAndGet() {
            SchedulePanelBarSearch.Controls.Clear();
            int numberWillChangeNumber = 30;
            foreach (var GetEachData in DataListGetAndSearch)
            {
                if (GetEachData.ErrorCondition == "")
                {
                    if (GetEachData.numberCondition != 0)
                    {
                        Panel pan;
                        if (numberWillChangeNumber == 30)
                        {

                            pan = new Panel
                            {
                                Name = (GetData != false ? GetEachData.UsernameGet : GetEachData.UsernameSearch) + "_Search",
                                AccessibleDescription = (GetData != false ? GetEachData.UsernameGet : GetEachData.UsernameSearch),
                                AccessibleName = (GetData != false ? GetEachData.FirstLastNameGet : GetEachData.FirstLastNameSearch),
                                Width = 290,
                                Height = 50,
                                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                                Location = new Point(5, 20),
                                Cursor = Cursors.Hand,
                            };
                            pan.Click += new System.EventHandler(ClickedDataSearchToCalendar);
                        }
                        else
                        {
                            pan = new Panel
                            {
                                Name = (GetData != false ? GetEachData.UsernameGet : GetEachData.UsernameSearch) + "_Search",
                                AccessibleDescription = (GetData != false ? GetEachData.UsernameGet : GetEachData.UsernameSearch),
                                AccessibleName = (GetData != false ? GetEachData.FirstLastNameGet : GetEachData.FirstLastNameSearch),
                                Width = 290,
                                Height = 50,
                                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                                Location = new Point(5, numberWillChangeNumber),
                                Cursor = Cursors.Hand
                            };
                            pan.Click += new System.EventHandler(ClickedDataSearchToCalendar);
                        }
                        SchedulePanelBarSearch.Controls.Add(pan);

                        PictureBox pictureSearch = new PictureBox
                        {
                            Name = (GetData != false ? GetEachData.UsernameGet : GetEachData.UsernameSearch) + "_PictureSearch",
                            AccessibleDescription = (GetData != false ? GetEachData.ImageuserGet : GetEachData.ImageuserSearch),
                            Image = Image.FromFile((GetData != false ? GetEachData.ImageuserGet : GetEachData.ImageuserSearch)),
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Location = new Point(5, 3),
                            Size = new Size(45, 45)
                        };

                        pan.Controls.Add(pictureSearch);
                        using (System.Drawing.Drawing2D.GraphicsPath pathPicture = new GraphicsPath())
                        {
                            pathPicture.AddEllipse(0, 0, pictureSearch.Width, pictureSearch.Height);
                            Region region = new Region(pathPicture);
                            pictureSearch.Region = region;
                        }

                        Label label = new Label()
                        {
                            Text = (GetData != false ? GetEachData.FirstLastNameGet : GetEachData.FirstLastNameSearch),
                            Location = new Point(55, 20),
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                            BackColor = System.Drawing.ColorTranslator.FromHtml("#1C2833"),
                        };
                        pan.Controls.Add(label);
                        numberWillChangeNumber += 45;
                    }
                    else
                    {
                        var labelIfFind = new Label()
                        {
                            Text = "No Name Found.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Width = 200,
                            Location = new Point(95, 196)
                        };
                        SchedulePanelBarSearch.Controls.Add(labelIfFind);

                    }
                }
                else {
                    MessageBox.Show("You Need to Check Your Connection.");
                }
            }
        }


        //This is the click event for search bar TO SET A SCHEDULE TO USER.........................................
        private static string usernameAccesssibleToDatabase;
        private static List<CalendarList> GetTheScheduleOfUserClicked = new List<CalendarList>();
        protected async void ClickedDataSearchToCalendar(object controls, EventArgs e) {
            NoAssignedAddSched.Controls.Clear();
            NoAssignedAddSched.AutoScroll = true;
            int numberCountPanel = 8;
            int numberForAdmins = 0;
            int numberScanIfMax = 0;
            int numberScanMinAdmin = 0;
            string doneToScannAdmin = "";
            loadingOrWaiting.Show();
            this.Hide();

            int numberCountScroll = 0;
            VScrollBar scroll = new VScrollBar() {
                Name = "scrollCalendarSchedAdd",
                Dock = DockStyle.Right,
                Enabled = false,
                Visible = true
            };
            this.NoAssignedAddSched.Controls.Add(scroll);

            YourScheduleAndOther.Visible = false;
            SetScheduleForOtherUser.Visible = true;
            Panel pans = (Panel)controls;
            usernameAccesssibleToDatabase = pans.AccessibleDescription.ToString();
            if (usernameAccesssibleToDatabase.Length != 0) {
                //ITO YUNG MAG LALAGAY PALANG NG IMAGE AND NAME NG SINELECT NI USER
                //PARA MAKAPAG SET NG SCEDULE DUN SA PINILI NYA.....................
                foreach (Control LabelImage in pans.Controls) {
                    if (LabelImage.GetType() == typeof(PictureBox)) {
                        PictureBoxScheduleAddCalendar.Image = Image.FromFile(LabelImage.AccessibleDescription);
                    } else if (LabelImage is Label) {
                        NameOfUserAddCalendar.Text = LabelImage.Text;

                        //ITO YUNG KUKUHAIN NA YUNG SCHEDULE NG CLINICK NI USER PARA MAG SET NG SCHED 
                        //TITIGNAN NYA KUNG MERON BANG SCHED OR WALA PA.....................
                        List<CalendarList> listGet = (await Task.Run(() => calendarClass.GetSchedUserClicked(usernameAccesssibleToDatabase)).ConfigureAwait(true));
                        foreach (var calendarlist in listGet) {
                            if (String.IsNullOrEmpty(calendarlist.ErrCheck))
                            {
                                if (calendarlist.numberCount != 0)
                                {
                                    GetTheScheduleOfUserClicked = await Task.Run(() => calendarClass.getAllDataInAddSched(usernameAccesssibleToDatabase));

                                    //SCANNING KUNG MAY ADMIN BA NA SCHEDULE OR WALA...............
                                    int scanIfHaveAdminOrNo(List<CalendarList> scanForAdmin)
                                    {
                                        int scanAdminHave = 0;
                                        doneToScannAdmin = "Have";
                                        foreach (var scan in scanForAdmin)
                                        {
                                            numberScanIfMax++;
                                            if (String.IsNullOrEmpty(scan.HandlingAdmin) != true)
                                            {
                                                scanAdminHave++;
                                            }
                                        }
                                        doneToScannAdmin = "";
                                        return scanAdminHave;
                                    }

                                    Task<int> intFunctionAdmin = new Task<int>(() => scanIfHaveAdminOrNo(GetTheScheduleOfUserClicked));
                                    intFunctionAdmin.Start();

                                    numberForAdmins = await intFunctionAdmin.ConfigureAwait(true);
                                    doneToScannAdmin = "";

                                    if (doneToScannAdmin != "Have") {
                                        if (numberForAdmins <= 0)
                                        {
                                            //SHOW AND DESIGN OF SCHEDULE OF USER ARRIVED............................
                                            foreach (var showcalendar in GetTheScheduleOfUserClicked)
                                            {
                                                if (showcalendar.ErrCheck == "")
                                                {
                                                    numberCountScroll++;
                                                    if (numberCountScroll > 3)
                                                    {
                                                        scroll.Visible = false;
                                                    }

                                                    functionDelegateControls createCon = new functionDelegateControls(createControlsSched);
                                                    createCon.Invoke(showcalendar.NameUserWhoAdded, showcalendar.DateTimeRange,
                                                        showcalendar.ImageUserWhoAdded, numberCountPanel, "");

                                                    if (numberCountPanel == 8)
                                                    {
                                                        loadingOrWaiting.Hide();
                                                        this.Show();
                                                    }
                                                    numberCountPanel += 93;
                                                }
                                                else
                                                {
                                                    loadingOrWaiting.Show();
                                                    this.Hide();
                                                }
                                            }
                                        }
                                        else {
                                            //SHOWING THE ADMIN SCHED ASSIGNED TO THE USER YOU CLICKED................
                                            foreach (var ShowAdminSched in GetTheScheduleOfUserClicked) {
                                                if (ShowAdminSched.ErrCheck == "") {
                                                    if (ShowAdminSched.HandlingAdmin == "ADMIN") {
                                                        numberForAdmins--;
                                                        numberScanMinAdmin++;
                                                        numberCountScroll++;

                                                        functionDelegateControls delegFunc = new functionDelegateControls(createControlsSched);
                                                        delegFunc.Invoke(ShowAdminSched.NameUserWhoAdded, ShowAdminSched.DateTimeRange,
                                                            ShowAdminSched.ImageUserWhoAdded, numberCountPanel, ShowAdminSched.HandlingAdmin);

                                                        numberCountPanel += 93;
                                                    }
                                                }
                                            }

                                            if (numberForAdmins == 0) { 
                                                //SHOW THE LEAST OF THE HOLE DATABASE SCHEDULE FOR THE USER YOU CLICKED....
                                                if (numberScanMinAdmin != numberScanIfMax)
                                                {
                                                    numberCountPanel = 8;
                                                    string asd = "";
                                                    //SHOW AND DESIGN OF SCHEDULE OF USER ARRIVED..............
                                                    foreach (var showcalendar in GetTheScheduleOfUserClicked)
                                                    {
                                                        if (showcalendar.ErrCheck == "")
                                                        {
                                                            if (showcalendar.HandlingAdmin == "" || showcalendar.HandlingAdmin is null) {
                                                                if (asd == "") {
                                                                    asd = "have";
                                                                    numberCountScroll++;
                                                                    if (numberCountScroll > 3)
                                                                    {
                                                                        scroll.Visible = false;
                                                                    }

                                                                    functionDelegateControls createCon = new functionDelegateControls(createControlsSched);

                                                                    await Task.Run(() => createCon.Invoke(showcalendar.NameUserWhoAdded, showcalendar.DateTimeRange,
                                                                        showcalendar.ImageUserWhoAdded, numberCountPanel, ""));

                                                                    if (numberCountPanel == 8)
                                                                    {
                                                                        loadingOrWaiting.Hide();
                                                                        this.Show();
                                                                    }
                                                                    numberCountPanel += 93;
                                                                    asd = "";
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            loadingOrWaiting.Show();
                                                            this.Hide();
                                                        }
                                                    }
                                                }
                                                else {
                                                    loadingOrWaiting.Hide();
                                                    this.Show();
                                                }
                                            }
                                        }
                                    }

                                }
                                else
                                {
                                    loadingOrWaiting.Hide();
                                    this.Show();
                                    Label label = new Label()
                                    {
                                        Text = "No Assigned Schedule.",
                                        ForeColor = System.Drawing.Color.CornflowerBlue,
                                        Location = new Point(159, 141),
                                        Width = 200,
                                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                                    };
                                    NoAssignedAddSched.Controls.Add(label);
                                }
                            }
                            else {
                                loadingOrWaiting.Hide();
                                this.Show();
                                MessageBox.Show("You must check your internet");

                                Label label = new Label()
                                {
                                    Text = "Please Check Your Connection.",
                                    ForeColor = System.Drawing.Color.CornflowerBlue,
                                    Location = new Point(130, 141),
                                    Width = 400,
                                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                                };
                                NoAssignedAddSched.Controls.Add(label);
                            }
                        }
                    }
                }
            }

        }


        protected delegate void functionDelegateControls(string NameUserWhoAdded, string DateTimeRange, string ImageUserWhoAdded, int numberCountPanel, string HandlingAdmin);
        protected void createControlsSched(string NameUserWhoAdded, string DateTimeRange, string ImageUserWhoAdded, int numberCountPanel, string HandlingAdmin) {

            Panel panAdd = new Panel
            {
                Name = NameUserWhoAdded + "_SchedAdd",
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(401, 86),
                Location = new Point(8, (numberCountPanel == 8 ? 8 : numberCountPanel))
            };

            NoAssignedAddSched.Controls.Add(panAdd);

            PictureBox boxPic = new PictureBox
            {
                Name = "pictureBoxScheduleAdd",
                Size = new Size(55, 32),
                Location = new Point(10, 7),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(ImageUserWhoAdded)
            };
            panAdd.Controls.Add(boxPic);


            Panel panName = new Panel
            {
                Name = "PanName",
                Size = new Size(323, 32),
                Location = new Point(71, 7),
                BackColor = ColorTranslator.FromHtml("#1C2833")
            };
            panAdd.Controls.Add(panName);

            Label labelNameinPanName = new Label
            {
                Name = "NameLabel",
                Text = NameUserWhoAdded,
                ForeColor = System.Drawing.Color.CornflowerBlue,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(3, 8),
                Width = 323
            };
            panName.Controls.Add(labelNameinPanName);



            Panel panSched = new Panel
            {
                Name = "PanSched",
                Size = new Size(323, 32),
                Location = new Point(71, 46),
                BackColor = ColorTranslator.FromHtml("#1C2833")
            };
            panAdd.Controls.Add(panSched);

            Label labelSchedinPanSched = new Label
            {
                Name = "SchedLabel",
                Text = DateTimeRange,
                ForeColor = System.Drawing.Color.CornflowerBlue,
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Location = new Point(3, 8),
                Width = 300
            };
            panSched.Controls.Add(labelSchedinPanSched);

            CheckBox checkControlWantToDelete = new CheckBox {
                Name = "HandleDataOfDate_" + DateTimeRange,
                AccessibleDescription = DateTimeRange.ToString(),
                Size = new Size(15, 14),
                Text = "",
                Location = new Point(300, 10),
                Visible = false
            };

            checkControlWantToDelete.Click += new System.EventHandler(CheckBoxCheckForDeleteData);
            panSched.Controls.Add(checkControlWantToDelete);

            Button bttnSched = new Button
            {
                FlatStyle = FlatStyle.Flat,
                ForeColor = ColorTranslator.FromHtml("#B3B6B7"),
                Text = "Sched:",
                Size = new Size(55, 32),
                Location = new Point(10, 46),
                BackColor = ColorTranslator.FromHtml("#1C2833")
            };
            bttnSched.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CD6155");
            bttnSched.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1C2833");
            bttnSched.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C2833");
            panAdd.Controls.Add(bttnSched);
        }





        protected static string[] CalendarRange = new string[3] { "", "", "" };
        protected static string[] DateTimeRange = new string[] { "", "", "", "", "" };
        protected static string handlingConverterMonth = "";
        public static string[] CalendarConverter = new string[] { "Januany", "February", "March", "April", "May", 
        "June", "July", "August", "September", "October", "November", "December" };
        public static string checkTime = "";
        public static string checkDate = "";
        public void SelectedIndex_AllComboBoxCalenderSet(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.AccessibleName == "HrsDate")
            {
                DateTimeRange[0] = comboBoxHrs.SelectedItem.ToString();
            }
            else if (comboBox.AccessibleName == "MntDate")
            {
                DateTimeRange[1] = comboBoxMnt.SelectedItem.ToString();
            }
            else if (comboBox.AccessibleName == "APDate")
            {
                DateTimeRange[2] = comboBoxAP.SelectedItem.ToString();

            } else if (comboBox.AccessibleName == "HrsDateRange") {

                DateTimeRange[3] = TimeRangeHrs.SelectedItem.ToString();

            } else if (comboBox.AccessibleName == "MntDateRange") {
                DateTimeRange[4] = TimeRangeMnt.SelectedItem.ToString();
            }

            checkTime = DateTimeRange[0] + ":" + DateTimeRange[1] + " " + DateTimeRange[2] + " "
                + (String.IsNullOrEmpty(DateTimeRange[3]) && String.IsNullOrEmpty(DateTimeRange[4]) ? "" : "/ ")
                + DateTimeRange[3]+":"+ DateTimeRange[4]+" "+(DateTimeRange[3] == "0" || 
                DateTimeRange[3] == "" ? "Mnts": (DateTimeRange[3] == "1" ? "Hr":"Hrs"));
            CheckingWay();
        }

        //CLICK NEXT AND BACK FOR 'Set Time' in Add Schedule......................................
        protected void clickNextAndBack(object controls, EventArgs e) {
            PictureBox picCon = (PictureBox)controls;
            if (picCon.Name == "NextToSeeRange")
            {
                DateTime.Visible = false;
                rangeTimePan.Visible = true;
                picCon.Visible = false;
                BackToSeeTimeDate.Visible = true;
            }
            else if(picCon.Name == "BackToSeeTimeDate")
            {
                rangeTimePan.Visible = false;
                DateTime.Visible = true;
                picCon.Visible = false;
                NextToSeeRange.Visible = true;
            }
        }


        //MONTH CALENDAR SELECT 'SelectionRange.Start'..............................
        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string stringSelectionRange = monthCalendar1.SelectionStart.ToString();
            string handle = "";
            int countNumberCalendar = 0;
            bool conditionNumberDate = false;
            for (int CountSelection = 0; CountSelection < stringSelectionRange.Length; CountSelection++)
            {
                if (conditionNumberDate != true)
                {
                    switch (stringSelectionRange[CountSelection])
                    {
                        case '/':
                            CalendarRange[countNumberCalendar] = handle;
                            countNumberCalendar++;
                            handle = "";
                            break;
                        case ' ':
                            conditionNumberDate = true;
                            CalendarRange[countNumberCalendar] = handle;
                            break;
                        default:
                            handle += stringSelectionRange[CountSelection];
                            break;
                    }
                }

                if (CountSelection >= stringSelectionRange.Length-1) {
                    for (int numberCountConverter = CalendarConverter.Length;numberCountConverter > 0; numberCountConverter--) {
                        if (numberCountConverter == Convert.ToInt32(CalendarRange[0])) {
                            handlingConverterMonth = CalendarConverter[numberCountConverter-1];
                        }
                    }
                }
            }

            checkDate = handlingConverterMonth + " " + CalendarRange[1] + ", " + CalendarRange[2] + " ";
            CheckingWay();
        }

        //THIS IS CHECKING WAY TO CHECK YOUR SETTING SCHEDULE TO THE USER................................
        protected void CheckingWay()
        {
            CheckingDataSchedule.Text = checkDate + " " + checkTime;
        }




        //ITO YUNG ADD SCHEDULE FOR THE USER...............................................................
        protected List<CalendarList> ScheduleListToAddAndDelete = new List<CalendarList>();
        protected async void AddScheduleCalendar(object control, EventArgs e) {
            loadingOrWaiting.Show();
            this.Hide();
            int numberCountPanels = 8;
            int count_number = 0;

            string CheckScanningSched = "await";


            for (int countMonthRange = 0; countMonthRange < CalendarRange.Length; countMonthRange++) {
                if (String.IsNullOrEmpty(CalendarRange[countMonthRange]) != true) {
                    count_number++;
                }
                if (CalendarRange.Length - 1 <= countMonthRange)
                {
                    if (count_number == 3) {
                        count_number = 0;
                        int LengthOfArray = DateTimeRange.Length;
                        while (LengthOfArray > 0) {
                            LengthOfArray -= 1;
                            if (DateTimeRange[LengthOfArray] != "") {
                                count_number++;
                            }
                            if (LengthOfArray == 0) {
                                if (count_number > 4)
                                {
                                    ScheduleListToAddAndDelete = new List<CalendarList>();
                                    ScheduleListToAddAndDelete.Add(new CalendarList
                                    {
                                        UsernameOfUserThatWillAdd = usernameAccesssibleToDatabase,
                                        NameUserWhoAdded = "Kyle Velarde",
                                        ImageUserWhoAdded = "C:/Users/Kyle_velarde/Desktop/Microsoft c# system/icons/219953-people/png/user-16.png",
                                        DateTimeRange = checkDate + checkTime,
                                        calendarRangeAddMonth = CalendarRange[0],
                                        calendarRangeAddConvert = handlingConverterMonth,
                                        calendarRangeAddDay = CalendarRange[1],
                                        calendarRangeAddYear = CalendarRange[2],
                                        DateTimeRangeAddHrs = DateTimeRange[0],
                                        DateTimeRangeAddMnt = DateTimeRange[1],
                                        DateTimeRangeAddAP = DateTimeRange[2],
                                        SetDurationTimeAddHrs = DateTimeRange[3],
                                        SetDurationTimeAdd = DateTimeRange[4]
                                    });

                                    //CHECK SCANNING SCHEDULE..............................
                                    CheckScanningSched = await Task.Run(() => calendarClass.ScheduleCheckScanning(ScheduleListToAddAndDelete)).ConfigureAwait(true);

                                    if (CheckScanningSched == "")
                                    {

                                        //ADD SCHEDULE AND RETURN THE TABLE BY USING A LIST....................
                                        List<CalendarList> awaitProgToAdd = await Task.Run(() => calendarClass.functionThatWillAddSched(ScheduleListToAddAndDelete)).ConfigureAwait(true);

                                        //CLEARING THE DATA ADD SCHDULE IN MonthCalendar and to Set time..................
                                        CheckingDataSchedule.Text = "None";
                                        handlingConverterMonth = "";
                                        checkTime = "";
                                        checkDate = "";
                                        for (int countCalendarRange = 0; CalendarRange.Length > countCalendarRange; countCalendarRange++)
                                        {
                                            CalendarRange[countCalendarRange] = "";
                                            if (CalendarRange.Length <= countCalendarRange + 1)
                                            {
                                                for (int countDateTimeRange = DateTimeRange.Length - 1; countDateTimeRange > -1; countDateTimeRange--)
                                                {

                                                    if (countDateTimeRange > 2)
                                                    {
                                                        ComboBox combo = (ComboBox)(rangeTimePan.Controls[arrComboBox[countDateTimeRange]]);
                                                        combo.Text = "";
                                                    }
                                                    else
                                                    {
                                                        ComboBox combo = (ComboBox)(DateTime.Controls[arrComboBox[countDateTimeRange]]);
                                                        combo.Text = "";
                                                    }

                                                    DateTimeRange[countDateTimeRange] = "";
                                                    if (countDateTimeRange == 0)
                                                    {
                                                        //RETURN THE DATA OUTPUT AFTER THE DATA IS SUCCESFULLY ADDED IN FROM USER ADD SCEDHULE..
                                                        foreach (var Show in awaitProgToAdd)
                                                        {

                                                            if (Show.ErrCheck == "SuccInserting")
                                                            {
                                                                NoAssignedAddSched.Controls.Clear();

                                                                int numberCountScroll = 0;
                                                                VScrollBar scroll = new VScrollBar()
                                                                {
                                                                    Name = "scrollCalendarSchedAdd",
                                                                    Dock = DockStyle.Right,
                                                                    Enabled = false,
                                                                    Visible = true
                                                                };
                                                                this.NoAssignedAddSched.Controls.Add(scroll);


                                                                foreach (var showcalendar in await Task.Run(() => calendarClass.getAllDataInAddSched(usernameAccesssibleToDatabase)))
                                                                {
                                                                    if (showcalendar.ErrCheck == "")
                                                                    {
                                                                        numberCountScroll++;

                                                                        if (numberCountScroll > 3)
                                                                        {
                                                                            scroll.Visible = false;
                                                                        }

                                                                        functionDelegateControls variableSched = new functionDelegateControls(createControlsSched);
                                                                        variableSched.Invoke(showcalendar.NameUserWhoAdded,
                                                                            showcalendar.DateTimeRange, showcalendar.ImageUserWhoAdded, numberCountPanels, "");

                                                                        if (numberCountPanels == 8)
                                                                        {
                                                                            loadingOrWaiting.Hide();
                                                                            this.Show();
                                                                        }

                                                                        numberCountPanels += 93;
                                                                    }
                                                                    else
                                                                    {
                                                                        loadingOrWaiting.Show();
                                                                        this.Hide();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //HERE IS AN ERROR MESSAGE IF NO INTERNET...............
                                                loadingOrWaiting.Show();
                                                this.Hide();
                                            }
                                        }
                                    }
                                    else {

                                        //GARBAGE MESSAGE FROM SCHEDULE SCANNING..........................
                                        string[] messageErr = new string[] {
                                            "The year need the same as our year to today.",
                                            "You have only 'ONE' add schedule from this user.",
                                            "The current schedule has already have in this user.",
                                            "This month is already done by the day.",
                                            "This day is already passed.",
                                            "ISSUE",
                                            "ISSUES",
                                            "The minute is done look at your dateTime.",
                                            "The Hrs is done look at your dateTime."
                                        };

                                        for (int scanningMessageErr = messageErr.Length-1; scanningMessageErr >= 0; scanningMessageErr-=1) {
                                            if (CheckScanningSched == messageErr[scanningMessageErr]) {
                                                MessageBox.Show(messageErr[scanningMessageErr]);
                                                loadingOrWaiting.Hide();
                                                this.Show();
                                            }
                                        }
                                    }


                                }
                                else {
                                    MessageBox.Show("Please Complete a time");

                                    loadingOrWaiting.Hide();
                                    this.Show();
                                }
                            }
                        }
                    }
                    else {
                        MessageBox.Show("Sorry Please Select A Month");
                        loadingOrWaiting.Hide();
                        this.Show();
                    }
                }
            }
        }





        //ITO NAMAN YUNG DELETE FUNCTION DUN SA ADD SCHEDULE DUN SA USER.......................................
        public static bool condition_checkHideShow = false;
        protected static string conditionNameCheck;
        protected static string conditionToHaveInCheck = "";
        private static int conditionToEndCheck = 0;
        public async void DeleteScheduleCalendarForAdd(object controls, EventArgs e) {
            int conditionToFinalCheckEnd = 0;
            if (condition_checkHideShow != true)
            {
                condition_checkHideShow = true;
            }
            else {
                condition_checkHideShow = false;
            }


            //ITO YUNG PARA MA DETECT YUNG SCHEDULE NI USER AND ILALABAS YUNG CHECKBOX PARA MA DELETE...................
            foreach (Control showCheckBox1 in NoAssignedAddSched.Controls) {
                if (showCheckBox1.GetType() == typeof(Panel)) {
                    if (showCheckBox1.Name == "Kyle Velarde_SchedAdd") {
                        foreach (Control panelScheds in showCheckBox1.Controls) {
                            if (panelScheds is Panel) {
                                if (panelScheds.Name == "PanSched") {
                                    foreach (Control checkboxFindFinal in panelScheds.Controls) {
                                        if (checkboxFindFinal.GetType() == typeof(CheckBox)) {
                                            checkboxFindFinal.Visible = condition_checkHideShow;
                                            conditionNameCheck = showCheckBox1.Name;
                                            if (String.IsNullOrEmpty(checkboxFindFinal.Text) != true)
                                            {
                                                conditionToHaveInCheck = "Have";

                                                loadingOrWaiting.Show();
                                                this.Hide();

                                                Task<string> task = new Task<string>(() => calendarClass.deleteFucntion(usernameAccesssibleToDatabase,
                                                 checkboxFindFinal.AccessibleDescription));
                                                task.Start();

                                                if ((await task.ConfigureAwait(true)) == "Check Your Connection.")
                                                {
                                                    MessageBox.Show("Check Your Connection");
                                                }
                                                else
                                                {

                                                    conditionToFinalCheckEnd++;
                                                }

                                            }



                                            if (conditionNameCheck == showCheckBox1.Name) {
                                                if (conditionToHaveInCheck != "") {
                                                    if (conditionToFinalCheckEnd == conditionToEndCheck) {

                                                        foreach (var checkIfHaveTable in (await Task.Run(() => calendarClass.GetSchedUserClicked(usernameAccesssibleToDatabase)).ConfigureAwait(true))) {
                                                            if (String.IsNullOrEmpty(checkIfHaveTable.ErrCheck) == true)
                                                            {
                                                                NoAssignedAddSched.Controls.Clear();
                                                                conditionToEndCheck = 0;
                                                                conditionNameCheck = "";
                                                                conditionToHaveInCheck = "";

                                                                int numberCountScroll = 0;
                                                                VScrollBar scroll = new VScrollBar()
                                                                {
                                                                    Name = "scrollCalendarSchedAdd",
                                                                    Dock = DockStyle.Right,
                                                                    Enabled = false,
                                                                    Visible = true
                                                                };
                                                                this.NoAssignedAddSched.Controls.Add(scroll);

                                                                if (checkIfHaveTable.numberCount == 1)
                                                                {

                                                                    int numberCountPanel = 8;

                                                                    List<CalendarList> listreturnValue = await Task.Run(() => calendarClass.getAllDataInAddSched(usernameAccesssibleToDatabase)).ConfigureAwait(true);
                                                                    try
                                                                    {
                                                                        foreach (var returnValue in listreturnValue)
                                                                        {
                                                                            numberCountScroll++;

                                                                            if (numberCountScroll > 3) {
                                                                                scroll.Visible = false;
                                                                            }

                                                                            functionDelegateControls returnfunctiondele = new functionDelegateControls(createControlsSched);
                                                                            returnfunctiondele.Invoke(returnValue.NameUserWhoAdded, returnValue.DateTimeRange,
                                                                                returnValue.ImageUserWhoAdded, numberCountPanel, "");
                                                                            numberCountPanel += 93;
                                                                        }
                                                                    }
                                                                    catch (Exception err)
                                                                    {
                                                                        MessageBox.Show(err.ToString());
                                                                    }
                                                                }
                                                                else {
                                                                    Label label = new Label()
                                                                    {
                                                                        Text = "No Assigned Schedule.",
                                                                        ForeColor = System.Drawing.Color.CornflowerBlue,
                                                                        Location = new Point(159, 141),
                                                                        Width = 200,
                                                                        Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                                                                    };
                                                                    NoAssignedAddSched.Controls.Add(label);
                                                                }

                                                                //TO RETURN THE FORM OF USER AND HIDE THE LOADING FORM..........
                                                                loadingOrWaiting.Hide();
                                                                this.Show();
                                                            }
                                                            else {
                                                                MessageBox.Show("Check Your Internet");
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //CHECKBOX FUNCTION TO CHECK USER WANT TO DELETE A SCHEDULE...............................
        public void CheckBoxCheckForDeleteData(object controls, EventArgs e) {
            CheckBox checkCon = (CheckBox)controls;
            if (String.IsNullOrEmpty(checkCon.Text) == true)
            {
                checkCon.Text = "CheckEraseSched";
                conditionToEndCheck += 1;
            }
            else {
                checkCon.Text = "";
                conditionToEndCheck -= 1;
            }
        }
    }
}

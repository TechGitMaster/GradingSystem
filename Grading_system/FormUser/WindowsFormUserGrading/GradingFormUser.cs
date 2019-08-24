using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
        protected static List<CalendarList> ListScheduleSelf = new List<CalendarList>();
        protected static string[] arrayInNavigator = new string[] { "", "", "", "" };
        private static string handleUsername = "", handleFirsLastName = "", handleImage = "", handleFirstNameOwn = "",
        handleImageOwnUser = "";
        protected System.Windows.Forms.Timer TimerSchedUserSelf = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer TimerReportSelf = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer timerGradingSub = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer TimerGrading = new System.Windows.Forms.Timer();
        protected System.Windows.Forms.Timer TimerControls = new System.Windows.Forms.Timer();

        private int NumberHandleCondition = 0;
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

            //CLICK TO VISIBLE THE READ STATION IN REPORT.................................
            EraseHideReportReadStation.Click += new System.EventHandler(
                (object controls, EventArgs e) => {
                    panelReadingStation.Visible = false; TimerReportSelf.Start();
                });


            //HIDE AND OUT THE PANELS IN PANELBOX and
            //Click the button for example 'Reports' the Paint Graphics will follow-
            //-Where ever the 'Reports' is there.......................
            DrawLine2Final.Paint += new System.Windows.Forms.PaintEventHandler(paintDrawLine2);

            TimerSchedUserSelf.Interval = 3000;
            TimerSchedUserSelf.Tick += async (object ob, EventArgs e) => {
                System.Windows.Forms.Timer time = (System.Windows.Forms.Timer)ob;
                time.Stop();

                ListScheduleSelf = await Task.Run(() => calendarClass.GetAllSchedThisUser("vee")).ConfigureAwait(true);

                Thread th = new Thread(() => {
                    int number = 0;
                    foreach (var count in ListScheduleSelf)
                    {
                        if (count.ErrCheck == "")
                        {
                            if (count.numberCount > 0)
                            {
                                number++;
                            }
                        }
                    }
                    if (NumberHandleCondition != number)
                    {
                        Action ac = () => this.intervalVoidShowAndScanSched("vee");
                        this.BeginInvoke(ac);
                    }
                    else {
                        Action times = () => time.Start();
                        this.BeginInvoke(times);
                    }
                });
                th.Start();
            };
            foreach (Control buttonsClick in navigator.Controls) {
                if (buttonsClick.GetType() == typeof(Button)) {

                    if (buttonsClick.Name is "Calendar") {
                        ScheduleUserPanel2.Controls.Clear();
                        this.intervalVoidShowAndScanSched("vee");
                    }

                    buttonsClick.Click += new System.EventHandler((object sender, EventArgs e) => {
                        int numberDetectIf = 0, numberCountToBack = 0;
                        Button bttn = (Button)sender;
                        DrawLine2Final.Location = new Point(0, bttn.Location.Y);

                        foreach (Panel panelBoxs in PanelBox.Controls)
                        {
                            if (bttn.Name != panelBoxs.AccessibleDescription.ToString())
                            {
                                panelBoxs.Visible = false;
                                numberDetectIf++;
                            }
                            else
                            {
                                panelBoxs.Visible = true;
                                switch (numberDetectIf) {
                                    case 1:
                                        if (arrayInNavigator[numberDetectIf] == "")
                                        {
                                            arrayInNavigator[numberDetectIf] = "Have";
                                            TimerSchedUserSelf.Stop();
                                            TimerReportSelf.Stop();
                                            TimerControls.Stop();
                                            while (numberCountToBack < arrayInNavigator.Length)
                                            {
                                                if (numberDetectIf != numberCountToBack)
                                                {
                                                    arrayInNavigator[numberCountToBack] = "";
                                                }
                                                numberCountToBack++;
                                            }
                                        }
                                        break;
                                    case 2:
                                        if (arrayInNavigator[numberDetectIf] == "")
                                        {

                                            bttn.Enabled = false;

                                            Thread th = new Thread(() => {

                                                arrayInNavigator[numberDetectIf] = "Have";
                                                TimerSchedUserSelf.Stop();
                                                TimerGrading.Stop();
                                                TimerControls.Stop();
                                                while (numberCountToBack < arrayInNavigator.Length)
                                                {
                                                    if (numberDetectIf != numberCountToBack)
                                                    {
                                                        arrayInNavigator[numberCountToBack] = "";
                                                    }
                                                    numberCountToBack++;
                                                }
                                            });
                                            th.Start();

                                            this.DataGatherListOfReport(bttn);

                                        }
                                        break;
                                    case 3:
                                        if (arrayInNavigator[numberDetectIf] == "")
                                        {
                                            arrayInNavigator[numberDetectIf] = "Have";
                                            TimerSchedUserSelf.Stop();
                                            TimerReportSelf.Stop();
                                            TimerGrading.Stop();
                                            while (numberCountToBack < arrayInNavigator.Length)
                                            {
                                                if (numberDetectIf != numberCountToBack)
                                                {
                                                    arrayInNavigator[numberCountToBack] = "";
                                                }
                                                numberCountToBack++;
                                            }
                                        }
                                        break;
                                    default:
                                        if (arrayInNavigator[numberDetectIf] == "")
                                        {
                                            arrayInNavigator[numberDetectIf] = "Have";
                                            TimerGrading.Stop();
                                            TimerControls.Stop();
                                            TimerReportSelf.Stop();

                                            while (numberCountToBack < arrayInNavigator.Length)
                                            {
                                                if (numberDetectIf != numberCountToBack)
                                                {
                                                    arrayInNavigator[numberCountToBack] = "";
                                                }
                                                numberCountToBack++;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    });
                }
            }


            //BACK TO NATURAL SCHED......................................
            BackSched.Click += new System.EventHandler((object sender, EventArgs e) => {
                TimerSchedUserSelf.Start();
                Thread th = new Thread(() => {
                    this.BeginInvoke((Action)delegate () {
                        YourScheduleAndOther.Visible = true;
                        SchedSetMess.Visible = false;
                    });
                });
                th.Start();
            });




            //INTERVAL SECTION....................................................

            //REPORT INTERVAL..........................................
            TimerReportSelf.Interval = 5000;
            TimerReportSelf.Tick += new System.EventHandler((object controls, EventArgs e) => {
                this.DataGatherListOfReport(Reports);
            });




            //TIME SHOW IN CALENDAR CLASS.......................................
            string Hrs, Mmn, Sec, AP = "";
            System.Windows.Forms.Timer timerCalendar = new System.Windows.Forms.Timer();
            deleteSchedYours.Click += new System.EventHandler(deleteSchedUserWhoAssigned);
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




            //CALENDAR CONTROLS...............................................................................
            //.......................................................................



            //Items Add the Hrs number, Minutes Number and AM PM AND DATE.................................
            comboBoxHrs.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxMnt.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxAP.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            TimeRangeHrs.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            TimeRangeMnt.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);

            NextToSeeRange.Click += new System.EventHandler(clickNextAndBack);
            BackToSeeTimeDate.Click += new System.EventHandler(clickNextAndBack);

            DeleteScheduleOwnUser.Click += new System.EventHandler(deleteScheduleOwnUser);

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


            //CALENDAR END FOR FORM LOAD..............................................







            //REPORT START...........................................................


            foreach (Panel flowPanel in FlowPercentagePanel.Controls) {
                flowPanel.Paint += new System.Windows.Forms.PaintEventHandler(flowPaintPercent);
            }

            //SHOW EVERY SINGLE CLICK IN SELECETED..................................
            foreach (Panel panReport in NavigatorReport.Controls)
            {
                panReport.Click += new System.EventHandler(ReportFunctionShow);
                foreach (Control label in panReport.Controls)
                {
                    if (label.GetType() == typeof(Label))
                    {
                        label.Click += new System.EventHandler(ReportFunctionShow);
                    }
                }
            }

            //END OF REPORT..................................................................
            // .................................................................
            // .................................................................
            // .................................................................
            //  .................................................................













            //GRADING START..........................................................
            //..........................................................................................
            //..........................................................................................
            //..........................................................................................
            //..........................................................................................



            this.gettingDataSearch();

            timerGradingSub.Tick += new System.EventHandler(gradingInterval);
            timerGradingSub.Interval = 3000;


            ReadSubjects.Click += new System.EventHandler(StartReadSubjects);
            DeleteSubjects.Click += new System.EventHandler(StartDeleteSubjects);

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


        //ITO YUNG PARA MAG SHOW ANG MGA SCHEDULE NI USER.......................................
        private static bool conditionFirst = true;
        private static int EqualNumber;
        protected async void intervalVoidShowAndScanSched(string userName) {
            bool conditionSecond = true;
            string conditionToShow = "";
            string conditionToThisFinal = "";
            int numberCountToRepeat = 0;
            EqualNumber = 0;
            LoadingScreen load = new LoadingScreen();
            List<CalendarList> handleDataSched = new List<CalendarList>();
            List<CalendarList> AdminAndUserHandle = new List<CalendarList>();


            if (conditionFirst != false)
            {
                Calendar getSchedThisUser = new Calendar();
                handleDataSched = await Task.Run(() => getSchedThisUser.GetAllSchedThisUser(userName)).ConfigureAwait(true);
                conditionFirst = false;

                //SCANNING IF HAVE A ADMIN OR NO..............................................
                if (await Task.Run(() => ScanForAdmin(handleDataSched)) == "DoneScan")
                {
                    await Task.Run(() => forEachShowSched());
                    Thread.Sleep(100);
                    if (conditionToThisFinal == "DoneShowSched")
                    {
                        TimerSchedUserSelf.Start();
                    }
                }
            }
            else {
                conditionSecond = false;
                handleDataSched = ListScheduleSelf;
                conditionSecond = true;

                if (conditionSecond == true) {
                    if (await Task.Run(() => ScanForAdmin(handleDataSched)).ConfigureAwait(true) == "DoneScan") {
                        await Task.Run(() => forEachShowSched());

                        if (conditionToThisFinal == "DoneShowSched")
                        {
                            TimerSchedUserSelf.Start();
                        }
                    }
                }
            }

            //SCANNING FUNCTION THAT WILL SCAN IF THE USER HAVE A SCHEDULE IN ADMIN....................
            async Task<string> ScanForAdmin(List<CalendarList> handleDataSchedScan) {
                int numberCountEqual = 0, numberCountHole = 0, numberCountAdmin = 0;
                string asd = "";
                string trys()
                {
                    //CHECK IF THERE IS HAVE ADMIN SCHED ASSIGNED AND COUNT IT..........................
                    foreach (var scanAdmin in handleDataSchedScan)
                    {
                        if (scanAdmin.numberCount > 0)
                        {
                            numberCountHole++;
                            if (scanAdmin.HandlingAdmin == "ADMIN")
                            {
                                numberCountAdmin = numberCountAdmin + 1;
                            }
                        }
                        else {
                            numberCountHole = 0;
                        }
                    }

                    EqualNumber = numberCountHole;

                    NumberHandleCondition = EqualNumber;

                    //CONDITION IF SUCCESFUL HAVE AN ADMIN SCHEDULE OR NOT .........................
                    //IT WILL DEFINE IF HAVE ADMIN SCHED OR NO......................
                    if (numberCountAdmin > 0)
                    {
                        //IF HAVE THIS WILL ADDED IN NEW LIST.....................
                        foreach (var scanAdminFinal in handleDataSchedScan)
                        {
                            if (scanAdminFinal.HandlingAdmin == "ADMIN")
                            {
                                AdminAndUserHandle.Add(new CalendarList
                                {
                                    ErrCheck = scanAdminFinal.ErrCheck,
                                    numberCount = scanAdminFinal.numberCount,
                                    NameUserWhoAdded = scanAdminFinal.NameUserWhoAdded,
                                    ImageUserWhoAdded = scanAdminFinal.ImageUserWhoAdded,
                                    DateTimeRangeAddHrs = scanAdminFinal.DateTimeRangeAddHrs,
                                    DateTimeRangeAddMnt = scanAdminFinal.DateTimeRangeAddMnt,
                                    DateTimeRangeAddAP = scanAdminFinal.DateTimeRangeAddAP,
                                    calendarRangeAddMonth = scanAdminFinal.calendarRangeAddMonth,
                                    calendarRangeAddConvert = scanAdminFinal.calendarRangeAddConvert,
                                    calendarRangeAddDay = scanAdminFinal.calendarRangeAddDay,
                                    calendarRangeAddYear = scanAdminFinal.calendarRangeAddYear,
                                    SetDurationTimeAdd = scanAdminFinal.SetDurationTimeAdd,
                                    SetDurationTimeAddHrs = scanAdminFinal.SetDurationTimeAddHrs,
                                    DateTimeRange = scanAdminFinal.DateTimeRange,
                                    HandlingAdmin = scanAdminFinal.HandlingAdmin
                                });
                                numberCountEqual++;
                                numberCountAdmin--;
                            }
                        }


                        if (numberCountAdmin == 0)
                        {
                            //BUT IF THERE IS HAVE A USER AFTER ADDED ALL ADMIN THIS WILL HAPPEND
                            if (numberCountEqual != numberCountHole)
                            {
                                //SCAN IT AND ADD USERS........................
                                foreach (var scanAdminFinals in handleDataSchedScan)
                                {
                                    if (scanAdminFinals.HandlingAdmin != "ADMIN")
                                    {
                                        AdminAndUserHandle.Add(new CalendarList
                                        {
                                            ErrCheck = scanAdminFinals.ErrCheck,
                                            numberCount = scanAdminFinals.numberCount,
                                            NameUserWhoAdded = scanAdminFinals.NameUserWhoAdded,
                                            ImageUserWhoAdded = scanAdminFinals.ImageUserWhoAdded,
                                            DateTimeRangeAddHrs = scanAdminFinals.DateTimeRangeAddHrs,
                                            DateTimeRangeAddMnt = scanAdminFinals.DateTimeRangeAddMnt,
                                            DateTimeRangeAddAP = scanAdminFinals.DateTimeRangeAddAP,
                                            calendarRangeAddMonth = scanAdminFinals.calendarRangeAddMonth,
                                            calendarRangeAddConvert = scanAdminFinals.calendarRangeAddConvert,
                                            calendarRangeAddDay = scanAdminFinals.calendarRangeAddDay,
                                            calendarRangeAddYear = scanAdminFinals.calendarRangeAddYear,
                                            SetDurationTimeAdd = scanAdminFinals.SetDurationTimeAdd,
                                            SetDurationTimeAddHrs = scanAdminFinals.SetDurationTimeAddHrs,
                                            DateTimeRange = scanAdminFinals.DateTimeRange,
                                            HandlingAdmin = scanAdminFinals.HandlingAdmin
                                        });
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        AdminAndUserHandle = handleDataSchedScan;
                    }
                    return "DoneScan";
                }
                asd = await Task.Run(trys);
                return asd;
            }


            async void forEachShowSched() {
                int locationY = 5;
                Thread thr = new Thread(() =>
                {
                    if (Application.OpenForms.OfType<GradingFormUser>().Count() == 1) {
                        ScheduleUserPanel2.BeginInvoke((Action)delegate ()
                        {
                            ScheduleUserPanel2.Controls.Clear();
                        });
                    }
                });
                thr.Start();

                foreach (var getData in AdminAndUserHandle)
                {
                    if (getData.ErrCheck == "")
                    {
                        if (getData.numberCount > 0)
                        {
                            if (conditionToShow == "")
                            {

                                conditionToShow = "Wait";

                                conditionToShow = await Task.Run(() => wait());

                                string wait()
                                {
                                    //THREAD THAT IGNORING THE ERROR..............................
                                    Thread th = new Thread(async () =>
                                    {
                                        if (ScheduleUserPanel2.InvokeRequired != false)
                                        {
                                            ScheduleUserPanel2.BeginInvoke((Action)delegate ()
                                            {
                                                var delegateFunc = new functionDelegateControls(ControlsLoadSHow);
                                                delegateFunc.Invoke(getData.NameUserWhoAdded, getData.DateTimeRange,
                                                    getData.ImageUserWhoAdded, locationY, getData.HandlingAdmin);
                                                locationY += 85;
                                            });

                                        }
                                        else {
                                            Task StringReturn = new Task(() => this.ControlsLoadSHow(getData.NameUserWhoAdded, getData.DateTimeRange,
                                                    getData.ImageUserWhoAdded, locationY, getData.HandlingAdmin));
                                            StringReturn.Start();
                                            await StringReturn.ConfigureAwait(true);
                                            locationY += 80;
                                        };
                                    });
                                    th.Start();

                                    return "";
                                }
                                numberCountToRepeat++;
                            }
                        }
                        else
                        {
                            Thread ths = new Thread(() =>
                            {
                                Label labelNoSched = new Label
                                {
                                    Name = "UserNoSchedule",
                                    Location = new Point(156, 173),
                                    ForeColor = System.Drawing.Color.CornflowerBlue,
                                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                    Size = new Size(212, 16),
                                    Text = "No Schedule Yet."
                                };

                                Action ac = () => ScheduleUserPanel2.Controls.Add(labelNoSched);
                                ScheduleUserPanel2.BeginInvoke(ac);
                            });

                            ths.Start();

                        }
                    }
                    else
                    {
                        MessageBox.Show(getData.ErrCheck);
                        Thread ths = new Thread(() =>
                        {
                            Label labelNoSched = new Label
                            {
                                Name = "NoConnection",
                                Location = new Point(156, 173),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                Size = new Size(212, 16),
                                Text = "No Connection."
                            };

                            if (Application.OpenForms.OfType<GradingFormUser>().Count() == 1) {
                                Action ac = () => ScheduleUserPanel2.Controls.Add(labelNoSched);
                                ScheduleUserPanel2.BeginInvoke(ac);
                            }
                        });

                        ths.Start();
                    }
                }
                if (numberCountToRepeat == EqualNumber)
                {
                    conditionToThisFinal = "DoneShowSched";
                }
            }

        }


        //THIS IS THE TASK STRING THAT DO TO DELETE THE SCHEDULE...............................................
        protected static string checkIfHavings = "", thatWillDelete = "false", seeError = "";
        protected static List<string> handleDataMessage = new List<string>();
        protected static string[] handleMessageinarrayConvert;
        protected static int numberFinalForMessage = -1;

        protected async Task<string> DoDeleteSched(string scheduleDelete, string whoAddedName, string[] messageDelete,
            int numberFinalForMessages) {
            checkIfHavings = "Have";
            if (thatWillDelete == "true") {
                if (seeError == "")
                {
                    seeError = await Task.Run(() => calendarClass.deletedOwnSchedule("vee", scheduleDelete, whoAddedName,
                        messageDelete[numberFinalForMessages]));
                    if (seeError != "")
                    {
                        MessageBox.Show(seeError);
                    }
                }
            }

            return seeError;
        }


        //ITO YUNG DELETE SCHEDULE DUN KAY USER PARA MAKADILIT SIYA NG SHCEDULE NYA......................
        protected static int CountToDetect = 0;
        protected static List<List<string>> datahandleDateName = new List<List<string>>();
        protected static List<List<string>> handleDataForVoid = new List<List<string>>();
        protected static int numberCountToSectionCom = 0;
        protected static int numberCountsDelete = 0;

        public void deleteSchedUserWhoAssigned(object control, EventArgs e)
        {
            checkIfHavings = "";
            CountToDetect = 0;
            numberCountToSectionCom = 0;
            numberFinalForMessage = -1;
            numberCountsDelete = 0;
            datahandleDateName = new List<List<string>>();
            handleDataMessage = new List<string>();
            handleDataForVoid = new List<List<string>>();

            if (EqualNumber > 0)
            {
                TimerSchedUserSelf.Stop();
                foreach (Panel pan in ScheduleUserPanel2.Controls)
                {
                    foreach (Control checkbox in pan.Controls)
                    {
                        if (checkbox.GetType() == typeof(CheckBox))
                        {
                            if (checkbox.Visible == false)
                            {
                                checkbox.Visible = true;
                            }
                            else
                            {
                                if (checkbox.Text != "")
                                {
                                    //DETERMINE IF THE USER WANT TO DELETE SCHED.................
                                    CountToDetect++;

                                    //ADD DATA LIST DATE AND NAME...................................
                                    datahandleDateName.Add(new List<string> {
                                           CountToDetect.ToString(),
                                           checkbox.AccessibleDescription,
                                           checkbox.AccessibleName
                                    });

                                    handleDataForVoid.Add(new List<string> {
                                        checkbox.AccessibleDescription, checkbox.AccessibleName
                                    });

                                }
                                else
                                {
                                    checkbox.Visible = false;
                                }
                            }
                        }
                    }
                }

                if (CountToDetect > 0) {
                    thatWillDelete = "false";
                    CommentSectionSchedDelete.Visible = true;
                    this.showNameDateCommentSec();
                }
            }
            else
            {
                MessageBox.Show("You had no schedule.");
            }
        }


        //SHOW THE NAME AND DATE OF OTHER USER MEssage STATEMENT........
        protected void showNameDateCommentSec() {
            numberCountToSectionCom++;
            bool conditionBool = false;
            foreach (var stringCount in datahandleDateName) {
                if (conditionBool != true)
                {
                    int numbercountToDisplay = 0;
                    int numbercountToDisplays = 0;
                    foreach (var equalCount in stringCount) {
                        if (conditionBool != true)
                        {
                            if (numbercountToDisplays != 1) {
                                numbercountToDisplays++;
                                if (Convert.ToInt32(equalCount) == numberCountToSectionCom)
                                {
                                    conditionBool = true;
                                }
                            }
                        }
                        else
                        {
                            numbercountToDisplay++;
                            if (numbercountToDisplay <= 1)
                            {
                                DateScheduleDelete.Text = equalCount;
                            }
                            else
                            {
                                NameScheduleDelete.Text = equalCount;
                            }
                        }
                    }
                }
            }
        }

        //BUTTON ADD MESSAGE AND AFTER REACH THE NUMBER OF SELECTED IT WILL DELETE..................
        public async void deleteScheduleOwnUser(object controls, EventArgs e) {
            if (messageDeleteSchedule.Text.Length > 0)
            {
                numberCountsDelete++;
                handleDataMessage.Add(messageDeleteSchedule.Text);
                messageDeleteSchedule.Text = "";

                if (numberCountsDelete == CountToDetect)
                {
                    List<Task> taskDelete = new List<Task>();
                    thatWillDelete = "true";
                    handleMessageinarrayConvert = handleDataMessage.ToArray();
                    CommentSectionSchedDelete.Visible = false;

                    foreach (List<string> stringData in handleDataForVoid) {
                        string[] arrayConvert = stringData.ToArray();
                        numberFinalForMessage++;
                        taskDelete.Add(DoDeleteSched(arrayConvert[0], arrayConvert[1], handleMessageinarrayConvert,
                            numberFinalForMessage));
                    }

                    await Task.WhenAll(taskDelete);

                    if (checkIfHavings == "Have")
                    {
                        if (String.IsNullOrEmpty(seeError) == true)
                        {
                            conditionFirst = true;
                            ScheduleUserPanel2.Controls.Clear();
                            this.intervalVoidShowAndScanSched("vee");
                        }
                        else
                        {
                            MessageBox.Show("The Schedule you want to delete is unnable.");
                        }
                    }

                }
                else {
                    this.showNameDateCommentSec();
                }
            }
            else {
                MessageBox.Show("You must to say something........");
            }
        }



        //SHOW CONTROLS DATA SCHEDULE....................
        protected void ControlsLoadSHow(string NameUserWhoAdded, string DateTimeRange, string ImageUserWhoAdded,
                                        int numberCountPanel, string HandlingAdmin) {

            Panel pan = new Panel
            {
                Name = NameUserWhoAdded + "_ScheduleYours",
                BackColor = System.Drawing.ColorTranslator.FromHtml("#1C2833"),
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                Size = new Size(390, 80),
                Location = new Point(5, numberCountPanel)
            };
            ScheduleUserPanel2.Controls.Add(pan);

            PictureBox pic = new PictureBox {
                Name = NameUserWhoAdded + "_ScheduleYoursPic",
                Image = Image.FromFile(ImageUserWhoAdded),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(53, 39),
                Location = new Point(5, 3),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            pan.Controls.Add(pic);

            Button bttnName = new Button
            {
                Name = NameUserWhoAdded + "_ScheduleYoursBttnName",
                Text = NameUserWhoAdded,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                ForeColor = System.Drawing.Color.CornflowerBlue,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Location = new Point(64, 3),
                Size = new Size(314, 37)
            };
            bttnName.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CD6155");
            bttnName.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1C2833");
            bttnName.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C2833");
            pan.Controls.Add(bttnName);

            Button AdminOrUser = new Button
            {
                Name = "AdminOrUser",
                Text = (HandlingAdmin != "ADMIN" ? "User:" : "Admin"),
                Location = new Point(5, 48),
                Size = new Size(53, 25),
                Font = new Font("Microsoft Sans Serif", (HandlingAdmin != "ADMIN" ? 9 : 9), FontStyle.Regular),
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                FlatStyle = FlatStyle.Flat
            };
            AdminOrUser.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CD6155");
            AdminOrUser.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1C2833");
            AdminOrUser.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C2833");
            pan.Controls.Add(AdminOrUser);


            CheckBox deletedBox = new CheckBox
            {
                Name = "DeleteBoxSchedule",
                AccessibleDescription = DateTimeRange,
                AccessibleName = NameUserWhoAdded,
                Text = "",
                Size = new Size(15, 14),
                Location = new Point(354, 53),
                Visible = false
            };
            deletedBox.Click += new System.EventHandler((object control, EventArgs e) =>
            {
                CheckBox checkdelete = (CheckBox)control;
                if (String.IsNullOrEmpty(checkdelete.Text))
                {
                    checkdelete.Text = "DeleteSched";
                }
                else {
                    checkdelete.Text = "";
                }
            });
            pan.Controls.Add(deletedBox);

            Button bttnSched = new Button
            {
                Name = NameUserWhoAdded + "_ScheduleYoursBttSched",
                Text = DateTimeRange,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                ForeColor = System.Drawing.Color.CornflowerBlue,
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = System.Windows.Forms.FlatStyle.Flat,
                Location = new Point(64, 45),
                Size = new Size(314, 28)
            };
            bttnSched.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CD6155");
            bttnSched.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1C2833");
            bttnSched.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C2833");

            pan.Controls.Add(bttnSched);
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
        private VScrollBar scroll;
        protected async void ClickedDataSearchToCalendar(object controls, EventArgs e) {
            NoAssignedAddSched.Controls.Clear();
            NoAssignedAddSched.AutoScroll = true;
            SchedSetMess.Visible = true;
            int numberCountPanel = 8;
            int numberForAdmins = 0;
            int numberScanIfMax = 0;
            int numberScanMinAdmin = 0;
            string doneToScannAdmin = "";
            loadingOrWaiting.Show();
            this.Hide();
            TimerSchedUserSelf.Stop();

            int numberCountScroll = 0;
            scroll = new VScrollBar() {
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

                                            //SHOW FIRST THE ADMIN SCHEDULE...............................
                                            //Task<string> messageTask = new Task<string>(() => adminShowAndEtc(GetTheScheduleOfUserClicked,
                                            //   numberForAdmins, numberScanMinAdmin, numberScanIfMax));
                                            // messageTask.Start();
                                            string message = adminShowAndEtc(GetTheScheduleOfUserClicked,
                                               numberForAdmins, numberScanMinAdmin, numberScanIfMax, scroll);

                                            if (message == "Succesful to show")
                                            {
                                                loadingOrWaiting.Hide();
                                                this.Show();
                                            }
                                            else if (message is "Please Check Your Connection.")
                                            {
                                                loadingOrWaiting.Show();
                                                this.Hide();
                                                MessageBox.Show("Please Check Your Connection.");
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




        //ADMIN DETECT SHOWS FIRST FUNCTION................................................
        public string adminShowAndEtc(List<CalendarList> GetTheScheduleOfUserClicked, int numberForAdmins,
                                                int numberScanMinAdmin, int numberScanIfMax, VScrollBar scroll) {
            //SHOWING THE ADMIN SCHED ASSIGNED TO THE USER YOU CLICKED................
            int numberCountScroll = 0;
            int numberCountPanel = 8;
            string handleMessageReturningBack = "";
            foreach (var ShowAdminSched in GetTheScheduleOfUserClicked)
            {
                if (ShowAdminSched.ErrCheck == "")
                {
                    if (ShowAdminSched.HandlingAdmin == "ADMIN")
                    {
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

            if (numberForAdmins == 0)
            {
                //SHOW THE LEAST OF THE HOLE DATABASE SCHEDULE FOR THE USER YOU CLICKED....
                if (numberScanMinAdmin != numberScanIfMax)
                {
                    string waiting = "";
                    //SHOW AND DESIGN OF SCHEDULE OF USER ARRIVED..............
                    foreach (var showcalendar in GetTheScheduleOfUserClicked)
                    {
                        if (showcalendar.ErrCheck == "")
                        {
                            if (showcalendar.HandlingAdmin == "" || showcalendar.HandlingAdmin is null)
                            {
                                if (waiting == "")
                                {
                                    waiting = "have";
                                    numberCountScroll++;
                                    if (numberCountScroll > 3)
                                    {
                                        scroll.Visible = false;
                                    }

                                    functionDelegateControls createCon = new functionDelegateControls(createControlsSched);

                                    createCon.Invoke(showcalendar.NameUserWhoAdded, showcalendar.DateTimeRange,
                                        showcalendar.ImageUserWhoAdded, numberCountPanel, "");

                                    numberCountPanel += 93;
                                    waiting = "";
                                }
                            }
                            handleMessageReturningBack = "Succesful to show";
                        }
                        else
                        {
                            handleMessageReturningBack = "Please Check Your Connection.";
                        }
                    }
                }
                else
                {
                    handleMessageReturningBack = "Succesful to show";
                }
            }

            return handleMessageReturningBack;
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
                Size = new Size(55, 32),
                Text = (HandlingAdmin != "ADMIN") ? "User:" : "Admin:",
                Font = new Font("Microsoft Sans Serif", ((HandlingAdmin != "ADMIN") ? 10 : 9), FontStyle.Regular),
                Location = new Point(10, 46),
                BackColor = ColorTranslator.FromHtml("#1C2833")
            };
            bttnSched.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#CD6155");
            bttnSched.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#1C2833");
            bttnSched.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#1C2833");
            panAdd.Controls.Add(bttnSched);

            if (HandlingAdmin == "") {
                Thread.Sleep(1000);
            }
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
                + DateTimeRange[3] + ":" + DateTimeRange[4] + " " + (DateTimeRange[3] == "0" ||
                DateTimeRange[3] == "" ? "Mnts" : (DateTimeRange[3] == "1" ? "Hr" : "Hrs"));
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
            else if (picCon.Name == "BackToSeeTimeDate")
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

                if (CountSelection >= stringSelectionRange.Length - 1) {
                    for (int numberCountConverter = CalendarConverter.Length; numberCountConverter > 0; numberCountConverter--) {
                        if (numberCountConverter == Convert.ToInt32(CalendarRange[0])) {
                            handlingConverterMonth = CalendarConverter[numberCountConverter - 1];
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
                                                                string doneToScannAdmin = "";
                                                                int numberScanIfMax = 0;
                                                                int numberForAdmins = 0;
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


                                                                //SCANNING KUNG MAY ADMIN BA NA SCHEDULE OR WALA FUNCTION...............
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

                                                                //SCAN AND RETURN THE DATA IF HAVE AN ADMIN SCHED OR NOT.................
                                                                Task<int> intFunctionAdmin = new Task<int>(() => scanIfHaveAdminOrNo(GetTheScheduleOfUserClicked));
                                                                intFunctionAdmin.Start();

                                                                numberForAdmins = await intFunctionAdmin.ConfigureAwait(true);
                                                                doneToScannAdmin = "";

                                                                //GET THE DATA INTO DATABASE AND RETURN IT HERE.......................... 
                                                                List<CalendarList> calenListSched = await Task.Run(() => calendarClass.getAllDataInAddSched(usernameAccesssibleToDatabase));

                                                                if (doneToScannAdmin != "Have") {
                                                                    if (numberForAdmins <= 0)
                                                                    {
                                                                        foreach (var showcalendar in calenListSched)
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
                                                                    else {
                                                                        int numberScanMinAdmin = 0;
                                                                        //SHOW FIRST THE ADMIN SCHEDULE...............................
                                                                        // Task<string> messageTask = new Task<string>(() => adminShowAndEtc(calenListSched,
                                                                        //     numberForAdmins, numberScanMinAdmin, numberScanIfMax));
                                                                        //  messageTask.Start();

                                                                        string message = adminShowAndEtc(calenListSched,
                                                                           numberForAdmins, numberScanMinAdmin, numberScanIfMax, scroll);

                                                                        if (message == "Succesful to show")
                                                                        {
                                                                            loadingOrWaiting.Hide();
                                                                            this.Show();
                                                                        }
                                                                        else if (message is "Please Check Your Connection.")
                                                                        {
                                                                            loadingOrWaiting.Show();
                                                                            this.Hide();
                                                                            MessageBox.Show("Please Check Your Connection.");
                                                                        }

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

                                        for (int scanningMessageErr = messageErr.Length - 1; scanningMessageErr >= 0; scanningMessageErr -= 1) {
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
                                                                    string doneToScannAdmin = "";
                                                                    int numberScanIfMax = 0;
                                                                    int numberForAdmins = 0;
                                                                    //SCANNING KUNG MAY ADMIN BA NA SCHEDULE OR WALA FUNCTION...............
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

                                                                    //SCAN AND RETURN THE DATA IF HAVE AN ADMIN SCHED OR NOT.................
                                                                    Calendar calen = new Calendar();
                                                                    List<CalendarList> listreturnValue = await Task.Run(() => calen.getAllDataInAddSched(usernameAccesssibleToDatabase)).ConfigureAwait(true);
                                                                    Task<int> intFunctionAdmin = new Task<int>(() => scanIfHaveAdminOrNo(listreturnValue));
                                                                    intFunctionAdmin.Start();

                                                                    numberForAdmins = await intFunctionAdmin.ConfigureAwait(true);
                                                                    doneToScannAdmin = "";


                                                                    if (doneToScannAdmin != "Have") {
                                                                        if (numberForAdmins == 0)
                                                                        {
                                                                            int numberCountPanel = 8;
                                                                            foreach (var returnValue in listreturnValue)
                                                                            {
                                                                                numberCountScroll++;

                                                                                if (numberCountScroll > 3)
                                                                                {
                                                                                    scroll.Visible = false;
                                                                                }

                                                                                functionDelegateControls returnfunctiondele = new functionDelegateControls(createControlsSched);
                                                                                returnfunctiondele.Invoke(returnValue.NameUserWhoAdded, returnValue.DateTimeRange,
                                                                                    returnValue.ImageUserWhoAdded, numberCountPanel, "");
                                                                                numberCountPanel += 93;
                                                                            }
                                                                        }
                                                                        else {
                                                                            //SCANNING FOR ADMON AND SHOW IT................
                                                                            int numberScanMinAdmin = 0;
                                                                            //SHOW FIRST THE ADMIN SCHEDULE...............................
                                                                            // Task<string> messageTask = new Task<string>(() => adminShowAndEtc(calenListSched,
                                                                            //     numberForAdmins, numberScanMinAdmin, numberScanIfMax));
                                                                            //  messageTask.Start();

                                                                            string message = adminShowAndEtc(listreturnValue,
                                                                               (numberForAdmins), numberScanMinAdmin, numberScanIfMax, scroll);

                                                                            if (message == "Succesful to show")
                                                                            {
                                                                                loadingOrWaiting.Hide();
                                                                                this.Show();
                                                                            }
                                                                            else if (message is "Please Check Your Connection.")
                                                                            {
                                                                                loadingOrWaiting.Show();
                                                                                this.Hide();
                                                                                MessageBox.Show("Please Check Your Connection.");
                                                                            }
                                                                        }
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

        //END OF CALENDAR SCHED.....................................................................
        //..........................................................................................
        //..........................................................................................
        //..........................................................................................
        //..........................................................................................
        //..........................................................................................







        //REPORT START..........................................................................................
        //..........................................................................................
        //..........................................................................................
        //..........................................................................................
        //..........................................................................................

        protected List<ReportList> reportListGetAllData = new List<ReportList>();
        protected Report reportClass = new Report();
        protected static int[] handleForPercent = new int[] { 0, 0, 0 };
        private int numberCountForListReport = 0, numberCountPanel = 0, CalendarNumberCount = 0, GradingNumberCountList = 0;
        private static bool conditionToGetData = true;
        private static string reportErr = "false", reportNoHaving = "false";
        public string[] LabelArr = new string[] { "GradingLabelPercent", "CalendarLabelPercent", "OALabelPercent" };
        public string[] PanelFlowPercent = new string[] { "GradingFlowReport", "CalendarFlowReport", "OverAllFlowReport" };
        protected static string condition_to_Paint = "false", HandlingSeeIfHavingError = "Null";
        public static List<Task> taskVoidShowData = new List<Task>();
        public static string conditionifEqual = "false";
        private static string conditionToDeleteReport = "", conditionToReadReport = "";
        //DELEGATE TYPE COUNT PERCENT.................................
        protected delegate void delegateflowPercent(int[] PercentArray, List<ReportList> ListOfDataReport);
        //DELEGATE TYPE SHOW DATA.............................
        protected delegate void delegateFlowShowData(int id, string NameWhoMessage, string ImageUser, string Message,
                                string ColorDeclared, string DayReport, string MonthReport, string TimeMessage,
                                string FullTimeMessage, int HeightTopOfPanel, int HeightTopOfPanelCalendar,
                                int HeightTopOfPanelGrading);


        //SHOW REPORT EVERY SINGLE CLICK ALL, C AND G....................................................
        public void ReportFunctionShow(object controls, EventArgs e)
        {
            string textCon = "";
            Control Con = (Control)controls;
            Panel pans = null;
            Label label = null;

            if (Con is Panel)
            {
                textCon = "asd";
                pans = (Panel)Con;
                pans.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A2728");
            }
            else if (Con is Label)
            {
                textCon = "as";
                label = (Label)Con;

                switch (label.Name) {
                    case "AllLabel":
                        AllReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A2728");
                        break;
                    case "CalendarLabel":
                        CalendarReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A2728");
                        break;
                    case "GradingLabel":
                        GradingReport.BackColor = System.Drawing.ColorTranslator.FromHtml("#2A2728");
                        break;
                }
            }

            foreach (Control pan in PanelShowReports.Controls)
            {
                if (pan is Panel)
                {
                    if (pan.AccessibleName != "LineCost")
                    {
                        if ((textCon == "asd" ? pans.Name : label.AccessibleName) == pan.AccessibleName)
                        {
                            pan.Visible = true;
                        }
                        else
                        {
                            pan.Visible = false;
                            foreach (Control panel in NavigatorReport.Controls) {
                                if (panel.GetType() == typeof(Panel)) {
                                    if (pan.AccessibleName == panel.Name) {
                                        panel.BackColor = System.Drawing.ColorTranslator.FromHtml("28, 40, 51");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        //REPORT VOID THAT WILL START FROM THIS...................................................
        protected async void DataGatherListOfReport(Button bttn) {
            int numberCountList = 0, ConditionToGet = 0;
            numberCountPanel = 0;
            condition_to_Paint = "false";
            taskVoidShowData = new List<Task>();
            TimerReportSelf.Stop();
            HandlingSeeIfHavingError = "Null";
            conditionifEqual = "false";
            reportNoHaving = "false";

            for (int numberCountPercent = 0; handleForPercent.Length > numberCountPercent; numberCountPercent++)
            {
                handleForPercent[numberCountPercent] = 0;
            }

            if (conditionToGetData != false)
            {
                conditionToGetData = false;
                ConditionToGet = 1;
                reportListGetAllData = await Task.Run(() => reportClass.getAllDatainReport("vee")).ConfigureAwait(true);
                ConditionToGet = 2;

                if (ConditionToGet != 1)
                {
                    int AwaitGetCount()
                    {
                        foreach (var GetData in reportListGetAllData)
                        {
                            if (String.IsNullOrEmpty(GetData.ErrCatch))
                            {
                                if (GetData.countNumber == 1)
                                {
                                    numberCountList++;

                                    switch (GetData.ColorDeclared)
                                    {
                                        //CALENDAR COUNT........................
                                        case "#17202A":
                                            handleForPercent[1]++;
                                            break;
                                        case "Coral":
                                            handleForPercent[0]++;
                                            break;
                                    }
                                    handleForPercent[2] = numberCountList;
                                    CalendarNumberCount = handleForPercent[1];
                                    GradingNumberCountList = handleForPercent[0];
                                }
                                else
                                {
                                    conditionToGetData = true;
                                }
                            }
                            else
                            {
                                conditionToGetData = true;
                                HandlingSeeIfHavingError = "Have";
                                MessageBox.Show("Check Your Internet.");
                            }
                        }
                        return numberCountList;
                    }


                    Task<int> intGetCounts = new Task<int>(() => AwaitGetCount());
                    intGetCounts.Start();
                    numberCountForListReport = await intGetCounts.ConfigureAwait(true);
                    if (HandlingSeeIfHavingError == "Null")
                    {
                        if (numberCountForListReport != 0)
                        {
                            PanelOverAll.Controls.Clear();

                            bool conditionIf() {

                                if (CalendarNumberCount == 0)
                                {
                                    Thread th = new Thread(() => ths());
                                    void ths()
                                    {
                                        var labelReports = new Label
                                        {
                                            Name = "ReportNoHaving",
                                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                            ForeColor = System.Drawing.Color.CornflowerBlue,
                                            Size = new Size(233, 16),
                                            Location = new Point(265, 117)
                                        };
                                        labelReports.Text = "No Reports The Calendar For Now.";
                                        Action ac = new Action(() =>
                                        {
                                            panelCalendarShow.Controls.Clear();
                                            panelCalendarShow.Controls.Add(labelReports);
                                        });
                                        panelCalendarShow.BeginInvoke(ac);
                                    }
                                }
                                else
                                {
                                    panelCalendarShow.Controls.Clear();
                                }

                                if (GradingNumberCountList == 0)
                                {
                                    Thread th = new Thread(() =>
                                    {
                                        var labelReport = new Label
                                        {
                                            Name = "ReportNoHaving",
                                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                            ForeColor = System.Drawing.Color.CornflowerBlue,
                                            Size = new Size(233, 16),
                                            Location = new Point(265, 117)
                                        };

                                        labelReport.Text = "No Reports The Grading For Now.";
                                        GradingShowReports.BeginInvoke((Action)delegate ()
                                        {
                                            GradingShowReports.Controls.Clear();
                                            GradingShowReports.Controls.Add(labelReport);
                                        });
                                    });
                                    th.Start();
                                }
                                else {
                                    GradingShowReports.Controls.Clear();
                                }
                                return true;
                            }

                            Task<bool> taskBool = new Task<bool>(conditionIf);
                            taskBool.Start();

                            if ((await taskBool.ConfigureAwait(true)) == true) {
                                delegateflowPercent delegateCount = new delegateflowPercent(calendarPercentReport);
                                delegateCount.Invoke(handleForPercent, reportListGetAllData);
                            }
                        }
                        else
                        {
                            //WALA PANG REPORT SA LAHAT.......................... 
                            //DO MESSAGE HERE THAT THE REPORT HAD NO REPORTS...........

                            PanelOverAll.Controls.Clear();
                            panelCalendarShow.Controls.Clear();
                            GradingShowReports.Controls.Clear();
                            var labelReport = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            var labelReports = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            var labelReportss = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            PanelOverAll.Controls.Add(labelReport);
                            panelCalendarShow.Controls.Add(labelReports);
                            GradingShowReports.Controls.Add(labelReportss);

                            delegateflowPercent delegateCount = new delegateflowPercent(calendarPercentReport);
                            delegateCount.Invoke(handleForPercent, reportListGetAllData);

                        }
                    }
                    else {

                        //PLEASE CHECK YOUR INTERNET CONNECTION......................................

                        bttn.Enabled = true;
                        PanelOverAll.Controls.Clear();
                        panelCalendarShow.Controls.Clear();
                        GradingShowReports.Controls.Clear();
                        var labelReportError = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        var labelReportErrors = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };
                        var labelReportErrorss = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        PanelOverAll.Controls.Add(labelReportError);
                        panelCalendarShow.Controls.Add(labelReportErrors);
                        GradingShowReports.Controls.Add(labelReportErrorss);
                        CalendarLabelPercent.Text = "Error";
                        GradingLabelPercent.Text = "Error";
                        OALabelPercent.Text = "Error";
                        TimerReportSelf.Start();
                    }
                }

            }
            else {
                reportListGetAllData = await Task.Run(() => reportClass.getAllDatainReport("vee")).ConfigureAwait(true);

                int returnDataNumber()
                {
                    foreach (var handleData in reportListGetAllData)
                    {
                        if (String.IsNullOrEmpty(handleData.ErrCatch))
                        {
                            if (handleData.countNumber == 1)
                            {
                                numberCountList++;
                                if (handleData.ColorDeclared != "Coral")
                                {
                                    handleForPercent[1] = handleForPercent[1] + 1;
                                }
                                else
                                {
                                    handleForPercent[0]++;
                                }
                                handleForPercent[2] = numberCountList;
                            }
                            else
                            {
                                //No Reports......Message
                                reportNoHaving = "true";
                            }
                        }
                        else
                        {
                            reportErr = "true";
                            MessageBox.Show("Please Check Your Internet");
                        }
                    }

                    return numberCountList;
                }
                Task<int> returnData = new Task<int>(returnDataNumber);
                returnData.Start();

                if (numberCountForListReport != (await returnData))
                {
                    if (reportErr == "false")
                    {
                        numberCountForListReport = numberCountList;
                        if (reportNoHaving != "true")
                        {
                            if (numberCountList != 0)
                            {
                                PanelOverAll.Controls.Clear();
                                if (CalendarNumberCount != handleForPercent[1]) {
                                    if (handleForPercent[1] != 0)
                                    {
                                        CalendarNumberCount = handleForPercent[1];
                                        panelCalendarShow.Controls.Clear();
                                    }
                                    else {
                                        CalendarNumberCount = handleForPercent[1];
                                        panelCalendarShow.Controls.Clear();
                                        var labelReport = new Label
                                        {
                                            Name = "ReportNoHaving",
                                            Text = "No Reports The Calendar For Now.",
                                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                            ForeColor = System.Drawing.Color.CornflowerBlue,
                                            Size = new Size(233, 16),
                                            Location = new Point(265, 117)
                                        };
                                        panelCalendarShow.Controls.Add(labelReport);
                                    }
                                }


                                if (GradingNumberCountList != handleForPercent[0])
                                {
                                    if (handleForPercent[0] != 0)
                                    {
                                        GradingNumberCountList = handleForPercent[0];
                                        GradingShowReports.Controls.Clear();
                                    }
                                    else
                                    {
                                        GradingNumberCountList = handleForPercent[0];
                                        GradingShowReports.Controls.Clear();
                                        var labelReport = new Label
                                        {
                                            Name = "ReportNoHaving",
                                            Text = "No Reports The Calendar For Now.",
                                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                            ForeColor = System.Drawing.Color.CornflowerBlue,
                                            Size = new Size(233, 16),
                                            Location = new Point(265, 117)
                                        };
                                        GradingShowReports.Controls.Add(labelReport);
                                    }
                                }


                                delegateflowPercent delegates = new delegateflowPercent(calendarPercentReport);
                                delegates.Invoke(handleForPercent, reportListGetAllData);
                            }
                        }
                        else {
                            PanelOverAll.Controls.Clear();
                            panelCalendarShow.Controls.Clear();
                            GradingShowReports.Controls.Clear();
                            var labelReport = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            var labelReports = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            var labelReportss = new Label
                            {
                                Name = "ReportNoHaving",
                                Text = "No Reports For Now.",
                                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                ForeColor = System.Drawing.Color.CornflowerBlue,
                                Size = new Size(233, 16),
                                Location = new Point(265, 117)
                            };

                            PanelOverAll.Controls.Add(labelReport);
                            GradingShowReports.Controls.Add(labelReports);
                            panelCalendarShow.Controls.Add(labelReportss);

                            delegateflowPercent delegateCount = new delegateflowPercent(calendarPercentReport);
                            delegateCount.Invoke(handleForPercent, reportListGetAllData);
                        }
                    }
                    else {
                        reportNoHaving = "false";
                        bttn.Enabled = true;
                        PanelOverAll.Controls.Clear();
                        panelCalendarShow.Controls.Clear();
                        GradingShowReports.Controls.Clear();
                        var labelReportError = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        var labelReportErrors = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        var labelReportErrorss = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        PanelOverAll.Controls.Add(labelReportError);
                        GradingShowReports.Controls.Add(labelReportErrors);
                        panelCalendarShow.Controls.Add(labelReportErrorss);

                        CalendarLabelPercent.Text = "Error";
                        GradingLabelPercent.Text = "Error";
                        OALabelPercent.Text = "Error";
                        TimerReportSelf.Start();
                    }
                }
                else {
                    // Start the interval.........................
                    bttn.Enabled = true;

                    if (reportErr != "false") {

                        PanelOverAll.Controls.Clear();
                        panelCalendarShow.Controls.Clear();
                        GradingShowReports.Controls.Clear();
                        var labelReportError = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        var labelReportErrors = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        var labelReportErrorss = new Label
                        {
                            Name = "ReportError",
                            Text = "Please Check Your Internet.",
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.Color.CornflowerBlue,
                            Size = new Size(290, 16),
                            Location = new Point(267, 117)
                        };

                        CalendarLabelPercent.Text = "Error";
                        GradingLabelPercent.Text = "Error";
                        OALabelPercent.Text = "Error";

                        if (reportNoHaving == "false")
                        {
                            reportErr = "false";
                            conditionifEqual = "false";


                            if (CalendarNumberCount == 0)
                            {
                                panelCalendarShow.Controls.Clear();
                                var labelReports = new Label
                                {
                                    Name = "ReportNoHaving",
                                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                    ForeColor = System.Drawing.Color.CornflowerBlue,
                                    Size = new Size(233, 16),
                                    Location = new Point(265, 117)
                                };
                                labelReports.Text = "No Reports The Calendar For Now.";
                                panelCalendarShow.Controls.Add(labelReports);
                            }
                            else
                            {
                                panelCalendarShow.Controls.Clear();
                            }

                            if (GradingNumberCountList == 0)
                            {
                                var labelReport = new Label
                                {
                                    Name = "ReportNoHaving",
                                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                                    ForeColor = System.Drawing.Color.CornflowerBlue,
                                    Size = new Size(233, 16),
                                    Location = new Point(265, 117)
                                };
                                GradingShowReports.Controls.Clear();
                                labelReport.Text = "No Reports The Grading For Now.";
                                GradingShowReports.Controls.Add(labelReport);
                            }
                            else
                            {
                                GradingShowReports.Controls.Clear();
                            }

                        }
                        else {
                            PanelOverAll.Controls.Add(labelReportError);
                            GradingShowReports.Controls.Add(labelReportErrors);
                            panelCalendarShow.Controls.Add(labelReportErrorss);
                            conditionifEqual = "true";
                        }

                    }

                    delegateflowPercent delegateCount = new delegateflowPercent(calendarPercentReport);
                    delegateCount.Invoke(handleForPercent, reportListGetAllData);

                    TimerReportSelf.Start();
                }

            }
        }

        //COUNT THE NUMBER THAT WILL PERCENT AND THE PAINT FLOW................................
        protected async void calendarPercentReport(int[] PercentArray, List<ReportList> ListOfDataReport) {
            for (int ArrayLength = 0; ArrayLength < 3; ArrayLength++) {
                if (condition_to_Paint != "true")
                {
                    Label label = (Label)(PercentagePanel.Controls[LabelArr[ArrayLength]]);
                    if (ArrayLength <= 2)
                    {
                        if (ArrayLength == 2)
                        {
                            label.Text = ((PercentArray[ArrayLength] * 100) / 2000.0).ToString() + "%";

                            condition_to_Paint = "true";
                            delegateflowPercent delegateFlowReturnAgain = new delegateflowPercent(calendarPercentReport);
                            delegateFlowReturnAgain.Invoke(PercentArray, ListOfDataReport);

                        }
                        else
                        {
                            label.Text = ((PercentArray[ArrayLength] * 100) / 1000.0).ToString() + "%";
                        }
                    }
                }
                else {
                    numberCountPanel--;
                    Panel pan = (Panel)(FlowPercentagePanel.Controls[PanelFlowPercent[ArrayLength]]);
                    pan.Invalidate();

                    if (ArrayLength + 1 >= 3) {
                        int HeightTopOfPanelOverALl = 13, HeightTopOfPanelCalendar = 13, numberCountCalendar = 0,
                            HeightTopOfPanelGrading = 13, numberCountGrading = 0, numberCount = 0;
                        if (PercentArray[2] != 0) {
                            if (conditionifEqual != "true") {
                                foreach (var DataGather in reportListGetAllData) {
                                    numberCount++;

                                    //OVER ALL LOCATION Y IN SHOW REPORTS............................
                                    if (numberCount <= 4)
                                    {
                                        if (HeightTopOfPanelOverALl != 13)
                                        {
                                            HeightTopOfPanelOverALl += 60;
                                        }
                                    }
                                    else
                                    {
                                        numberCount = 0;
                                        HeightTopOfPanelOverALl += 90;
                                    }


                                    //CALENDAR LOCATION Y IN SHOW REPORTS............................
                                    if (DataGather.ColorDeclared != "Coral") {
                                        numberCountCalendar++;
                                        if (numberCountCalendar <= 4)
                                        {
                                            if (HeightTopOfPanelCalendar != 13)
                                            {
                                                HeightTopOfPanelCalendar += 60;
                                            }
                                        }
                                        else
                                        {
                                            numberCountCalendar = 0;
                                            HeightTopOfPanelCalendar += 90;
                                        }
                                    }



                                    //GRADING LOCATION Y IN SHOW REPORTS............................
                                    if (DataGather.ColorDeclared != "#17202A")
                                    {
                                        numberCountGrading++;
                                        if (numberCountGrading <= 4)
                                        {
                                            if (HeightTopOfPanelGrading != 13)
                                            {
                                                HeightTopOfPanelGrading += 60;
                                            }
                                        }
                                        else
                                        {
                                            numberCountGrading = 0;
                                            HeightTopOfPanelGrading += 90;
                                        }
                                    }


                                    taskVoidShowData.Add(WayToShow(DataGather.id, DataGather.NameWhoMessage, DataGather.ImageAssest,
                                        DataGather.Message, DataGather.ColorDeclared, DataGather.DayReport,
                                        DataGather.MonthReport, DataGather.TimeMessage, DataGather.FullTimeMessage,
                                        HeightTopOfPanelOverALl, HeightTopOfPanelCalendar, HeightTopOfPanelGrading));

                                    //GRADING LOCATION Y IN SHOW REPORTS............................
                                    if (DataGather.ColorDeclared != "#17202A")
                                    {
                                        if (HeightTopOfPanelGrading == 13)
                                        {
                                            HeightTopOfPanelGrading = 14;
                                        }
                                    }

                                    //CALENDAR LOCATION Y IN SHOW REPORTS............................
                                    if (DataGather.ColorDeclared != "Coral") {
                                        if (HeightTopOfPanelCalendar == 13)
                                        {
                                            HeightTopOfPanelCalendar = 14;
                                        }
                                    }
                                    //OVER ALL LOCATION Y IN SHOW REPORTS............................
                                    if (HeightTopOfPanelOverALl == 13) {
                                        HeightTopOfPanelOverALl = 14;
                                    }
                                }

                                await Task.WhenAll(taskVoidShowData);
                            }
                        }

                        Reports.Enabled = true;
                        TimerReportSelf.Start();
                    }
                }
            }
        }

        //ONPAINT THAT ALSWAYS REAL TIME AUPDATE THE PAINT.....................................
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            try
            {
                if (numberCountPanel >= 0) {
                    this.flowPaintPercent((FlowPercentagePanel.Controls[PanelFlowPercent[numberCountPanel]]), e);
                }
            }
            catch (Exception es) {
                MessageBox.Show(numberCountPanel.ToString());
                MessageBox.Show(es.ToString());
            }
        }


        //PAINT AREA FLOW SEE.......................
        public void flowPaintPercent(object controls, PaintEventArgs e)
        {
            Panel pan = (Panel)controls;
            if (condition_to_Paint != "false")
            {
                if (pan.Name == "CalendarFlowReport")
                {
                    Graphics grap = e.Graphics;
                    Pen penShadow = new Pen(System.Drawing.ColorTranslator.FromHtml("#212F3C"), 30);
                    grap.DrawLine(penShadow, 0, 0, 110, 0);
                    Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("#1ABC9C"), 30);
                    grap.DrawLine(pen, 0, 0, ((handleForPercent[1] * 110) / 1000), 0);
                    grap.Dispose();
                }
                else if (pan.Name == "GradingFlowReport")
                {
                    Graphics grap = e.Graphics;
                    Pen penShadow = new Pen(System.Drawing.ColorTranslator.FromHtml("#212F3C"), 30);
                    grap.DrawLine(penShadow, 0, 0, 110, 0);
                    Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("Coral"), 30);
                    grap.DrawLine(pen, 0, 0, ((handleForPercent[0] * 110) / 1000), 0);
                    grap.Dispose();
                }
                else {
                    Graphics grap = e.Graphics;
                    Pen penShadow = new Pen(System.Drawing.ColorTranslator.FromHtml("#212F3C"), 30);
                    grap.DrawLine(penShadow, 0, 0, 110, 0);
                    Pen pen = new Pen(System.Drawing.ColorTranslator.FromHtml("#B3B6B7"), 30);
                    grap.DrawLine(pen, 0, 0, ((handleForPercent[2] * 110) / 2000), 0);
                    grap.Dispose();
                }
            }
        }




        //SHOW NA YUNG CONTROLS SA REPORTS.........................................
        protected async Task<int> WayToShow(int id, string NameWhoMessage, string ImageAssest, string Message,
                                string ColorDeclared, string DayReport, string MonthReport, string TimeMessage,
                                string FullTimeMessage, int HeightTopOfPanel, int HeightTopOfPanelCalendar,
                                int HeightTopOfPanelGrading) {

            delegateFlowShowData result = new delegateFlowShowData(showDataGather);
            string ShowData()
            {
                result.Invoke(id, NameWhoMessage, ImageAssest, Message,
                             ColorDeclared, DayReport, MonthReport, TimeMessage, FullTimeMessage, HeightTopOfPanel,
                             HeightTopOfPanelCalendar, HeightTopOfPanelGrading);
                return "";
            }

            Task<string> task = new Task<string>(() => ShowData());
            task.Start();

            await task.ConfigureAwait(true);

            return 0;
        }


        //SHOW ALL DATA IN REPORT..............................................................
        protected void showDataGather(int id, string NameWhoMessage, string ImageUser, string Message,
                                string ColorDeclared, string DayReport, string MonthReport, string TimeMessage,
                                string FullTimeMessage, int HeightTopOfPanel, int HeightTopOfPanelCalendar,
                                int HeightTopOfPanelGrading) {

            //FOR SCPECIFIC GRADING AND CALENDAR....................................
            Panel pan = new Panel {
                Name = "NameWhoMessage",
                Size = new Size(634, 45),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#1C2833"),
                Location = new Point(12, ((ColorDeclared == "Coral") ? HeightTopOfPanelGrading : HeightTopOfPanelCalendar))
            };

            PictureBox boxPic = new PictureBox {
                Image = Image.FromFile(ImageUser),
                Size = new Size(56, 35),
                Location = new Point(3, 5),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Panel panName = new Panel {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(152, 35),
                Location = new Point(67, 5)
            };

            Panel panName2 = new Panel {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(145, 28),
                Location = new Point(2, 4)
            };

            Label nameLabel = new Label
            {
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Text = NameWhoMessage,
                Location = new Point(1, 6),
                Size = new Size(200, 18)
            };



            Panel panMessage = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(250, 34),
                Location = new Point(226, 5)
            };

            Panel panMessage2 = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(232, 28),
                Location = new Point(8, 3)
            };

            Label nameMessage = new Label
            {
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Text = Message,
                Location = new Point(1, 6),
                Size = new Size(200, 18)
            };

            Button bttnColor = new Button {
                AccessibleDescription = id.ToString(),
                BackColor = ColorTranslator.FromHtml((ColorDeclared != "#17202A" ? "Coral" : "#1ABC9C")),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(483, 15),
                Size = new Size(15, 17)
            };
            bttnColor.Click += new System.EventHandler(BttnDeleteReports);

            Panel panDate = new Panel {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Location = new Point(505, 5),
                Size = new Size(121, 35)
            };

            Label labelDate = new Label
            {
                Size = new Size(150, 15),
                Location = new Point(3, 11),
                Text = FullTimeMessage,
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            };




            //FOR OVAERALL CONTROLS ADD..............................
            Panel pans = new Panel
            {
                Name = "NameWhoMessage",
                Size = new Size(634, 45),
                BackColor = System.Drawing.ColorTranslator.FromHtml("#1C2833"),
                Location = new Point(12, HeightTopOfPanel)
            };

            PictureBox boxPics = new PictureBox
            {
                Image = Image.FromFile(ImageUser),
                Size = new Size(56, 35),
                Location = new Point(3, 5),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Panel panNames = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(152, 35),
                Location = new Point(67, 5)
            };

            Panel panName2s = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(145, 28),
                Location = new Point(2, 4)
            };

            Label nameLabels = new Label
            {
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Text = NameWhoMessage,
                Location = new Point(1, 6),
                Size = new Size(200, 18)
            };



            Panel panMessages = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(250, 34),
                Location = new Point(226, 5)
            };

            Panel panMessage2s = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(232, 28),
                Location = new Point(8, 3)
            };

            Label nameMessages = new Label
            {
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                Text = Message,
                Location = new Point(1, 6),
                Size = new Size(200, 18)
            };

            Button bttnColors = new Button
            {
                AccessibleDescription = id.ToString(),
                BackColor = ColorTranslator.FromHtml((ColorDeclared != "#17202A" ? "Coral" : "#1ABC9C")),
                FlatStyle = FlatStyle.Flat,
                Location = new Point(483, 15),
                Size = new Size(15, 17)
            };
            bttnColors.Click += new System.EventHandler(BttnDeleteReports);

            Panel panDates = new Panel
            {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Location = new Point(505, 5),
                Size = new Size(121, 35)
            };

            Label labelDates = new Label
            {
                Size = new Size(150, 15),
                Location = new Point(3, 11),
                Text = FullTimeMessage,
                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            };




            Thread th = new Thread(() =>
            {

                //FOR SCPECIFIC GRADING AND CALENDAR....................................
                pan.Controls.Add(boxPic);

                pan.Controls.Add(panName);
                panName.Controls.Add(panName2);
                panName2.Controls.Add(nameLabel);

                pan.Controls.Add(panMessage);
                panMessage.Controls.Add(panMessage2);
                panMessage2.Controls.Add(nameMessage);

                pan.Controls.Add(bttnColor);

                pan.Controls.Add(panDate);
                panDate.Controls.Add(labelDate);




                //FOR OVAERALL CONTROLS ADD..............................
                pans.Controls.Add(boxPics);

                pans.Controls.Add(panNames);
                panNames.Controls.Add(panName2s);
                panName2s.Controls.Add(nameLabels);

                pans.Controls.Add(panMessages);
                panMessages.Controls.Add(panMessage2s);
                panMessage2s.Controls.Add(nameMessages);

                pans.Controls.Add(bttnColors);

                pans.Controls.Add(panDates);
                panDates.Controls.Add(labelDates);


                //FOR OVAERALL CONTROLS ADD..............................
                PanelOverAll.BeginInvoke((Action)delegate () {
                    PanelOverAll.Controls.Add(pans);
                });


                //FOR SCPECIFIC GRADING AND CALENDAR....................................
                Action ac = new Action(() => {

                    if (ColorDeclared != "Coral") {
                        panelCalendarShow.Controls.Add(pan);
                    }
                    else {
                        GradingShowReports.Controls.Add(pan);
                    }

                });
                this.BeginInvoke(ac);

            });
            th.Start();
        }


        //CLICK THE RECYLE ICON AND READ ICON TO PAST DELETE AND READ EACH PANEL IN REPORTS..........................
        private void PictureBox6_Click(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (pic.Name is "pictureBoxDeleteReports")
            {
                if (conditionToReadReport == "")
                {
                    if (String.IsNullOrEmpty(pic.AccessibleName))
                    {
                        pic.AccessibleName = "Have";
                        conditionToDeleteReport = "Have";
                        MessageBox.Show("Start Delete");
                    }
                    else
                    {
                        pic.AccessibleName = "";
                        conditionToDeleteReport = "";
                        MessageBox.Show("Stop Delete");
                    }
                }
                else
                {
                    MessageBox.Show("You Need to unClick the Read Icon To Access the Delete Icon");
                }
            }
            else {
                if (conditionToDeleteReport == "")
                {
                    if (String.IsNullOrEmpty(pic.AccessibleName))
                    {
                        pic.AccessibleName = "Have";
                        conditionToReadReport = "Have";
                        MessageBox.Show("Start to Read");
                    }
                    else
                    {
                        pic.AccessibleName = "";
                        conditionToReadReport = "";
                        MessageBox.Show("Stop to Read");
                    }
                }
                else
                {
                    MessageBox.Show("You Need to unClick the Delete Icon To Acces the Read Icon");
                }
            }

        }

        //CLICK THE COLOR TO DELETE OR READ THE PANEL IN REPORT................................
        public async void BttnDeleteReports(object controls, EventArgs e) {
            Control bttn = (Control)controls;
            Report rep = new Report();
            Button bttns = null;
            List<ReportList> reportList = new List<ReportList>();
            if (bttn.GetType() == typeof(Button)) {
                bttns = (Button)bttn;
                if (String.IsNullOrEmpty(conditionToReadReport) != true)
                {
                    TimerReportSelf.Stop();
                    panelReadingStation.Visible = true;

                    reportList = await Task.Run(() => rep.getStringById(bttns.AccessibleDescription, "vee")).ConfigureAwait(true);
                    foreach (var dataHandle in reportList) {
                        nameReadStation.Text = String.Format("Name: {0}", dataHandle.NameWhoMessage);
                        DateReadStation.Text = String.Format("Last Date: {0}", dataHandle.FullTimeMessage);
                        TextReadStation.Text = String.Format("{0}", dataHandle.Message);
                    }
                }
                else if (String.IsNullOrEmpty(conditionToDeleteReport) != true)
                {
                    TimerReportSelf.Stop();
                    rep.deletePanelSection = bttns.AccessibleDescription + " " + "vee";
                    if (rep.deletePanelSection == "success")
                    {
                        DataGatherListOfReport((Button)(navigator.Controls["Reports"]));
                    }
                    else {
                        MessageBox.Show("Please Check Your Internet");
                    }
                }
            }
        }


        //END OF REPORT.....................................................................................................................
        //..................................................................................................................................
        //..................................................................................................................................
        //..................................................................................................................................
        //..................................................................................................................................






        //START GRADING..................................................................................................................
        //..................................................................................................................
        //.................................................................................................................
        //..................................................................................................................



        private static Grading grading = new Grading();
        private List<GradingList> listSearchBar = new List<GradingList>();
        private delegate void handlingDataShows(string UserNameOwner, string UserNameCreator,
            string NameCreator, string ImageCreator, string SubjectCreator, string ColorCreator, string DateTimeCreated,
            int numberCountForTopPanel);
        private List<GradingList> listHandleSubjectAndName = new List<GradingList>();
        private static List<Task> taskDoSeeSearch = new List<Task>();
        private static bool conditionToFirstCome = false, conditionToSeeSub = false;
        private static int handleDataSub = 0;
        protected static string deleteSubject = "", ReadSubject = "";

        //STARTING TO GET THE DATA SEARCH.........................................................
        private async void gettingDataSearch() {
            int numberCountTopPanel = 9;
            listSearchBar = new List<GradingList>();
            taskDoSeeSearch = new List<Task>();

            SearchPanGrading.Controls.Clear();

            listSearchBar = await Task.Run(() => grading.gettingData()).ConfigureAwait(true);

            foreach (var dataSearch in listSearchBar) {
                if (String.IsNullOrEmpty(dataSearch.err) != false)
                {
                    taskDoSeeSearch.Add(doTheTashSeeSearch(dataSearch.id, dataSearch.UserName, dataSearch.FirstLastName,
                        dataSearch.ImageUser, numberCountTopPanel));

                    numberCountTopPanel += 53;
                }
                else {
                    MessageBox.Show("having Err");
                    Thread th = new Thread(() =>
                    {
                        Label labelErr = new Label
                        {
                            Name = "LabelNoSearchGrading",
                            Text = "Please Check Your Internet.",
                            Size = new Size(200, 16),
                            Location = new Point(68, 193),
                            Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                            ForeColor = System.Drawing.ColorTranslator.FromHtml("CornflowerBlue")
                        };

                        Action ac = new Action(() => SearchPanGrading.Controls.Add(labelErr));
                        SearchPanGrading.BeginInvoke(ac);
                    });
                    th.Start();
                }
            }

            await Task.WhenAll(taskDoSeeSearch);
        }






        //TASK DO.....................................
        //DO THE TASK TO SHOW ONE BY ONE USER IN SEARCH......................................................
        private async Task<string> doTheTashSeeSearch(int id, string Username, string FirstLastname, string ImageUser,
            int numberCountTopPanel) {

            Task<string> taskReturn = new Task<string>(() => SeeSearch(id, Username, FirstLastname, ImageUser, numberCountTopPanel));
            taskReturn.Start();

            string awaitStringDone = await taskReturn.ConfigureAwait(true);

            return awaitStringDone;
        }





        //DO THE TASK AND PAST IT INTO DELEGATE TO SHOW THE SUBJECT AND WHO'S CREATED THAT.........................
        protected Task taskSHowSubAndNameTeach(int idGrade, string UserNameOwner, string UserNameCreator,
            string NameCreator, string ImageCreator, string SubjectCreator, string ColorCreator, string DateTimeCreated, int numberCount,
            int numberCountForTopPanel)
        {

            handlingDataShows delegateHandle = new handlingDataShows(this.subjectShowHandle);
            delegateHandle.Invoke(UserNameOwner, UserNameCreator,
            NameCreator, ImageCreator, SubjectCreator, ColorCreator, DateTimeCreated, numberCountForTopPanel);

            if (numberCount == handleDataSub) {
                GradingCreateSubject.Visible = true;
                timerGradingSub.Start();
            }

            return Task.CompletedTask;
        }


        //....................................................










        //THIS IS THE ZONE TO SHOW ALL THE INFORMATIONS.............................................................

        //FINAL SHOW THE USER IN SEARCH.............................................................................
        protected string SeeSearch(int id, string Username, string FirstLastname, string ImageUser, int numberCountTopPanel) {
            Thread th = new Thread(ThreadUsing);

            void ThreadUsing()
            {
                Panel pan = new Panel
                {
                    Name = Username,
                    AccessibleName = FirstLastname,
                    AccessibleDescription = ImageUser,
                    Size = new Size(276, 50),
                    Location = new Point(12, numberCountTopPanel),
                    BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A")
                };
                pan.Click += new System.EventHandler(ClickSearched);

                PictureBox pic = new PictureBox
                {
                    Name = Username,
                    AccessibleName = FirstLastname,
                    AccessibleDescription = ImageUser,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = Image.FromFile(ImageUser),
                    Size = new Size(48, 42),
                    Location = new Point(5, 4)
                };
                pic.Click += new System.EventHandler(ClickSearched);

                Label label = new Label {
                    Name = Username,
                    AccessibleName = FirstLastname,
                    AccessibleDescription = ImageUser,
                    Text = FirstLastname,
                    Location = new Point(57, 24),
                    ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular),
                    Size = new Size(200, 16)
                };
                label.Click += new System.EventHandler(ClickSearched);

                Action ac = () => {
                    pan.Controls.Add(pic);
                    using (System.Drawing.Drawing2D.GraphicsPath pathDraw = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        pathDraw.AddEllipse(0, 0, 48, 42);
                        Region rg = new Region(pathDraw);
                        pic.Region = rg;
                    }
                    pan.Controls.Add(label);
                    SearchPanGrading.Controls.Add(pan);
                };

                SearchPanGrading.BeginInvoke(ac);
            }
            th.Start();

            return "Done";
        }


        //THIS IS TO SHOW THE SUBJECT OF THE USER OTHER.....................................
        private void subjectShowHandle(string UserNameOwner, string UserNameCreator,
            string NameCreator, string ImageCreator, string SubjectCreator, string ColorCreator, string DateTimeCreated,
            int numberCountForTopPanel) {
            int[] handleColorByEach = new int[] { 0, 0, 0 };
            string HandleStringColor = "";
            int handleCountCondition = 0;

            //SEPERATE THE BOND OF STRING IT WILL TURN INTO INT.....................................
            for (int handle = 0; handle < ColorCreator.Length; handle++) {
                if (ColorCreator[handle].ToString() != "," && ColorCreator[handle].ToString() != " ")
                {
                    HandleStringColor = HandleStringColor + ColorCreator[handle].ToString();
                }
                else {
                    if (ColorCreator[handle].ToString() == " ") {
                        handleColorByEach[handleCountCondition] = Convert.ToInt32(HandleStringColor);
                        handleCountCondition = handleCountCondition + 1;
                        HandleStringColor = "";
                    }
                }

                if (handle >= ColorCreator.Length - 1) {
                    handleColorByEach[2] = Convert.ToInt32(HandleStringColor);
                }
            }



            Thread th = new Thread(() => {
                Panel pansub = new Panel {
                    Size = new Size(372, 32),
                    Location = new Point(6, numberCountForTopPanel),
                    BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A")
                };

                Label labelsub = new Label
                {
                    Size = new Size(372, 16),
                    Location = new Point(9, 9),
                    Text = SubjectCreator,
                    ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                    Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular)
                };

                Panel pancolor = new Panel
                {
                    Size = new Size(60, 32),
                    Location = new Point(384, numberCountForTopPanel),
                    BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A")
                };

                Button bttnColor = new Button {
                    Name = "BttnColor",
                    AccessibleName = SubjectCreator,
                    AccessibleDescription = UserNameCreator,
                    Size = new Size(31, 23),
                    Location = new Point(15, 4),
                    BackColor = System.Drawing.Color.FromArgb(handleColorByEach[0], handleColorByEach[1], handleColorByEach[2]),
                };
                bttnColor.Click += new System.EventHandler((object controls, EventArgs e) => clickColorsSub(controls, e));
                bttnColor.FlatStyle = FlatStyle.Flat;

                Action ac = () => {
                    SubjectJarPanel.Controls.Add(pansub);
                    pansub.Controls.Add(labelsub);
                    SubjectJarPanel.Controls.Add(pancolor);
                    pancolor.Controls.Add(bttnColor);
                };
                SubjectJarPanel.BeginInvoke(ac);

                NameTeachSubject.BeginInvoke((Action)delegate {
                    handlingDataShows delegateHandle = new handlingDataShows(this.subjectShowNameOfTeacher);
                    delegateHandle.Invoke("", "",
                    NameCreator, ImageCreator, "", "", DateTimeCreated, numberCountForTopPanel);
                });
            });
            th.Start();

        }



        //THIS IS THE NAME OF EVERY USER IN SUBJET CREATED.......................................
        private void subjectShowNameOfTeacher(string UserNameOwner, string UserNameCreator,
            string NameCreator, string ImageCreator, string SubjectCreator, string ColorCreator, string DateTimeCreated,
            int numberCountForTopPanel)
        {
            Panel pan = new Panel {
                BackColor = System.Drawing.ColorTranslator.FromHtml("#17202A"),
                Size = new Size(255, 33),
                Location = new Point(8, numberCountForTopPanel - 1)
            };

            PictureBox boxPic = new PictureBox {
                Size = new Size(33, 28),
                Location = new Point(6, 2),
                Image = Image.FromFile(ImageCreator),
                SizeMode = PictureBoxSizeMode.StretchImage,
            };

            Label label = new Label();
            label.Text = NameCreator;
            label.ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7");
            label.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            label.Size = new Size(290, 16);
            label.Location = new Point(44, 11);

            Thread th = new Thread(() => {
                NameTeachSubject.BeginInvoke((Action)delegate
                {
                    NameTeachSubject.Controls.Add(pan);
                    pan.Controls.Add(boxPic);
                    pan.Controls.Add(label);
                });
            });
            th.Start();
        }


        //......................................................................................









        //BUTTON OF EVERY USER IN SEARCHED BOX..........................................................
        private void ClickSearched(object control, EventArgs e) {
            CreateSubject.AccessibleDescription = "vee";
            Control con = (Control)control;
            PictureBox pic = null;
            Label label = null;
            Panel panel = null;
            if (con.GetType() == typeof(Panel)) {
                panel = (Panel)con;
                handleUsername = panel.Name;
                handleFirsLastName = panel.AccessibleName;
                handleImage = panel.AccessibleDescription;
                handleDataSub = 0;
                conditionToFirstCome = true;
                this.showClickedInfoUserGradingSearch(handleUsername, handleFirsLastName, handleImage);
            } else if (con.GetType() == typeof(Label)) {
                label = (Label)con;
                handleUsername = label.Name;
                handleFirsLastName = label.AccessibleName;
                handleImage = label.AccessibleDescription;
                handleDataSub = 0;
                conditionToFirstCome = true;
                this.showClickedInfoUserGradingSearch(handleUsername, handleFirsLastName, handleImage);
            }
            else {
                pic = (PictureBox)con;
                handleUsername = pic.Name;
                handleFirsLastName = pic.AccessibleName;
                handleImage = pic.AccessibleDescription;
                handleDataSub = 0;
                conditionToFirstCome = true;
                this.showClickedInfoUserGradingSearch(handleUsername, handleFirsLastName, handleImage);
            }
        }




        //INTERVAL IN GRADING SUBJECT CREATE....................................
        protected void gradingInterval(object control, EventArgs e) {
            this.showClickedInfoUserGradingSearch(handleUsername, handleFirsLastName, handleImage);
        }




        //AFTER CLICK THE BUTTON THE CURRENT VALUE WILL RUN THIS FUNCTION AND SHOW TO OTHER PANEL..................................
        private async void showClickedInfoUserGradingSearch(string UsernameSelected, string FirstLastName, string ImageUser) {
            grading = new Grading();
            timerGradingSub.Stop();
            int numberCount = 0, numberHandleTopPanel = 11;
            List<GradingList> listHandle = new List<GradingList>();
            List<Task> taskSHowSubAndNameTeach = new List<Task>();
            listHandle = await Task.Run(() => grading.getDataAccordingSelectedUser(UsernameSelected)).ConfigureAwait(true);

            if ((Int32)(listHandle.Count) != 0) {
                Int32 count = listHandle.Count();
                if (conditionToFirstCome != true)
                {
                    handleDataSub = count;
                    conditionToFirstCome = true;
                    SubjectJarPanel.Controls.Clear();
                    NameTeachSubject.Controls.Clear();
                }
                else {
                    if (handleDataSub == count)
                    {
                        timerGradingSub.Start();
                        conditionToSeeSub = true;
                        foreach (var checknumber in listHandle) {
                            if (checknumber.numberGrade == 0)
                            {
                                conditionToSeeSub = false;
                                SubjectJarPanel.Controls.Clear();
                                NameTeachSubject.Controls.Clear();

                            }
                        }
                    }
                    else {
                        handleDataSub = count;
                        conditionToSeeSub = false;
                        SubjectJarPanel.Controls.Clear();
                        NameTeachSubject.Controls.Clear();
                    }
                }

                foreach (var doList in listHandle) {
                    if (String.IsNullOrEmpty(doList.errGrade) != false)
                    {
                        if (conditionToSeeSub != true)
                        {
                            if (doList.numberGrade == 0)
                            {


                                Thread th = new Thread(() => ThreadSee());
                                void ThreadSee()
                                {

                                    Label labelMessageSubject = new Label();
                                    labelMessageSubject.Text = "No Subject For Now";
                                    labelMessageSubject.Size = new Size(160, 20);
                                    labelMessageSubject.ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7");
                                    labelMessageSubject.Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular);
                                    labelMessageSubject.Location = new Point(163, 145);


                                    Label NameTeachsSubject = new Label();
                                    NameTeachsSubject.Text = "No Teachers Created";
                                    NameTeachsSubject.Size = new Size(160, 20);
                                    NameTeachsSubject.ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7");
                                    NameTeachsSubject.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
                                    NameTeachsSubject.Location = new Point(70, 120);

                                    this.BeginInvoke((Action)delegate ()
                                    {
                                        pictureGradingSet.Image = Image.FromFile(ImageUser);
                                        labelnameSet.Text = FirstLastName;

                                        NameTeachSubject.Controls.Add(NameTeachsSubject);
                                        SubjectJarPanel.Controls.Add(labelMessageSubject);
                                        GradingCreateSubject.Visible = true;
                                        timerGradingSub.Start();
                                    });
                                }

                                th.Start();
                                handleDataSub = 0;
                            }
                            else
                            {
                                numberCount++;
                                taskSHowSubAndNameTeach.Add(this.taskSHowSubAndNameTeach(doList.idGrade, doList.UserNameOwner,
                                    doList.UserNameCreator, doList.NameCreator, doList.ImageCreator,
                                    doList.SubjectCreator, doList.ColorCreator, doList.DateTimeCreated, numberCount, numberHandleTopPanel));
                                numberHandleTopPanel = numberHandleTopPanel + 37;

                                if (numberCount == (Int32)(listHandle.Count)) {
                                    labelnameSet.Text = FirstLastName;
                                    await Task.WhenAll(taskSHowSubAndNameTeach);
                                }
                            }
                        }
                    }
                    else
                    {
                        Thread th = new Thread(ths);
                        MessageBox.Show("Please Check Your Internet Connection");
                        void ths() {
                            Label labelErrSub = new Label {
                                Text = "Please Check Your Internet Connection.",
                                ForeColor = System.Drawing.ColorTranslator.FromHtml("#B3B6B7"),
                                Size = new Size(280, 20),
                                Location = new Point(103, 145),
                                Font = new Font("Microsoft Sans Serif", 11, FontStyle.Regular)
                            };
                            SubjectJarPanel.BeginInvoke((Action)delegate () {
                                labelnameSet.Text = "Undefined name";
                                GradingCreateSubject.Visible = true;
                                SubjectJarPanel.Controls.Add(labelErrSub);

                                handleDataSub = 0;
                                timerGradingSub.Start();
                            });
                        }
                        th.Start();
                    }
                }
            }
            return;
        }

        //CLICK THE LETTER X TO HIDE THE CREATE SUBJECT.............................................
        private void Label41_Click(object sender, EventArgs e)
        {
            CreateSubject.AccessibleDescription = "";
            GradingCreateSubject.Visible = false;
            timerGradingSub.Stop();
        }

        private async void CreateSubject_Click(object sender, EventArgs e)
        {
            if (textBox1CreateSub.Text.Length != 0) {
                timerGradingSub.Stop();
                listHandleSubjectAndName = new List<GradingList>();
                listHandleSubjectAndName = await Task.Run(() => grading.savingCreateSubAndReturn(
                    textBox1CreateSub.Text, "kyle velarde", handleImage,
                    CreateSubject.AccessibleDescription, handleUsername));
                foreach (var handleDataSub in listHandleSubjectAndName) {
                    if (handleDataSub.errGrade == "")
                    {
                        if (handleDataSub.SubjectCreator == "Same") {
                            MessageBox.Show(String.Format("The '{0}' has already have in database sorry.", textBox1CreateSub.Text));
                        }
                    }
                    else {
                        MessageBox.Show("Please Check Your Connection.");
                    }
                }

                this.showClickedInfoUserGradingSearch(handleUsername, handleFirsLastName, handleImage);
            }
        }



        //deleteSubject
      //      ReadSubject
        //BUTTON DELETE SUBJECTS...................................................
        private static void StartDeleteSubjects(object control, EventArgs e)
        {
            if (ReadSubject == "")
            {
                if (deleteSubject == "")
                {
                    deleteSubject = "Start Delete Subjects";
                    MessageBox.Show("Start delete");
                }
                else
                {
                    deleteSubject = "";
                    MessageBox.Show("Stop deleting");
                }
            }
            else {
                MessageBox.Show("Please Unclick the Read Icon");
            }
        }



        //BUTTON READ THE SUBJECTS............................................
        private static void StartReadSubjects(object control, EventArgs e)
        {
            if (deleteSubject == "")
            {
                if (ReadSubject == "")
                {
                    ReadSubject = "Start Read Subjects";
                    MessageBox.Show("Start read");
                }
                else
                {
                    ReadSubject = "";
                    MessageBox.Show("Stop reading");
                }
            }
            else {
                MessageBox.Show("Please Unclick the Delete Icon");
            }
        }







        private void clickColorsSub(object controls, EventArgs e) {
            if (ReadSubject != "")
            {
                Control con = (Control)(controls);
                if (con.GetType() == typeof(Button)) {
                }
            } else if (String.IsNullOrEmpty(deleteSubject) != true) {
            }
        }




    }
}

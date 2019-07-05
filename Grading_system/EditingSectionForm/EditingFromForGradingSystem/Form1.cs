using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditingFromForGradingSystem
{
    public partial class Form1 : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = 0x20000;
                return cp;
            }
        }
        public Form1()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(load_form);

            monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(MonthCalendar1_DateChanged);
            comboBoxHrs.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxMnt.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);
            comboBoxAP.SelectedIndexChanged += new System.EventHandler(SelectedIndex_AllComboBoxCalenderSet);

            using (GraphicsPath grap = new GraphicsPath()) {
                grap.AddEllipse(0, 0, PictureBoxScheduleAddCalendar.Width, PictureBoxScheduleAddCalendar.Height);
                Region region = new Region(grap);
                PictureBoxScheduleAddCalendar.Region = region;
            }

            VScrollBar bar = new VScrollBar();
            bar.Name = "asd";
            bar.Maximum = 100;
            bar.Dock = DockStyle.Right;
            bar.Top = 0;
            bar.Left = ScheduleUserPanel2.Width - bar.Width;
            bar.Height = ScheduleUserPanel2.Height;
            bar.Visible = true;
            ScheduleUserPanel2.Controls.Add(bar);
            bar.Scroll += (sender, e) => { ScheduleUserPanel2.VerticalScroll.Value = bar.Value; };



            //THIS IS TIME OF CALENDAR................
            timer1.Tick += new System.EventHandler(TimeCalendar);

            TimeCalendarLabel.Text = "00:00:00 Null";
            timer1.Start();
        }


        //FUNCTION TICK START THE LOOPING TO SEE THE DYNAMICALY RESTORE TIME...............
        public string Hrs, Mmn, Sec, AP = "";
        private void TimeCalendar(object sender, EventArgs e) {
            int hrs = DateTime.Now.Hour;
            int mmn = DateTime.Now.Minute;
            int second = DateTime.Now.Second;
            if (hrs > 11)
            {
                if (hrs <= 12)
                {
                    Hrs = String.Format("{0}", (hrs - 12));
                }
                else {
                    Hrs = String.Format("0{0}", (hrs - 12));
                }

                AP = "PM";
            }
            else {
                if (hrs < 10)
                {
                    Hrs = String.Format("0{0}", hrs);
                }
                else {
                    Hrs = String.Format("{0}", hrs);
                }

                AP = "AM";
            }

            if (mmn < 10)
            {
                Mmn = String.Format("0{0}", mmn);
            }
            else {
                Mmn = String.Format("{0}", mmn);
            }

            if (second < 10)
            {
                Sec = "0" + second.ToString();
            }
            else {
                Sec = second.ToString();
            }


            Label label = new Label();
            label.Text = "Time:";
            label.Width = 65;
            label.Location = new System.Drawing.Point(0, 0);
            label.ForeColor = System.Drawing.Color.CornflowerBlue;
            TimeCalendarLabel.Controls.Add(label);

            TimeCalendarLabel.Text = ":         "+Hrs+":"+Mmn+":"+Sec+" "+AP;
        }

        public void load_form(object sender, EventArgs e) {
            string[] arrComboBox = new string[] { "comboBoxHrs", "comboBoxMnt", "comboBoxAP" };
            int[] arrComboHrs = new int[] { 1,2,3,4,5,6,7,8,9,10,11,12 };
            int[] arrComboMNT = new int[] { 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 59 };
            for (int numberCountCom = 2;numberCountCom >= 0;numberCountCom--) {
                switch(arrComboBox[numberCountCom])
                {
                    case "comboBoxHrs":
                        ComboBox comboHrs = (ComboBox)(TimeForDate.Controls[arrComboBox[numberCountCom]]);
                        foreach (int handleHrs in arrComboHrs) {
                            comboHrs.Items.Add(handleHrs);
                        }
                        break;
                    case "comboBoxMnt":
                        ComboBox comboMNT = (ComboBox)(TimeForDate.Controls[arrComboBox[numberCountCom]]);
                        foreach (int handleMNT in arrComboMNT)
                        {
                            comboMNT.Items.Add(handleMNT);
                        }
                        break;
                    case "comboBoxAP":
                        comboBoxAP.Items.Add("AM");
                        comboBoxAP.Items.Add("PM");
                    break;
                }
            }
        }

        protected static string[] CalendarRange = new string[3];
        protected static string[] DateTimeRange = new string[] { "", "", ""};
        public static string asd = "";
        public static string asdd = "";
        private void MonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            string stringSelectionRange = monthCalendar1.SelectionStart.ToString();
            string handle = "";
            int countNumberCalendar = 0;
            bool conditionNumberDate = false;
             for (int CountSelection = 0;CountSelection < stringSelectionRange.Length;CountSelection++) {
                if (conditionNumberDate != true) {
                    switch (stringSelectionRange[CountSelection]) {
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
             }

            asd = CalendarRange[0] + "/" + CalendarRange[1] + "/" + CalendarRange[2]+" ";
            CheckingWay();
        }

        public void SelectedIndex_AllComboBoxCalenderSet(object sender, EventArgs e) {
            Control comboBox = (Control)sender;
            if (comboBox.GetType() == typeof(ComboBox) || comboBox is ComboBox) {
                if (comboBox.AccessibleName == "HrsDate")
                {
                    DateTimeRange[0] = comboBoxHrs.SelectedItem.ToString();
                }
                else if (comboBox.AccessibleName == "MntDate")
                {
                    DateTimeRange[1] = comboBoxMnt.SelectedItem.ToString();
                }
                else {
                    DateTimeRange[2] = comboBoxAP.SelectedItem.ToString();
                }

                asdd = DateTimeRange[0] + ":" + DateTimeRange[1] + " " + DateTimeRange[2];
            }
            CheckingWay();
        }

        protected void CheckingWay() {
            CheckingDataSchedule.Text = asd + asdd;
        }


    }
}

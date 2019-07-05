using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Security.Cryptography;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace ClassUserForm
{
    public class Calendar
    {

        //THIS IS THE ARRIVED USER IN CALENDAR GET ALL DATA BEFORE IT LOADING....................................
        public List<CalendarList> GetAllDataIn_searchbargradingaccounts() {
            List<CalendarList> calendarHandleData = new List<CalendarList>();
            int conditionToSee = 0;
            MySqlConnection connGetDataAllIn_searchbargradingaccounts = new MySqlConnection("" +
                "Server=localhost;Database=grading_accounts;Uid=root;Pwd=");

            try
            {
                connGetDataAllIn_searchbargradingaccounts.Open();
                MySqlCommand comm = connGetDataAllIn_searchbargradingaccounts.CreateCommand();
                comm.CommandText = "SELECT * FROM searchbargradingaccounts";
                using (MySqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (conditionToSee == 0)
                        {
                            conditionToSee++;
                        }
                        calendarHandleData.Add(new CalendarList
                        {
                            numberCondition = (int)conditionToSee,
                            ErrorCondition = "",
                            UsernameGet = (string)reader["UserName"],
                            FirstLastNameGet = reader["FirstLastName"].ToString(),
                            ImageuserGet = String.Format("{0}", reader["ImageUser"])
                        });
                    }

                    if (conditionToSee == 0)
                    {
                        calendarHandleData.Add(new CalendarList
                        {
                            numberCondition = (int)conditionToSee
                        });
                    }
                    connGetDataAllIn_searchbargradingaccounts.Close();
                    return calendarHandleData;

                }
            } catch (Exception e) {
                string asd = e.ToString();
                calendarHandleData.Add(new CalendarList {
                    ErrorCondition = "Check Your Connection."
                });
                return calendarHandleData;
            }

        }

        //Search Bar to find the value that needed to User Find calendar FIND.................................
        public List<CalendarList> GettingSearchText(string textBoxSearch) {
            List<CalendarList> JarSearchDoneValue = new List<CalendarList>();
            int conditionToSee = 0;
            MySqlConnection connSearchText = new MySqlConnection("Server=localhost;Database=grading_accounts;" +
                "Uid=root;Pwd=");
            try {
                connSearchText.Open();
                MySqlCommand commFindText = connSearchText.CreateCommand();
                commFindText.CommandText = "SELECT * FROM `searchbargradingaccounts` WHERE `FirstLastName` LIKE " +
                    "@textBoxSearch";
                commFindText.Parameters.AddWithValue("@textBoxSearch", '%' + textBoxSearch + '%');

                using (MySqlDataReader reader = commFindText.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (conditionToSee == 0) {
                            conditionToSee++;
                        }
                        JarSearchDoneValue.Add(new CalendarList
                        {
                            numberCondition = (int)conditionToSee,
                            UsernameSearch = (string)reader["UserName"],
                            ErrorCondition = "",
                            FirstLastNameSearch = (string)reader["FirstLastName"],
                            ImageuserSearch = (string)reader["ImageUser"],
                        });
                    }
                }

                if (conditionToSee == 0)
                {
                    JarSearchDoneValue.Add(new CalendarList
                    {
                        numberCondition = (int)conditionToSee,
                        ErrorCondition = ""
                    });
                }

                connSearchText.Close();
                return JarSearchDoneValue;
            }
            catch (Exception e) {
                string asd = e.ToString();
                JarSearchDoneValue.Add(new CalendarList {
                    ErrorCondition = "Check Your Connection."
                });
                return JarSearchDoneValue;
            }
        }



        //ITO YUNG PARA MAKITA YUNG ARRIVAL SCHED NI USER DUN SA 'ADD SCHED'.....................................
        public List<CalendarList> GetSchedUserClicked(string NameOfUser) {
            int numberCon = 0;
            List<CalendarList> handleData = new List<CalendarList>();
            MySqlConnection connGetSched = new MySqlConnection(String.Format("Server=localhost;Database=" +
                "grading_accounts_{0};Uid=root;Pwd=", NameOfUser));
            try {
                connGetSched.Open();
                MySqlCommand comm = connGetSched.CreateCommand();
                comm.CommandText = "SELECT * FROM `calendarsched`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        if (numberCon == 0) {
                            numberCon++;

                            handleData.Add(new CalendarList
                            {
                                numberCount = numberCon,
                                ErrCheck = ""
                            });
                        }
                    }
                }
                if (numberCon == 0) {
                    handleData.Add(new CalendarList {
                        numberCount = numberCon,
                        ErrCheck = ""
                    });
                }

                Thread.Sleep(2000);
                return handleData;
            }
            catch (Exception e)
            {
                string asd = e.ToString();

                handleData.Add(new CalendarList
                {
                    ErrCheck = "You must check your internet"
                });

                return handleData;
            }
        }




        //SCANNING IF HINDI TATAMAAN YUNG IBANG SCHEDULE NG IBANG USER PAG NAG ADD KA NG SCHEDULE...................
        public async Task<string> ScheduleCheckScanning(List<CalendarList> HandlingScheduleAdd) {
            string[] handleData = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };
            string handleCondition = "";
            string handleMessage = "";
            string handleForEach = "";
            string forFirstCome = "";
            List<CalendarList> scandforsched = new List<CalendarList>();
            foreach (var handlingScheduleAdd in HandlingScheduleAdd) {
                handleData = new string[] {
                    handlingScheduleAdd.UsernameOfUserThatWillAdd,
                    handlingScheduleAdd.NameUserWhoAdded,
                    handlingScheduleAdd.DateTimeRange,
                    handlingScheduleAdd.calendarRangeAddMonth,
                    handlingScheduleAdd.calendarRangeAddConvert,
                    handlingScheduleAdd.calendarRangeAddDay,
                    handlingScheduleAdd.calendarRangeAddYear,
                    handlingScheduleAdd.DateTimeRangeAddHrs,
                    handlingScheduleAdd.DateTimeRangeAddMnt,
                    handlingScheduleAdd.DateTimeRangeAddAP,
                    handlingScheduleAdd.SetDurationTimeAddHrs,
                    handlingScheduleAdd.SetDurationTimeAdd
                };
            }

            MySqlConnection connScan = new MySqlConnection(String.Format("Server=localhost;" +
                "Database=grading_accounts_{0};Uid=root;Pwd=", handleData[0]));

            try {
                connScan.Open();

                //USING AWAIT THAT SUCCESFULL TO ASS IN THE LIST.......................................
                Task<string> taskAddList = new Task<string>(AddList);
                taskAddList.Start();
                handleForEach = await taskAddList.ConfigureAwait(false);


                //GET THE DATA SCEDHULE IN DATABASE...................................
                string AddList()
                {
                    MySqlCommand comm = connScan.CreateCommand();
                    comm.CommandText = "SELECT * FROM `calendarsched`";
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scandforsched.Add(new CalendarList
                            {
                                NameUserWhoAdded = (string)reader["NameUserSchedWhoAdd"],
                                DateTimeRangeAddHrs = (string)reader["HrsSched"],
                                DateTimeRangeAddMnt = (string)reader["MntSched"],
                                DateTimeRangeAddAP = (string)reader["APSched"],
                                calendarRangeAddMonth = (string)reader["MonthSched"],
                                calendarRangeAddConvert = (string)reader["MonthSchedConvert"],
                                calendarRangeAddDay = (string)reader["DaySched"],
                                calendarRangeAddYear = (string)reader["YearSched"],
                                SetDurationTimeAdd = (string)reader["SetDurationMint"],
                                SetDurationTimeAddHrs = (string)reader["SetDurationHrs"],
                                DateTimeRange = (string)reader["TimeDateSchedFinal"]
                            });
                        }
                    }
                    Thread.Sleep(2000);
                    return "have";
                }

                if (String.IsNullOrEmpty(handleForEach) != true) {

                    //GET EACH DATA AND CHECK IF HAVE A SAME SCHEDULE..........................
                    foreach (var HandleDataEach in scandforsched) {
                        if (handleCondition == "") {
                            forFirstCome = "Have";
                            handleCondition = "have";

                            //AWAIT AND RETURN DATA IF HAVE FOUND AND SAME DATA IN DATABASE OR NOT..........

                            handleMessage = await Task.Run(() => scanningAreaSched(
                                HandleDataEach.NameUserWhoAdded, HandleDataEach.DateTimeRangeAddHrs,
                                HandleDataEach.DateTimeRangeAddMnt, HandleDataEach.DateTimeRangeAddAP,
                                HandleDataEach.calendarRangeAddMonth, HandleDataEach.calendarRangeAddConvert,
                                HandleDataEach.calendarRangeAddDay, HandleDataEach.calendarRangeAddYear,
                                HandleDataEach.SetDurationTimeAdd, HandleDataEach.SetDurationTimeAddHrs,
                                HandleDataEach.DateTimeRange
                                )).ConfigureAwait(false);

                            if (handleMessage == "") {
                                handleCondition = "";
                            }
                        }
                    }

                    if (forFirstCome != "Have") {
                        if (DateTime.Now.Year == Convert.ToInt32(handleData[6]))
                        {
                            if (DateTime.Now.Month <= Convert.ToInt32(handleData[3]))
                            {
                                if (DateTime.Now.Day <= Convert.ToInt32(handleData[5]))
                                {
                                    if (DateTime.Now.Day == Convert.ToInt32(handleData[5]))
                                    {
                                        int addingHandle = 0;
                                        //THIS IS FOR ADDING HRS....................
                                        if (handleData[9] == "PM")
                                        {
                                            if (handleData[7] == "12")
                                            {
                                                addingHandle = 12;
                                            }
                                            else
                                            {
                                                addingHandle = 12 + Convert.ToInt32(handleData[7]);

                                            }
                                        }
                                        else
                                        {
                                            addingHandle = Convert.ToInt32(handleData[7]);
                                        }

                                        if (DateTime.Now.Hour <= addingHandle)
                                        {
                                            if (DateTime.Now.Hour == addingHandle)
                                            {
                                                if (DateTime.Now.Minute <= Convert.ToInt32(handleData[8]))
                                                {
                                                    handleMessage = "";
                                                }
                                                else
                                                {
                                                    handleMessage = "The minute is done look at your dateTime.";
                                                }
                                            }
                                            else {
                                                handleMessage = "";
                                            }
                                        }
                                        else
                                        {
                                            handleMessage = "The Hrs is done look at your dateTime.";
                                        }
                                    }
                                    else {
                                        handleMessage = "";
                                    }
                                }
                                else {
                                    handleMessage = "This day is already passed.";
                                }
                            }
                            else {
                                handleMessage = "This month is already done by the day.";
                            }
                        }
                        else {
                            handleMessage = "The year need the same as our year to today.";
                        }
                    }
                }
            } catch (Exception e) {
                throw e;
            }


            //(((((A.I)))))) SCANNING SCHEDULE MONTH YEAR AND DAY STATEMENT.............................................
            async Task<string> scanningAreaSched(
                string NameUserWhoAdded, string DateTimeRangeAddHrs,
                string DateTimeRangeAddMnt, string DateTimeRangeAddAP,
                string calendarRangeAddMonth, string calendarRangeAddConvert,
                string calendarRangeAddDay, string calendarRangeAddYear,
                string SetDurationTimeAdd, string SetDurationTimeAddHrs,
                string DateTimeRange
                ) {
                string MessageManaged = "";
                string handleAwait = "";
                //if (NameUserWhoAdded != handleData[1])
               // {
                    if (DateTimeRange != handleData[2])
                    {
                        if (DateTime.Now.Year == Convert.ToInt32(handleData[6]))
                        {
                            if (DateTime.Now.Month <= Convert.ToInt32(handleData[3]))
                            {
                                if (DateTime.Now.Month == Convert.ToInt32(handleData[3]))
                                {
                                    if (DateTime.Now.Day <= Convert.ToInt32(handleData[5]))
                                    {
                                    //Scanning......................

                                    if (Convert.ToInt32(calendarRangeAddDay) == Convert.ToInt32(handleData[5]))
                                    {
                                        
                                                if (handleAwait == "")
                                                {
                                                    handleAwait = "Awaited";
                                                    int addingHandle = 0;
                                                    int currentSchedHandle = 0;

                                                    //THIS IS FOR ADDING HRS....................
                                                    if (handleData[9] == "PM")
                                                    {
                                                        if (handleData[7] == "12")
                                                        {
                                                            addingHandle = 12;
                                                        }
                                                        else
                                                        {
                                                            addingHandle = 12 + Convert.ToInt32(handleData[7]);

                                                        }
                                                    }
                                                    else
                                                    {
                                                        addingHandle = Convert.ToInt32(handleData[7]);
                                                    }

                                                    //THIS IS FOR CURRENT HRS SCHED.........................
                                                    if (DateTimeRangeAddAP == "PM")
                                                    {
                                                        if (DateTimeRangeAddHrs == "12")
                                                        {
                                                            currentSchedHandle = 12;
                                                        }
                                                        else
                                                        {
                                                            currentSchedHandle = 12 + Convert.ToInt32(DateTimeRangeAddHrs);

                                                        }
                                                    }
                                                    else
                                                    {
                                                        currentSchedHandle = Convert.ToInt32(DateTimeRangeAddHrs);
                                                    }

                                            if (DateTime.Now.Hour <= addingHandle)
                                            {
                                                if (DateTime.Now.Minute <= Convert.ToInt32(handleData[8]))
                                                {
                                                    //AFTER CONVERT THE PM EXAMPLE 1 PM TO 13 PM OR NOT THIS SHOULD DO 
                                                    //SCANNING HRS AND MNT........
                                                    Task<string> functionScanTask = new Task<string>(() => scanningFinalForSched(
                                                        currentSchedHandle.ToString(),
                                                        DateTimeRangeAddMnt, SetDurationTimeAdd, SetDurationTimeAddHrs,
                                                        addingHandle.ToString(), handleData[8], handleData[10], handleData[11]));
                                                    functionScanTask.Start();

                                                    //HANDLING MESSAGE KUNG MAY MA AATTACH BA SIYANG SCHED NG IBA OR HINDI...
                                                    MessageManaged = await functionScanTask.ConfigureAwait(false);

                                                    handleAwait = "";
                                                }
                                                else
                                                {
                                                    MessageManaged = "The minute is done look at your dateTime.";
                                                }
                                            }
                                            else
                                            {
                                                MessageManaged = "The Hrs is done look at your dateTime.";
                                            }
                                        }
                                    }
                                    else {
                                        //ITO YUNG KUNG WALA SIYANG NAHANAPNA KAPAREHO NG DAY SA SCHEDULE DUN SA USER.......
                                        //THAT MEANS ACCEPTED SIYA PARA MA ADD SA SCHED NG USER...............
                                        MessageManaged = "";
                                    }
                                    }
                                    else
                                    {
                                        MessageManaged = "This day is already passed.";
                                    }
                                }
                                else
                                {
                                //Scanning..................
                                //ITO YUNG MAG KA IBA NG MONTH TO DAY AND YUNG SINELECT NI USER NA MONTH.......

                                if (Convert.ToInt32(calendarRangeAddDay) == Convert.ToInt32(handleData[5]))
                                {
                                    if (handleAwait == "")
                                    {
                                        handleAwait = "Awaited";
                                        int addingHandle = 0;
                                        int currentSchedHandle = 0;

                                        //THIS IS FOR ADDING HRS....................
                                        if (handleData[9] == "PM")
                                        {
                                            if (handleData[7] == "12")
                                            {
                                                addingHandle = 12;
                                            }
                                            else
                                            {
                                                addingHandle = 12 + Convert.ToInt32(handleData[7]);

                                            }
                                        }
                                        else
                                        {
                                            addingHandle = Convert.ToInt32(handleData[7]);
                                        }

                                        //THIS IS FOR CURRENT HRS SCHED.........................
                                        if (DateTimeRangeAddAP == "PM")
                                        {
                                            if (DateTimeRangeAddHrs == "12")
                                            {
                                                currentSchedHandle = 12;
                                            }
                                            else
                                            {
                                                currentSchedHandle = 12 + Convert.ToInt32(DateTimeRangeAddHrs);

                                            }
                                        }
                                        else
                                        {
                                            currentSchedHandle = Convert.ToInt32(DateTimeRangeAddHrs);
                                        }


                                        //AFTER CONVERT THE PM EXAMPLE 1 PM TO 13 PM OR NOT THIS SHOULD DO 
                                        //SCANNING HRS AND MNT........
                                        Task<string> functionScanTask = new Task<string>(() => scanningFinalForSched(
                                            currentSchedHandle.ToString(),
                                            DateTimeRangeAddMnt, SetDurationTimeAdd, SetDurationTimeAddHrs,
                                            addingHandle.ToString(), handleData[8], handleData[10], handleData[11]));
                                        functionScanTask.Start();

                                        //HANDLING MESSAGE KUNG MAY MA AATTACH BA SIYANG SCHED NG IBA OR HINDI...
                                        MessageManaged = await functionScanTask.ConfigureAwait(false);

                                        handleAwait = "";
                                    }
                                }
                                else
                                {

                                    //ITO YUNG KUNG WALA SIYANG NAHANAPNA KAPAREHO NG DAY SA SCHEDULE DUN SA USER.......
                                    //THAT MEANS ACCEPTED SIYA PARA MA ADD SA SCHED NG USER...............
                                    MessageManaged = "";
                                }
                            }
                            }
                            else
                            {
                                MessageManaged = "This month is already done by the day.";
                            }
                        }
                        else {
                            MessageManaged = "The year need the same as our year to today.";
                        }
                    }
                    else {
                        MessageManaged = "The current schedule has already have in this user.";
                    }
               // }
               // else {
               //     MessageManaged = "You have only 'ONE' add schedule from this user.";
               // }
                return MessageManaged;
            }


            Thread.Sleep(2000);
            return handleMessage;
        }

        //((((((((((((((A.I))))))))))))))) SCANNING SCHEDULE HRS AND MNT........................
        string scanningFinalForSched(string DateTimeRangeAddHrs,
                string DateTimeRangeAddMnt, string SetDurationTimeAdd, 
                string SetDurationTimeAddHrs, string HHrsAddSched, string MMntAddSched, 
                string hrsAddSchedRange, string mntAddSchedRange) {

            string MessageManaged = "";

            int handleHrsDoneToAdd = Convert.ToInt32(DateTimeRangeAddHrs) + Convert.ToInt32(SetDurationTimeAddHrs);
            int handlerMntDoneToAdd = Convert.ToInt32(DateTimeRangeAddMnt) + Convert.ToInt32(SetDurationTimeAdd);
            if (handlerMntDoneToAdd >= 60)
            {
                //PRESENT...............................
                handleHrsDoneToAdd += 1;
                handlerMntDoneToAdd = handlerMntDoneToAdd - 60;
                if (Convert.ToInt32(HHrsAddSched) >= handleHrsDoneToAdd)
                {
                    if (Convert.ToInt32(HHrsAddSched) == handleHrsDoneToAdd)
                    {

                        if (handlerMntDoneToAdd < Convert.ToInt32(MMntAddSched))
                        {
                            //PASOK NO ISSUE...................
                            MessageManaged = "";
                        }
                        else
                        {
                            //ISSUE........................
                            MessageManaged = "ISSUE";
                        }
                    }
                    else
                    {
                        //PASOK NO ISSUE..................
                        MessageManaged = "";
                    }
                }
                else
                {
                    //PAST...........
                    if (Convert.ToInt32(HHrsAddSched) == Convert.ToInt32(DateTimeRangeAddHrs))
                    {
                        //ISSUE.................
                        MessageManaged = "ISSUE";
                    }
                    else if (Convert.ToInt32(HHrsAddSched) < Convert.ToInt32(DateTimeRangeAddHrs))
                    {

                        int hrsAddSched = Convert.ToInt32(HHrsAddSched) + Convert.ToInt32(hrsAddSchedRange);
                        int mntAddSched = Convert.ToInt32(MMntAddSched) + Convert.ToInt32(mntAddSchedRange);
                        if (mntAddSched >= 60)
                        {
                            hrsAddSched = hrsAddSched + 1;
                            mntAddSched = mntAddSched - 60;

                            if (hrsAddSched == Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                if (mntAddSched < Convert.ToInt32(DateTimeRangeAddMnt))
                                {

                                    //PASOK NO ISSUE.................
                                    MessageManaged = "";
                                }
                                else
                                {

                                    //ISSUE......................
                                    MessageManaged = "ISSUE";
                                }
                            }
                            else if (hrsAddSched < Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                //PASOK NO ISSUE......................
                                MessageManaged = "";
                            }
                            else
                            {

                                //ISSUE......................
                                MessageManaged = "ISSUE";
                            }
                        }
                        else
                        {

                            if (hrsAddSched == Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                if (mntAddSched < Convert.ToInt32(DateTimeRangeAddMnt))
                                {

                                    //PASOK NO ISSUE.................
                                    MessageManaged = "";
                                }
                                else
                                {

                                    //ISSUE......................
                                    MessageManaged = "ISSUE";
                                }
                            }
                            else if (hrsAddSched < Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                //PASOK NO ISSUE......................
                                MessageManaged = "";
                            }
                            else
                            {

                                //ISSUE......................
                                MessageManaged = "ISSUE";
                            }
                        }
                    }
                    else {
                        //.........................
                        if (handleHrsDoneToAdd >= Convert.ToInt32(HHrsAddSched))
                        {
                            //ISSUE......................
                            MessageManaged = "ISSUES";
                        }
                        else {
                            //PASOK NO ISSUE......................
                            MessageManaged = "";
                        }
                    }
                }
            }
            else
            {

                if (Convert.ToInt32(HHrsAddSched) >= handleHrsDoneToAdd)
                {
                    if (Convert.ToInt32(HHrsAddSched) == handleHrsDoneToAdd)
                    {
                        if (handlerMntDoneToAdd < Convert.ToInt32(mntAddSchedRange))
                        {
                            //PASOK NO ISSUE...................
                            MessageManaged = "";
                        }
                        else
                        {
                            //ISSUE........................
                            MessageManaged = "ISSUE";
                        }
                    }
                    else
                    {
                        //PASOK NO ISSUE..................
                        MessageManaged = "";
                    }
                }
                else
                {
                    //PAST...........
                    if (Convert.ToInt32(HHrsAddSched) == Convert.ToInt32(DateTimeRangeAddHrs))
                    {
                        //ISSUE.................
                        MessageManaged = "ISSUE";
                    }
                    else if (Convert.ToInt32(HHrsAddSched) < Convert.ToInt32(DateTimeRangeAddHrs))
                    {

                        int hrsAddSched = Convert.ToInt32(HHrsAddSched) + Convert.ToInt32(hrsAddSchedRange);
                        int mntAddSched = Convert.ToInt32(MMntAddSched) + Convert.ToInt32(mntAddSchedRange);
                        if (mntAddSched >= 60)
                        {
                            hrsAddSched = hrsAddSched + 1;
                            mntAddSched = mntAddSched - 60;

                            if (hrsAddSched == Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                if (mntAddSched < Convert.ToInt32(DateTimeRangeAddMnt))
                                {

                                    //PASOK NO ISSUE.................
                                    MessageManaged = "";
                                }
                                else
                                {

                                    //ISSUE......................
                                    MessageManaged = "ISSUE";
                                }
                            }
                            else if (hrsAddSched < Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                //PASOK NO ISSUE......................
                                MessageManaged = "";
                            }
                            else
                            {

                                //ISSUE......................
                                MessageManaged = "ISSUE";
                            }
                        }
                        else
                        {

                            if (hrsAddSched == Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                if (mntAddSched < Convert.ToInt32(DateTimeRangeAddMnt))
                                {

                                    //PASOK NO ISSUE.................
                                    MessageManaged = "";
                                }
                                else
                                {

                                    //ISSUE......................
                                    MessageManaged = "ISSUE";
                                }
                            }
                            else if (hrsAddSched < Convert.ToInt32(DateTimeRangeAddHrs))
                            {
                                //PASOK NO ISSUE......................
                                MessageManaged = "";
                            }
                            else
                            {

                                //ISSUE......................
                                MessageManaged = "ISSUE";
                            }
                        }
                    }
                    else
                    {
                        //.........................
                        if (handleHrsDoneToAdd >= Convert.ToInt32(HHrsAddSched))
                        {
                            //ISSUE......................
                            MessageManaged = "ISSUES";
                        }
                        else
                        {
                            //PASOK NO ISSUE......................
                            MessageManaged = "";
                        }
                    }
                }
            }

            Thread.Sleep(1000);
            return MessageManaged;
        }








        //INSERTING DATA ADD SCHEDULE FOR THE USER.....................................
        public List<CalendarList> functionThatWillAddSched(List<CalendarList> AddScheduleHandle) {
            List<CalendarList> CheckErr = new List<CalendarList>();
            string[] arrayHandle = new string[13];
            foreach (var addSchedHandleData in AddScheduleHandle) {
                arrayHandle = new string[] {addSchedHandleData.UsernameOfUserThatWillAdd, 
                addSchedHandleData.NameUserWhoAdded,
                addSchedHandleData.ImageUserWhoAdded,
                addSchedHandleData.DateTimeRangeAddHrs,
                addSchedHandleData.DateTimeRangeAddMnt,
                addSchedHandleData.DateTimeRangeAddAP,
                addSchedHandleData.calendarRangeAddMonth,
                addSchedHandleData.calendarRangeAddConvert,
                addSchedHandleData.calendarRangeAddDay,
                addSchedHandleData.calendarRangeAddYear,
                addSchedHandleData.SetDurationTimeAdd,
                addSchedHandleData.SetDurationTimeAddHrs,
                addSchedHandleData.DateTimeRange };
            }


            MySqlConnection connAddSched = new MySqlConnection("Server=localhost;Database=grading_accounts_" +
                    arrayHandle[0] +";"+ "Uid=root;Pwd=");
            try
            {
                connAddSched.Open();
                MySqlCommand commAddSched = connAddSched.CreateCommand();
                commAddSched.CommandText = "INSERT INTO `calendarsched` (`id`, `NameUserSchedWhoAdd`, `ImgUserSchedWhoAdd`, " +
                    "`HrsSched`, `MntSched`, `APSched`, `MonthSched`, `MonthSchedConvert`, `DaySched`," +
                    " `YearSched`, `SetDurationHrs`, `SetDurationMint`, `TimeDateSchedFinal`, `AdminCheck`) VALUES " +
                    "('', @namewhoAdd, @imgwhoAdd, @hrsSched, @mntSched, @APsched, @monthSched, @MonthConvert," +
                    "@daySched, @YearSched, @setDurationHrs, @setDurationMnt, @timeDateSchedFinal, @admin)";
                commAddSched.Parameters.AddWithValue("@namewhoAdd", arrayHandle[1]);
                commAddSched.Parameters.AddWithValue("@imgwhoAdd", arrayHandle[2]);
                commAddSched.Parameters.AddWithValue("@hrsSched", arrayHandle[3]);
                commAddSched.Parameters.AddWithValue("@mntSched", arrayHandle[4]);
                commAddSched.Parameters.AddWithValue("@APsched", arrayHandle[5]);
                commAddSched.Parameters.AddWithValue("@monthSched", arrayHandle[6]);
                commAddSched.Parameters.AddWithValue("@MonthConvert", arrayHandle[7]);
                commAddSched.Parameters.AddWithValue("@daySched", arrayHandle[8]);
                commAddSched.Parameters.AddWithValue("@YearSched", arrayHandle[9]);
                commAddSched.Parameters.AddWithValue("@setDurationMnt", arrayHandle[10]);
                commAddSched.Parameters.AddWithValue("@setDurationHrs", arrayHandle[11]);
                commAddSched.Parameters.AddWithValue("@timeDateSchedFinal", arrayHandle[12]);
                commAddSched.Parameters.AddWithValue("@admin", "");
                commAddSched.ExecuteNonQuery();

                connAddSched.Close();

                CheckErr.Add(new CalendarList{
                    ErrCheck = "SuccInserting"
                });

                Thread.Sleep(2000);
                return CheckErr;
            }
            catch (Exception e)
            {
                string err = "" + e;
                CheckErr.Add(new CalendarList
                {
                    ErrCheck = "Please Check Your Internet"
                });
                return CheckErr;
            }
        }



        //ITO YUNG PARA MAKITA ANG BUONG SHEDULE NA PININDOT NG NI OWN USER DUN SA USER ADD SCHEDULE.................
        public List<CalendarList> getAllDataInAddSched(string usernameUser) {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=grading_accounts_"+usernameUser+"" +
                ";Uid=root;Pwd=");
            List<CalendarList> listSchedAdd = new List<CalendarList>();
            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `calendarsched`";
                using (MySqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listSchedAdd.Add(new CalendarList
                        {
                            ErrCheck = "",
                            NameUserWhoAdded = (string)reader["NameUserSchedWhoAdd"],
                            ImageUserWhoAdded = (string)reader["ImgUserSchedWhoAdd"],
                            DateTimeRangeAddHrs = (string)reader["HrsSched"],
                            DateTimeRangeAddMnt = (string)reader["MntSched"],
                            DateTimeRangeAddAP = (string)reader["APSched"],
                            calendarRangeAddMonth = (string)reader["MonthSched"],
                            calendarRangeAddConvert = (string)reader["MonthSchedConvert"],
                            calendarRangeAddDay = (string)reader["DaySched"],
                            calendarRangeAddYear = (string)reader["YearSched"],
                            SetDurationTimeAdd = (string)reader["SetDurationMint"],
                            SetDurationTimeAddHrs = (string)reader["SetDurationHrs"],
                            DateTimeRange = (string)reader["TimeDateSchedFinal"],
                            HandlingAdmin = (string)reader["AdminCheck"]
                        });
                    }
                }

                conn.Close();
                Thread.Sleep(2000);
                return listSchedAdd;
            } catch (Exception e) {
                string er = e.ToString();
                listSchedAdd.Add(new CalendarList {
                    ErrCheck = "Check Your Connection."
                });
                return listSchedAdd;
            }
        }




        //THIS IS DELETED SCHEDULE FOR THE SCHED OTHER USER......................................
        public string deleteFucntion(string userDeletedAddSched, string DateTimeRange) {
            flowToDeleteAddSched schedDelegateFlow = new flowToDeleteAddSched(DeleteAddSchedFinal);
            string schedReturn = schedDelegateFlow.Invoke(userDeletedAddSched, DateTimeRange);

            Thread.Sleep(2000);

            return schedReturn;
        }

        private delegate string flowToDeleteAddSched(string userDeletedAddSched, string DateTimeRange);

        protected string DeleteAddSchedFinal(string userDeletedAddSched, string DateTimeRange) {
            MySqlConnection connDeleteSched = new MySqlConnection(String.Format("" +
                "Server=localhost;Database=grading_accounts_{0};Uid=root;Pwd=", userDeletedAddSched));
            try {
                connDeleteSched.Open();
                MySqlCommand comm = connDeleteSched.CreateCommand();
                comm.CommandText = "DELETE FROM `calendarsched` WHERE `TimeDateSchedFinal`=@DateRange";
                comm.Parameters.AddWithValue("@DateRange", DateTimeRange);
                comm.ExecuteNonQuery();

                string asd = "";
                return asd;
            }
            catch (Exception e) {
                string err = e.ToString();
                string checkInter = "Check Your Connection.";
                return checkInter;
            }
        }
    }
}
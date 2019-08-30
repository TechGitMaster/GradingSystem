using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MySql.Data.MySqlClient;

namespace ClassUserForm
{
    public class Grading
    {

        //GET ALL DATA IN SEARCHING USER............................................
        public List<GradingList> gettingData() {
            List<GradingList> ListHandleDataSearch = new List<GradingList>();
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=grading_accounts;Uid=root;Pwd=");

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `searchbargradingaccounts`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        ListHandleDataSearch.Add(new GradingList {
                            err = "",
                            id = (int)reader["id"],
                            UserName = (string)reader["Username"],
                            FirstLastName = (string)reader["FirstLastName"],
                            ImageUser = (string)reader["ImageUser"]
                        });
                    }
                }


            } catch (Exception e) {
                string errs = e.ToString();
                ListHandleDataSearch.Add(new GradingList{
                    err = "Having Err"
                });

            }
            

            return ListHandleDataSearch;
        }



        //CHECKING WHEN THE SELFUSER CLICKED THE SELECTED USER IN SEARCH....................................................
        public async Task<List<GradingList>> getDataAccordingSelectedUser(string UsernameSelected) {
            List<GradingList> handleGradeData = new List<GradingList>();
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0};Uid=root;" +
                "Pwd=", UsernameSelected));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `gradinghandlingdata`";
                Task<int> returnInt = new Task<int>(() => returnCountList());
                int returnCountList()
                {
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            handleGradeData.Add(new GradingList
                            {
                                errGrade = "",
                                numberGrade = 1,
                                idGrade = (int)reader["id"],
                                UserNameOwner = (string)reader["UserNameOwner"],
                                UserNameCreator = (string)reader["UserNameCreator"],
                                NameCreator = (string)reader["NameCreator"],
                                ImageCreator = (string)reader["ImageCreator"],
                                SubjectCreator = (string)reader["SubjectCreator"],
                                ColorCreator = (string)reader["ColorCreator"],
                                DateTimeCreated = (string)reader["DateTimeCreated"]
                            });
                        }
                    }
                    Int32 length = handleGradeData.Count;
                    return length;
                }
                returnInt.Start();
                if ((await returnInt.ConfigureAwait(false)) == 0)
                {
                    handleGradeData.Add(new GradingList {
                        errGrade = "",
                        numberGrade = 0
                    });
                }

            } catch (Exception e) {
                string err = e.ToString();
                handleGradeData.Add(new GradingList
                {
                    errGrade = "Having",
                    numberGrade = 0
                });
            }

            return handleGradeData;
        }



        //SAVING ANG CREATING NEW SUBJECTS.......................................................................
        public async Task<List<GradingList>> savingCreateSubAndReturn(string textBoxCreateSub, string fullNameOwn, string ImageOwnUser,
            string UsernameOwn, string UsernameOther) {
            Random ran = new Random();
            List<GradingList> listHandle = new List<GradingList>();
            string handlingDate = "", handleConditionScanIfHave = "";
            int numberCountSee = 0;
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0}" +
                ";Uid=root;Pwd=", UsernameOther));


            Task<string> taskDo = new Task<string>(() => returnIfSuccessOrNo());

            //SCANNING IF HAVING OR NOT............................................
            string returnIfSuccessOrNo() {
                try {
                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "SELECT `SubjectCreator` FROM gradinghandlingdata";
                    using (MySqlDataReader reader = comm.ExecuteReader()) {
                        while (reader.Read()) {
                            numberCountSee = numberCountSee + 1;
                            if (handleConditionScanIfHave != "Sorry_It_have") {
                                if (((string)reader["SubjectCreator"]) != textBoxCreateSub)
                                {
                                    handleConditionScanIfHave = "No_Same";
                                }
                                else {
                                    handleConditionScanIfHave = "Sorry_It_have";
                                }
                            }
                        }

                        conn.Close();
                    }

                    if (numberCountSee == 0) {
                        handleConditionScanIfHave = "No_Same";
                    }

                } catch (Exception e) {
                    string err = e.ToString();
                    listHandle.Add(new GradingList {
                        SubjectCreator = "",
                        errGrade = "Conn"
                    });
                }
                return handleConditionScanIfHave;
            }
            taskDo.Start();

            handleConditionScanIfHave = await taskDo.ConfigureAwait(false);

            if (handleConditionScanIfHave == "No_Same")
            {
                try
                {
                    string dateTime()
                    {
                        string handleReturnDate = String.Format("{0}/{1}/{2} {3}:{4} {5}",
                            DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(),
                            DateTime.Now.Year.ToString(), (DateTime.Now.Hour <= 9 ? "0" + DateTime.Now.Hour.ToString() :
                            (DateTime.Now.Hour > 12 ? ("0" + (DateTime.Now.Hour - 12)).ToString() : DateTime.Now.Hour.ToString())),
                            (DateTime.Now.Minute <= 9 ? "0" + DateTime.Now.Minute.ToString() :
                            DateTime.Now.Minute.ToString()), (DateTime.Now.Hour < 11 ? "AM" : "PM"));

                        return handleReturnDate;
                    }

                    Task<string> taskdo = new Task<string>(dateTime);
                    taskdo.Start();
                    handlingDate = await taskdo.ConfigureAwait(false);

                    if (handlingDate.Length != 0)
                    {
                        conn.Open();
                        MySqlCommand comm = conn.CreateCommand();
                        comm.CommandText = "INSERT INTO `gradinghandlingdata` (`id`, `UserNameOwner`, `UserNameCreator`, `NameCreator`, " +
                            "`ImageCreator`, `SubjectCreator`, `ColorCreator`, `DateTimeCreated`) VALUES ('', @UserNameOwner, @UserNameCreator," +
                            "@NameCreator, @ImageCreator, @SubjectCreator, @ColorCreator, @DateTimeCreated)";
                        comm.Parameters.AddWithValue("@UserNameOwner", UsernameOwn);
                        comm.Parameters.AddWithValue("@UserNameCreator", UsernameOther);
                        comm.Parameters.AddWithValue("@NameCreator", fullNameOwn);
                        comm.Parameters.AddWithValue("@ImageCreator", ImageOwnUser);
                        comm.Parameters.AddWithValue("@SubjectCreator", textBoxCreateSub);
                        comm.Parameters.AddWithValue("@ColorCreator", String.Format("{0}, {1}, {2}", (ran.Next(234).ToString()),
                            (ran.Next(134).ToString()), (ran.Next(100).ToString())));
                        comm.Parameters.AddWithValue("@DateTimeCreated", handlingDate);
                        comm.ExecuteNonQuery();

                        comm.CommandText = String.Format("CREATE DATABASE `grading_accounts_{0}_{1}`", textBoxCreateSub, UsernameOther);
                        comm.ExecuteNonQuery();

                        MySqlConnection connSubject = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_" +
                            "{0}_{1};Uid=root;Pwd=", textBoxCreateSub, UsernameOther));
                        try
                        {
                            connSubject.Open();
                            MySqlCommand commSubject = connSubject.CreateCommand();
                            commSubject.CommandText = "CREATE TABLE `gradingHandleSem`(" +
                                "id int(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY," +
                                "QuaterName varchar(225) NOT NULL," +
                                "Comments varchar(100) NOT NULL," +
                                "TableName varchar(225) NOT NULL," +
                                "DateTime varchar(225) NOT NULL" +
                                ")";
                            commSubject.ExecuteNonQuery();
                            connSubject.Close();


                            try {

                                comm.CommandText = "INSERT INTO `reports` (`id`, `NameWho`, `ImageUser`, `Message`, `ColorDeclared`, " +
                                            "`DayReport`, `MonthReport`, `TimeMessage`, `MonthDateTime`) VALUES ('', @name, @image, @message, @color," +
                                            "@day, @month, @timemesage, @monthdate)";
                                comm.Parameters.AddWithValue("@name", fullNameOwn);
                                comm.Parameters.AddWithValue("@image", ImageOwnUser);
                                comm.Parameters.AddWithValue("@message", "New added Subject in your Grading List '" + textBoxCreateSub
                                     + "'");
                                comm.Parameters.AddWithValue("@color", "Coral");
                                comm.Parameters.AddWithValue("@day", DateTime.Now.Day.ToString());
                                comm.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
                                comm.Parameters.AddWithValue("@timemesage", ((DateTime.Now.Hour > 12 ? (DateTime.Now.Hour - 12).ToString()
                                    : DateTime.Now.Hour.ToString()) + ":" + DateTime.Now.Minute.ToString() + ": " + (DateTime.Now.Hour < 11 ?
                                    "AM" : "PM")));
                                comm.Parameters.AddWithValue("@monthdate", handlingDate);
                                comm.ExecuteNonQuery();

                            } catch (Exception e) {
                                string ee = e.ToString();

                                listHandle.Add(new GradingList
                                {
                                    SubjectCreator = "",
                                    errGrade = "Conn"
                                });
                            }
                        }
                        catch (Exception e)
                        {
                            string ee = e.ToString();

                            listHandle.Add(new GradingList
                            {
                                SubjectCreator = "",
                                errGrade = "Conn"
                            });
                        }
                    }

                }
                catch (Exception e)
                {
                    string err = e.ToString();
                    listHandle.Add(new GradingList
                    {
                        SubjectCreator = "",
                        errGrade = "Conn"
                    });
                }
            }
            else {
                listHandle.Add(new GradingList {
                    errGrade = "",
                    SubjectCreator = "Same"
                });
            }
                return listHandle;
        }





        //CHECKING IF THE QUATER HAD 4 GRADES.........................................................
        private string conditionQuaterCheck = "false", conditionCheckingHaveDatabase = "false";
        private delegate string funcDataMethod(string conditionQuaterCheckss);



        //CHECKING IF HE/SHE REACH THE MAXIMUM OF 4 QUATER THORUGH GETTER AND SETTER 
        public string checkingHad4 {
            get { return conditionQuaterCheck;  }
            set {
                conditionQuaterCheck = value;

                //CALL METHOD.......................
                funcDataMethod handleFs = new funcDataMethod(this.ReturnCon);
                conditionQuaterCheck = handleFs.Invoke(conditionQuaterCheck);
            }
        }


        //CHECKING IF HAVE A SAME NAME IN QUATER THOUGH GETTER AND SETTER......................................
        public string checkingHaveName {
            get { return conditionCheckingHaveDatabase; }
            set {
                conditionCheckingHaveDatabase = value;

                //CALL METHOD....................................
                var delegateFunc = new funcDataMethod(this.handleCheckHaveName);
                conditionCheckingHaveDatabase = delegateFunc.Invoke(conditionCheckingHaveDatabase);
            }
        }

        







        //METHOD OF GETTER AND SETTER.........................
        //METHOD THAT CHECK IF HE/SHE REACH THE MAXIMUM 4 OF QUATERS...................................
        private string ReturnCon(string conditionQuaterChecks) {
            string handleSubject = "", handleCreator = "", handleReturnValue = "";
            int numcountData = 0;
            for (int numCount = 0;numCount < conditionQuaterChecks.Length;numCount++) {
                if (conditionQuaterChecks[numCount] != ',')
                {
                    handleCreator = handleCreator + conditionQuaterChecks[numCount].ToString();
                }
                else {
                    handleSubject = handleCreator;
                    handleCreator = "";
                }
            }

            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0}_{1};" +
                "Uid=root;Pwd=", handleSubject, handleCreator));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `gradinghandlesem`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        numcountData++;
                    }
                }
                conn.Close();
                if (numcountData == 4)
                {
                    handleReturnValue = "false";
                }
                else {
                    handleReturnValue = "true";
                }
            } catch (Exception e) {
                string err = e.ToString();
                handleReturnValue = "Please Check your Internet Connection.";
            }
            return handleReturnValue;
        }




        //METHOD TO CHECK IF THE NAME PENDING HAD ALREADY IN DATABASE...........................................
        public string handleCheckHaveName(string conditionCheckingHaveDatabase) {
            string[] arrayHandleData = new string[] { "", "", ""};
            int numberCOunt = 0, numberCountArray = 0;
            string handleReturn = "false";
            MySqlConnection conn = new MySqlConnection();

            //SEPERATE SESSION................................
            while (numberCOunt <= conditionCheckingHaveDatabase.Length) {
                if (numberCOunt != conditionCheckingHaveDatabase.Length) {
                    if (conditionCheckingHaveDatabase[numberCOunt] != ',')
                    {
                        arrayHandleData[numberCountArray] = arrayHandleData[numberCountArray] +
                            conditionCheckingHaveDatabase[numberCOunt].ToString();
                    }
                    else {
                        numberCountArray++;
                    }
                }
                numberCOunt++;
            }

            conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0}_{1};Uid=root;Pwd=",
                arrayHandleData[1], arrayHandleData[2]));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT `QuaterName` FROM `gradinghandlesem`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        if (handleReturn == "false") {
                            if ((string)reader["QuaterName"] == arrayHandleData[0]) {
                                handleReturn = "true";
                            }
                        }
                    }
                }

            } catch(Exception e) {
                handleReturn = e.ToString();
                handleReturn = "Please Check Your Connection.";
            }

            return handleReturn;
        }



        //LAST LINE OF GETTER AND SETTER METHODS..........................................





        //GETTING DATA IN QUATER...................................................................
        public async Task<List<GradingList>> gettingDataQuaters(string handleNameOfSubject, string handleUserCreator) {
            List<GradingList> gradingHandleQuater = new List<GradingList>();
            string CheckIfHave = "false";
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_" +
                "{0}_{1};Uid=root;Pwd=", handleNameOfSubject.ToLower(), handleUserCreator));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `gradinghandlesem`";
                Task<string> returnTask = new Task<string>(() => returnCon());
                string returnCon()
                {
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CheckIfHave = "true";
                            gradingHandleQuater.Add(new GradingList
                            {
                                errQuaterFetch = "",
                                handlingIfHaveQuater = CheckIfHave,
                                idQuater = Convert.ToInt32(reader["id"]),
                                quatername = (string)(reader["QuaterName"]),
                                commentsQuater = (string)(reader["Comments"]),
                                tableNameQuater = (string)(reader["TableName"]),
                                dateTimeQuater = (string)(reader["DateTime"])
                            });
                        }
                    }
                    return CheckIfHave;
                }
                returnTask.Start();

                if ((await returnTask.ConfigureAwait(false)) != "true") {
                    gradingHandleQuater.Add(new GradingList {
                        errQuaterFetch = "",
                        handlingIfHaveQuater = CheckIfHave
                    });
                }
            } catch (Exception e) {
                string err = e.ToString();
                gradingHandleQuater.Add(new GradingList
                {
                    errQuaterFetch = err,
                });
            }

            return gradingHandleQuater;
        }


        //ADD QUATERS.................................................................................
        public async Task<List<GradingList>> addQuaters(string handleNameTable, string nameofQuater, 
            string NameOfSubject, string nameofUserCreate, string UserNameOwn) {
            int countArrayString = 0;
            List<string> handleSeperatedTable = new List<string>();
            List<GradingList> handleDataListReturn = new List<GradingList>();
            string handleCombineDataTable = "", handleAddedDataBase = "", handleDate = "", handleNameOwn = "", HandleImageOwn = "";

            MySqlConnection connGetOwn = new MySqlConnection("Server=localhost;Database=grading_accounts;Uid=root;Pwd=");
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0}_{1};" +
                "Uid=root;Pwd=", NameOfSubject, nameofUserCreate)); 


            bool SeperateDataAndCombine() {

                for (int countLength = 0; handleNameTable.Length > countLength;countLength++) {
                    if (handleNameTable[countLength].ToString() != ",")
                    {
                        handleCombineDataTable = handleCombineDataTable + handleNameTable[countLength];
                    }
                    else {
                        handleSeperatedTable.Add(handleCombineDataTable);
                        handleCombineDataTable = "";
                    }

                    if (handleNameTable.Length-1 <= countLength) {
                        string[] handleDataCombineds = handleSeperatedTable.ToArray();

                        handleCombineDataTable = "CREATE TABLE `"+ nameofQuater + "`(" +
                            "id int(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY, ";

                        while (countArrayString < handleDataCombineds.Length)
                        {
                            if (handleDataCombineds.Length - 1 != countArrayString)
                            {
                                handleCombineDataTable += handleDataCombineds[countArrayString] + " varchar(25) NOT NULL, ";
                                countArrayString++;
                            }
                            else {
                                handleCombineDataTable += handleDataCombineds[countArrayString] + " varchar(25) NOT NULL)";
                                countArrayString++;
                            }
                        }
                    }
                }

                return true;
            }
            Task<bool> boolReturn = new Task<bool>(SeperateDataAndCombine);
            boolReturn.Start();

            if ((await boolReturn.ConfigureAwait(false)) == true) {

                bool returnValueDone()
                {
                    int handleCountToDate = 0;
                    foreach (string handle in handleSeperatedTable)
                    {
                          handleCountToDate++;
                          handleAddedDataBase += handle + ",";
                    }

                    if (countArrayString == handleCountToDate) {
                        handleDate = (DateTime.Now.Month <= 9 ? "0"+DateTime.Now.Month.ToString():DateTime.Now.Month.ToString())
                            +"/"+(DateTime.Now.Day <= 9 ? "0"+DateTime.Now.Day.ToString():DateTime.Now.Day.ToString())+'/'+
                            DateTime.Now.Year.ToString()+" "+(DateTime.Now.Hour < 12 ? (DateTime.Now.Hour <= 9 ? 
                            "0"+DateTime.Now.Hour.ToString(): DateTime.Now.Hour.ToString()): ((DateTime.Now.Hour-12) <= 9 ?
                            "0"+(DateTime.Now.Hour-12).ToString(): (DateTime.Now.Hour - 12).ToString()))+":"+
                            (DateTime.Now.Minute <= 9 ? "0"+DateTime.Now.Minute.ToString(): DateTime.Now.Minute.ToString())+" "+
                            (DateTime.Now.Hour <= 11 ? "AM":"PM");
                    }

                    return true;
                }
                Task<bool> taskReturn = new Task<bool>(() => returnValueDone());
                taskReturn.Start();

                if ((await taskReturn.ConfigureAwait(false)) != false) {
                    try
                    {
                        connGetOwn.Open();
                        MySqlCommand commGetOwn = connGetOwn.CreateCommand();
                        commGetOwn.CommandText = "SELECT `FirstLastName`,`ImageUser` FROM `searchbargradingaccounts` WHERE `UserName`=" +
                            "@username";
                        commGetOwn.Parameters.AddWithValue("@username", UserNameOwn);
                        using (MySqlDataReader reader = commGetOwn.ExecuteReader())
                        {
                            while (reader.Read()) {
                                handleNameOwn = (string)reader["FirstLastName"];
                                HandleImageOwn = (string)reader["ImageUser"];
                            }

                            connGetOwn.Close();
                        }
                            try
                            {
                                conn.Open();
                                MySqlCommand comm = conn.CreateCommand();
                                comm.CommandText = "INSERT INTO `gradinghandlesem` (`id`, `QuaterName`, `Comments`, `TableName`, `DateTime`)" +
                                   "VALUES ('', @Quater, '', @Table, @Date)";
                                comm.Parameters.AddWithValue("@Quater", nameofQuater);
                                comm.Parameters.AddWithValue("@Table", handleAddedDataBase);
                                comm.Parameters.AddWithValue("@Date", handleDate);
                                comm.ExecuteNonQuery();

                                try
                                {
                                    comm.CommandText = handleCombineDataTable;
                                    comm.ExecuteNonQuery();



                                    MySqlConnection connReport = new MySqlConnection("Server=localhost;Database=grading_accounts_" + nameofUserCreate +
                                        ";Uid=root;Pwd=");

                                    try
                                    {
                                        connReport.Open();
                                    MySqlCommand commReport = connReport.CreateCommand();
                                       commReport.CommandText = "INSERT INTO `reports` (`id`, `NameWho`, `ImageUser`, `Message`, `ColorDeclared`, " +
                                            "`DayReport`, `MonthReport`, `TimeMessage`, `MonthDateTime`) VALUES ('', @name, @image, @message, @color," +
                                            "@day, @month, @timemesage, @monthdate)";
                                             commReport.Parameters.AddWithValue("@name", handleNameOwn);
                                             commReport.Parameters.AddWithValue("@image", HandleImageOwn);
                                             commReport.Parameters.AddWithValue("@message", "New quater added in this subject '"+NameOfSubject+"', "+"The" +
                                                 " name of Quater is '"+nameofQuater
                                                  +"'");
                                             commReport.Parameters.AddWithValue("@color", "Coral");
                                             commReport.Parameters.AddWithValue("@day", DateTime.Now.Day.ToString());
                                             commReport.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
                                             commReport.Parameters.AddWithValue("@timemesage", ((DateTime.Now.Hour > 12 ? (DateTime.Now.Hour-12).ToString()
                                                 : DateTime.Now.Hour.ToString())+":"+DateTime.Now.Minute.ToString()+": "+(DateTime.Now.Hour < 11 ? 
                                                 "AM":"PM")));
                                             commReport.Parameters.AddWithValue("@monthdate", handleDate);
                                        commReport.ExecuteNonQuery();
                                    connReport.Close();



                                    try {

                                        comm.CommandText = "SELECT * FROM `gradinghandlesem`";
                                        using (MySqlDataReader reader = comm.ExecuteReader()) {
                                            while (reader.Read()) {
                                                handleDataListReturn.Add(new GradingList{
                                                    errQuaterFetch = "",
                                                    idQuater = Convert.ToInt32(reader["id"]),
                                                    quatername = (string)(reader["QuaterName"]),
                                                    commentsQuater = (string)(reader["Comments"]),
                                                    tableNameQuater = (string)(reader["TableName"]),
                                                    dateTimeQuater = (string)(reader["DateTime"])
                                                });
                                            }
                                        }
                                        conn.Close();

                                    } catch (Exception e) {
                                        string err = e.ToString();
                                        handleDataListReturn.Add(new GradingList
                                        {
                                            errQuaterFetch = err
                                        });
                                    }

                                    }
                                    catch (Exception e)
                                    {
                                        string err = e.ToString();
                                        handleDataListReturn.Add(new GradingList
                                        {
                                            errQuaterFetch = err
                                        });
                                    }
                                }
                                catch (Exception e)
                                {
                                    string err = e.ToString();
                                    handleDataListReturn.Add(new GradingList
                                    {
                                        errQuaterFetch = err
                                    });
                                }

                            }
                            catch (Exception e)
                            {
                                string err = e.ToString();
                                handleDataListReturn.Add(new GradingList
                                {
                                    errQuaterFetch = err
                                });
                            }
                    }
                    catch (Exception e) {
                        handleDataListReturn.Add(new GradingList
                        {
                            errQuaterFetch = e.ToString()
                        });
                    }
                }

            }

            return handleDataListReturn;

        }

    }
}

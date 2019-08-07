using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace ClassUserForm
{
    public class Report
    {
        //GET ALL THE REPORT DATA............................................................
        public List<ReportList> getAllDatainReport(string userName) {
            List<ReportList> ListHandleData = new List<ReportList>();
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0};Uid=root" +
                ";Pwd=", userName));
            int numberCountHave = 0;
            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `reports`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        if (numberCountHave != 1) {
                            numberCountHave++;
                        }
                        ListHandleData.Add(new ReportList
                        {
                            ErrCatch = "",
                            countNumber = numberCountHave,
                            id = (int)reader["id"],
                            NameWhoMessage = (string)reader["NameWho"],
                            ImageAssest = (string)reader["ImageUser"],
                            Message = (string)reader["Message"],
                            ColorDeclared = (string)reader["ColorDeclared"],
                            DayReport = (string)reader["DayReport"],
                            MonthReport = (string)reader["MonthReport"],
                            TimeMessage = (string)reader["TimeMessage"],
                            FullTimeMessage = (string)reader["MonthDateTime"]
                        });
                    }
                }

                if (numberCountHave == 0) {
                    ListHandleData.Add(new ReportList
                    {
                        ErrCatch = "",
                        countNumber = numberCountHave
                    });
                }
            }
            catch (Exception e) {
                ListHandleData.Add(new ReportList{
                    ErrCatch = e.ToString()
                });
            }
            return ListHandleData;
        }

        
        //DATA GET TO READER..........................................................
        public List<ReportList> getStringById(string idUserWho, string UsernameOwn) {
            List<ReportList> handleString = new List<ReportList>();
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0};" +
                "Uid=root;Pwd=", UsernameOwn));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `reports` WHERE `id`=@id";
                comm.Parameters.AddWithValue("@id", idUserWho);
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        handleString.Add(new ReportList {
                            ErrCatch = "",
                            NameWhoMessage = (string)reader["NameWho"],
                            ImageAssest = (string)reader["ImageUser"],
                            Message = (string)reader["Message"],
                            ColorDeclared = (string)reader["ColorDeclared"],
                            DayReport = (string)reader["DayReport"],
                            MonthReport = (string)reader["MonthReport"],
                            TimeMessage = (string)reader["TimeMessage"],
                            FullTimeMessage = (string)reader["MonthDateTime"]
                        });
                    }
                }
            } catch (Exception e) {
                string err = e.ToString();
                handleString.Add(new ReportList{
                   ErrCatch = "Error"
                });
            }

            return handleString;
        }



        //DELETE PANEL IN REPORT.................................................
        public static string handleDataReturnBack = "";
        public string deletePanelSection {
            get { return handleDataReturnBack; }
            set {
                handleDataReturnBack = value;
                handleDataReturnBack = this.dataDelete(handleDataReturnBack);
            }
        }

        private string dataDelete(string handleData) {
            string handleId = "", handleUsernameOwn = "", handleStringCount = "", returnStringHandle = "";

            for (int count = 0;count < handleData.Length;count++) {
                if (handleData[count].ToString() != " ")
                {
                    handleStringCount += handleData[count];
                }
                else {
                    handleId = handleStringCount;
                    handleStringCount = "";
                }

                if (handleData.Length < count+2) {
                    handleUsernameOwn = handleStringCount;
                    handleStringCount = "";
                }
            }

            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=grading_accounts_"+handleUsernameOwn+
                ";Uid=root;Pwd=");

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "DELETE FROM `reports` WHERE `id`=@id";
                comm.Parameters.AddWithValue("@id", handleId);
                comm.ExecuteNonQuery();

                returnStringHandle = "success";
            } catch (Exception e) {
                string err = e.ToString();
                returnStringHandle = "Error";
            }

            return returnStringHandle;
        } 


    }
}

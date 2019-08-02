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



    }
}

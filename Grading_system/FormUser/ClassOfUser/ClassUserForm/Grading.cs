using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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



        //CHECKING WHEN THE USER CLICKED THE SELECTED USER IN SEARCH....................................................
        public List<GradingList> getDataAccordingSelectedUser(string UsernameSelected) {
            List<GradingList> handleGradeData = new List<GradingList>();
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0};Uid=root;" +
                "Pwd=", UsernameSelected));

            try {
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT * FROM `gradinghandlingdata`";
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    while (reader.Read()) {
                        handleGradeData.Add(new GradingList {
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
                if (length == 0)
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




        public async Task<List<GradingList>> savingCreateSubAndReturn(string textBoxCreateSub, string fullNameOwn, string ImageOwnUser,
            string UsernameOwn, string UsernameOther) {
            Random ran = new Random();
            List<GradingList> listHandle = new List<GradingList>();
            string handlingDate = "";
            MySqlConnection conn = new MySqlConnection(String.Format("Server=localhost;Database=grading_accounts_{0}" +
                ";Uid=root;Pwd=", UsernameOther));
            try {
                string dateTime() {
                    string handleReturnDate = String.Format("{0}/{1}/{2} {3}:{4} {5}",
                        DateTime.Now.Month.ToString(), DateTime.Now.Day.ToString(), 
                        DateTime.Now.Year.ToString(), (DateTime.Now.Hour <= 9 ? "0"+ DateTime.Now.Hour.ToString() :
                        (DateTime.Now.Hour > 12 ? ("0"+(DateTime.Now.Hour-12)).ToString():DateTime.Now.Hour.ToString())), 
                        (DateTime.Now.Minute <= 9 ? "0"+ DateTime.Now.Minute.ToString() : 
                        DateTime.Now.Minute.ToString()), (DateTime.Now.Hour < 11 ? "AM":"PM"));

                    return handleReturnDate;
                }

                Task<string> taskdo = new Task<string>(dateTime);
                taskdo.Start();
                handlingDate = await taskdo.ConfigureAwait(false);

                if (handlingDate.Length != 0) {
                    conn.Open();
                    MySqlCommand comm = conn.CreateCommand();
                    comm.CommandText = "INSERT INTO `gradinghandlingdata` (`id`, `UserNameOwner`, `UserNameCreator`, `NameCreator`, " +
                        "`ImageCreator`, `SubjectCreator`, `ColorCreator`, `DateTimeCreated`) VALUES ('', @UserNameOwner, @UserNameCreator," +
                        "@NameCreator, @ImageCreator, @SubjectCreator, @ColorCreator, @DateTimeCreated)";
                    comm.Parameters.AddWithValue("@UserNameOwner", UsernameOther);
                    comm.Parameters.AddWithValue("@UserNameCreator", UsernameOwn);
                    comm.Parameters.AddWithValue("@NameCreator", fullNameOwn);
                    comm.Parameters.AddWithValue("@ImageCreator", ImageOwnUser);
                    comm.Parameters.AddWithValue("@SubjectCreator", textBoxCreateSub);
                    comm.Parameters.AddWithValue("@ColorCreator", String.Format("{0}, {1}, {2}", (ran.Next(234).ToString()),
                        (ran.Next(134).ToString()), (ran.Next(100).ToString())));
                    comm.Parameters.AddWithValue("@DateTimeCreated", handlingDate);
                    comm.ExecuteNonQuery();
                }

            } catch (Exception e) {
                string err = e.ToString();
            }


            return listHandle;
        }

    }
}

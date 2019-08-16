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

    }
}

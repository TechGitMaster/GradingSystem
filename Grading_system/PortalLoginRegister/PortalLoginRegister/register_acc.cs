using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
namespace ClassesOfFirstCome
{

    public class register_acc
    {

        /// <summary>
        /// Checking the username if the data of username is already had in database..................
        /// </summary>
        protected static string handle_username = "";
        public string check_if_username_have_database {
            get { return handle_username; }
            set {
                handle_username = value;
                handle_username = final_checking_the_username(handle_username);
            }
        }
        protected static string final_checking_the_username(string handle_username) {
            MySqlConnection conn = new MySqlConnection("Server=localhost;Database=grading_accounts;" +
                "Uid=root;Pwd=");
            try {
                string condition_bool = "";
                conn.Open();
                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "SELECT `Username` FROM `data_accounts` WHERE Username=@username";
                comm.Parameters.AddWithValue("@username", handle_username);
                using (MySqlDataReader reader = comm.ExecuteReader()) {
                    counts();
                    int counts()
                    {
                        int count_if_have = 0;
                        while (reader.Read())
                        {
                            if (handle_username == (string)reader["Username"]) {
                                count_if_have++;
                            }
                        }
                            if (count_if_have == 0)
                            {
                                condition_bool = "false";
                            }
                            else {
                                condition_bool = "true";
                            }
                        return 0;
                    }
                    conn.Close();
                    return condition_bool;
                }
            } catch (Exception e) { return e.ToString(); };
        }

        //End of username see if username is had in database..................................


        //INSERTING AND CREATE NEW ACCOUNT OF USER.............................
        protected static string[] arrayHandleInfoCreate;
        public string[] dataInformationUser {
            get { return arrayHandleInfoCreate;  }
            set {
                arrayHandleInfoCreate = value;
                arrayHandleInfoCreate = insertingDataToDatabaseUser(value);
            }
        }

        protected static string[] insertingDataToDatabaseUser(string[] arrayInfo) {
            MySqlConnection connCreate = new MySqlConnection(""+
                "Server=localhost;Database=grading_accounts;UId=root;Pwd=");
            string firstlastnameCombine = String.Format("{0} {1}", arrayInfo[0], arrayInfo[1]);

            //INSERTING INTO data_accounts FOR ADMIN PANEL............................................
            try
            {
                connCreate.Open();
                MySqlCommand commCreate = connCreate.CreateCommand();
                commCreate.CommandText = "INSERT INTO `data_accounts` (`id`, `First name`, `Last name`, `FirstLastName`, " +
                    "`Username`, `Email`, `Password`, `Gender`, `ImageUser`) VALUES " +
                    "('', @FirstName, @LastName, @FirstLastName, @UserName, @Email, @Password, @Gender, @ImageUser)";
                commCreate.Parameters.AddWithValue("@FirstName", arrayInfo[0]);
                commCreate.Parameters.AddWithValue("@LastName", arrayInfo[1]);
                commCreate.Parameters.AddWithValue("@FirstLastName", firstlastnameCombine);
                commCreate.Parameters.AddWithValue("@UserName", arrayInfo[2]);
                commCreate.Parameters.AddWithValue("@Email", arrayInfo[3]);
                commCreate.Parameters.AddWithValue("@Gender", arrayInfo[6]);
                commCreate.Parameters.AddWithValue("@ImageUser", arrayInfo[7]);
                hashPassword();
                void hashPassword()
                {
                    var password = Encoding.ASCII.GetBytes(arrayInfo[4]);
                    var sha = new SHA256Managed();
                    var hash = sha.ComputeHash(password);
                    string handleBytes = "";
                    foreach (byte b in hash) {
                        handleBytes += b;
                    }

                    commCreate.Parameters.AddWithValue("@Password", handleBytes);
                }


                commCreate.ExecuteNonQuery();

                //INSERTING DATA INTO searchbargradingaccounts FOR ALL SEARCHING BAR INTO GRADING SYSTEM...........
                try
                {

                    MySqlCommand commSearchBar = connCreate.CreateCommand();
                    commSearchBar.CommandText = "INSERT INTO `searchbargradingaccounts` (`id`, `UserName`, " +
                        "`FirstLastName`, `ImageUser`) VALUES ('', @username, @firstlastname, @imageuser)";
                    commSearchBar.Parameters.AddWithValue("@username", arrayInfo[2]);
                    commSearchBar.Parameters.AddWithValue("@firstlastname", firstlastnameCombine);
                    commSearchBar.Parameters.AddWithValue("@imageuser", arrayInfo[7]);
                    commSearchBar.ExecuteNonQuery();

                    connCreate.Close();
                    string[] senderMessage = new string[] { "Succesful" };
                    return senderMessage;
                } catch (Exception e) {
                    string exceps = e.ToString();
                    string[] senderMessage = new string[] { "Please Check Your Internet " +
                    "Connection and Try Again." };
                    return senderMessage;
                }
            } catch (Exception e) {
                string exceps = e.ToString();
                string[] senderMessage = new string[] { "Please Check Your Internet " +
                    "Connection and Try Again." };
                return senderMessage;
            }
        }
    }
}

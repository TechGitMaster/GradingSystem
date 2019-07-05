using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassesOfFirstCome;
using WindowsFormUserGrading;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace FormLoginRegister
{ 
    public partial class Form1 : Form
    {
        /// <summary>
        /// To Shadow the border of This form......
        /// </summary>
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


            //Erase login function;
            erase_login.Click += new System.EventHandler((object sender, EventArgs e) => { this.Close(); });

            mousemove.MouseDown += new System.Windows.Forms.MouseEventHandler(mouse_down);
            mousemove.MouseUp += new System.Windows.Forms.MouseEventHandler(mouse_up);
            mousemove.MouseMove += new System.Windows.Forms.MouseEventHandler(mouse_move);
            createAccount.Click += new System.EventHandler(createAccounts);
        }


        /// <summary>
        /// MOVEVABLE form while the form is to FormBorderStyle.none
        /// </summary>

        private Point point = new Point(0, 0);
        public bool conditions = false;
        public void mouse_down(object sender, MouseEventArgs e) {
            point = new Point(e.X, e.Y);
            conditions = true;
        }

        public void mouse_up(object sender, MouseEventArgs e)
        {
            conditions = false;
        }

        public void mouse_move(object sender, MouseEventArgs e)
        {
            if (conditions) {
                Point change = PointToScreen(e.Location);
                this.Location = new Point(change.X - point.X, change.Y - point.Y);
            }
        }


        /// <summary>
        /// Button for login...........................................
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static int handling_number_user = 0;
        private void Loginbttn1_Click(object sender, EventArgs e)
        {
            //CREATING ARRAY FOR HANDLING DATA LOGIN USER................................
            List<string> array_data_login = new List<string>();

            //GETTING THE DATA IN TextBox LOGIN...................
            foreach (Control get_text in Controls) {
                if (get_text is TextBox) {

                    //INSERT IT TO NEW ARRAY.........................
                    if (String.IsNullOrEmpty(get_text.Text) != true) {
                        array_data_login.Add(get_text.Text);
                    }
                }
            }

            //CHECKING IF THE array_data_login IS SUCCESFULY ADDED THE DATA LOGIN.............
            string[] handle_array_login = array_data_login.ToArray();
            if (handle_array_login.Length == 2)
            {
                List<login> handle_data_check = new List<login>();
                using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=grading_accounts;Uid=root;Pwd=;")) {
                    conn.Open();
                    try {
                        MySqlCommand comm = conn.CreateCommand();
                        comm.CommandText = "SELECT `id`, `Username`, `Password` FROM data_accounts WHERE Username=@username AND Password=@pass";
                        comm.Parameters.AddWithValue("@username", handle_array_login[1]);
                        hashPassword();
                        void hashPassword()
                        {
                            var stringPassword = Encoding.ASCII.GetBytes(handle_array_login[0]);
                            var sha = new SHA256Managed();
                            var computeHash = sha.ComputeHash(stringPassword);
                            string handlePasswordHash = "";
                            foreach (byte b in computeHash) {
                                handlePasswordHash += b;
                            }
                            comm.Parameters.AddWithValue("@pass", handlePasswordHash);
                        }
                        using (MySqlDataReader reader = comm.ExecuteReader()) {
                            while (reader.Read()) {
                                handle_data_check.Add(new login
                                {
                                    Id = (int)reader["id"],
                                    username = (string)reader["Username"],
                                    password = (string)reader["Password"]
                                });
                            }
                            reader.Close(); 
                            int checking_if_have = 0;
                            foreach (var get_data_inside in handle_data_check) {
                                get_data_inside.get_the_datas = handling_number_user;
                                handling_number_user = get_data_inside.get_the_datas;
                                checking_if_have++;
                            }
                            if (checking_if_have == 1)
                            {
                                GradingFormUser openUser = new GradingFormUser();
                                this.Hide();
                                openUser.Show();
                                openUser.dragingIdUserLogin(handling_number_user);
                            }
                            else {
                                MessageBox.Show("You dont have account yet");
                            }
                        }
                        
                    }
                    catch (Exception ex) { MessageBox.Show(ex.ToString());  };
                }
            }
            else {
                //CREATING NEW ARRAY TO HANDLE AND GET Tag name Which have a name of 
                //Username and Password....
                List<string> array_handling_checking = new List<string>();
                
                //GET THE TEXTBOX Tag and add into Array that created..............
                foreach (Control checking_text in Controls) {
                    if (checking_text.GetType() == typeof(TextBox)) {
                        if (String.IsNullOrEmpty(checking_text.Text)) {
                            checking_text.BackColor = System.Drawing.Color.Red;
                            array_handling_checking.Add(checking_text.Tag.ToString());
                        }
                    }
                }

                //LOOPING THE ARRAY THAT HAD A VALUE TO SHOW WHICH IS EMPTY TEXTBOX..........
                string handler_message_report = "";
                string[] handler_cheking = array_handling_checking.ToArray();
                for (int number_report = handler_cheking.Length-1; number_report > -1; number_report--) {
                    handler_message_report += handler_cheking[number_report] +" is empty need have a value."+"\n";
                }
                MessageBox.Show(handler_message_report);
            }
        }

        public void createAccounts(object sender, EventArgs e) {
            createAccount form2 = new createAccount();
            this.Hide();
            form2.Show();
        }
    }
}

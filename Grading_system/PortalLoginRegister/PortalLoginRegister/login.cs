using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassesOfFirstCome
{
    public class login
    {
        public string username;
        public string password;
        public int Id;
        private int Id_handle;
        public int get_the_datas {
            get {
                return Id_handle;
            }
            set {
                Id_handle = value;
                Id_handle = Id;
            }
        }
    }
}

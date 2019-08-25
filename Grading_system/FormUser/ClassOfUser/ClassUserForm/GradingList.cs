using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassUserForm
{
    public class GradingList
    {

        //SEARCH THE USERS...............................................................
        public string err;
        public int id;
        public string UserName;
        public string FirstLastName;
        public string ImageUser;


        //SUBJET HANDLING DATA SELECTED.........................................................
        public string errGrade;
        public int numberGrade;
        public int idGrade;
        public string UserNameOwner;
        public string UserNameCreator;
        public string NameCreator;
        public string ImageCreator;
        public string SubjectCreator;
        public string ColorCreator;
        public string DateTimeCreated;


        //QUATER HANDLING AND COMMENTS BY QUATER...................................................
        public string errQuaterFetch;
        public string handlingIfHaveQuater;
        public int idQuater;
        public string quatername;
        public string commentsQuater;
        public string tableNameQuater;
        public string dateTimeQuater;


    }
}

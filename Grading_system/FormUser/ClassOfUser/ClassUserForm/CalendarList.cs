using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassUserForm
{
    public class CalendarList
    {
        //CONDITION BEETWEN SEARCH AND GET DATA ARRIVED..........................
        public int numberCondition;
        public string ErrorCondition;

        //GET ALL DATA IN searchbargradingaccounts TO SHOW THE ALL USER THAT CAN SET A SCHEDULE..................
        public string UsernameGet;
        public string FirstLastNameGet;
        public string ImageuserGet;

        //THIS IS SEARCH BOX USER IN CALENDAR TO FIND AND SET A SCHEDULE TO USER...........
        public string UsernameSearch;
        public string FirstLastNameSearch;
        public string ImageuserSearch;


        //ITO YUNG MAG ADD NA SI USER NG SCHEDULE DUN SA PINILI NYANG IADD NA SCHED SA TEACHER
        //AT GET THE DATA.........................
        public int numberCount;
        public string ErrCheck;
        public string UsernameOfUserThatWillAdd;
        public string NameUserWhoAdded;
        public string ImageUserWhoAdded;
        public string DateTimeRange;
        public string calendarRangeAddMonth;
        public string calendarRangeAddConvert;
        public string calendarRangeAddDay;
        public string calendarRangeAddYear;
        public string DateTimeRangeAddHrs;
        public string DateTimeRangeAddMnt;
        public string DateTimeRangeAddAP;
        public string SetDurationTimeAdd;
        public string SetDurationTimeAddHrs;
        public string HandlingAdmin;
    }
}

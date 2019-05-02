using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Utils
{
    class DateHandler
    {
        public static DateTime GenerateFromString(string dateString, string timeString)
        {
            DateTime date = DateTime.Now;
            string[] dateSplit = dateString.Split('.');
            string[] timeSplit = timeString.Split(':');
            date = new DateTime(Convert.ToInt32(dateSplit[0]), Convert.ToInt32(dateSplit[1]), Convert.ToInt32(dateSplit[2]),Convert.ToInt32(timeSplit[0]),Convert.ToInt32(timeSplit[1]),0);
            return date;
        }
    }
}
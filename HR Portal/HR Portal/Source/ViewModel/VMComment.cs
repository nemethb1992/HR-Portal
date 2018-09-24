using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class VMComment
    {
        MySql mySql = new MySql();
        public static void add(string comment, int project_id, int applicant_id, int teljesites)
        {
            DateTime dateTime = DateTime.Now;
            if (teljesites >= 10)
            {
                teljesites = 10;
            }
            string command = "INSERT INTO megjegyzesek (jelolt_id,projekt_id,hr_id,hr_nev,megjegyzes,datum,ertekeles) VALUES (" + applicant_id + "," + project_id + "," + Session.UserData[0].id + ",'" + Session.UserData[0].name + "','" + comment + "','" + dateTime.ToString("yyyy. MM. dd.") + "'," + teljesites + ")";
            MySql.update(command);
            MySql.close();
        }
        public static void delete(int megjegyzes_id, int hr_id, int projekt_id, int jelolt_id)
        {
            string command = "DELETE FROM megjegyzesek WHERE megjegyzesek.id = " + megjegyzes_id + " AND hr_id = " + hr_id + " AND projekt_id = " + projekt_id + " AND jelolt_id = " + jelolt_id + "";
            MySql.update(command);
            MySql.close();
        }
    }
}

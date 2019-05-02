using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    public class Interview
    {
        DateTime date;

        public Interview()
        {
            date = DateTime.Today;
        }
        public static List<ModelKompetenciak> Data_Kompetencia() // javítva használja: newprojectpanel
        {
            List<ModelKompetenciak> list = ModelKompetenciak.GetModelKompetenciak("SELECT * FROM kompetenciak");
            return list;
        }

        public List<ModelInterview> Data_Interview() //javított
        {
            string command = "SELECT interview.id,megnevezes_projekt,jeloltek.nev,interview.projekt_id,interview.jelolt_id,jeloltek.email,interview.hr_id,felvitel_datum,date_start,interju_cim,interju_leiras,helyszin,sent, time_end,time_start FROM interview" +
                " INNER JOIN projektek ON interview.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id" +
                " WHERE interview.jelolt_id = " + Session.ApplicantID + "" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.GetModelInterview(command);
            return list;
        }

        public List<ModelInterview> Data_MyPreviousInterviews() //javított
        {
            string command = "SELECT interview.id,megnevezes_projekt,jeloltek.nev,interview.projekt_id,interview.jelolt_id,jeloltek.email,interview.hr_id,felvitel_datum,date_start,interju_cim,interju_leiras,helyszin, sent, time_end ,time_start FROM interview " +
                "INNER JOIN projektek ON interview.projekt_id = projektek.id " +
                "INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id " +
                "LEFT JOIN interju_resztvevo_kapcs ON interview.id = interju_resztvevo_kapcs.interju_id " +
                "WHERE interju_resztvevo_kapcs.user_id = " + Session.UserData.id + " AND date_start > '" + date.Year + "." + Utilities.DateCorrect(date.Month) +"."+ Utilities.DateCorrect(date.Day) + ".' ORDER BY date_start";
            List<ModelInterview> list = ModelInterview.GetModelInterview(command);
            return list;
        }
        public List<ModelInterview> Data_MyPastInterviews() //javított
        {
            string command = "SELECT interview.id,megnevezes_projekt,jeloltek.nev,interview.projekt_id,interview.jelolt_id,jeloltek.email,interview.hr_id,felvitel_datum,date_start,interju_cim,interju_leiras,helyszin, sent, time_end,time_start FROM interview " +
                "INNER JOIN projektek ON interview.projekt_id = projektek.id " +
                "INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id " +
                "LEFT JOIN interju_resztvevo_kapcs ON interview.id = interju_resztvevo_kapcs.interju_id " +
                "WHERE interju_resztvevo_kapcs.user_id = " + Session.UserData.id + " AND date_start < '" + date.Year + "." + Utilities.DateCorrect(date.Month) + "." + Utilities.DateCorrect(date.Day) + ".' ORDER BY date_start DESC";
            List<ModelInterview> list = ModelInterview.GetModelInterview(command);
            return list;
        }

        public static List<ModelInterview> Data_InterviewById() //javított
        {
            string command = "SELECT interview.id,megnevezes_projekt,jeloltek.nev,interview.projekt_id,interview.jelolt_id,jeloltek.email,interview.hr_id,felvitel_datum,date_start,interju_cim,interju_leiras,helyszin, sent, time_end ,time_start FROM interview" +
                " INNER JOIN projektek ON interview.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id" +
                " WHERE interview.id = " + Session.InterViewID + "" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.GetModelInterview(command);
            return list;
        }

        public static List<ModelErtesitendok> Data_ProjektErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_ertesitendok_kapcs ON ertesitendok_id = users.id WHERE projekt_id = " + Session.ProjektID + " GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            return list;
        }

        public static List<ModelErtesitendok> Data_InterjuErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email, kategoria, jogosultsag, validitas FROM interju_resztvevo_kapcs left JOIN users ON user_id = users.id WHERE interju_id = " + Session.InterViewID + " GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            return list;
        }

        public static void telephoneFilterInsert(int ismerte, int muszakok, string utazas) //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = 1 WHERE projekt_id = " + Session.ProjektID + " AND jelolt_id = " + Session.ApplicantID + "";
            mySql.Execute(command);
            command = "UPDATE jeloltek SET pmk_ismerte = " + ismerte + "  WHERE id = " + Session.ApplicantID + "";
            mySql.Execute(command);
            command = "INSERT INTO jelolt_statisztika (id, jelolt_id, utazas, muszakok) VALUES(null, " + Session.ApplicantID + ", '" + utazas + "', " + muszakok + ")";
            mySql.Execute(command);
            mySql.Close();
        }


        public void addInterview(string date_start, string cim, string leiras, string helyszin, string idopont_start, string idopont_end) // javítva
        {
            MySqlDB mySql = new MySqlDB();
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO `interview` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `date_start`, `interju_cim`, `interju_leiras`, `helyszin`,  `time_start`, `time_end`) VALUES (" + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData.id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + date_start + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont_start + "', '"+idopont_end+"');";
            mySql.Execute(command);
            mySql.Close();
        }

        public void madeSent(int id) // javítva
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE `interview` SET sent = 1 WHERE id = "+id+"";
            mySql.Execute(command);
            mySql.Close();
        }

        public void interviewDelete(int id) // javítva
        {
            MySqlDB mySql = new MySqlDB();
            mySql.Execute("DELETE FROM interview WHERE interview.id=" + id + " AND hr_id=" + Session.UserData.id + "");
            mySql.Execute("DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ");
            mySql.Close();
        }

        public static bool HasTest() // javítva
        {
            MySqlDB mySql = new MySqlDB();
            string command = "SELECT count(id) FROM kompetencia_jelolt_kapcs WHERE interju_id = " + Session.InterViewID + " AND hr_id = " + Session.UserData.id + "";
            bool response = mySql.Bind(command);
            mySql.Close();
            return response;
        }

        public static void UpdateTest(List<int> list) // javítva használja: interviewpanel
        {
            MySqlDB mySql = new MySqlDB();
            string command = "INSERT INTO `kompetencia_jelolt_kapcs` (`interju_id`, `projekt_id`, `jelolt_id`, `hr_id`, `k1_id`, `k1_val`, `k2_id`, `k2_val`, `k3_id`, `k3_val`, `k4_id`, `k4_val`, `k5_id`, `k5_val`, tamogatom) VALUES ("+Session.InterViewID + ", " + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData.id + ", " + list[0] + ", " + list[1] + ", " + list[2] + ", " + list[3] + ", " + list[4] + ", " + list[5] + ", " + list[6] + ", " + list[7] + ", " + list[8] + ", " + list[9] + ", " + list[10] + ");";
            mySql.Execute(command);
            mySql.Close();
        }

        public static void Insert(int id) // javítva
        {
            MySqlDB mySql = new MySqlDB();
            if (!mySql.IsExists("SELECT * FROM interju_resztvevo_kapcs WHERE interju_id = "+ Session.InterViewID + " AND user_id = "+id+""))
            {
                string command = "INSERT INTO `interju_resztvevo_kapcs` (`id`, `interju_id`, `user_id`) VALUES (NULL, " + Session.InterViewID + ", " + id + ");";
                mySql.Execute(command);
            }
            mySql.Close();
        }

        public static void DeleteInvited(int id) // javítva
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM `interju_resztvevo_kapcs` WHERE user_id = " + id + "";
            mySql.Execute(command);
            mySql.Close();
        }
    }
}

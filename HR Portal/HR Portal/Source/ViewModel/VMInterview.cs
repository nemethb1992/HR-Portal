using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    public class VMInterview
    {
        public static List<ModelKompetenciak> Data_Kompetencia() // javítva használja: newprojectpanel
        {
            List<ModelKompetenciak> list = ModelKompetenciak.getModelKompetenciak("SELECT * FROM kompetenciak");
            MySql.close();
            return list;
        }

        public static List<ModelInterview> Data_InterviewById() //javított
        {
            string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
                " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
                " WHERE interjuk_kapcs.id = " + Session.InterViewID + "" +
                " ORDER BY felvitel_datum";
            List<ModelInterview> list = ModelInterview.getModelInterview(command);
            MySql.close();
            return list;
        }

        public static List<ModelErtesitendok> Data_ProjektErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON ertesitendok_id = users.id WHERE projekt_id = " + Session.ProjektID + "";
            List<ModelErtesitendok> list = ModelErtesitendok.getModelErtesitendok(command);
            MySql.close();
            return list;
        }

        public static List<ModelErtesitendok> Data_InterjuErtesitendokKapcsolt() // javítva használja: interviewpanel
        {
            string command = "SELECT users.id, name, email FROM interju_resztvevo_kapcs left JOIN users ON user_id = users.id WHERE interju_id = " + Session.InterViewID + " GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.getModelErtesitendok(command);
            MySql.close();
            return list;
        }

        public static bool hasKompetencia() // javítva
        {
            string command = "SELECT * FROM kompetencia_jelolt_kapcs WHERE interju_id = " + Session.InterViewID + " AND hr_id = " + Session.UserData[0].id + "";
            bool response = MySql.bind(command);
            MySql.close();
            return response;
        }

        public static void kompetenciaUpdate(List<int> list) // javítva használja: interviewpanel
        {
            string command = "INSERT INTO `kompetencia_jelolt_kapcs` (`id`, `interju_id`, `projekt_id`, `jelolt_id`, `hr_id`, `k1_id`, `k1_val`, `k2_id`, `k2_val`, `k3_id`, `k3_val`, `k4_id`, `k4_val`, `k5_id`, `k5_val`, tamogatom) VALUES (null, " + Session.InterViewID + ", " + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData[0].id + ", " + list[0] + ", " + list[1] + ", " + list[2] + ", " + list[3] + ", " + list[4] + ", " + list[5] + ", " + list[6] + ", " + list[7] + ", " + list[8] + ", " + list[9] + ", " + list[10] + ");";
            MySql.update(command);
            MySql.close();
        }

        public static void insertInterviewInvited(int id) // javítva
        {
            string command = "INSERT INTO `interju_resztvevo_kapcs` (`id`, `interju_id`, `user_id`) VALUES (NULL, " + Session.InterViewID + ", " + id + ");";
            MySql.update(command);
            MySql.close();
        }

        public static void deleteInterviewInvited(int id) // javítva
        {
            string command = "DELETE FROM `interju_resztvevo_kapcs` WHERE user_id = " + id + "";
            MySql.update(command);
            MySql.close();
        }
    }
}

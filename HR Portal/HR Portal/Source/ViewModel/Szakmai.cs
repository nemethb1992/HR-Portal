using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using HR_Portal_Test.Source.Model.Applicant;
using HR_Portal_Test.Source.Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class Szakmai
    {
        public static List<ModelSzakmaiBevont> Data_SzakmaiProject()
        {
            string command = "SELECT coalesce((SELECT count(jelolt_id) FROM projekt_jelolt_kapcs WHERE projekt_id = projektek.id),0) as jeloltek_db, projektek.id, megnevezes_projekt, megnevezes_munka FROM projektek LEFT JOIN munkakor ON munkakor.id = projektek.munkakor INNER JOIN projekt_ertesitendok_kapcs ON projektek.id = projekt_ertesitendok_kapcs.projekt_id WHERE projektek.statusz = 1 AND ertesitendok_id = " + Session.UserData.id + " GROUP BY id";
            List<ModelSzakmaiBevont> list = ModelSzakmaiBevont.GetModelSzakmaiBevont(command);
            return list;
        }

        public static List<ModelInterview> Data_SzakmaiInterview()
        {
            string command = "SELECT interview.id, megnevezes_projekt, interview.projekt_id, interview.jelolt_id, jeloltek.nev,date_start,time_start,time_end,interju_cim,helyszin, sent FROM interju_resztvevo_kapcs INNER JOIN interview ON interju_resztvevo_kapcs.interju_id = interview.id " +
                " INNER JOIN projektek ON interview.projekt_id = projektek.id" +
                " INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id" +
                " WHERE interju_resztvevo_kapcs.user_id = " + Session.UserData.id + " ORDER BY date_start";
            List<ModelInterview> list = ModelInterview.GetSzakmaiInterview(command);
            return list;
        }

        public List<ModelApplicantSzakmaiList> Data_JeloltKapcsSzakmai(int projekt_id)
        {
            string command = " SELECT jeloltek.id, jeloltek.nev, coalesce((jelolt_allapot_szakmai.state),0) as state FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id LEFT JOIN jelolt_allapot_szakmai ON jeloltek.id = jelolt_allapot_szakmai.jelolt_id WHERE projekt_jelolt_kapcs.projekt_id = "+ projekt_id;
            List<ModelApplicantSzakmaiList> list = new ModelApplicantSzakmaiList().Get(command);
            return list;
        }
        public static List<ModelSzakmaiInterviewIgeny> Data_SzakmaiInterviewIgeny(int projekt_id)
        {
            string command = "SELECT * FROM jelolt_allapot_szakmai WHERE user_id="+Session.UserData.id +" AND projekt_id="+ projekt_id;
            List<ModelSzakmaiInterviewIgeny> list = new ModelSzakmaiInterviewIgeny().Get(command);
            return list;
        }
    }
}

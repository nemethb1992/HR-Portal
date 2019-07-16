using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Other;
using HR_Portal.Source.Model.Project;
using HR_Portal.View.Usercontrol.Panels;

namespace HR_Portal.Source
{
    class Utilities
    {

        public enum Views { ApplicantList, ApplicantDataSheet, ProjectList, ProjectDataSheet, InterviewPanel, ProjectJeloltDataSheet, FavoritePanel };

        private static Random random = new Random();
        public static void SetReturnPage(Views view)
        {
            Session.lastPage = view;
        }

        //public List<ModelInterview> Data_Interview() //javított
        //{
        //    string command = "SELECT interview.id,megnevezes_projekt,jeloltek.nev,interview.projekt_id,interview.jelolt_id,jeloltek.email,interview.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interview" +
        //        " INNER JOIN projektek ON interview.projekt_id = projektek.id" +
        //        " INNER JOIN jeloltek ON interview.jelolt_id = jeloltek.id" +
        //        " WHERE jelolt_id = " + Session.ApplicantID+"" +
        //        " AND projekt_id="+ Session.ProjektID+"" +
        //        " ORDER BY felvitel_datum";
        //    List<ModelInterview> list = ModelInterview.GetModelInterview(command);
        //    MySql.Close();
        //    return list;
        //}

        
        public List<ModelKompetenciaSummary> Data_KompetenciaJeloltKapcs() // javítva
        {
            string command = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID +" AND projekt_id = "+ Session.ProjektID+"";
            List < ModelKompetenciaSummary > list = ModelKompetenciaSummary.GetModelKompetenciaSummary(command);
            return list;
        }
        
        public List<ModelCimkek> Data_Cimkek() // javítva
        {
            return new ModelCimkek().GetAll();
        }
        public List<ModelSmallProject> Data_PorjectListSmall()
        {
            return ModelSmallProject.GetModelSmallProject("SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1");
        }

        public List<ModelTamogatas> Data_KompetenciaTamogatas() // javítva
        {
            string command = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID + " AND projekt_id = " + Session.ProjektID + "";
            return ModelTamogatas.GetModelTamogatas(command);
        }

        public List<ModelMunkakor> Data_Munkakor() //javított
        {
            return ModelMunkakor.GetModelMunkakor("SELECT * FROM munkakor");
        }

        public List<ModelStatusz> Data_Statusz() //javított
        {
            return ModelStatusz.GetModelStatusz("SELECT * FROM statusz");
        }

        public List<ModelPc> Data_Pc() //javított
        {
            return ModelPc.GetModelPc("SELECT * FROM pc");
        }

        public List<ModelVegzettseg> Data_Vegzettseg() //javított
        {
            return ModelVegzettseg.GetModelVegzettseg("SELECT * FROM vegzettsegek");
        }

        public List<ModelNyelv> Data_Nyelv() //javított
        {
            return ModelNyelv.GetModelNyelv("SELECT * FROM nyelv");
        }

        public List<ModelErtesulesek> Data_Ertesulesek() //javított
        {
            return ModelErtesulesek.GetModelErtesulesek("SELECT * FROM ertesulesek");
        }

        public List<ModelNem> Data_Nemek() //javított
        {
            return ModelNem.GetModelNem("SELECT * FROM nemek");
        }

        public List<ModelApplicantList> Data_JeloltKapcs()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interview " +
                "WHERE jelolt_id = jeloltek.id AND projekt_id = " + Session.ProjektID + " Group by projekt_id),0) as interjuk_db, " +
                "coalesce((SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id),0) as project_db, " +
                "jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, " +
                "jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota,jeloltek.statusz, jeloltek.friss, jeloltek.kategoria, jeloltek.megjegyzes, (SELECT EXISTS(SELECT * FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id)) as allasban " +
                "FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "LEFT JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "LEFT JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + Session.ProjektID + " GROUP BY jeloltek.id ";
            return ModelApplicantList.GetModelApplicantList(command);
        }
        
        public List<ModelComment> Data_CommentProject()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID;
            return ModelComment.GetModelComment(command);
        }

        public List<ModelComment> Data_CommentApplicant()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum FROM megjegyzesek WHERE jelolt_id=" + Session.ApplicantID;
            return ModelComment.GetModelComment(command);
        }

        public List<ModelErtesitendok> Data_ErtesitendokCheckbox(string ertesitendok_src = "")
        {
            string command = "SELECT id, name ,email, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + ertesitendok_src + "%'";
            return ModelErtesitendok.GetModelErtesitendok(command);
        }

        public List<ModelErtesitendok> Data_ErtesitendokKapcs() // javítva
        {
            string command = "SELECT users.id, name, email, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + Session.ProjektID + " AND users.validitas = 1 GROUP BY users.id";
            return ModelErtesitendok.GetModelErtesitendok(command);
        }

        public List<ModelApplicantListbox> Data_JeloltForCheckbox()
        {
            string command = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != " + Session.ProjektID + " OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            List<ModelApplicantListbox> list = ModelApplicantListbox.GetModelApplicantListboxShort(command);
            return list;
        }





        public void applicantArchiver(int id, int statusz) // javított
        {
            if (statusz == 0)
            {
                statusz = 1;
            }
            else
            {
                statusz = 0;
            }
            string command = "UPDATE jeloltek SET statusz=" + statusz + " WHERE jeloltek.id = " + id + ";";
            MySqlDB mySql = new MySqlDB();
            mySql.Execute(command);
            mySql.Close();
        }



        public void ApplicantStatusChange(int stat) // javított
        {
            Session.ApplicantStatusz = 0;
            Session.ApplicantStatusz = stat;
        }
        public List<ModelErtesitendok> Data_Ertesitendok()
        {
            return ModelErtesitendok.GetModelErtesitendok("SELECT * FROM users WHERE kategoria = 0 AND validitas = 1");
        }
        public List<ModelFreelancerList> Data_Freelancer()
        {
            return ModelFreelancerList.getFreelancerList("SELECT * FROM freelancer_list");
        }
        public List<ModelMunkakor> Data_Munkakorok()
        {
            return ModelMunkakor.GetModelMunkakor("SELECT * FROM munkakor");
        }
        
        public void Delete(int id, string table)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM " + table + " WHERE id=" + id + "";
            mySql.Execute(command);
            mySql.Close();
        }

        public void SettingsInsert(string content, string table, string other = "")
        {
            string command = "";

            switch (table)
            {
                case "ertesitendok":
                    command = "INSERT INTO `ertesitendok` (`id`, `ertesitendok_nev`, `email`, `telefon`) VALUES (NULL, '" + content + "', 'email', '000');";
                    break;
                case "vegzettsegek":
                    command = "INSERT INTO `vegzettsegek` (`id`, `megnevezes_vegzettseg`) VALUES(NULL, '" + content + "')";
                    break;
                case "munkakor":
                    command = "INSERT INTO `munkakor` (`id`, `megnevezes_munka`) VALUES (NULL, '" + content + "');";
                    break;
                case "pc":
                    command = "INSERT INTO `pc` (`id`, `megnevezes_pc`) VALUES (NULL, '" + content + "');";
                    break;
                case "ertesulesek":
                    command = "INSERT INTO `ertesulesek` (`id`, `ertesules_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
                case "nyelv":
                    command = "INSERT INTO `nyelv` (`id`, `megnevezes_nyelv`) VALUES (NULL, '" + content + "');";
                    break;
                case "kompetenciak":
                    command = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
                case "cimkek":
                    command = "INSERT INTO `jelolt_cimkek` (`id`, `cimke_megnevezes`) VALUES (NULL, '" + content + "');";
                    break;
                case "freelancer":
                    command = "INSERT INTO `freelancer_list` (`id`, `name`, `email`, `rid`) VALUES (NULL, '" + content + "', '" + other + "', '" + RandomString(10)+"');";
                    break;
            }
            MySqlDB mySql = new MySqlDB();
            mySql.Execute(command);
            mySql.Close();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int ComboBoxValueSetter(List<ModelId> ossz_li, List<ModelId> projekt_li)
        {
            int i = 0;
            foreach (var item in ossz_li)
            {
                if (item.id == projekt_li[0].id)
                {
                    break;
                }
                i++;
            }
            return i;
        }
        public static void NavigateTo(Grid grid, UIElement obj)
        {
            grid.Children.Clear();
            grid.Children.Add(obj);
        }

        public static string DateCorrect(int num)
        {
            return (num < 10 ? "0" + num.ToString() : num.ToString());
        }
        //public void AddInterPlusOne()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot + 1 WHERE projekt_id = "+pcontrol.ProjektID+ " AND jelolt_id = "+acontrol.ApplicantID+"";
        //    mysql.update(command);
        //}

        //public void progress_delete()
        //{
        //    string command = "UPDATE projekt_jelolt_kapcs SET allapot = allapot - 1 WHERE projekt_id = " + pcontrol.ProjektID + " AND jelolt_id = " + acontrol.ApplicantID + "";
        //    mysql.update(command);
        //}
    }
}

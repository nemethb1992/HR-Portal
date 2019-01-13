using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Other;
using HR_Portal.Source.Model.Project;
using HR_Portal.View.Usercontrol.Panels;

namespace HR_Portal.Source
{
    class CommonUtility
    {

        public enum Views { ApplicantList, ApplicantDataSheet, ProjectList, ProjectDataSheet, InterviewPanel, ProjectJeloltDataSheet };

        public static void SetReturnPage(Views view)
        {
            Session.lastPage = view;
        }
        
        //public List<ModelInterview> Data_Interview() //javított
        //{
        //    string command = "SELECT interjuk_kapcs.id,megnevezes_projekt,jeloltek.nev,interjuk_kapcs.projekt_id,interjuk_kapcs.jelolt_id,jeloltek.email,interjuk_kapcs.hr_id,felvitel_datum,interju_datum,interju_cim,interju_leiras,helyszin ,idopont FROM interjuk_kapcs" +
        //        " INNER JOIN projektek ON interjuk_kapcs.projekt_id = projektek.id" +
        //        " INNER JOIN jeloltek ON interjuk_kapcs.jelolt_id = jeloltek.id" +
        //        " WHERE jelolt_id = " + Session.ApplicantID+"" +
        //        " AND projekt_id="+ Session.ProjektID+"" +
        //        " ORDER BY felvitel_datum";
        //    List<ModelInterview> list = ModelInterview.GetModelInterview(command);
        //    MySql.Close();
        //    return list;
        //}


        public void addInterview(string interju_datum, string cim, string leiras, string helyszin, string idopont) // javítva
        {
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO `interjuk_kapcs` (`projekt_id`, `jelolt_id`, `hr_id`, `felvitel_datum`, `interju_datum`, `interju_cim`, `interju_leiras`, `helyszin`,  `idopont`) VALUES (" + Session.ProjektID + ", " + Session.ApplicantID + ", " + Session.UserData.id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "', '" + interju_datum + "', '" + cim + "', '" + leiras + "', '" + helyszin + "', '" + idopont + "');";
            MySql.Execute(command);
            MySql.Close();
        }

        public void interviewDelete(int id) // javítva
        {
            MySql.Execute("DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.id=" + id + " AND hr_id=" + Session.UserData.id + "");
            MySql.Execute("DELETE FROM interju_resztvevo_kapcs WHERE interju_id=" + id + " ");
            MySql.Close();
        }
        
        public List<ModelKompetenciaSummary> Data_KompetenciaJeloltKapcs() // javítva
        {
            string command = "SELECT coalesce(AVG(k1_val),0) as k1_val,coalesce(AVG(k2_val),0) as k2_val,coalesce(AVG(k3_val),0) as k3_val,coalesce(AVG(k4_val),0) as k4_val,coalesce(AVG(k5_val),0) as k5_val, tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID +" AND projekt_id = "+ Session.ProjektID+"";
            List < ModelKompetenciaSummary > list = ModelKompetenciaSummary.GetModelKompetenciaSummary(command);
            MySql.Close();
            return list;
        }

    

        public List<ModelTamogatas> Data_KompetenciaTamogatas() // javítva
        {
            string command = "SELECT tamogatom FROM kompetencia_jelolt_kapcs WHERE jelolt_id = " + Session.ApplicantID + " AND projekt_id = " + Session.ProjektID + "";
            List<ModelTamogatas> list = ModelTamogatas.GetModelTamogatas(command);
            MySql.Close();
            return list;
        }

        public List<ModelMunkakor> Data_Munkakor() //javított
        {
            string command = "SELECT * FROM munkakor";
            List<ModelMunkakor> list = ModelMunkakor.GetModelMunkakor(command);
            MySql.Close();
            return list;
        }

        public List<ModelStatusz> Data_Statusz() //javított
        {
            string command = "SELECT * FROM statusz";
            List<ModelStatusz> list = ModelStatusz.GetModelStatusz(command);
            MySql.Close();
            return list;
        }

        public List<ModelPc> Data_Pc() //javított
        {
            string command = "SELECT * FROM pc";
            List<ModelPc> list = ModelPc.GetModelPc(command);
            MySql.Close();
            return list;
        }

        public List<ModelVegzettseg> Data_Vegzettseg() //javított
        {
            string command = "SELECT * FROM vegzettsegek";
            List<ModelVegzettseg> list = ModelVegzettseg.GetModelVegzettseg(command);
            MySql.Close();
            return list;
        }

        public List<ModelNyelv> Data_Nyelv() //javított
        {
            string command = "SELECT * FROM nyelv";
            List<ModelNyelv> list = ModelNyelv.GetModelNyelv(command);
            MySql.Close();
            return list;
        }

        public List<ModelErtesulesek> Data_Ertesulesek() //javított
        {
            string command = "SELECT * FROM ertesulesek";
            List<ModelErtesulesek> list = ModelErtesulesek.GetModelErtesulesek(command);
            MySql.Close();
            return list;
        }

        public List<ModelNem> Data_Nemek() //javított
        {
            string command = "SELECT * FROM nemek";
            List<ModelNem> list = ModelNem.GetModelNem(command);
            MySql.Close();
            return list;
        }

        public List<ModelApplicantList> Data_JeloltKapcs()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs " +
                "WHERE jelolt_id = jeloltek.id AND projekt_id = " + Session.ProjektID + " Group by projekt_id),0) as interjuk_db, " +
                "jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, " +
                "jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott,jeloltek.statusz, jeloltek.friss, (SELECT EXISTS(SELECT * FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id)) as allasban " +
                "FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "LEFT JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "LEFT JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + Session.ProjektID + " GROUP BY jeloltek.id ";
            List<ModelApplicantList> list = ModelApplicantList.GetModelApplicantList(command);
            MySql.Close();
            return list;
        }
        
        public List<ModelComment> Data_CommentProject()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID;
            List<ModelComment> list = ModelComment.GetModelComment(command);
            MySql.Close();
            return list;
        }

        public List<ModelComment> Data_CommentApplicant()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum FROM megjegyzesek WHERE jelolt_id=" + Session.ApplicantID;
            List<ModelComment> list = ModelComment.GetModelComment(command);
            MySql.Close();
            return list;
        }

        public List<ModelErtesitendok> Data_ErtesitendokCheckbox(string ertesitendok_src)
        {
            string command = "SELECT id, name ,email, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + ertesitendok_src + "%'";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            MySql.Close();
            return list;
        }

        public List<ModelErtesitendok> Data_ErtesitendokKapcs() // javítva
        {
            string command = "SELECT users.id, name, email, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + Session.ProjektID + " AND users.validitas = 1 GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            MySql.Close();
            return list;
        }

        public List<ModelApplicantListbox> Data_JeloltForCheckbox(string nevsrc)
        {
            string command = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != " + Session.ProjektID + " OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            List<ModelApplicantListbox> list = ModelApplicantListbox.GetModelApplicantListboxShort(command);
            MySql.Close();
            return list;
        }

        public void addErtesitendokInsert(int index)
        {
            string command = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
            MySql.Execute(command);
            MySql.Close();
        }

        public void jeloltKapcsDelete(int id)
        {
            string command;
            command = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Execute(command);
            command = "DELETE FROM interjuk_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Execute(command);
            MySql.Close();
        }

        public void jeloltKapcsUpdate(int id, int allapota)
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = " + allapota + " WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Execute(command);
            MySql.Close();
        }

        public void ertesitendokKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Execute(command);
            MySql.Close();
        }

        public void publishProject(int stat)
        {
            string command = "UPDATE projektek SET publikalt= " + stat + " WHERE projektek.id = " + Session.ProjektID + ";";
            MySql.Execute(command);
            MySql.Close();
        }

        public void projectArchiver(int id, int statusz) // javított
        {
            if (statusz == 0)
            {
                statusz = 1;
            }
            else
            {
                statusz = 0;
            }
            string command = "UPDATE projektek SET statusz=" + statusz + " WHERE projektek.id = " + id + ";";
            MySql.Execute(command);
            MySql.Close();
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
            MySql.Execute(command);
            MySql.Close();
        }

        public void ProjectStatusChange(int stat) // javított
        {
            Session.ProjectStatusz = 0;
            Session.ProjectStatusz = stat;
        }

        public void ApplicantStatusChange(int stat) // javított
        {
            Session.ApplicantStatusz = 0;
            Session.ApplicantStatusz = stat;
        }

        public void projectDescriptionUpdate(string type, string content) // javított
        {
            string command = "";
            switch (type)
            {
                case "feladatok":
                    command = "UPDATE projektek SET feladatok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "elvarasok":
                    command = "UPDATE projektek SET elvarasok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "kinalunk":
                    command = "UPDATE projektek SET kinalunk='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                case "elonyok":
                    command = "UPDATE projektek SET elonyok='" + content + "' WHERE projektek.id = " + Session.ProjektID + "";
                    break;
                default:
                    break;
            }
            MySql.Execute(command);
            MySql.Close();
        }

        public void projectCostInsert(string megnevezes, string osszeg)  // javított
        {
            string command = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, " + Session.ProjektID + ", '" + megnevezes + "', " + osszeg + ");";
            MySql.Execute(command);
            MySql.Close();
        }

        public void projectCostDelete(int id)  // javított
        {
            string command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            MySql.Execute(command);
            MySql.Close();
        }
        public List<ModelErtesitendok> Data_Ertesitendok()
        {
            string command = "SELECT * FROM users WHERE kategoria = 0 AND validitas = 1";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            MySql.Close();
            return list;
        }

        public List<ModelMunkakor> Data_Munkakorok()
        {
            string command = "SELECT * FROM munkakor";
            List<ModelMunkakor> list = ModelMunkakor.GetModelMunkakor(command);
            MySql.Close();
            return list;
        }
        
        public void settingDelete(int id, string table)
        {
            string command = "DELETE FROM " + table + " WHERE id=" + id + "";
            MySql.Execute(command);
            MySql.Close();
        }

        public void settingInsert(string content, string table)
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
            }
            MySql.Execute(command);
            MySql.Close();
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

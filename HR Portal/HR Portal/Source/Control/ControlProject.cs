using HR_Portal.Source;
using System;
using System.Collections.Generic;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Source
{
    class ControlProject
    {
        ControlApplicant aControl = new ControlApplicant();
        
        public List<ModelApplicantList> Data_JeloltKapcs()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id AND projekt_id = " + Session.ProjektID + " Group by projekt_id),0) as interjuk_db, jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id INNER JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id INNER JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + Session.ProjektID + " GROUP BY jeloltek.id ";
            List<ModelApplicantList> list = ModelApplicantList.GetModelApplicantList(command);
            MySql.Close();
            return list;
        }

        public List<ModelVegzettseg> Data_Vegzettseg()
        {
            string command = "SELECT * FROM vegzettsegek";
            List<ModelVegzettseg> list = ModelVegzettseg.GetModelVegzettseg(command);
            MySql.Close();
            return list;
        }

        public List<ModelNyelv> Data_Nyelv()
        {
            string command = "SELECT * FROM nyelv";
            List<ModelNyelv> list = ModelNyelv.GetModelNyelv(command);
            MySql.Close();
            return list;
        }

        public List<ModelComment> Data_CommentProject()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID;
            List<ModelComment> list = ModelComment.GetModelComment(command);
            MySql.Close();
            return list;
        }

        public List<ModelComment> Data_CommentKapcs()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID +" AND jelolt_id="+ Session.ApplicantID +"";
            List<ModelComment> list = ModelComment.GetModelComment(command);
            MySql.Close();
            return list;
        }

        public List<ModelErtesitendok> Data_ErtesitendokCheckbox(string ertesitendok_src)
        {
            string command = "SELECT id, name ,email FROM users WHERE name LIKE '%"+ertesitendok_src+"%' AND kategoria = 0";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            MySql.Close();
            return list;
        }

        public List<ModelHr> Data_HrCheckbox(string nev_src)
        {
            string command = "SELECT id, name, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + nev_src + "%' AND kategoria = 1 GROUP BY users.name";
            List<ModelHr> list = ModelHr.GetModelHr(command);
            MySql.Close();
            return list;
        }

        public List<ModelHr> Data_HrProject()
        {
            string command = "SELECT users.id, name, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_hr_kapcs ON users.id = projekt_hr_kapcs.hr_id INNER JOIN projektek ON projektek.id = projekt_hr_kapcs.projekt_id WHERE projektek.id = " + Session.ProjektID + " AND users.kategoria = 1 GROUP BY users.id";
            List<ModelHr> list = ModelHr.GetModelHr(command);
            MySql.Close();
            return list;
        }
      
        public List<ModelErtesitendok> Data_ErtesitendokKapcs() // javítva
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + Session.ProjektID + " AND kategoria = 0 GROUP BY users.id";
            List<ModelErtesitendok> list = ModelErtesitendok.GetModelErtesitendok(command);
            MySql.Close();
            return list;
        }

        public List<ModelApplicantListbox> Data_JeloltForCheckbox(string nevsrc)
        {
            string command = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != "+ Session.ProjektID +" OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            List<ModelApplicantListbox> list = ModelApplicantListbox.GetModelApplicantListboxShort(command);
            MySql.Close();
            return list;
        }


        public void addHrInsert(int index)
        {
            string command = "INSERT INTO projekt_hr_kapcs (id, projekt_id, hr_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
            MySql.Update(command);
            MySql.Close();
        }

        public void addErtesitendokInsert(int index)
        {
            string command = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
            MySql.Update(command);
            MySql.Close();
        }

        public void jeloltKapcsDelete(int id)
        {
            string command;
            command = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = "+id+" AND projekt_id = " + Session.ProjektID + ";";
            MySql.Update(command);
            command = "DELETE FROM interjuk_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Update(command);
            MySql.Close();
        }

        public void jeloltKapcsUpdate(int id, int allapota)
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = "+allapota+" WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Update(command);
            MySql.Close();
        }

        public void ertesitendokKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Update(command);
            MySql.Close();
        }

        public void hrKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_hr_kapcs WHERE hr_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            MySql.Update(command);
            MySql.Close();
        }

        public void publishProject(int stat)
        {
            string command = "UPDATE projektek SET publikalt= "+ stat + " WHERE projektek.id = " + Session.ProjektID + ";";
            MySql.Update(command);
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
            string command = "UPDATE projektek SET statusz="+ statusz + " WHERE projektek.id = " + id + ";";
            MySql.Update(command);
            MySql.Close();
        }







        public void statusChange(int stat) // javított
        {
            if(Session.ProjectStatusz != null)
                Session.ProjectStatusz = 0;
            Session.ProjectStatusz = stat;
        }

        public void projectDescriptionUpdate(string type, string content) // javított
        {
            string command = "";
            switch (type)
            {
                case "feladatok":
                    command = "UPDATE projektek SET feladatok='" + content + "' WHERE projektek.id = " + Session.ProjektID + " AND hr_id = "+ Session.UserData[0].id+"";
                    break;
                case "elvarasok":
                    command = "UPDATE projektek SET elvarasok='" + content + "' WHERE projektek.id = " + Session.ProjektID + " AND hr_id = " + Session.UserData[0].id + "";
                    break;
                case "kinalunk":
                    command = "UPDATE projektek SET kinalunk='" + content + "' WHERE projektek.id = " + Session.ProjektID + " AND hr_id = " + Session.UserData[0].id + "";
                    break;
                case "elonyok":
                    command = "UPDATE projektek SET elonyok='" + content + "' WHERE projektek.id = " + Session.ProjektID + " AND hr_id = " + Session.UserData[0].id + "";
                    break;
                default:
                    break;
            }
            MySql.Update(command);
            MySql.Close();
        }

        public List<ModelKoltsegek> Data_ProjectCost()  // javított
        {
            string command = "SELECT * FROM projekt_koltsegek WHERE projekt_id = "+ Session.ProjektID +"";
            List<ModelKoltsegek> list = ModelKoltsegek.GetModelKoltsegek(command);
            MySql.Close();
            return list;
        }

        public void projectCostInsert(string megnevezes, string osszeg)  // javított
        {
            string command = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, "+ Session.ProjektID +", '"+megnevezes+"', "+osszeg+");";
            MySql.Update(command);
            MySql.Close();
        }

        public void projectCostDelete(int id)  // javított
        {
            string command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            MySql.Update(command);
            MySql.Close();
        }
    }
}

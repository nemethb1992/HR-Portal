using HR_Portal.Source;
using System;
using System.Collections.Generic;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Control
{
    class ControlProject
    {

        Source.MySql mySql = new Source.MySql();
        ControlApplicant aControl = new ControlApplicant();





        public List<ModelApplicantList> Data_JeloltKapcs()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id AND projekt_id = " + Session.ProjektID + " Group by projekt_id),0) as interjuk_db, jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott FROM jeloltek INNER JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id INNER JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id INNER JOIN munkakor ON jeloltek.munkakor = munkakor.id WHERE projektek.id =" + Session.ProjektID + " GROUP BY jeloltek.id ";
            List<ModelApplicantList> list = ModelApplicantList.getModelApplicantList(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelVegzettseg> Data_Vegzettseg()
        {
            string command = "SELECT * FROM vegzettsegek";
            List<ModelVegzettseg> list = mySql.Vegzettseg_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelNyelv> Data_Nyelv()
        {
            string command = "SELECT * FROM nyelv";
            List<ModelNyelv> list = mySql.getNyelv(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelComment> Data_CommentProject()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID;
            List<ModelComment> list = mySql.Megjegyzesek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelComment> Data_CommentKapcs()
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE projekt_id=" + Session.ProjektID +" AND jelolt_id="+ Session.ApplicantID +"";
            List<ModelComment> list = mySql.Megjegyzesek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelErtesitendok> Data_ErtesitendokCheckbox(string ertesitendok_src)
        {
            string command = "SELECT id, name ,email FROM users WHERE name LIKE '%"+ertesitendok_src+"%' AND kategoria = 0";
            List<ModelErtesitendok> list = mySql.getErtesitendok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelHr> Data_HrCheckbox(string nev_src)
        {
            string command = "SELECT id, name, kategoria, jogosultsag, validitas FROM users WHERE name LIKE '%" + nev_src + "%' AND kategoria = 1 GROUP BY users.name";
            List<ModelHr> list = mySql.getHrShort(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelHr> Data_HrProject()
        {
            string command = "SELECT users.id, name, kategoria, jogosultsag, validitas FROM users INNER JOIN projekt_hr_kapcs ON users.id = projekt_hr_kapcs.hr_id INNER JOIN projektek ON projektek.id = projekt_hr_kapcs.projekt_id WHERE projektek.id = " + Session.ProjektID + " AND users.kategoria = 1 GROUP BY users.id";
            List<ModelHr> list = mySql.getHrShort(command);
            Source.MySql.close();
            return list;
        }
      
        public List<ModelErtesitendok> Data_ErtesitendokKapcs() // javítva
        {
            string command = "SELECT users.id, name, email FROM users INNER JOIN projekt_ertesitendok_kapcs ON users.id = projekt_ertesitendok_kapcs.ertesitendok_id  WHERE projekt_ertesitendok_kapcs.projekt_id =" + Session.ProjektID + " AND kategoria = 0 GROUP BY users.id";
            List<ModelErtesitendok> list = mySql.getErtesitendok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelApplicantListbox> Data_JeloltForCheckbox(string nevsrc)
        {
            string command = "SELECT jeloltek.id, nev FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON projekt_jelolt_kapcs.jelolt_id = jeloltek.id WHERE projekt_jelolt_kapcs.projekt_id != "+ Session.ProjektID +" OR projekt_jelolt_kapcs.projekt_id IS NULL GROUP BY jeloltek.id";
            List<ModelApplicantListbox> list = ModelApplicantListbox.getModelApplicantListboxShort(command);
            Source.MySql.close();
            return list;
        }

        public void addJeloltInsert(int jelolt_index, int projekt_index)
        {
            string command = "SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = "+jelolt_index+"";
            if (!Source.MySql.isExists(command))
            {
                Source.MySql.close();
                DateTime dateTime = DateTime.Now;
                command = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id, datum) VALUES (NULL, " + projekt_index + ", " + jelolt_index + ", " + Session.UserData[0].id + ", '" + dateTime.ToString("yyyy.MM.dd.") + "' );";
                mySql.update(command);
            }
            Source.MySql.close();
        }

        public void addHrInsert(int index)
        {
            string command = "INSERT INTO projekt_hr_kapcs (id, projekt_id, hr_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
            mySql.update(command);
            Source.MySql.close();
        }

        public void addErtesitendokInsert(int index)
        {
            string command = "INSERT INTO projekt_ertesitendok_kapcs (id, projekt_id, ertesitendok_id) VALUES (NULL, " + Session.ProjektID + ", " + index + " );";
            mySql.update(command);
            Source.MySql.close();
        }

        public void jeloltKapcsDelete(int id)
        {
            string command;
            command = "DELETE FROM projekt_jelolt_kapcs WHERE jelolt_id = "+id+" AND projekt_id = " + Session.ProjektID + ";";
            mySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void jeloltKapcsUpdate(int id, int allapota)
        {
            string command = "UPDATE projekt_jelolt_kapcs SET allapota = "+allapota+" WHERE jelolt_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void ertesitendokKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_ertesitendok_kapcs WHERE ertesitendok_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void hrKapcsDelete(int id)
        {
            string command = "DELETE FROM projekt_hr_kapcs WHERE hr_id = " + id + " AND projekt_id = " + Session.ProjektID + ";";
            mySql.update(command);
            Source.MySql.close();
        }

        public void publishProject(int stat)
        {
            string command = "UPDATE projektek SET publikalt= "+ stat + " WHERE projektek.id = " + Session.ProjektID + ";";
            mySql.update(command);
            Source.MySql.close();
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
            mySql.update(command);
            Source.MySql.close();
        }

        public void projectDelete(int id) // javított
        {
            string command;
            command = "DELETE FROM projektek WHERE projektek.id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM projekt_hr_kapcs WHERE projekt_hr_kapcs.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM projekt_ertesitendok_kapcs WHERE projekt_ertesitendok_kapcs.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.projekt_id = " + id + ";";
            mySql.update(command);
            command = "DELETE FROM interjuk_kapcs WHERE interjuk_kapcs.projekt_id=" + id + " AND hr_id=" + Session.UserData[0].id + "";
            mySql.update(command);
            Source.MySql.close();
        }

        public void projectInsert(List<ModelInsertProject> items) // javított newprojectpanel
        {
            string command = "INSERT INTO projektek (`id`, `hr_id`, `megnevezes_projekt`, `pc`, `vegzettseg`, `tapasztalat_ev`, `statusz`, `fel_datum`, `le_datum`, `nyelvtudas`, `munkakor`, `szuldatum`, `ber`,  `kepesseg1`, `kepesseg2`, `kepesseg3`, `kepesseg4`, `kepesseg5`, `feladatok`, `elvarasok`, `kinalunk`)" +
                " VALUES (NULL, "+items[0].hr_id+ ", '" + items[0].megnevezes_projekt+ "'," + items[0].pc+ "," + items[0].vegzettseg+ "," + items[0].tapasztalat_ev+ "," + items[0].statusz+ ",'" + items[0].fel_datum+ "','" + items[0].le_datum+ "'," + items[0].nyelvtudas+ "," + items[0].munkakor+ "," + items[0].szuldatum + "," + items[0].ber+ "," + items[0].kepesseg1+ "," + items[0].kepesseg2+ "," + items[0].kepesseg3+ "," + items[0].kepesseg4+ "," + items[0].kepesseg5+ ",'" + items[0].feladatok+ "','" + items[0].elvarasok+ "','" + items[0].kinalunk+ "');";
            mySql.update(command);
            int proID = Convert.ToInt16(mySql.listQuery("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            Source.MySql.close();
        }

        public void projectUpdate(List<ModelInsertProject> items) // javított newprojectpanel
        {
            string command = "UPDATE projektek SET " +
                " `hr_id` =  " + items[0].hr_id + ", " +
                "`megnevezes_projekt` =  '" + items[0].megnevezes_projekt + "', " +
                " `pc` =  " + items[0].pc + ", " +
                "`vegzettseg` =  " + items[0].vegzettseg + ", " +
                "`tapasztalat_ev` =  " + items[0].tapasztalat_ev + ", " +
                "`statusz` =  " + items[0].statusz + ", " +
                "`nyelvtudas` =  " + items[0].nyelvtudas + ", " +
                "`munkakor` =  " + items[0].munkakor + ", " +
                "`szuldatum` =  " + items[0].szuldatum + ", " +
                "`ber` =  " + items[0].ber + ", " +
                "`kepesseg1` =  " + items[0].kepesseg1 + ", " +
                "`kepesseg2` =  " + items[0].kepesseg2 + ", " +
                "`kepesseg3` =  " + items[0].kepesseg3 + ", " +
                "`kepesseg4` =  " + items[0].kepesseg4 + ", " +
                "`kepesseg5` =  " + items[0].kepesseg5 + " WHERE id = "+ Session.ProjektID + "";
            mySql.update(command);
            int proID = Convert.ToInt16(mySql.listQuery("SELECT projektek.id FROM projektek WHERE projektek.megnevezes_projekt = '" + items[0].megnevezes_projekt + "' AND projektek.pc = " + items[0].pc + " AND projektek.munkakor = '" + items[0].munkakor + "'", "projektek", 1)[0]);
            Session.ProjektID = proID;
            Source.MySql.close();
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
            mySql.update(command);
            Source.MySql.close();
        }

        public List<ModelKoltsegek> Data_ProjectCost()  // javított
        {
            string command = "SELECT * FROM projekt_koltsegek WHERE projekt_id = "+ Session.ProjektID +"";
            List<ModelKoltsegek> list = mySql.Koltsegek_MySql_listQuery(command);
            Source.MySql.close();
            return list;
        }

        public void projectCostInsert(string megnevezes, string osszeg)  // javított
        {
            string command = "INSERT INTO `projekt_koltsegek` (id, projekt_id, koltseg_megnevezes, osszeg) VALUES (null, "+ Session.ProjektID +", '"+megnevezes+"', "+osszeg+");";
            mySql.update(command);
            Source.MySql.close();
        }

        public void projectCostDelete(int id)  // javított
        {
            string command = "DELETE FROM projekt_koltsegek WHERE projekt_koltsegek.id = " + id + "";
            mySql.update(command);
            Source.MySql.close();
        }
    }
}

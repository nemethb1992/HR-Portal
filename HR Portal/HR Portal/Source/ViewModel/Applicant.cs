using HR_Portal.Public.templates;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{

    public class Applicant
    {

        public ModelFullApplicant data;
        public List<ModelFullApplicant> list;
        public Applicant(int applicantId = 0)
        {
            this.data = GetFullApplicant(applicantId)[0];
            this.list = GetFullApplicant(applicantId);
        }
        public static List<ModelApplicantList> GetApplicantList(ModelApplicantSearchBar sw)
        {
            double listNo = (sw.numberLimit != 0 ? sw.numberLimit : 10);

            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interview WHERE jelolt_id = jeloltek.id GROUP BY jelolt_id),0) as interjuk_db, " +
                "coalesce((SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id),0) as project_db, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor) as munkakor, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2) as munkakor2, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3) as munkakor3, " +
                "jeloltek.id,jeloltek.nev,szuldatum,reg_date,allapota,kolcsonzott,jeloltek.statusz,jeloltek.megjegyzes,email,friss,profession_type, " +
                "(SELECT EXISTS(SELECT * FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id)) as allasban " +
                "FROM jeloltek " +
                "LEFT JOIN megjegyzesek ON jeloltek.id = megjegyzesek.jelolt_id " +
                "LEFT JOIN munkakor on jeloltek.munkakor = munkakor.id " +
                "LEFT JOIN jelolt_cimke_kapcs on jelolt_cimke_kapcs.jelolt_id = jeloltek.id " +
                "LEFT JOIN jelolt_cimkek on jelolt_cimkek.id = jelolt_cimke_kapcs.cimke_id " +
                "LEFT JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id LIKE '%%' AND jeloltek.statusz =" + Session.ApplicantStatusz;

            if (sw.nev != "")
            {
                command += " AND jeloltek.nev LIKE '%" + sw.nev + "%' ";
            }
            if (sw.lakhely != "")
            {
                command += " AND jeloltek.lakhely LIKE '%" + sw.lakhely + "%' ";
            }
            if (sw.email != "")
            {
                command += " AND jeloltek.email LIKE '%" + sw.email + "%' ";
            }
            if (sw.eletkor != "")
            {
                command += " AND jeloltek.szuldatum <= " + sw.eletkor + " ";
            }

            //if (searchValue[0].tapasztalat != "" && searchValue[0].tapasztalat != "0")
            //{
            //    command += "AND jeloltek.tapasztalat_ev >= " + searchValue[0].tapasztalat + " ";
            //}

            if (sw.regdate != "")
            {
                command += " AND jeloltek.reg_date LIKE '%" + sw.regdate + "%' ";
            }
            if (sw.interjuk != "" && sw.interjuk != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id) FROM interview WHERE jelolt_id = jeloltek.id Group by projekt_id),0) >= " + sw.interjuk + " ";
            }
            if (sw.nemekStr != "")
            {
                command += " AND jeloltek.neme LIKE '%" + sw.nemekStr + "%' ";
            }
            if (sw.munkakorStr != "")
            {
                command += " AND jeloltek.munkakor LIKE '%" + sw.munkakorStr + "%' ";
            }
            if (sw.vegzettsegStr != "")
            {
                command += " AND jeloltek.vegz_terulet LIKE '%" + sw.vegzettsegStr + "%' ";
            }
            if (sw.cimke != "")
            {
                command += " AND jelolt_cimkek.cimke_megnevezes LIKE '%" + sw.cimke + "%' ";
            }
            if (sw.cimke != "")
            {
                command += " OR megjegyzesek.megjegyzes LIKE '%" + sw.cimke + "%' ";
            }
            if (sw.szabad == "1")
            {
                command += "  AND projekt_jelolt_kapcs.id IS NULL ";
            }
            if (sw.allasbanBool)
            {
                command += "  AND projekt_jelolt_kapcs.id IS NOT NULL ";
            }
            command += " GROUP BY jeloltek.email ";

            switch (sw.HeaderSelected)
            {
                case "1":
                    command += " ORDER BY jeloltek.nev" + sw.sorrend;
                    break;
                case "2":
                    command += " ORDER BY jeloltek.statusz" + sw.sorrend;
                    break;
                case "3":
                    command += " ORDER BY jeloltek.reg_date" + sw.sorrend;
                    break;
                default:
                    command += " ORDER BY jeloltek.reg_date DESC, friss DESC";
                    break;
            }
            command += " LIMIT "+ listNo + " OFFSET "+Session.ApplicantSearchPage * listNo + "";

            return ModelApplicantList.GetModelApplicantList(command);
        }

        public static List<ModelApplicantList> Data_FavoriteApplicants()
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interview " +
                "WHERE jelolt_id = jeloltek.id Group by projekt_id),0) as interjuk_db, " +
                "coalesce((SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id),0) as project_db, " +
                "jeloltek.id,nev,jeloltek.szuldatum,megnevezes_munka,email,reg_date,kepesseg1,kepesseg2,kepesseg3,kepesseg4,kepesseg5, " +
                "jeloltek.munkakor, jeloltek.munkakor2, jeloltek.munkakor3, allapota, kolcsonzott,jeloltek.statusz, jeloltek.friss, jeloltek.profession_type, jeloltek.megjegyzes, (SELECT EXISTS(SELECT * FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id)) as allasban " +
                "FROM jeloltek LEFT JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "LEFT JOIN projektek ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "LEFT JOIN munkakor ON jeloltek.munkakor = munkakor.id " +
                "LEFT JOIN jelolt_megfigyelt ON jeloltek.id = jelolt_megfigyelt.jelolt_id WHERE jelolt_megfigyelt.user_id = "+Session.UserData.id+ " GROUP BY jeloltek.id  ORDER BY jelolt_megfigyelt.date";
            List<ModelApplicantList> list = ModelApplicantList.GetModelApplicantList(command);
            return list;
        }

        public static ModelFullApplicant GetFullApplicantByEmail(string email)
        {
            string command = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,jeloltek.statusz,folderUrl,hirlevel,jeloltek.megjegyzes,profession_type," +
                "coalesce((SELECT nem FROM nemek WHERE nemek.id = jeloltek.neme),'') AS neme," +
                "(SELECT nemek.id FROM nemek WHERE nemek.id = jeloltek.neme) AS id_neme," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor),'') AS munkakor," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),'') AS munkakor2," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),'') AS munkakor3," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor),0) AS id_munkakor," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),0) AS id_munkakor2," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),0) AS id_munkakor3," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),'') AS nyelvtudas," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),'') AS nyelvtudas2," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),0) AS id_nyelvtudas," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),0) AS id_nyelvtudas2," +
                "coalesce((SELECT ertesules_megnevezes FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),'') AS ertesules_megnevezes, " +
                "coalesce((SELECT ertesulesek.id FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),0) AS id_ertesult, " +
                "coalesce((SELECT megnevezes_vegzettseg FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),'') AS vegz_terulet, " +
                "coalesce((SELECT vegzettsegek.id FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),0) AS id_vegz_terulet " +
                "FROM jeloltek WHERE jeloltek.email = '" + email + "'";

            ModelFullApplicant applicant = ModelFullApplicant.GetModelFullApplicant(command)[0];

            return applicant;
        }

        public List<ModelFullApplicant> GetFullApplicant(int id = 0)
        {
            string command = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,jeloltek.statusz,folderUrl,hirlevel,profession_type," +
                "coalesce((SELECT nem FROM nemek WHERE nemek.id = jeloltek.neme),'') AS neme," +
                "(SELECT nemek.id FROM nemek WHERE nemek.id = jeloltek.neme) AS id_neme," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor),'') AS munkakor," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),'') AS munkakor2," +
                "coalesce((SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),'') AS munkakor3," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor),0) AS id_munkakor," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor2),0) AS id_munkakor2," +
                "coalesce((SELECT munkakor.id FROM munkakor WHERE munkakor.id = jeloltek.munkakor3),0) AS id_munkakor3," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),'') AS nyelvtudas," +
                "coalesce((SELECT megnevezes_nyelv FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),'') AS nyelvtudas2," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas),0) AS id_nyelvtudas," +
                "coalesce((SELECT nyelv.id FROM nyelv WHERE nyelv.id = jeloltek.nyelvtudas2),0) AS id_nyelvtudas2," +
                "coalesce((SELECT ertesules_megnevezes FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),'') AS ertesules_megnevezes, " +
                "coalesce((SELECT ertesulesek.id FROM ertesulesek WHERE ertesulesek.id = jeloltek.ertesult),0) AS id_ertesult, " +
                "coalesce((SELECT megnevezes_vegzettseg FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),'') AS vegz_terulet, " +
                "coalesce((SELECT vegzettsegek.id FROM vegzettsegek WHERE vegzettsegek.id = jeloltek.vegz_terulet),0) AS id_vegz_terulet " +
                "FROM jeloltek WHERE jeloltek.id = " + (id.Equals(0) ? Session.ApplicantID : id) + "";

            List<ModelFullApplicant> list = ModelFullApplicant.GetModelFullApplicant(command);
           

            return list;
        }

        public static void DeleteApplicant(int id)   //javított használja: applicantlist
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM jeloltek WHERE jeloltek.id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM kepessegek WHERE kepessegek.jelolt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = " + id + ";";
            mySql.Execute(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.jelolt_id = " + id + ";";
            mySql.Execute(command);
            mySql.Close();
            try{
                Files.DeleteFolder(id);
            }
            catch {
            }
        }

        public static void AddToFavorite(int id)  //javított
        {
            MySqlDB mySql = new MySqlDB();
            if(!mySql.IsExists("SELECT * FROM jelolt_megfigyelt WHERE user_id = "+ Session.UserData.id + " AND jelolt_id = "+id+""))
            {
                DateTime date = DateTime.Today;
                string command = "INSERT INTO jelolt_megfigyelt (user_id,jelolt_id,date) VALUES(" + Session.UserData.id + "," + id + ",'" + date.Year + "." + Utilities.DateCorrect(date.Month) + "." + Utilities.DateCorrect(date.Day) + ".');";
                mySql.Execute(command);
            }
            mySql.Close();
        }
        public static void DeleteFromFavorite(int id)  //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM jelolt_megfigyelt WHERE jelolt_megfigyelt.jelolt_id = " + id + ";";
            mySql.Execute(command);
            mySql.Close();
        }


        public static void Insert(ModelFullApplicant data)  //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + data.nev + "',  '" + data.email + "', '" + data.telefon + "', '" + data.lakhely + "', " + data.ertesult + ", " + data.szuldatum + ", " + data.neme + "," + data.tapasztalat_ev + "," + data.munkakor + "," + data.munkakor2 + "," + data.munkakor3 + "," + data.vegz_terulet + "," + data.nyelvtudas + "," + data.nyelvtudas2 + ",'" + data.reg_date + "');";
            mySql.Execute(command);
            command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + data.email + "' AND jeloltek.nev = '" + data.nev + "'";
            mySql.Close();
            Session.ApplicantID = Convert.ToInt16(mySql.UniqueList(command, "jeloltek", 1)[0]);
            mySql.Close();
        }

        public static void Update(ModelFullApplicant data)  //javított
        {
            MySqlDB mySql = new MySqlDB();
            string query = "UPDATE jeloltek SET " +
                " `nev` = '" + data.nev + "'" +
                ", `email` = '" + data.email + "'" +
                ", `telefon` = '" + data.telefon + "'" +
                ", `lakhely` = '" + data.lakhely + "'" +
                ", `ertesult` = " + data.ertesult + "" +
                ", `szuldatum` = '" + data.szuldatum + "'" +
                ", `neme` = " + data.neme + "" +
                ", `tapasztalat_ev` = " + data.tapasztalat_ev + "" +
                ", `munkakor` = " + data.munkakor + "" +
                ", `munkakor2` = " + data.munkakor2 + "" +
                ", `munkakor3` = " + data.munkakor3 + "" +
                ", `vegz_terulet` = " + data.vegz_terulet + "" +
                ", `nyelvtudas` = " + data.nyelvtudas + "" +
                ",`nyelvtudas2` = " + data.nyelvtudas2 + "" +
                ", `reg_date`  = '" + data.reg_date + "'" +
                "WHERE jeloltek.id = " + Session.ApplicantID + "";
            mySql.Execute(query);
            int appID = Convert.ToInt16(mySql.UniqueList("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + data.email + "' AND jeloltek.nev = '" + data.nev + "' AND jeloltek.lakhely = '" + data.lakhely + "'", "jeloltek", 1)[0]);
            Session.ApplicantID = appID;
            mySql.Close();
        }

        public List<ModelSmallProject> Data_ProjectList(int id = 0) //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek " +
                "INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id = " + (id.Equals(0) ? Session.ApplicantID : id) + " " +
                "GROUP BY projektek.id";
            List<ModelSmallProject> list = ModelSmallProject.GetModelSmallProject(command);
            return list;
        }

        public static List<ModelApplicantListbox> GetAllActive(string name = "") //javított
        {
            string command = "SELECT id, nev FROM jeloltek WHERE jeloltek.statusz = 1 AND nev LIKE '%"+name+"%'";
            List<ModelApplicantListbox> list = ModelApplicantListbox.GetModelApplicantListboxShort(command);
            return list;
        }


        public static void FirstOpen(int applicantId)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE jeloltek SET friss = false WHERE id = " + applicantId + ";";
            mySql.Execute(command);
            mySql.Close();

            try
            {
                Applicant applicant = new Applicant(applicantId);
                new Email().Send(applicant.data.email, new EmailTemplate().Udvozlo_Email(applicant.data.nev));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteProject(int id)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + Session.ApplicantID + ";";
            mySql.Execute(command);
            mySql.Close();
        }
        
        public void AddToProject(int projekt_index)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = " + data.id + " AND projekt_id = "+projekt_index+"";
            if (!mySql.IsExists(command))
            {
                mySql.Close();
                command = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id,allapota, datum) VALUES (NULL, " + projekt_index + ", " + data.id + ", " + Session.UserData.id + ",4, '" + DateTime.Now.ToString("yyyy.MM.dd") + "' );";
                mySql.Execute(command);
            }
            mySql.Close();
        }
    }
}

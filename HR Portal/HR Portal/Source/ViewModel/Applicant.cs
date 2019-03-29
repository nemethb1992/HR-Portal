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
        public static List<ModelApplicantList> GetApplicantList(List<ModelApplicantSearchBar> sw)
        {
            double listNo = (sw[0].numberLimit != 0 ? sw[0].numberLimit : 10);

            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id GROUP BY jelolt_id),0) as interjuk_db, " +
                "coalesce((SELECT count(projekt_id) FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id),0) as project_db, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor) as munkakor, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2) as munkakor2, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3) as munkakor3, " +
                "jeloltek.id,jeloltek.nev,szuldatum,reg_date,allapota,kolcsonzott,jeloltek.statusz,jeloltek.megjegyzes,email,friss,profession_type, " +
                "(SELECT EXISTS(SELECT * FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = jeloltek.id)) as allasban " +
                "FROM jeloltek " +
                "LEFT JOIN megjegyzesek ON jeloltek.id = megjegyzesek.jelolt_id " +
                "LEFT JOIN munkakor on jeloltek.munkakor = munkakor.id " +
                "LEFT JOIN projekt_jelolt_kapcs ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id LIKE '%%' AND jeloltek.statusz =" + Session.ApplicantStatusz;

            if (sw[0].nev != "")
            {
                command += " AND jeloltek.nev LIKE '%" + sw[0].nev + "%' ";
            }
            if (sw[0].lakhely != "")
            {
                command += " AND jeloltek.lakhely LIKE '%" + sw[0].lakhely + "%' ";
            }
            if (sw[0].email != "")
            {
                command += " AND jeloltek.email LIKE '%" + sw[0].email + "%' ";
            }
            if (sw[0].eletkor != "")
            {
                command += " AND jeloltek.szuldatum <= " + sw[0].eletkor + " ";
            }

            //if (searchValue[0].tapasztalat != "" && searchValue[0].tapasztalat != "0")
            //{
            //    command += "AND jeloltek.tapasztalat_ev >= " + searchValue[0].tapasztalat + " ";
            //}

            if (sw[0].regdate != "")
            {
                command += " AND jeloltek.reg_date LIKE '%" + sw[0].regdate + "%' ";
            }
            if (sw[0].interjuk != "" && sw[0].interjuk != "0")
            {
                command += " AND coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id Group by projekt_id),0) >= " + sw[0].interjuk + " ";
            }
            if (sw[0].nemekStr != "")
            {
                command += " AND jeloltek.neme LIKE '%" + sw[0].nemekStr + "%' ";
            }
            if (sw[0].munkakorStr != "")
            {
                command += " AND jeloltek.munkakor LIKE '%" + sw[0].munkakorStr + "%' ";
            }
            if (sw[0].vegzettsegStr != "")
            {
                command += " AND jeloltek.vegz_terulet LIKE '%" + sw[0].vegzettsegStr + "%' ";
            }
            if (sw[0].cimke != "")
            {
                command += " AND megjegyzesek.megjegyzes LIKE '%" + sw[0].cimke + "%' ";
            }
            if (sw[0].szabad == "1")
            {
                command += "  AND projekt_jelolt_kapcs.id IS NULL ";
            }
            if (sw[0].allasbanBool)
            {
                command += "  AND projekt_jelolt_kapcs.id IS NOT NULL ";
            }
            command += " GROUP BY jeloltek.email ";

            switch (sw[0].HeaderSelected)
            {
                case "1":
                    command += " ORDER BY jeloltek.nev" + sw[0].sorrend;
                    break;
                case "2":
                    command += " ORDER BY jeloltek.statusz" + sw[0].sorrend;
                    break;
                case "3":
                    command += " ORDER BY jeloltek.reg_date" + sw[0].sorrend;
                    break;
                default:
                    command += " ORDER BY jeloltek.reg_date DESC, friss DESC";
                    break;
            }
            command += " LIMIT "+ listNo + " OFFSET "+Session.ApplicantSearchPage * listNo + "";

            return ModelApplicantList.GetModelApplicantList(command);
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
            MySql mySql = new MySql();
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

        public static void Insert(ModelFullApplicant data)  //javított
        {
            MySql mySql = new MySql();
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
            MySql mySql = new MySql();
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
            MySql mySql = new MySql();
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
            MySql mySql = new MySql();
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + Session.ApplicantID + ";";
            mySql.Execute(command);
            mySql.Close();
        }
        
        public void AddToProject(int projekt_index)
        {
            MySql mySql = new MySql();
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

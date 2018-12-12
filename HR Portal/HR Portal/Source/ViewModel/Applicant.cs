using HR_Portal.Public.templates;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Project;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.ViewModel
{
    interface Applicant
    {
        List<ModelApplicantList> GetApplicantList(List<ModelApplicantSearchBar> searchValue = null);

        List<ModelFullApplicant> GetFullApplicant(int id = 0);

        List<ModelSmallProject> Data_ProjectList();

        List<ModelSmallProject> Data_PorjectListSmall();

        void Insert(List<ModelFullApplicant> items);

        void Update(List<ModelFullApplicant> items);

        void DeleteApplicant(int id);

        void DeleteProject(int id);

        void AddToProject(int jelolt_index, int projekt_index);

        void FirstOpen(int applicantId);
    }
    class ApplicantImplementation : Applicant
    {
        public List<ModelApplicantList> GetApplicantList(List<ModelApplicantSearchBar> sw = null)
        {
            string command = "SELECT coalesce((SELECT count(projekt_id) FROM interjuk_kapcs WHERE jelolt_id = jeloltek.id GROUP BY jelolt_id),0) as interjuk_db, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor) as munkakor, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor2) as munkakor2, " +
                "(SELECT megnevezes_munka FROM munkakor WHERE munkakor.id = jeloltek.munkakor3) as munkakor3, " +
                "jeloltek.id,jeloltek.nev,szuldatum,reg_date,allapota,kolcsonzott,jeloltek.statusz,email,friss, " +
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
                    command += " ORDER BY jeloltek.id" + sw[0].sorrend;
                    break;
                case "2":
                    command += " ORDER BY jeloltek.nev" + sw[0].sorrend;
                    break;
                case "3":
                    command += " ORDER BY jeloltek.munkakor" + sw[0].sorrend;
                    break;
                case "4":
                    command += " ORDER BY interjuk_db" + sw[0].sorrend;
                    break;
                case "5":
                    command += " ORDER BY jeloltek.szuldatum" + sw[0].sorrend;
                    break;
                case "6":
                    command += " ORDER BY jeloltek.reg_date" + sw[0].sorrend;
                    break;
                default:
                    command += " ORDER BY jeloltek.reg_date DESC, friss DESC";
                    break;
            }
            command += " LIMIT 50";

            List<ModelApplicantList> list = ModelApplicantList.GetModelApplicantList(command);

            MySql.Close();

            return list;
        }

        public List<ModelFullApplicant> GetFullApplicant(int id = 0)
        {
            if (id == 0)
                id = Session.ApplicantID;
            string command = "SELECT jeloltek.id,nev,email,telefon,lakhely,pmk_ismerte,szuldatum,neme,tapasztalat_ev, reg_date,felvett,jeloltek.megjegyzes,jeloltek.statusz,folderUrl,hirlevel," +
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
                "FROM jeloltek WHERE jeloltek.id = " + id + "";

            List<ModelFullApplicant> list = ModelFullApplicant.GetModelFullApplicant(command);

            MySql.Close();

            return list;
        }

        public void DeleteApplicant(int id)   //javított használja: applicantlist
        {
            string command = "DELETE FROM jeloltek WHERE jeloltek.id = " + id + ";";
            MySql.Execute(command);
            command = "DELETE FROM kepessegek WHERE kepessegek.jelolt_id = " + id + ";";
            MySql.Execute(command);
            command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_jelolt_kapcs.jelolt_id = " + id + ";";
            MySql.Execute(command);
            command = "DELETE FROM megjegyzesek WHERE megjegyzesek.jelolt_id = " + id + ";";
            MySql.Execute(command);
            MySql.Close();
        }

        public void Insert(List<ModelFullApplicant> items)  //javított
        {
            string command = "INSERT INTO jeloltek (`id`, `nev`, `email`, `telefon`, `lakhely`, `ertesult`, `szuldatum`, neme, `tapasztalat_ev`, `munkakor`, `munkakor2`, `munkakor3`, `vegz_terulet`, `nyelvtudas`,`nyelvtudas2`, `reg_date`) " +
                "VALUES(NULL, '" + items[0].nev + "',  '" + items[0].email + "', '" + items[0].telefon + "', '" + items[0].lakhely + "', " + items[0].ertesult + ", " + items[0].szuldatum + ", " + items[0].neme + "," + items[0].tapasztalat_ev + "," + items[0].munkakor + "," + items[0].munkakor2 + "," + items[0].munkakor3 + "," + items[0].vegz_terulet + "," + items[0].nyelvtudas + "," + items[0].nyelvtudas2 + ",'" + items[0].reg_date + "');";
            MySql.Execute(command);
            command = "SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "'";
            MySql.Close();
            Session.ApplicantID = Convert.ToInt16(MySql.UniqueList(command, "jeloltek", 1)[0]);
            MySql.Close();
        }

        public void Update(List<ModelFullApplicant> items)  //javított
        {
            string query = "UPDATE jeloltek SET " +
                " `nev` = '" + items[0].nev + "'" +
                ", `email` = '" + items[0].email + "'" +
                ", `telefon` = '" + items[0].telefon + "'" +
                ", `lakhely` = '" + items[0].lakhely + "'" +
                ", `ertesult` = " + items[0].ertesult + "" +
                ", `szuldatum` = '" + items[0].szuldatum + "'" +
                ", `neme` = " + items[0].neme + "" +
                ", `tapasztalat_ev` = " + items[0].tapasztalat_ev + "" +
                ", `munkakor` = " + items[0].munkakor + "" +
                ", `munkakor2` = " + items[0].munkakor2 + "" +
                ", `munkakor3` = " + items[0].munkakor3 + "" +
                ", `vegz_terulet` = " + items[0].vegz_terulet + "" +
                ", `nyelvtudas` = " + items[0].nyelvtudas + "" +
                ",`nyelvtudas2` = " + items[0].nyelvtudas2 + "" +
                ", `reg_date`  = '" + items[0].reg_date + "'" +
                "WHERE jeloltek.id = " + Session.ApplicantID + "";
            MySql.Execute(query);
            int appID = Convert.ToInt16(MySql.UniqueList("SELECT jeloltek.id FROM jeloltek WHERE jeloltek.email = '" + items[0].email + "' AND jeloltek.nev = '" + items[0].nev + "' AND jeloltek.lakhely = '" + items[0].lakhely + "'", "jeloltek", 1)[0]);
            Session.ApplicantID = appID;
            MySql.Close();
        }

        public List<ModelSmallProject> Data_ProjectList() //javított
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek " +
                "INNER JOIN projekt_jelolt_kapcs ON projektek.id = projekt_jelolt_kapcs.projekt_id " +
                "INNER JOIN jeloltek ON jeloltek.id = projekt_jelolt_kapcs.jelolt_id " +
                "WHERE jeloltek.id = " + Session.ApplicantID + " " +
                "GROUP BY projektek.id";
            List<ModelSmallProject> list = ModelSmallProject.GetModelSmallProject(command);
            MySql.Close();
            return list;
        }

        public List<ModelSmallProject> Data_PorjectListSmall()  
        {
            string command = "SELECT projektek.id, megnevezes_projekt FROM projektek WHERE statusz = 1";
            List<ModelSmallProject> list = ModelSmallProject.GetModelSmallProject(command);
            MySql.Close();
            return list;
        }

        public void FirstOpen(int applicantId)  
        {
            string command = "UPDATE jeloltek SET friss = false WHERE id = " + applicantId + ";";
            MySql.Execute(command);
            MySql.Close();

            //try
            //{
            //    List<ModelFullApplicant> applicantData = GetFullApplicant();
            //    new Email().Send(applicantData[0].email, new EmailTemplate().Udvozlo_Email(applicantData[0].nev));
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        public void DeleteProject(int id)  
        {
            string command = "DELETE FROM projekt_jelolt_kapcs WHERE projekt_id = " + id + " AND jelolt_id = " + Session.ApplicantID + ";";
            MySql.Execute(command);
            MySql.Close();
        }
        
        public void AddToProject(int jelolt_index, int projekt_index)
        {
            string command = "SELECT * FROM projekt_jelolt_kapcs WHERE jelolt_id = " + jelolt_index + " AND projekt_id = "+projekt_index+"";
            if (!MySql.IsExists(command))
            {
                MySql.Close();
                command = "INSERT INTO projekt_jelolt_kapcs (id, projekt_id, jelolt_id, hr_id, datum) VALUES (NULL, " + projekt_index + ", " + jelolt_index + ", " + Session.UserData.id + ", '" + DateTime.Now.ToString("yyyy.MM.dd.") + "' );";
                MySql.Execute(command);
            }
            MySql.Close();
        }
    }
}

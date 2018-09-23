using System.Collections.Generic;
using HR_Portal.Source.Model;

namespace HR_Portal.Control
{
    class ControlSettings
    {
        Source.MySql mySql = new Source.MySql();

        public List<ModelErtesitendok> Data_Ertesitendok()
        {
            string command = "SELECT * FROM users WHERE kategoria = 0";
            List <ModelErtesitendok> list = mySql.getErtesitendok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelNyelv> Data_Nyelv()
        {
            string command = "SELECT * FROM nyelv";
            List <ModelNyelv> list = ModelNyelv.getModelNyelv(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelMunkakor> Data_Munkakorok()
        {
            string command = "SELECT * FROM munkakor";
            List <ModelMunkakor> list = mySql.getMunkakorok(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelPc> Data_Pc()
        {
            string command = "SELECT * FROM pc";
            List <ModelPc> list = mySql.getPc(command);
            Source.MySql.close();
            return list;
        }

        public List<ModelVegzettseg> Data_Vegzettseg()
        {
            string query = "SELECT * FROM vegzettsegek";
            List <ModelVegzettseg> list = mySql.Vegzettseg_MySql_listQuery(query);
            Source.MySql.close();
            return list;
        }

        public List<ModelErtesulesek> Data_Ertesulesek()
        {
            string command = "SELECT * FROM ertesulesek";
            List <ModelErtesulesek> list = ModelErtesulesek.getModelErtesulesek(command);
            Source.MySql.close();
            return list;
        }

        public void settingDelete(int id, string table)
        {
            string command = "DELETE FROM "+table+" WHERE id="+id+"";
            mySql.update(command);
            Source.MySql.close();
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
            mySql.update(command);
            Source.MySql.close();
        }
        //public void kompetenciaíró()
        //{
        //    string kompressed = "proaktivitás;önállóság;együttműködő képesség;kommunikációs képesség;releváns szakmai tapasztalat;rendszerben való gondolkodás;jó problémamegoldó képesség;jó kommunikációs képesség;nyitottság;rugalmasság;konfliktus kezelés;terhelhetőség;pontosság;kommunikációs német nyelvtudás;kommunikációs angol nyelvtudás;minőségközpontú szemlélet;lojalitás;precíz munkavégzés;monotónia tűrés;hatékony időgazdálkodás;magabiztos fellépés;jó kézügyesség;jó állóképesség;tanulási, fejlődési hajlandóság;többműszakos munkarend vállalása; analitikus gondolkodás; önálló döntéshozás;műszaki gondolkodás;projekt szemlélet;gyakorlatias személyiség;önálló, precíz személyiség;dinamikus személyiség;csapatmunka;";
        //    string[] s = kompressed.Split(';');
        //    foreach (var item in s)
        //    {
        //        string command = "INSERT INTO `kompetenciak` (`id`, `kompetencia_megnevezes`) VALUES (NULL, '" + item + "');";
        //        mySql.update(command);
        //    }
        //    MessageBox.Show("kész");
        //}
    }
}

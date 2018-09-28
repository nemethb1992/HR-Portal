using System.Collections.Generic;
using HR_Portal.Source.Model;
using HR_Portal.Source.Model.Project;

namespace HR_Portal.Source
{
    class ControlApplicant
    {
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

        public List<ModelComment> Data_Comment() //javított
        {
            string command = "SELECT id, jelolt_id, projekt_id, hr_id, hr_nev, megjegyzes, datum, ertekeles FROM megjegyzesek WHERE jelolt_id=" + Session.ApplicantID;
            List<ModelComment> list = ModelComment.GetModelComment(command);
            MySql.Close();
            return list;
        }




    }
}
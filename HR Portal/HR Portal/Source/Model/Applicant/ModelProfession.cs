using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    public class ModelProfession
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string reg_date { get; set; }
        public int projekt_id { get; set; }
        public string projekt { get; set; }

        public int vegzettseg { get; set; }
        public int nyelvtudas { get; set; }
        public int neme { get; set; }
        public int ertesult { get; set; }
        public string szuldatum { get; set; }
        public string lakhely { get; set; }


        public List<ModelProfession> Get(string command)
        {
            MySql mySql = new MySql();
            List<ModelProfession> list = new List<ModelProfession>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelProfession
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        name = mySql.sdr["nev"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        telephone = mySql.sdr["telefon"].ToString(),
                        reg_date = mySql.sdr["reg_date"].ToString(),
                        projekt = mySql.sdr["projekt"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }


}

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
        public string projekt { get; set; }

        public int vegzettseg { get; set; }
        public int nyelvtudas { get; set; }
        public int neme { get; set; }
        public int ertesult { get; set; }
        public string szuldatum { get; set; }
        public string lakhely { get; set; }


        public List<ModelProfession> Get(string command)
        {
            List<ModelProfession> list = new List<ModelProfession>();

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelProfession
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        name = MySql.sdr["nev"].ToString(),
                        email = MySql.sdr["email"].ToString(),
                        telephone = MySql.sdr["telefon"].ToString(),
                        reg_date = MySql.sdr["reg_date"].ToString(),
                        projekt = MySql.sdr["projekt"].ToString(),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }


}

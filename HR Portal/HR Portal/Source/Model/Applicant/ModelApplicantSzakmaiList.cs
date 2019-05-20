using HR_Portal.Source;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal_Test.Source.Model.Applicant
{
    class ModelApplicantSzakmaiList
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int state { get; set; }
        public string color { get; set; }
        public string label { get; set; }

        public List<ModelApplicantSzakmaiList> Get(string command)
        {
            List<ModelApplicantSzakmaiList> list = new List<ModelApplicantSzakmaiList>();

            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    string color = "White";
                    string label = "-";

                    if(Convert.ToInt32(mySql.sdr["state"]) == 1)
                    {
                        label = "Behívott";
                        color = "#e6ffe6";
                    }

                    list.Add(new ModelApplicantSzakmaiList
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        nev = mySql.sdr["nev"].ToString(),
                        state = (Convert.ToInt32(mySql.sdr["state"])),
                        color = color,
                        label = label
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}

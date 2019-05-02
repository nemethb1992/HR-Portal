using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Applicant
{
    public class ModelApplicantListbox
    {      
        public int id { get; set; }
        public string nev { get; set; }

        public static List<ModelApplicantListbox> GetModelApplicantListboxShort(string command)
        {
            List<ModelApplicantListbox> list = new List<ModelApplicantListbox>();
            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelApplicantListbox
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nev = mySql.sdr["nev"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}

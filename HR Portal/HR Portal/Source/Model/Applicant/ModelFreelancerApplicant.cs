using HR_Portal.Source;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal_Test.Source.Model.Applicant
{
    public class ModelFreelancerApplicant
    {
        public int id { get; set; }
        public string nev { get; set; }
        public int szuldatum { get; set; }
        public string email { get; set; }
        public int project_db { get; set; }
        public int kategoria { get; set; }
        public int bekuldo { get; set; }
        public string lakhely { get; set; }
        public string reg_datum { get; set; }
        public string freelancername { get; set; }
        
        public static List<ModelFreelancerApplicant> GetModelFreelancerApplicantList(string command)
        {
            List<ModelFreelancerApplicant> list = new List<ModelFreelancerApplicant>();
            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();

                while (mySql.sdr.Read())
                {
                    list.Add(new ModelFreelancerApplicant
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nev = mySql.sdr["nev"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        freelancername = mySql.sdr["freelancername"].ToString(),
                        lakhely = mySql.sdr["lakhely"].ToString(),
                        szuldatum = Convert.ToInt32(mySql.sdr["szuldatum"]),
                        project_db = Convert.ToInt32(mySql.sdr["project_db"]),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        bekuldo = Convert.ToInt32(mySql.sdr["bekuldo"]),
                        reg_datum = mySql.sdr["reg_date"].ToString()
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
}

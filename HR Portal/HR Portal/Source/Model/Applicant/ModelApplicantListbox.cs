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

            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelApplicantListbox
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        nev = MySql.sdr["nev"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
}

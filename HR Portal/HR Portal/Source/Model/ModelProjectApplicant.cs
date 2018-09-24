using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    //public class ModelProjectApplicantRelation
    //{
    //    public int projekt_id { get; set; }
    //    public int jelolt_id { get; set; }
    //    public int hr_id { get; set; }
    //    public string datum { get; set; }

    //    public static List<ModelProjectApplicantRelation> getModelProjectApplicantRelation(string command)
    //    {
    //        List<ModelProjectApplicantRelation> list = new List<ModelProjectApplicantRelation>();

    //        if (MySql.open() == true)
    //        {
    //            MySql.cmd = new MySqlCommand(command, MySql.conn);
    //            MySql.sdr = MySql.cmd.ExecuteReader();
    //            while (MySql.sdr.Read())
    //            {
    //                list.Add(new ModelProjectApplicantRelation
    //                {
    //                    projekt_id = Convert.ToInt32(MySql.sdr["projekt_id"]),
    //                    jelolt_id = Convert.ToInt32(MySql.sdr["jelolt_id"]),
    //                    hr_id = Convert.ToInt32(MySql.sdr["hr_id"]),
    //                    datum = MySql.sdr["datum"].ToString()
    //                });
    //            }
    //            MySql.sdr.Close();
    //        }
    //        return list;
    //    }
    //}


    //public class kompetencia_jelolt_kapcs_struct
    //{
    //    public int id { get; set; }
    //    public int interju_id { get; set; }
    //    public int projekt_id { get; set; }
    //    public int jelolt_id { get; set; }
    //    public int hr_id { get; set; }
    //    public int k1_id { get; set; }
    //    public int k1_val { get; set; }
    //    public int k2_id { get; set; }
    //    public int k2_val { get; set; }
    //    public int k3_id { get; set; }
    //    public int k3_val { get; set; }
    //    public int k4_id { get; set; }
    //    public int k4_val { get; set; }
    //    public int k5_id { get; set; }
    //    public int k5_val { get; set; }
    //    public int tamogatom { get; set; }


    //}
    


 
}

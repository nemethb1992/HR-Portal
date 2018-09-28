﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model.Project
{
    public class ModelKompetenciaSummary
    {
        public int k1_val { get; set; }
        public int k2_val { get; set; }
        public int k3_val { get; set; }
        public int k4_val { get; set; }
        public int k5_val { get; set; }
        public int tamogatom { get; set; }

        public static List<ModelKompetenciaSummary> GetModelKompetenciaSummary(string command)
        {
            List<ModelKompetenciaSummary> list = new List<ModelKompetenciaSummary>();

            if (MySql.Open() == true)
            {
                try
                {
                    MySql.cmd = new MySqlCommand(command, MySql.conn);
                    MySql.sdr = MySql.cmd.ExecuteReader();
                    while (MySql.sdr.Read())
                    {
                        list.Add(new ModelKompetenciaSummary
                        {
                            k1_val = Convert.ToInt32(MySql.sdr["k1_val"]),
                            k2_val = Convert.ToInt32(MySql.sdr["k2_val"]),
                            k3_val = Convert.ToInt32(MySql.sdr["k3_val"]),
                            k4_val = Convert.ToInt32(MySql.sdr["k4_val"]),
                            k5_val = Convert.ToInt32(MySql.sdr["k5_val"]),
                            tamogatom = Convert.ToInt32(MySql.sdr["tamogatom"]),
                        });
                    }
                }
                catch (Exception)
                {
                }

                MySql.sdr.Close();
            }
            return list;
        }
    }
}

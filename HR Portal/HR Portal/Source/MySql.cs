using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HR_Portal.Source.Model;
using System.Data;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Other;

namespace HR_Portal.Source
{
    public static class MySql
    {
        private const string CONNECTION_URL_1 = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        private const string CONNECTION_URL_2 = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmhrdemo; User ID=hr-admin; Password=pmhr2018";
        private const string CONNECTION_URL_3 = "Data Source = mysql.nethely.hu; Port=3306; Initial Catalog = hrcloudtest; User ID=hrcloudtest; Password=hrcloudtest2018";
        private const string CONNECTION_URL_4 = "Data Source = vpn.phoenix-mecano.hu; Port=29920; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";

        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        public static MySqlDataReader sdr;

        static MySql()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(CONNECTION_URL_1);
            }
        }
        
        public static bool IsConnected()
        {
            try
            {
                conn.Open();
            }
            catch
            {
                return false;
            }
                if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                return true;
            }
            return false;
        }

        public static bool Open()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Close()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public static void Update(string query)
        {
            if (Open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static int Count(string command)
        {
            int[] rows = new int[1];
            if (Open() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    rows[0] = Convert.ToInt32(sdr[0]);
                }
                sdr.Close();
            }
            
            return rows[0];
        }

        public static bool IsExists(string command)
        {
            int[] rows = new int[1];
            if (Open() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    rows[0] = Convert.ToInt32(sdr[0]);
                    if(rows.Length > 0)
                    {
                        sdr.Close();
                        return true;
                    }
                }
                sdr.Close();
            }

            return false;
        }

        public static List<string> UniqueList(string command, string table, int b)
        {
            List<string> dataSource = new List<string>();
            if (Open() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                int i;
                while (sdr.Read())
                {
                    for (i = 0; i < b; i++)
                    {
                        dataSource.Add(sdr[i].ToString());
                    }
                }
                sdr.Close();
            }
            return dataSource;
        }

        public static bool Bind(string query)
        {
            bool valid = false;
            if (Open() == true)
            {
                int seged = 0;
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    seged = Convert.ToInt32(sdr[0]);
                }
                sdr.Close();
                if (seged != 0)
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            return valid;
        }

        public static string GetRootUrl(string query)
        {
            string url = "";
            if (Open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    url = sdr["url"].ToString();
                    break;
                }
                sdr.Close();
            }
            return url;
        }

        public static List<Dictionary<string, string>> GetData(string command, string firstField, params string[] moreFields)
        {
            List<Dictionary<string, string>> li = new List<Dictionary<string, string>>();
            if (Open() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    row.Add(firstField, sdr[firstField].ToString());
                    foreach (var field in moreFields)
                    {
                        row.Add(field, sdr[field].ToString());
                    }
                    li.Add(row);
                }
    		}
    		return li;
        }
    }
}

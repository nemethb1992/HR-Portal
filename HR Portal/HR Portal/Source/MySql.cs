using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HR_Portal.Source.Model;
using System.Data;
using HR_Portal.Source.Model.Applicant;
using HR_Portal.Source.Model.Other;

namespace HR_Portal.Source
{
    public class MySql
    {
        //string connectionString = "Data Source = s7.nethely.hu; Initial Catalog = pmkcvtest; User ID=pmkcvtest; Password=pmkcvtest2018";
        //string connectionString = "Data Source = 192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";
        //string connectionString = "Data Source = vpn.phoenix-mecano.hu; Port=29920; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";

        private const string CONNECTION_URL = "Data Source =  192.168.144.189; Port=3306; Initial Catalog = pmkcvtest; User ID=hr-admin; Password=pmhr2018";

        public static MySqlConnection conn;
        public static MySqlCommand cmd;
        public static MySqlDataReader sdr;

        public MySql()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(CONNECTION_URL);
            }
        }
        
        public static bool isConnected()
        {
            try
            {
                conn.Open();
            }
            catch
            {
                return false;
            }
                if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
                return true;
            }
            return false;
        }

        public static bool open()
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

        public static bool close()
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

        public static void update(string query)
        {
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public int rowCount(string command)
        {
            int[] rows = new int[1];
            if (open() == true)
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
        public static bool isExists(string command)
        {
            int[] rows = new int[1];
            if (open() == true)
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

        public List<string> listQuery(string command, string table, int b)
        {
            List<string> dataSource = new List<string>();
            if (open() == true)
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

        public bool bind(string query)
        {
            bool valid = false;
            if (open() == true)
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

        public string getRootUrl(string query)
        {
            string url = "";
            if (open() == true)
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
    }
}

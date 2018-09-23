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

        private const string CONNECTION_URL = "Data Source = s7.nethely.hu; Initial Catalog = pmkcvtest; User ID=pmkcvtest; Password=pmkcvtest2018";

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

        public void update(string query)
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

        //MySQL  Specific 







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

        public List<ModelPc> getPc(string query)
        {
            List<ModelPc> items = new List<ModelPc>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelPc
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_pc = sdr["megnevezes_pc"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelVegzettseg> Vegzettseg_MySql_listQuery(string query)
        {
            List<ModelVegzettseg> items = new List<ModelVegzettseg>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelVegzettseg
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_vegzettseg = sdr["megnevezes_vegzettseg"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelStatusz> Statusz_MySql_listQuery(string query)
        {
            List<ModelStatusz> items = new List<ModelStatusz>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelStatusz
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        allapot = sdr["allapot"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }



        public List<ModelMunkakor> getMunkakorok(string query)
        {
            List<ModelMunkakor> items = new List<ModelMunkakor>();
            if (open() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelMunkakor
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        munkakor = sdr["megnevezes_munka"].ToString(),

                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelNem> Nem_MySql_listQuery(string query)
        {
            List<ModelNem> items = new List<ModelNem>();
            if (open() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelNem
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nem = sdr["nem"].ToString(),

                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelComment> Megjegyzesek_MySql_listQuery(string query)
        {
            List<ModelComment> items = new List<ModelComment>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelComment
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        jelolt_id = Convert.ToInt32(sdr["jelolt_id"]),
                        projekt_id = Convert.ToInt32(sdr["projekt_id"]),
                        hr_id = Convert.ToInt32(sdr["hr_id"]),
                        hr_nev = sdr["hr_nev"].ToString(),
                        megjegyzes = sdr["megjegyzes"].ToString(),
                        datum = sdr["datum"].ToString(),
                        ertekeles = Convert.ToInt32(sdr["ertekeles"]),

                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelErtesitendok> getErtesitendok(string query)
        {
            List<ModelErtesitendok> items = new List<ModelErtesitendok>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelErtesitendok
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        name = sdr["name"].ToString(),
                        email = sdr["email"].ToString(),

                    });
                }
                sdr.Close();
            }
            return items;
        }
        
        public List<ModelHr> getHrShort(string query)
        {
            List<ModelHr> items = new List<ModelHr>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelHr
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        name = sdr["name"].ToString(),
                        kategoria = Convert.ToInt32(sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(sdr["validitas"]),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelKoltsegek> Koltsegek_MySql_listQuery(string query)
        {
            List<ModelKoltsegek> items = new List<ModelKoltsegek>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelKoltsegek
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        koltseg_megnevezes = sdr["koltseg_megnevezes"].ToString(),
                        osszeg = Convert.ToInt32(sdr["osszeg"])

                    });
                }
                sdr.Close();
            }
            return items;
        }




        public List<ModelApplicantSearch> Jelolt_Search_MySql_listQuery(string query)
        {
            List<ModelApplicantSearch> items = new List<ModelApplicantSearch>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelApplicantSearch
                    {
                        nev = sdr["nev"].ToString(),
                        lakhely = sdr["lakhely"].ToString(),
                        email = sdr["email"].ToString(),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        tapasztalat = Convert.ToInt32(sdr["tapasztalat"]),
                        regdate = sdr["regdate"].ToString(),
                        neme = Convert.ToInt32(sdr["neme"]),
                        interju = Convert.ToInt32(sdr["interju"]),
                        munkakor = sdr["munkakor"].ToString(),
                        vegztettseg = sdr["vegztettseg"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelSzakmaiBevont> getSzakmaiProject(string query)
        {
            List<ModelSzakmaiBevont> items = new List<ModelSzakmaiBevont>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelSzakmaiBevont
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_projekt = sdr["megnevezes_projekt"].ToString(),
                        megnevezes_munka = sdr["megnevezes_munka"].ToString(),
                        jeloltek_db = Convert.ToInt32(sdr["jeloltek_db"]),

                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelKompetenciak> Kompetenciak_MySql_listQuery(string query)
        {
            List<ModelKompetenciak> items = new List<ModelKompetenciak>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelKompetenciak
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        kompetencia_megnevezes = sdr["kompetencia_megnevezes"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }





    }
}

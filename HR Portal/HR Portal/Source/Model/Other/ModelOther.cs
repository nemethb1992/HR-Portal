
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.Model
{
    public class ModelNyelv
    {
        public int id { get; set; }
        public string nyelv { get; set; }

        public static List<ModelNyelv> GetModelNyelv(string query)
        {
            List<ModelNyelv> items = new List<ModelNyelv>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(query, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    items.Add(new ModelNyelv
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        nyelv = MySql.sdr["megnevezes_nyelv"].ToString(),

                    });
                }
                MySql.sdr.Close();
            }
            return items;
        }
    }

    public class ModelErtesulesek
    {
        public int id { get; set; }
        public string ertesules_megnevezes { get; set; }

        public static List<ModelErtesulesek> GetModelErtesulesek(string command)
        {
            List<ModelErtesulesek> list = new List<ModelErtesulesek>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelErtesulesek
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        ertesules_megnevezes = MySql.sdr["ertesules_megnevezes"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelNem
    {
        public int id { get; set; }
        public string nem { get; set; }

        public static List<ModelNem> GetModelNem(string command)
        {
            List<ModelNem> list = new List<ModelNem>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelNem
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        nem = MySql.sdr["nem"].ToString(),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelMunkakor
    {
        public int id { get; set; }
        public string munkakor { get; set; }

        public static List<ModelMunkakor> GetModelMunkakor(string comannd)
        {
            List<ModelMunkakor> list = new List<ModelMunkakor>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(comannd, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelMunkakor
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        munkakor = MySql.sdr["megnevezes_munka"].ToString(),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelStatusz
    {
        public int id { get; set; }
        public string allapot { get; set; }

        public static List<ModelStatusz> GetModelStatusz(string command)
        {
            List<ModelStatusz> list = new List<ModelStatusz>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelStatusz
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        allapot = MySql.sdr["allapot"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelPc
    {
        public int id { get; set; }
        public string megnevezes_pc { get; set; }

        public static List<ModelPc> GetModelPc(string command)
        {
            List<ModelPc> list = new List<ModelPc>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelPc
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_pc = MySql.sdr["megnevezes_pc"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelVegzettseg
    {
        public int id { get; set; }
        public string megnevezes_vegzettseg { get; set; }

        public static List<ModelVegzettseg> GetModelVegzettseg(string command)
        {
            List<ModelVegzettseg> list = new List<ModelVegzettseg>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelVegzettseg
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        megnevezes_vegzettseg = MySql.sdr["megnevezes_vegzettseg"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelErtesitendok
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public static List<ModelErtesitendok> GetModelErtesitendok(string command)
        {
            List<ModelErtesitendok> list = new List<ModelErtesitendok>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelErtesitendok
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        name = MySql.sdr["name"].ToString(),
                        email = MySql.sdr["email"].ToString(),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelHr
    {
        public int id { get; set; }
        public string name { get; set; }
        public int kategoria { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }

        public static List<ModelHr> GetModelHr(string command)
        {
            List<ModelHr> list = new List<ModelHr>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelHr
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        name = MySql.sdr["name"].ToString(),
                        kategoria = Convert.ToInt32(MySql.sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(MySql.sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(MySql.sdr["validitas"]),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelComment
    {
        public int id { get; set; }
        public int jelolt_id { get; set; }
        public int projekt_id { get; set; }
        public int hr_id { get; set; }
        public string hr_nev { get; set; }
        public string megjegyzes { get; set; }
        public string datum { get; set; }
        public int ertekeles { get; set; }

        public static List<ModelComment> GetModelComment(string command)
        {
            List<ModelComment> list = new List<ModelComment>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelComment
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        jelolt_id = Convert.ToInt32(MySql.sdr["jelolt_id"]),
                        projekt_id = Convert.ToInt32(MySql.sdr["projekt_id"]),
                        hr_id = Convert.ToInt32(MySql.sdr["hr_id"]),
                        hr_nev = MySql.sdr["hr_nev"].ToString(),
                        megjegyzes = MySql.sdr["megjegyzes"].ToString(),
                        datum = MySql.sdr["datum"].ToString(),
                        ertekeles = Convert.ToInt32(MySql.sdr["ertekeles"]),

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelJeloltFile
    {
        public string fajlnev { get; set; }
        public string path { get; set; }
    }

    public class ModelKompetenciak
    {
        public int id { get; set; }
        public string kompetencia_megnevezes { get; set; }

        public static List<ModelKompetenciak> GetModelKompetenciak(string command)
        {
            List<ModelKompetenciak> list = new List<ModelKompetenciak>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelKompetenciak
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        kompetencia_megnevezes = MySql.sdr["kompetencia_megnevezes"].ToString(),
                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }

    public class ModelKoltsegek
    {
        public int id { get; set; }
        public int osszeg { get; set; }
        public string koltseg_megnevezes { get; set; }

        public static List<ModelKoltsegek> GetModelKoltsegek(string command)
        {
            List<ModelKoltsegek> list = new List<ModelKoltsegek>();
            if (MySql.Open() == true)
            {
                MySql.cmd = new MySqlCommand(command, MySql.conn);
                MySql.sdr = MySql.cmd.ExecuteReader();
                while (MySql.sdr.Read())
                {
                    list.Add(new ModelKoltsegek
                    {
                        id = Convert.ToInt32(MySql.sdr["id"]),
                        koltseg_megnevezes = MySql.sdr["koltseg_megnevezes"].ToString(),
                        osszeg = Convert.ToInt32(MySql.sdr["osszeg"])

                    });
                }
                MySql.sdr.Close();
            }
            return list;
        }
    }
    public class ModelId
    {
        public int id { get; set; }
    }
}

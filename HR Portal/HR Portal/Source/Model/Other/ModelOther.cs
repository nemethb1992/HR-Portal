
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace HR_Portal.Source.Model
{
    public class ModelList
    {
        public int id { get; set; }
        public object value { get; set; }
    }
    
        public class ModelNyelv
    {
        public int id { get; set; }
        public string nyelv { get; set; }

        public static List<ModelNyelv> GetModelNyelv(string query)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelNyelv> items = new List<ModelNyelv>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(query, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    items.Add(new ModelNyelv
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nyelv = mySql.sdr["megnevezes_nyelv"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return items;
        }
    }

    public class ModelErtesulesek
    {
        public int id { get; set; }
        public string ertesules_megnevezes { get; set; }

        public static List<ModelErtesulesek> GetModelErtesulesek(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelErtesulesek> list = new List<ModelErtesulesek>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelErtesulesek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        ertesules_megnevezes = mySql.sdr["ertesules_megnevezes"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelNem
    {
        public int id { get; set; }
        public string nem { get; set; }

        public static List<ModelNem> GetModelNem(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelNem> list = new List<ModelNem>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelNem
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        nem = mySql.sdr["nem"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelMunkakor
    {
        public int id { get; set; }
        public string munkakor { get; set; }

        public static List<ModelMunkakor> GetModelMunkakor(string comannd)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelMunkakor> list = new List<ModelMunkakor>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(comannd, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelMunkakor
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        munkakor = mySql.sdr["megnevezes_munka"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelStatusz
    {
        public int id { get; set; }
        public string allapot { get; set; }

        public static List<ModelStatusz> GetModelStatusz(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelStatusz> list = new List<ModelStatusz>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelStatusz
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        allapot = mySql.sdr["allapot"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelPc
    {
        public int id { get; set; }
        public string megnevezes_pc { get; set; }

        public static List<ModelPc> GetModelPc(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelPc> list = new List<ModelPc>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelPc
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_pc = mySql.sdr["megnevezes_pc"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelVegzettseg
    {
        public int id { get; set; }
        public string megnevezes_vegzettseg { get; set; }

        public static List<ModelVegzettseg> GetModelVegzettseg(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelVegzettseg> list = new List<ModelVegzettseg>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelVegzettseg
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        megnevezes_vegzettseg = mySql.sdr["megnevezes_vegzettseg"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelErtesitendok
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int kategoria { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }

        public static List<ModelErtesitendok> GetModelErtesitendok(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelErtesitendok> list = new List<ModelErtesitendok>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelErtesitendok
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        name = mySql.sdr["name"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(mySql.sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(mySql.sdr["validitas"]),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    //public class ModelHr
    //{
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public int kategoria { get; set; }
    //    public int jogosultsag { get; set; }
    //    public int validitas { get; set; }

    //    public static List<ModelHr> GetModelHr(string command)
    //    {
    //        List<ModelHr> list = new List<ModelHr>();
    //        if (mySql.Open() == true)
    //        {
    //            mySql.cmd = new MySqlCommand(command, mySql.conn);
    //            mySql.sdr = mySql.cmd.ExecuteReader();
    //            while (mySql.sdr.Read())
    //            {
    //                list.Add(new ModelHr
    //                {
    //                    id = Convert.ToInt32(mySql.sdr["id"]),
    //                    name = mySql.sdr["name"].ToString(),
    //                    kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
    //                    jogosultsag = Convert.ToInt32(mySql.sdr["jogosultsag"]),
    //                    validitas = Convert.ToInt32(mySql.sdr["validitas"]),
    //                });
    //            }
    //            mySql.sdr.Close();
    //        }
    //        return list;
    //    }
    //}

    public class ModelComment
    {
        public int id { get; set; }
        public int jelolt_id { get; set; }
        public int projekt_id { get; set; }
        public int hr_id { get; set; }
        public string hr_nev { get; set; }
        public string megjegyzes { get; set; }
        public string datum { get; set; }

        public static List<ModelComment> GetModelComment(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelComment> list = new List<ModelComment>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelComment
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        jelolt_id = Convert.ToInt32(mySql.sdr["jelolt_id"]),
                        projekt_id = Convert.ToInt32(mySql.sdr["projekt_id"]),
                        hr_id = Convert.ToInt32(mySql.sdr["hr_id"]),
                        hr_nev = mySql.sdr["hr_nev"].ToString(),
                        megjegyzes = mySql.sdr["megjegyzes"].ToString(),
                        datum = mySql.sdr["datum"].ToString(),

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }

    public class ModelFile
    {
        public string fajlnev { get; set; }
        public string color { get; set; }
        public string path { get; set; }
    }
    public class ModelStatList
    {
        public string type { get; set; }
        public string title { get; set; }
    }

    public class ModelKompetenciak
    {
        public int id { get; set; }
        public string kompetencia_megnevezes { get; set; }

        public static List<ModelKompetenciak> GetModelKompetenciak(string command)
        {
            MySqlDB mySql = new MySqlDB();
            List<ModelKompetenciak> list = new List<ModelKompetenciak>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelKompetenciak
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        kompetencia_megnevezes = mySql.sdr["kompetencia_megnevezes"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
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
            MySqlDB mySql = new MySqlDB();
            List<ModelKoltsegek> list = new List<ModelKoltsegek>();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelKoltsegek
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        koltseg_megnevezes = mySql.sdr["koltseg_megnevezes"].ToString(),
                        osszeg = Convert.ToInt32(mySql.sdr["osszeg"])

                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
    }
    public class ModelId
    {
        public int id { get; set; }
    }
}

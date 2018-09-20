using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using static HR_Portal.Source.ModelEmail;
using static HR_Portal.Source.ModelSzakmai;
using HR_Portal.Source.Model;

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

        //Initialize values
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
            if(conn.State == System.Data.ConnectionState.Open)
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
                conn.Open();
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
        //MySqlConnection conn = new MySqlConnection(dataSourceURL);

        // MySQL entites

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

        //public object[,] MysqlReaderExecute(string command, string table, int b)
        //{
        //    int a = rowCount("SELECT count(id) FROM " + table + "");
        //    object[,] dataSource = new object[a, b];
        //    if (this.open() == true)
        //    {

        //        cmd = new MySqlCommand(command, conn);
        //        sdr = cmd.ExecuteReader();
        //        int i, j = 0;
        //        while (sdr.Read())
        //        {
        //            for (i = 0; i < b; i++)
        //            {
        //                dataSource[j, i] = sdr[i];
        //            }
        //            j++;
        //        }
        //        sdr.Close();
        //    }
        //    return dataSource;
        //}

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



        public List<SmallProjectListItems> Small_Projekt_MySql_listQuery(string command)
        {
            List<SmallProjectListItems> items = new List<SmallProjectListItems>();
            if (open() == true)
            {
                cmd = new MySqlCommand(command, conn);
                sdr = cmd.ExecuteReader();
                int j = 0;
                while (sdr.Read())
                {
                    items.Add(new SmallProjectListItems
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_projekt = sdr["megnevezes_projekt"].ToString(),
                        jeloltek_db = 0
                    });
                    j++;
                }
                sdr.Close();
            }
            return items;
        }

        public List<SubProjekt> Sub_Projekt_MySql_listQuery(string query)
        {
            List<SubProjekt> items = new List<SubProjekt>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                int j = 0;
                while (sdr.Read())
                {
                    items.Add(new SubProjekt
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_projekt = sdr["megnevezes_projekt"].ToString()
                    });
                    j++;
                }
                sdr.Close();
            }
            return items;
        }

        public List<ProjectListItems> Projekt_MySql_listQuery  (string query)
        {
        List<ProjectListItems> items = new List<ProjectListItems>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    int jelolt;
                    try
                    {
                        jelolt = Convert.ToInt32(sdr["jeloltek_db"]);
                    }
                    catch (Exception)
                    {
                        jelolt = 0;
                    }
                    items.Add(new ProjectListItems
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        megnevezes_projekt = sdr["megnevezes_projekt"].ToString(),
                        megnevezes_munka = sdr["megnevezes_munka"].ToString(),
                        interjuk_db = Convert.ToInt32(sdr["interjuk_db"]),
                        statusz = Convert.ToInt32(sdr["statusz"]),
                        jeloltek_db = jelolt,
                        fel_datum = sdr["fel_datum"].ToString(),
                        Completion = 100
                    });


                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelErtesulesek> Ertesulesek_MySql_listQuery(string query)
        {
            List<ModelErtesulesek> items = new List<ModelErtesulesek>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelErtesulesek
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        ertesules_megnevezes = sdr["ertesules_megnevezes"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<file_url> file_url_ROOT_MySql_listQuery(string query)
        {
            List<file_url> items = new List<file_url>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new file_url
                    {
                        url = sdr["url"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
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

        public List<ProjectExtendedListItems> Projekt_Extended_MySql_listQuery(string query)
        {
            List<ProjectExtendedListItems> items = new List<ProjectExtendedListItems>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    int jelolt;
                    try
                    {
                        jelolt = Convert.ToInt32(sdr["jeloltek_db"]);
                    }
                    catch (Exception)
                    {
                        jelolt = 0;
                    }
                    items.Add(new ProjectExtendedListItems
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        hr_id = Convert.ToInt32(sdr["hr_id"]),
                        megnevezes_projekt = sdr["megnevezes_projekt"].ToString(),
                        megnevezes_vegzettseg = sdr["megnevezes_vegzettseg"].ToString(),
                        megnevezes_nyelv = sdr["megnevezes_nyelv"].ToString(),
                        megnevezes_munka = sdr["megnevezes_munka"].ToString(),
                        megnevezes_pc = sdr["megnevezes_pc"].ToString(),
                        megnevezes_hr = sdr["name"].ToString(),
                        fel_datum = sdr["fel_datum"].ToString(),
                        le_datum = sdr["le_datum"].ToString(),
                        pc = Convert.ToInt32(sdr["pc"]),
                        vegzettseg = Convert.ToInt32(sdr["vegzettseg"]),
                        tapasztalat_ev = Convert.ToInt32(sdr["tapasztalat_ev"]),
                        statusz = sdr["allapot"].ToString(),
                        publikalt = Convert.ToInt32(sdr["publikalt"]),
                        nyelvtudas = Convert.ToInt32(sdr["nyelvtudas"]),
                        munkakor = Convert.ToInt32(sdr["munkakor"]),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        ber = Convert.ToInt32(sdr["ber"]),
                        jeloltek_db = jelolt,
                        kepesseg1 = Convert.ToInt32(sdr["kepesseg1"]),
                        kepesseg2 = Convert.ToInt32(sdr["kepesseg2"]),
                        kepesseg3 = Convert.ToInt32(sdr["kepesseg3"]),
                        kepesseg4 = Convert.ToInt32(sdr["kepesseg4"]),
                        kepesseg5 = Convert.ToInt32(sdr["kepesseg5"]),
                        feladatok = sdr["feladatok"].ToString(),
                        elvarasok = sdr["elvarasok"].ToString(),
                        kinalunk = sdr["kinalunk"].ToString(),
                        elonyok = sdr["elonyok"].ToString(),
                    });

                }
                sdr.Close();
            }
            return items;
        }

        public List<SubJelolt> Jelolt_Short_MySql_listQuery(string query)
        {
            List<SubJelolt> items = new List<SubJelolt>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new SubJelolt
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<JeloltListItems> getApplicantList(string query)
        {
            List<JeloltListItems> items = new List<JeloltListItems>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    string allapot_megnev = "Beérkezett", kolcsonzott = "";
                    int allapot = 0;
                    try
                    {
                        allapot = Convert.ToInt32(sdr["allapota"]);
                    }
                    catch (Exception)
                    {
                    }
                    switch (allapot)
                    {
                        case 1:
                            allapot_megnev = "Telefonon szűrt";
                            break;
                        case 2:
                            allapot_megnev = "Felvett";
                            break;
                        case 3:
                            allapot_megnev = "Elutasított";
                            break;

                        default:
                            allapot_megnev = "Beérkezett";
                            break;
                    }

                    if (Convert.ToInt32(sdr["kolcsonzott"]) == 1)
                        kolcsonzott = "Kölcsönzött";
                    items.Add(new JeloltListItems
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                        munkakor = sdr["munkakor"].ToString(),
                        munkakor2 = sdr["munkakor2"].ToString(),
                        munkakor3 = sdr["munkakor3"].ToString(),
                        email = sdr["email"].ToString(),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        interjuk_db = Convert.ToInt32(sdr["interjuk_db"]),
                        allapota = allapot,
                        kolcsonzott = kolcsonzott,
                        allapot_megnevezes = allapot_megnev,
                        reg_datum = sdr["reg_date"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<SubJelolt> getApplicantShort(string query)
        {
            List<SubJelolt> items = new List<SubJelolt>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {

                    items.Add(new SubJelolt
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<JeloltExtendedList> JeloltExtended_MySql_listQuery(string query)
        {
            List<JeloltExtendedList> items = new List<JeloltExtendedList>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new JeloltExtendedList
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                        email = sdr["email"].ToString(),
                        telefon = sdr["telefon"].ToString(),
                        lakhely = sdr["lakhely"].ToString(),
                        ertesult = sdr["ertesules_megnevezes"].ToString(),
                        id_ertesult = Convert.ToInt32(sdr["id_ertesult"]),
                        szuldatum = Convert.ToInt32(sdr["szuldatum"]),
                        neme = sdr["neme"].ToString(),
                        id_neme = Convert.ToInt32(sdr["id_neme"]),
                        tapasztalat_ev = Convert.ToInt32(sdr["tapasztalat_ev"]),
                        munkakor = sdr["munkakor"].ToString(),
                        munkakor2 = sdr["munkakor2"].ToString(),
                        munkakor3 = sdr["munkakor3"].ToString(),
                        id_munkakor = Convert.ToInt32(sdr["id_munkakor"]),
                        id_munkakor2 = Convert.ToInt32(sdr["id_munkakor2"]),
                        id_munkakor3 = Convert.ToInt32(sdr["id_munkakor3"]),
                        vegz_terulet = sdr["vegz_terulet"].ToString(),
                        id_vegz_terulet = Convert.ToInt32(sdr["id_vegz_terulet"]),
                        nyelvtudas = sdr["nyelvtudas"].ToString(),
                        nyelvtudas2 = sdr["nyelvtudas2"].ToString(),
                        id_nyelvtudas = Convert.ToInt32(sdr["id_nyelvtudas"]),
                        id_nyelvtudas2 = Convert.ToInt32(sdr["id_nyelvtudas2"]),
                        reg_date = sdr["reg_date"].ToString(),
                        megjegyzes = sdr["megjegyzes"].ToString(),
                        folderUrl = sdr["folderUrl"].ToString(),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<JeloltListBox> JeloltekDatasourceListbox_MySql_listQuery(string query)
        {
            List<JeloltListBox> items = new List<JeloltListBox>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new JeloltListBox
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nev = sdr["nev"].ToString(),
                        interjuk_db = Convert.ToInt32(sdr["interjuk_db"]),

                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<ModelNyelv> getNyelv(string query)
        {
            List<ModelNyelv> items = new List<ModelNyelv>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new ModelNyelv
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        nyelv = sdr["megnevezes_nyelv"].ToString(),

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

        //public List<csatolmany_struct> Csatolmany_MySql_listQuery(string query)
        //{
        //    List<csatolmany_struct> items = new List<csatolmany_struct>();
        //    if (this.dbOpen() == true)
        //    {
        //        cmd = new MySqlCommand(query, conn);
        //        sdr = cmd.ExecuteReader();
        //        while (sdr.Read())
        //        {
        //            items.Add(new csatolmany_struct
        //            {
        //                id = Convert.ToInt32(sdr["id"]),
        //                kapcs_id = Convert.ToInt32(sdr["kapcs_id"]),
        //                tipus = Convert.ToInt32(sdr["tipus"]),
        //                fajlnev = sdr["fajlnev"].ToString().Split('.')[0],
        //                kiterjesztes = sdr["fajlnev"].ToString().Split('.')[1],
        //                kep = "/HRCloud;component/Public/imgs/" + sdr["kiterjesztes"].ToString() + ".png",
        //            });
        //        }
        //        sdr.Close();
        //    }
        //    return items;
        //}

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

        public List<projekt_jelolt_kapcs> getPojectApplicantRelation(string query)
        {
            List<projekt_jelolt_kapcs> items = new List<projekt_jelolt_kapcs>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new projekt_jelolt_kapcs
                    {
                        projekt_id = Convert.ToInt32(sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(sdr["jelolt_id"]),
                        hr_id = Convert.ToInt32(sdr["hr_id"]),
                        datum = sdr["datum"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<interju_struct> Interju_MySql_listQuery(string query)
        {
            List<interju_struct> items = new List<interju_struct>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new interju_struct
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        projekt_megnevezes = sdr["megnevezes_projekt"].ToString(),
                        jelolt_megnevezes = sdr["nev"].ToString(),
                        jelolt_email = sdr["email"].ToString(),
                        projekt_id = Convert.ToInt32(sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(sdr["jelolt_id"]),
                        hr_id = Convert.ToInt32(sdr["hr_id"]),
                        felvitel_datum = sdr["felvitel_datum"].ToString(),
                        interju_datum = sdr["interju_datum"].ToString(),
                        interju_cim = sdr["interju_cim"].ToString(),
                        interju_leiras = sdr["interju_leiras"].ToString(),
                        helyszin = sdr["helyszin"].ToString(),
                        idopont = sdr["idopont"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<interju_struct> getSzakmaiInterview(string query)
        {
            List<interju_struct> items = new List<interju_struct>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new interju_struct
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        projekt_megnevezes = sdr["megnevezes_projekt"].ToString(),
                        jelolt_megnevezes = sdr["nev"].ToString(),
                        projekt_id = Convert.ToInt32(sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(sdr["jelolt_id"]),
                        interju_datum = sdr["interju_datum"].ToString(),
                        interju_cim = sdr["interju_cim"].ToString(),
                        helyszin = sdr["helyszin"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<JeloltSearchItems> Jelolt_Search_MySql_listQuery(string query)
        {
            List<JeloltSearchItems> items = new List<JeloltSearchItems>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new JeloltSearchItems
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

        public List<Projekt_Bevont_struct> getSzakmaiProject(string query)
        {
            List<Projekt_Bevont_struct> items = new List<Projekt_Bevont_struct>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new Projekt_Bevont_struct
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

        public List<kompetencia_jelolt_kapcs_struct> Kompetenciak_jelolt_kapcs_MySql_listQuery(string query)
        {
            List<kompetencia_jelolt_kapcs_struct> items = new List<kompetencia_jelolt_kapcs_struct>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    items.Add(new kompetencia_jelolt_kapcs_struct
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        interju_id = Convert.ToInt32(sdr["interju_id"]),
                        projekt_id = Convert.ToInt32(sdr["projekt_id"]),
                        jelolt_id = Convert.ToInt32(sdr["jelolt_id"]),
                        hr_id = Convert.ToInt32(sdr["hr_id"]),
                        k1_id = Convert.ToInt32(sdr["k1_id"]),
                        k1_val = Convert.ToInt32(sdr["k1_val"]),
                        k2_id = Convert.ToInt32(sdr["k2_id"]),
                        k2_val = Convert.ToInt32(sdr["k2_val"]),
                        k3_id = Convert.ToInt32(sdr["k3_id"]),
                        k3_val = Convert.ToInt32(sdr["k3_val"]),
                        k4_id = Convert.ToInt32(sdr["k4_id"]),
                        k4_val = Convert.ToInt32(sdr["k4_val"]),
                        k5_id = Convert.ToInt32(sdr["k5_id"]),
                        k5_val = Convert.ToInt32(sdr["k5_val"]),
                        tamogatom = Convert.ToInt32(sdr["tamogatom"]),
                    });
                }
                sdr.Close();
            }
            return items;
        }

        public List<kompetencia_summary_struct> Kompetencia_summary_MySql_listQuery(string query)
        {
            List<kompetencia_summary_struct> items = new List<kompetencia_summary_struct>();
            if (open() == true)
            {
                try
                {
                    cmd = new MySqlCommand(query, conn);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        items.Add(new kompetencia_summary_struct
                        {
                            k1_val = Convert.ToInt32(sdr["k1_val"]),
                            k2_val = Convert.ToInt32(sdr["k2_val"]),
                            k3_val = Convert.ToInt32(sdr["k3_val"]),
                            k4_val = Convert.ToInt32(sdr["k4_val"]),
                            k5_val = Convert.ToInt32(sdr["k5_val"]),
                            tamogatom = Convert.ToInt32(sdr["tamogatom"]),
                        });
                    }
                }
                catch (Exception)
                {
                }

                sdr.Close();
            }
            return items;
        }

        public List<kompetencia_tamogatas> Kompetencia_tamogatas_MySql_listQuery(string query)
        {
            List<kompetencia_tamogatas> items = new List<kompetencia_tamogatas>();
            if (open() == true)
            {
                try
                {
                    cmd = new MySqlCommand(query, conn);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        items.Add(new kompetencia_tamogatas
                        {
                            tamogatom = Convert.ToInt32(sdr["tamogatom"]),
                        });
                    }
                }
                catch (Exception)
                {
                }

                sdr.Close();
            }
            return items;
        }

        public List<MailServer_m> ConnectionSMTP_DataSource(string query)
        {
            List<MailServer_m> items = new List<MailServer_m>();
            if (open() == true)
            {
                cmd = new MySqlCommand(query, conn);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    bool ssl = true;
                    if (Convert.ToInt32(sdr["ssl"]) == 0)
                    {
                        ssl = false;
                    }
                    items.Add(new MailServer_m
                    {
                        type = sdr["type"].ToString(),
                        mailserver = sdr["mailserver"].ToString(),
                        port = Convert.ToInt32(sdr["port"]),
                        ssl = ssl,
                        login = sdr["login"].ToString()
                    });
                }
                sdr.Close();
            }
            return items;
        }
    }
}

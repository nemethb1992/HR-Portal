using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.Model
{
    public class ModelUserData
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int kategoria { get; set; }
        public string kategoriaMegnevezes { get; set; }
        public int jogosultsag { get; set; }
        public int validitas { get; set; }
        public bool validitasBool { get; set; }
        public string belepve { get; set; }
        public string reg_datum { get; set; }

        public enum Kategoria { HR, Szakmai, Admin };

        public static List<ModelUserData> GetUserSession(string command)
        {
            MySql mySql = new MySql();
            List<ModelUserData> list = new List<ModelUserData>();

            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    string kategoriaSeged;
                    switch (Convert.ToInt32(mySql.sdr["kategoria"]))
                    {
                        case 2:
                            kategoriaSeged = Kategoria.Admin.ToString();
                            break;
                        case 1:
                            kategoriaSeged = Kategoria.HR.ToString();
                            break;
                        case 0:
                        default:
                            kategoriaSeged = Kategoria.Szakmai.ToString();
                            break;

                    }
                    list.Add(new ModelUserData
                    {
                        id = Convert.ToInt32(mySql.sdr["id"]),
                        username = mySql.sdr["username"].ToString(),
                        name = mySql.sdr["name"].ToString(),
                        email = mySql.sdr["email"].ToString(),
                        kategoria = Convert.ToInt32(mySql.sdr["kategoria"]),
                        kategoriaMegnevezes = kategoriaSeged,
                        jogosultsag = Convert.ToInt32(mySql.sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(mySql.sdr["validitas"]),
                        validitasBool = Convert.ToBoolean(mySql.sdr["validitas"]),
                        belepve = mySql.sdr["belepve"].ToString(),
                        reg_datum = mySql.sdr["reg_datum"].ToString(),
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();

            return list;
        }
    }
}

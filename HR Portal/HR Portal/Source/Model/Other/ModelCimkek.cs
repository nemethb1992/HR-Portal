using HR_Portal.Source;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class ModelCimkek
    {
        public int id { get; set; }
        public string cimke_megnevezes { get; set; }

        public List<ModelCimkek> GetAll()
        {
            List<ModelCimkek> list = new List<ModelCimkek>();

            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand("SELECT * FROM jelolt_cimkek", mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelCimkek
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        cimke_megnevezes = mySql.sdr["cimke_megnevezes"].ToString()
                    });
                    j++;
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }

        public List<ModelCimkek> GetRelated(int id)
        {
            List<ModelCimkek> list = new List<ModelCimkek>();

            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand("SELECT jelolt_cimkek.id, jelolt_cimkek.cimke_megnevezes FROM jelolt_cimke_kapcs INNER JOIN jelolt_cimkek ON jelolt_cimke_kapcs.cimke_id = jelolt_cimkek.id WHERE jelolt_cimke_kapcs.jelolt_id = " + id+"", mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelCimkek
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        cimke_megnevezes = mySql.sdr["cimke_megnevezes"].ToString()
                    });
                    j++;
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }
        public List<ModelCimkek> GetSearched(string word)
        {
            List<ModelCimkek> list = new List<ModelCimkek>();

            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand("SELECT * FROM jelolt_cimkek WHERE cimke_megnevezes LIKE '%" +word + "%' ", mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                int j = 0;
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelCimkek
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        cimke_megnevezes = mySql.sdr["cimke_megnevezes"].ToString()
                    });
                    j++;
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }

        public void Insert(string megnezeves) //javított
        {
            MySqlDB mySql = new MySqlDB();
            if (!mySql.IsExists("SELECT * FROM jelolt_cimkek WHERE cimke_megnevezes = '" + megnezeves + "'"))
            {
                string command = "INSERT INTO jelolt_cimkek (cimke_megnevezes) VALUES('" + megnezeves + "');";
                mySql.Execute(command);
            }
            mySql.Close();
        }

        public void Delete(int id) //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM jelolt_cimkek WHERE id = "+id+";";
            mySql.Execute(command);
            mySql.Close();
        }

        public void Update(int id, string megnevezes) //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE jelolt_cimkek SET cimkek_megnevezes = '"+megnevezes+"' WHERE id = " + id + ";";
            mySql.Execute(command);
            mySql.Close();
        }
        public void AddRelation(int jelolt_id, int cimke_id) //javított
        {
            MySqlDB mySql = new MySqlDB();
            if(!mySql.IsExists("SELECT * FROM jelolt_cimke_kapcs WHERE jelolt_id = "+ jelolt_id + " AND cimke_id = "+ cimke_id + ""))
            {
                string command = "INSERT INTO jelolt_cimke_kapcs (jelolt_id, cimke_id) VALUES(" + jelolt_id + ", " + cimke_id + ");";
                mySql.Execute(command);
            }
            mySql.Close();
        }

        public void DeleteRelation(int jelolt_id, int cimke_id) //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM jelolt_cimke_kapcs WHERE jelolt_id = " + jelolt_id + " AND cimke_id = " + cimke_id + "";
            mySql.Execute(command);
            mySql.Close();
        }

    }
}

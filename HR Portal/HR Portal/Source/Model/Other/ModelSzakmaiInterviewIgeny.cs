using HR_Portal.Source;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal_Test.Source.Model.Other
{
    class ModelSzakmaiInterviewIgeny
    {
        public int id { get; set; }
        public int jelolt_id { get; set; }
        public int user_id { get; set; }
        public int projekt_id { get; set; }
        public int state { get; set; }

        public List<ModelSzakmaiInterviewIgeny> Get(string command)
        {
            List<ModelSzakmaiInterviewIgeny> list = new List<ModelSzakmaiInterviewIgeny>();

            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    list.Add(new ModelSzakmaiInterviewIgeny
                    {
                        id = (Convert.ToInt32(mySql.sdr["id"])),
                        jelolt_id = (Convert.ToInt32(mySql.sdr["jelolt_id"])),
                        user_id = (Convert.ToInt32(mySql.sdr["user_id"])),
                        projekt_id = (Convert.ToInt32(mySql.sdr["projekt_id"])),
                        state = (Convert.ToInt32(mySql.sdr["state"]))
                    });
                }
                mySql.sdr.Close();
            }
            mySql.Close();
            return list;
        }

        public void Insert(ModelSzakmaiInterviewIgeny data) //javított
        {
            MySqlDB mySql = new MySqlDB();
            if (!mySql.IsExists("SELECT * FROM jelolt_allapot_szakmai WHERE user_id = " + Session.UserData.id + " AND projekt_id = "+data.projekt_id+" AND jelolt_id = "+data.jelolt_id+""))
            {
                string command = "INSERT INTO jelolt_allapot_szakmai (jelolt_id, user_id, projekt_id, state) VALUES("+data.jelolt_id+","+ data.user_id+ ","+ data.projekt_id+ ","+ data.state+ ");";
                mySql.Execute(command);
            }
            mySql.Close();
        }

        public void Delete(int id) //javított
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM jelolt_allapot_szakmai WHERE id = " + id + ";";
            mySql.Execute(command);
            mySql.Close();
        }
    }
}

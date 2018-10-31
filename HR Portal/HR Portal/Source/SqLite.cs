using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class SqLite
    {
        private const string CONNECTION_URL = "Data Source = innerDatabase.db";

        protected static SQLiteConnection conn;

        static SqLite()
        {
            conn = new SQLiteConnection(CONNECTION_URL);
        }

        protected static void Open()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        protected static void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public static void Update(string query)
        {
            var command = conn.CreateCommand();
            Open();
            command.CommandText = query;
            command.ExecuteNonQuery();
            Close();
        }

        public static string Query(string query)
        {
            Open();
            var command = conn.CreateCommand();
            command.CommandText = query;
            SQLiteDataReader sdr = command.ExecuteReader();
            string data = "";
            while (sdr.Read())
            {
                data = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            Close();
            return data;
        }
    }
}

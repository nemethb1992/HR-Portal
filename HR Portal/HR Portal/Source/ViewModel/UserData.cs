using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HR_Portal.Source.Model;

namespace HR_Portal.Source

{
    public class VMUserData
    {
        public static List<ModelUserData> Get()
        {
            List<ModelUserData> list = new List<ModelUserData>();
            string username = Session.ActiveDirectoryDomain;
            if (username.Length>0)
            {
                list = ModelUserData.GetUserSession("SELECT* FROM users WHERE username = '" + username + "'");
            }
            return list;
        }
    }
}

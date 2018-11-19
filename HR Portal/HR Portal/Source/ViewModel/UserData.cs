using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using HR_Portal.Source.Model;

namespace HR_Portal.Source

{
    public class UserData
    {
        public static List<ModelUserData> GetOwnDatas()
        {
            List<ModelUserData> list = new List<ModelUserData>();
            string username = Session.ActiveDirectoryDomain;
            if (username.Length>0)
            {
                list = ModelUserData.GetUserSession("SELECT * FROM users WHERE username = '" + username + "'");
            }
            return list;
        }

        public static List<ModelUserData> GetById(int userId)
        {
            return ModelUserData.GetUserSession("SELECT * FROM users WHERE id = '" + userId + "'"); ;
        }

        ///<summary>
        ///Visszaadja az összes felhasználót.
        ///List<ModelUserData>
        ///</summary>
        public static List<ModelUserData> GetAll()
        {
            List<ModelUserData> list = new List<ModelUserData>();
            string username = Session.ActiveDirectoryDomain;
            if (username.Length > 0)
            { 
                list = ModelUserData.GetUserSession("SELECT * FROM users");
            }
            return list;
        }
        ///<summary>
        ///Felhasználó id alapján megváltoztatja a hozzáférés jogát.
        ///</summary>
        public static void ModifyValidation(int id, bool valid)
        {
            string seged;
            if (valid)
                seged = "1";
            else
                seged = "0";
            MySql.Execute("UPDATE users SET validitas = '" + seged + "' WHERE users.id = '" + id + "';");
            MySql.Close();
        }

        ///<summary>
        ///Felhasználó id alapján megváltoztatja a hozzáférés típusát.
        ///- Hr felhasználó '1'
        ///- Szakmai felhasználó '0'
        ///</summary>
        public static void ModifyType(int id, int type)
        {
            MySql.Execute("UPDATE users SET kategoria = '" + type + "' WHERE users.id = '" + id + "';");
            MySql.Close();
        }

        public static void Modify(int id, string mezo, string value)
        {
            MySql.Execute("UPDATE users SET "+mezo+" = '" + value + "' WHERE users.id = '" + id + "';");
            MySql.Close();
        }

        ///<summary>
        ///Megmondja a felhasználóról hogy admin-e.
        ///</summary>
        public static bool IsAdmin(int applicantId)
        {
            bool value = MySql.Bind("SELECT * FROM users WHERE users.id = '" + applicantId + "' AND admin = 1;");
            MySql.Close();
            return value;
        }
    }
}

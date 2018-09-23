﻿
using System;
using System.Collections.Generic;
using System.Net;
using System.DirectoryServices.Protocols;
using MySql.Data.MySqlClient;
using HR_Portal.Source;
using HR_Portal.Source.Model;

namespace HR_Portal.Control
{
    class ControlLogin
    {
        Source.MySql mySql = new Source.MySql();
        SqLite sqLite = new SqLite();

        public bool userValidation(string name, string pass)
        {
            if (mySql.rowCount("SELECT count(id) FROM users WHERE username='" + name + "'") == 1)
            {
                DateTime dateTime = DateTime.Now;
                sqLite.update("UPDATE users SET belepve = '" + dateTime.ToString("yyyy. MM. dd.") + "' WHERE username = '" + name + "';");
                Source.MySql.close();
                return true;
            }
            else{
                Source.MySql.close();
            }
            return false;
        }
        public string getRememberedUser()
        {
            string user;
            try
            {
                user = sqLite.query("select username from app");
            }
            catch (Exception)
            {
                sqLite.update("CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);");
                user = sqLite.query("SELECT 'username' FROM 'app';");
            }
            Source.MySql.close();
            return user;
        }
        public void writeRememberedUser(string username) //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
            sqLite.update("INSERT INTO 'app' (username) VALUES ('" + username + "');");
            Source.MySql.close();
        }
        public void deleteRememberedUser() //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
            Source.MySql.close();
        }
        public bool mySqlUserValidation(string user) //javítva használja: login
        {
            bool respond = mySql.bind("SELECT count(id) FROM users WHERE username='" + user + "'");
            Source.MySql.close();
            return respond;
        }

        public void userRegistration(string username, string name, string email, int kategoria)
        {
            DateTime dateTime = DateTime.Now;
            mySql.update("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '"+ username + "', '"+ name + "', '"+ email + "', '"+ kategoria + "', '1', '1', '" + dateTime.ToString("yyyy. MM. dd.") + "', '" + dateTime.ToString("yyyy. MM. dd.") + "');");
            Source.MySql.close();
        }

        //UserSessionData   általános
        public List<ModelUserData> Data_UserSession(string username)  //javítva használja: login
        {
            MySqlDataReader sdr;
            List<ModelUserData> list = new List<ModelUserData>();
            if (Source.MySql.open() == true)
            {
                Source.MySql.cmd = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", Source.MySql.conn);
                sdr = Source.MySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new ModelUserData
                    {
                        id = Convert.ToInt32(sdr["id"]),
                        username = sdr["username"].ToString(),
                        name = sdr["name"].ToString(),
                        email = sdr["email"].ToString(),
                        kategoria = Convert.ToInt32(sdr["kategoria"]),
                        jogosultsag = Convert.ToInt32(sdr["jogosultsag"]),
                        validitas = Convert.ToInt32(sdr["validitas"]),
                        belepve = sdr["belepve"].ToString(),
                        reg_datum = sdr["reg_datum"].ToString(),
                    });
                }
                sdr.Close();
            }
            Source.MySql.close();
            return list;
        }
    }
}

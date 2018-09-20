﻿using HR_Portal.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.DirectoryServices.Protocols;
using HR_Portal.Source;
using MySql.Data.MySqlClient;

namespace HR_Portal.Control
{
    class ControlLogin
    {
        Session session = new Session();
        Model.MySql mySql = new Model.MySql();
        SqLite sqLite = new SqLite();

        public bool ActiveDirectoryValidation(string username, string password)
        {
            if (password.Length > 0)
            {
                try
                {
                    LdapDirectoryIdentifier LDAPdi = new LdapDirectoryIdentifier("192.168.144.21", 389);
                    LdapConnection ldapConnection = new LdapConnection(LDAPdi);
                    ldapConnection.AuthType = AuthType.Basic;
                    ldapConnection.SessionOptions.ProtocolVersion = 3;
                    NetworkCredential networkCredential = new NetworkCredential(username + "@pmhu.local", password);
                    ldapConnection.Bind(networkCredential);
                    ldapConnection.Dispose();
                    return true;
                }
                catch (LdapException e)
                {
                    Console.WriteLine("\r\nUnable to login:\r\n\t" + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("\r\nUnexpected exception occured:\r\n\t" + e.GetType() + ":" + e.Message);
                }
            }
            return false;
        }

        public bool userValidation(string name, string pass)
        {
            if (mySql.rowCount("SELECT count(id) FROM users WHERE username='" + name + "'") == 1)
            {
                DateTime dateTime = DateTime.Now;
                sqLite.update("UPDATE users SET belepve = '" + dateTime.ToString("yyyy. MM. dd.") + "' WHERE username = '" + name + "';");
                mySql.close();
                return true;
            }
            else{
                mySql.close();
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
            mySql.close();
            return user;
        }
        public void writeRememberedUser(string username) //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
            sqLite.update("INSERT INTO 'app' (username) VALUES ('" + username + "');");
            mySql.close();
        }
        public void deleteRememberedUser() //javítva használja: login
        {
            sqLite.update("DELETE FROM 'app';");
            mySql.close();
        }
        public bool mySqlUserValidation(string user) //javítva használja: login
        {
            bool respond = mySql.bind("SELECT count(id) FROM users WHERE username='" + user + "'");
            mySql.close();
            return respond;
        }
        public List<UserSessData> Data_UserSession(string username)  //javítva használja: login
        {
            MySqlDataReader sdr;
            List<UserSessData> list = new List<UserSessData>();
            if(mySql.open() == true)
            {
                mySql.cmd = new MySqlCommand("SELECT * FROM users WHERE username='" + username + "'", mySql.conn);
                sdr = mySql.cmd.ExecuteReader();
                while (sdr.Read())
                {
                    list.Add(new UserSessData
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
            mySql.close();
            return list;
        }
        public void userRegistration(string username, string name, string email, int kategoria)
        {
            DateTime dateTime = DateTime.Now;
            mySql.update("INSERT INTO `users` (`id`, `username`, `name`, `email`, `kategoria`, `jogosultsag`, `validitas`, `belepve`, `reg_datum`) VALUES (NULL, '"+ username + "', '"+ name + "', '"+ email + "', '"+ kategoria + "', '1', '1', '" + dateTime.ToString("yyyy. MM. dd.") + "', '" + dateTime.ToString("yyyy. MM. dd.") + "');");
            mySql.close();
        }
    }
}

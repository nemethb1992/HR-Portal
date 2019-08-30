using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    public class ActiveDirecotry
    {
        public static bool Bind(string username, string password)
        {
            if (password.Length > 0)
            {
                try
                {
                    LdapDirectoryIdentifier LDAPdi = new LdapDirectoryIdentifier("ldap.pmhu.local", 389); 
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
    }
}

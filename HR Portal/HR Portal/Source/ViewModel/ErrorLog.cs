using HR_Portal.Source.Model.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source.ViewModel
{
    class ErrorLog
    {
        public void Insert(List<ModelErrorLog> list)
        {
            string command = "INSERT INTO errorlog (`placeofbug`, `description`, `solution`, `date`) VALUES ('" + list[0].placeofbug + "','" + list[0].description + "','" + list[0].solution + "','" + list[0].date+ "')";
            MySql.Execute(command);
            MySql.Close();
        }

        public void Update(List<ModelErrorLog> list)
        {
            string command = "UPDATE pmkcvtest.errorlog SET placeofbug='"+list[0].placeofbug+ "', description='" + list[0].description + "', solution='" + list[0].solution + "', date='" + list[0].date + "', result='" + list[0].result + "', resultdate='" + list[0].resultdate + "'";
            MySql.Execute(command);
            MySql.Close();
        }

        public List<ModelErrorLog> GetErrorLog()
        {
            return new ModelErrorLog().Get("SELECT * FROM errorlog");
        }
    }
}

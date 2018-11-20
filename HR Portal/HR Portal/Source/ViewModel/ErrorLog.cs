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
        public void Insert(ModelErrorLog data)
        {
            string command = "INSERT INTO errorlog (`placeofbug`, `description`, `solution`, `date`) VALUES ('" + data.placeofbug + "','" + data.description + "','" + data.solution + "','" + data.date + "')";
            MySql.Execute(command);
            MySql.Close();
        }

        public void Update(ModelErrorLog data)
        {
            string command = "UPDATE pmkcvtest.errorlog SET placeofbug='"+data.placeofbug+ "', description='" + data.description + "', solution='" + data.solution + "', date='" + data.date + "', result='" + data.result + "', resultdate='" + data.resultdate + "'";
            MySql.Execute(command);
            MySql.Close();
        }

        public void Delete(int id)
        {
            string command = "DELETE FROM pmkcvtest.errorlog WHERE id ="+id;
            MySql.Execute(command);
            MySql.Close();
        }

        public List<ModelErrorLog> GetErrorLog()
        {
            return new ModelErrorLog().Get("SELECT * FROM errorlog");
        }
    }
}

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
            MySqlDB mySql = new MySqlDB();
            string command = "INSERT INTO errorlog (`placeofbug`, `description`, `solution`, `date`) VALUES ('" + data.placeofbug + "','" + data.description + "','" + data.solution + "','" + data.date + "')";
            mySql.Execute(command);
            mySql.Close();
        }

        public void Update(ModelErrorLog data)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "UPDATE pmkcvtest.errorlog SET placeofbug='"+data.placeofbug+ "', description='" + data.description + "', solution='" + data.solution + "', date='" + data.date + "', result='" + data.result + "', resultdate='" + data.resultdate + "'";
            mySql.Execute(command);
            mySql.Close();
        }

        public void Delete(int id)
        {
            MySqlDB mySql = new MySqlDB();
            string command = "DELETE FROM pmkcvtest.errorlog WHERE id ="+id;
            mySql.Execute(command);
            mySql.Close();
        }

        public List<ModelErrorLog> GetErrorLog()
        {
            return new ModelErrorLog().Get("SELECT * FROM errorlog");
        }
    }
}

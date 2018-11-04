using System;

namespace HR_Portal.Source.ViewModel
{
    class Comment
    {
        public static void Add(string comment, int project_id, int applicant_id)
        {
            DateTime dateTime = DateTime.Now;
            string command = "INSERT INTO megjegyzesek (jelolt_id,projekt_id,hr_id,hr_nev,megjegyzes,datum) VALUES (" + applicant_id + "," + project_id + "," + Session.UserData[0].id + ",'" + Session.UserData[0].name + "','" + comment.Substring(0, comment.Length-2) + "','" + dateTime.ToString("yyyy. MM. dd.") + "')";
            MySql.Update(command);
            MySql.Close();
        }
        public static void Delete(int megjegyzes_id)
        {
            string command = "DELETE FROM megjegyzesek WHERE megjegyzesek.id = " + megjegyzes_id;
            MySql.Update(command);
            MySql.Close();
        }
    }
}

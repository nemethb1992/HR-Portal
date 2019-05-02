using HR_Portal.Source.Model.Project;
using HR_Portal.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Portal.Source
{
    class AppointmentModel
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Start { get; set; }
        public string Time_start { get; set; }
        public string Time_end { get; set; }
        public string Location { get; set; }
        public List<string> Attendees { get; set; }

        public AppointmentModel TransformData(ModelInterview data)
        {
            AppointmentModel appdata = new AppointmentModel();
            appdata = new AppointmentModel()
            {
                Id = data.id,
                Subject = data.interju_cim,
                Body = data.interju_leiras,
                Start = data.date_start,
                Time_start = data.time_start,
                Time_end = data.time_end,
                Location = data.helyszin,
                Attendees = GetAttendees(data.id)
            };
            return appdata;
        }

        public List<string> GetAttendees(int interviewId)
        {
            List<string> attendees = new List<string>();
            MySqlDB mySql = new MySqlDB();
            if (mySql.Open() == true)
            {
                string command = "SELECT email FROM interju_resztvevo_kapcs LEFT JOIN users ON users.id = interju_resztvevo_kapcs.user_id WHERE interju_resztvevo_kapcs.interju_id = " + interviewId;
                mySql.cmd = new MySqlCommand(command, mySql.conn);
                mySql.sdr = mySql.cmd.ExecuteReader();
                while (mySql.sdr.Read())
                {
                    attendees.Add(mySql.sdr["email"].ToString());
                }
                mySql.sdr.Close();
            }
            return attendees;
        }
    }

}

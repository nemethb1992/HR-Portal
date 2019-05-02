using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using System.Net;
using HR_Portal.Utils;
using HR_Portal.Source;
using System.Windows;

namespace HR_Portal.Controller
{
    class Appointments
    {
        public void CreateMeeting(AppointmentModel data)
        {
            try
            {
            ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            SearchFilter sfSearchFilter = new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false);
            service.UseDefaultCredentials = true;

            service.AutodiscoverUrl(Session.UserData.email, adAutoDiscoCallBack);
            Appointment meeting = new Appointment(service);

            meeting.Subject = data.Subject;
            //meeting.Body.BodyType = BodyType.HTML;
            meeting.Body = data.Body;
            DateTime date_start = DateHandler.GenerateFromString(data.Start, data.Time_start);
            DateTime date_end = DateHandler.GenerateFromString(data.Start, data.Time_end);
            meeting.Start = date_start;
            meeting.End = date_end;
            meeting.Location = data.Location;
            foreach (var attendee in data.Attendees)
            {
                meeting.RequiredAttendees.Add(attendee);
            }
            meeting.ReminderMinutesBeforeStart = 30;
            meeting.Save(SendInvitationsMode.SendToAllAndSaveCopy);
            Item item = Item.Bind(service, meeting.Id, new PropertySet(ItemSchema.Subject));
            }
            catch (Exception e )
            {
                MessageBox.Show("Sikertelen létrehozás! \n\n Error: \n" + e );
            }
        }

        internal static bool adAutoDiscoCallBack(string redirectionUrl)
        {
            bool result = false;

            Uri redirectionUri = new Uri(redirectionUrl);

            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }

            return result;

        }
    }
}

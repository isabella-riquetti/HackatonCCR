using HackathonCCR.EDM.Models;
using HackathonCCR.MVC.Helper;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;

namespace HackathonCCR.MVC.Services
{
    public class EmailService : IEmailService
    {
        public Guid SendAppointment(User mentor, User discover, DateTime schedule)
        {
            var inviteId = Guid.NewGuid();
            var fromEmail = ConfigurationManager.AppSetting["Email:Email"];
            var emailPassword = ConfigurationManager.AppSetting["Email:Password"];

            var mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, "Degrau certo");
            var mentorEmail = new MailAddress(mentor.Email, mentor.Name);
            var discoverEmail = new MailAddress(discover.Email, discover.Name);
            mail.To.Add(mentorEmail);
            mail.To.Add(discoverEmail);
            mail.Subject = $"Degrau Certo: Mentoria agendada";
            mail.Body = $"Uma mentoria entre o mentor {mentor.Name} e o mentorada {discover.Name} foi agendada. Favor informar a inteção de comparecimento e utilizar o google meet para se reunirem.";

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Degrau Certo");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("BEGIN:VEVENT");
            str.AppendLine(string.Format("DTSTART:{0:yyyyMMddTHHmmssZ}", schedule.ToString("yyyyMMddTHHmmss")));
            str.AppendLine(string.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
            str.AppendLine(string.Format("DTEND:{0:yyyyMMddTHHmmssZ}", schedule.AddMinutes(30).ToString("yyyyMMddTHHmmss")));
            str.AppendLine(string.Format("UID:{0}", inviteId));
            str.AppendLine(string.Format("DESCRIPTION:{0}", mail.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", mail.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", mail.Subject));
            str.AppendLine(string.Format("ORGANIZER:{0}", mail.From.Address));

            str.AppendLine(string.Format("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={0};X-NUM-GUESTS=0:mailto:{1}", mail.To[0].DisplayName, mail.To[0].Address));
            str.AppendLine(string.Format("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN={0};X-NUM-GUESTS=0:mailto:{1}", mail.To[1].DisplayName, mail.To[1].Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
            MemoryStream stream = new MemoryStream(byteArray);

            Attachment attach = new Attachment(stream, "test.ics");

            mail.Attachments.Add(attach);

            var smtpclient = new System.Net.Mail.SmtpClient();
            smtpclient.Host = "smtp.gmail.com";
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = new System.Net.NetworkCredential(fromEmail, emailPassword);
            smtpclient.Send(mail);

            return inviteId;
        }

        public void Cancel(User mentor, User discover, Guid inviteId)
        {
            var fromEmail = ConfigurationManager.AppSetting["Email:Email"];
            var emailPassword = ConfigurationManager.AppSetting["Email:Password"];

            var mail = new MailMessage();
            mail.From = new MailAddress(fromEmail, "Degrau certo");

            var mentorEmail = new MailAddress(mentor.Email, mentor.Name);
            var discoverEmail = new MailAddress(discover.Email, discover.Name);
            mail.To.Add(mentorEmail);
            mail.To.Add(discoverEmail);

            mail.Subject = $"Degrau Certo: Mentoria cancelada";
            mail.Body = $"Um dos participantes precisou cancelar a mentoria entre o mentor {mentor.Name} e o mentorada {discover.Name}.";

            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");
            str.AppendLine("PRODID:-//Degrau Certo");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:CANCEL");
            str.AppendLine(string.Format("UID:{0}", inviteId));
            str.AppendLine(string.Format("DESCRIPTION:{0}", mail.Body));
            str.AppendLine(string.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", mail.Body));
            str.AppendLine(string.Format("SUMMARY:{0}", mail.Subject));
            str.AppendLine(string.Format("ORGANIZER:{0}", mail.From.Address));

            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:DISPLAY");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");

            byte[] byteArray = Encoding.ASCII.GetBytes(str.ToString());
            MemoryStream stream = new MemoryStream(byteArray);

            Attachment attach = new Attachment(stream, "test.ics");

            mail.Attachments.Add(attach);

            var smtpclient = new System.Net.Mail.SmtpClient();
            smtpclient.Host = "smtp.gmail.com";
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = new System.Net.NetworkCredential(fromEmail, emailPassword);
            smtpclient.Send(mail);
        }
    }
}

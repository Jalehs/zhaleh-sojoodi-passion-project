using passion_project.Models;
using passion_project.ViewModel.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace passion_project.Services
{
    public class EmailHelper
    {
        private EmailSettings _eSettings;
        public EmailHelper(EmailSettings _eSettings)
        {
            this._eSettings = _eSettings;
        }

        public bool SendMail(string recipient, string subject, AppointmentVM apVM)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_eSettings.FromEmail, _eSettings.DisplayName)
                };

                string toEmail = string.IsNullOrEmpty(recipient)
                ? _eSettings.ToEmail : recipient;

                mail.To.Add(new MailAddress(toEmail));
                foreach (string bcc in _eSettings.BccEmails)
                {
                    mail.Bcc.Add(new MailAddress(bcc));
                }

                // Subject and multipart/alternative Body
                mail.Subject = subject;

                string text = "This is a friendly reminder that you have the following appointment booked. ";
                string html = @"<h3>Hi " + apVM.PatientFirstName + " " + apVM.PatientLastName + "!</h3>" +
                    "<p>This is a friendly reminder that you have the following appointment booked:</p><hr />" +
                    "<div class='text-center p-3'> " + apVM.AppointmentDate + " " + apVM.AppointmentTime  + "<br /> With Dr. " +
                                                        apVM.DoctorFirstName + " " + apVM.DoctorLastName + "</div>" +
                    "<p>Please Call (604)688-5225 if you have any question</p> " +
                    "<p>Best,<br />" +
                    "Health Care Center</p>";

                mail.AlternateViews.Add(
                AlternateView.CreateAlternateViewFromString(text,

                null, MediaTypeNames.Text.Plain));
                mail.AlternateViews.Add(

                AlternateView.CreateAlternateViewFromString(html,
                null, MediaTypeNames.Text.Html));

                //optional priority setting
                mail.Priority = MailPriority.High;
                                         

                SmtpClient smtp = new SmtpClient(_eSettings.Domain, _eSettings.Port);
                smtp.Credentials = new NetworkCredential(_eSettings.UsernameLogin, _eSettings.UsernamePassword);
                smtp.EnableSsl = false;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}

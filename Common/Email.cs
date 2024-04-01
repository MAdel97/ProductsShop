/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using Microsoft.IdentityModel.Protocols;
using AutoMapper.Configuration;

namespace AcademicCoursesCRUD.Common
{
    public class Email
    {
        private readonly IConfiguration _config;
        public Email(IConfiguration config)
        {
            _config = config;
        }

        public static async Task<bool> SendMail(string fromemail, string frompassword, string toemail, string emailsubject, string emailbody)
        {
            //toemail = "ibrahim.maaty@bue.edu.eg";
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
            mail.From = new MailAddress(fromemail);
            mail.To.Add(toemail);
            mail.Subject = emailsubject;
            mail.Body = emailbody;
            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(fromemail, frompassword);
            SmtpServer.EnableSsl = true;

            await SmtpServer.SendMailAsync(mail);

            return true;
        }
    }
}
*/
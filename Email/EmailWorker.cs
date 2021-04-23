using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class EmailWorker
    {
       
        public async Task SendEmailAsync( CardDetails card)
        {
            MailMessage mail = new MailMessage();

            mail.To.Add("Higuain9fatty@gmail.com");
            mail.Subject = $"A Userstory is now DONE: {card.Contents}";
            mail.Body = "";
            mail.IsBodyHtml = false;
            mail.From = new MailAddress("kanbanboard1@gmail.com");
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //smtp
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("kanbanboard1@gmail.com", "kPVX9XWiKEXaThv");

            await smtpClient.SendMailAsync(mail);
        }

       
    }
}

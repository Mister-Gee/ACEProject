using ACEdatabaseAPI.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ACEdatabaseAPI.Helpers
{
    public class MailHelper
    {
        public static void SendEmail(string to, string subject, string message, EmailSettings emailSetting, string bcc = "")
        {
            try
            {
                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                subject = myTI.ToTitleCase(subject.ToLower());
                var loginInfo = new NetworkCredential(emailSetting.UserName, emailSetting.Mailpass);
                var smtpClient = new SmtpClient(emailSetting.MailHost, emailSetting.ServerPort);
                smtpClient.EnableSsl = emailSetting.UseSsl;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;

                var msg = new MailMessage();
                var sender = "ACE";
                var mailSender = emailSetting.MailSender;
                msg.From = new MailAddress(mailSender, sender);
                msg.Subject = subject;
                msg.Body = message;
                msg.IsBodyHtml = true;

                if (emailSetting.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                var tos = new List<string>();
                if (!string.IsNullOrEmpty(to))
                {
                    tos = to.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
                foreach (var item in tos)
                {
                    msg.To.Add(new MailAddress(item));
                }
                if (!string.IsNullOrEmpty(bcc) && bcc != "Error")
                {
                    msg.Bcc.Add(bcc);
                }
               

                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static void SendEmail(string to, string subject, string message, List<Attachment> attachments, EmailSettings emailSetting, string bcc = "")
        {
            var tos = new List<string>();
            var bccs = new List<string>();
            if (!string.IsNullOrEmpty(to))
            {
                tos = to.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            bccs.AddRange(bcc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList());
            bccs.Add(emailSetting.MailBcc);

            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            subject = myTI.ToTitleCase(subject.ToLower());
            var loginInfo = new NetworkCredential(emailSetting.UserName, emailSetting.Mailpass);
            var smtpClient = new SmtpClient(emailSetting.MailHost, emailSetting.ServerPort);
            smtpClient.EnableSsl = emailSetting.UseSsl;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;

            var msg = new MailMessage();


            var sender = "ACE";
            var mailSender = emailSetting.MailSender;
            msg.From = new MailAddress(mailSender, sender);
            
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;
            foreach (var attachment in attachments)
            {
                msg.Attachments.Add(attachment);
            }

            if (emailSetting.WriteAsFile)
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                smtpClient.EnableSsl = false;
            }

            foreach (var item in tos)
            {
                msg.To.Add(new MailAddress(item));
            }

            foreach (var item in bccs)
            {
                msg.Bcc.Add(new MailAddress(item));
            }
            try
            {
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public static Task SendMailAsync(string to, string subject, string message, EmailSettings emailSetting)
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            subject = myTI.ToTitleCase(subject.ToLower());
            var loginInfo = new NetworkCredential(emailSetting.UserName, emailSetting.Mailpass);
            var smtpClient = new SmtpClient(emailSetting.MailHost, emailSetting.ServerPort);
            smtpClient.EnableSsl = emailSetting.UseSsl;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;

            var msg = new MailMessage();
            msg.From = new MailAddress(emailSetting.MailSender);
            msg.To.Add(new MailAddress(to));
            msg.Bcc.Add(emailSetting.MailBcc);
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            if (emailSetting.WriteAsFile)
            {
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtpClient.PickupDirectoryLocation = emailSetting.FileLocation;
                smtpClient.EnableSsl = false;
            }

            var x = smtpClient.SendMailAsync(msg);

            return x;
        }

        public static string GetMailContent(IWebHostEnvironment _hostingEnvironment, string outerTemp, string innerTemp, string user, string[] msg)
        {

            var outerBody = "";
            //Read template file from the App_Data folder
            using (var sr = new StreamReader($"{_hostingEnvironment.ContentRootPath}\\Templates\\{outerTemp}"))
            {
                outerBody = sr.ReadToEnd();
            }
            var body = "";
            //Read template file from the Template folder
            using (var sr = new StreamReader($"{_hostingEnvironment.ContentRootPath}\\Templates\\{innerTemp}"))
            {
                body = sr.ReadToEnd();
            }
            var msgBody = string.Format(body, msg);
            string outerMsg;
            if (outerTemp.ToLower() == "nu_mail_new.txt" || outerTemp.ToLower() == "nu_mail_pos.txt")
            {
                outerMsg = string.Format(outerBody, msgBody);
            }
            else
            {
                outerMsg = string.Format(outerBody, user, msgBody);
            }
            return outerMsg;
        }
    }
}

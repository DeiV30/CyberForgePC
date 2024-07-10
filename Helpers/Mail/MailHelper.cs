namespace cyberforgepc.Helpers.Mail
{
    using cyberforgepc.Helpers.Mail.Model;
    using cyberforgepc.Helpers.Settings;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Net.Mail;
    using System.Threading.Tasks;

    public class MailHelper : IMailHelper
    {
        private readonly EmailSetting appSettings;


        private readonly ILogger logger;
        private readonly IHostEnvironment hostEnvironment;

        public MailHelper(IOptions<EmailSetting> appSettings, ILogger<MailHelper> logger, IHostEnvironment hostEnvironment)
        {
            this.appSettings = appSettings.Value;

            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }


        public void Send(string template, string toEmail, string subject, object model)
        {
            var mailHeader = new MailSetup
            {
                To = toEmail,
                IsHtml = true,
                Subject = subject
            };

            Send(template, mailHeader, model);
        }

        public void Send(string template, MailSetup header, object model)
        {
            var enumTemplate = GetEnumTemplate(template);
            var mailMessage = GetMailMessage(enumTemplate, header, model);

            SendMail(mailMessage).Wait();
        }

        private async Task SendMail(MailMessage message)
        {
            var smtpClient = GetSmtpClient();
            await smtpClient.SendMailAsync(message);
        }

        private SmtpClient GetSmtpClient()
        {
            var smtpClient = new SmtpClient(appSettings.Host);

            if (appSettings.Port != default)
                smtpClient.Port = appSettings.Port;

            if (!string.IsNullOrEmpty(appSettings.User))
                smtpClient.Credentials = new NetworkCredential(appSettings.User, appSettings.Password);

            smtpClient.EnableSsl = appSettings.EnableSsl;
            return smtpClient;
        }

        private MailMessage GetMailMessage(Enum template, MailSetup header, dynamic model)
        {
            string body = string.Format(Get(template), model);
            return GetMailMessage(header, body.Replace("{{", "{").Replace("}}", "}"));
        }

        private MailMessage GetMailMessage(MailSetup header, string body)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(appSettings.From),
                Subject = header.Subject,
                IsBodyHtml = header.IsHtml
            };

            mailMessage.To.Add(header.To);

            if (!string.IsNullOrEmpty(header.Cc))
                mailMessage.CC.Add(header.Cc);

            if (!string.IsNullOrEmpty(header.Bcc))
                mailMessage.Bcc.Add(header.Bcc);

            mailMessage.Body = body;

            return mailMessage;
        }

        private Enum GetEnumTemplate(string template)
        {
            return template switch
            {
                "welcome" => EnumTemplates.Welcome,
                "recoverPassword" => EnumTemplates.RecoverPassword,
                "activateAccount" => EnumTemplates.ActivateAccount,
                _ => null,
            };
        }

        private string Get(Enum template)
        {
            var pathToFile = Path.Combine(hostEnvironment.ContentRootPath,
                $"assets/Mail/{Enum.GetName(typeof(EnumTemplates), template)}.htm");

            using StreamReader reader = File.OpenText(pathToFile);
            return reader.ReadToEnd();
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            var mailMessage = (MailMessage)e.UserState;

            if (e.Cancelled)
                logger.LogInformation("[{0}] Send canceled.", mailMessage.To);

            if (e.Error != null)
                logger.LogInformation("[{0}] {1}", mailMessage.To, e.Error.ToString());
        }
    }
}

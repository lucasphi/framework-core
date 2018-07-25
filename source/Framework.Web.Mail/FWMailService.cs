using Framework.Security.Cryptography;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Web.Mail
{
    // for future refecence
    // https://hassantariqblog.wordpress.com/2017/03/20/asp-net-core-sending-email-with-gmail-account-using-asp-net-core-services/

    /// <summary>
    /// Framework mailing service implementation.
    /// </summary>
    public class FWMailService : IFWMailService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FWMailService" />.
        /// </summary>
        /// <param name="configuration">The application configuration settings.</param>
        public FWMailService(IOptions<FWMailSettings> configuration)
        {
            _settings = configuration.Value;
        }

        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that on completion updates the output.</returns>
        public async Task SendEmailAsync(FWMail mail)
        {
            if (mail.Debug && !_settings.Debug)
            {
                await Task.FromCanceled(new CancellationToken(true));
            }

            MailMessage message = CreateMessage(mail);

            using (var smtpClient = new SmtpClient(_settings.Host, _settings.Port))
            {
                var password = _settings.Password;
                if (_settings.SecurePassword)
                {
                    var crypto = FWEncryption.Create();
                    var key = CreateKey();

                    password = crypto.Decrypt(password, key);
                }
                smtpClient.Credentials = new NetworkCredential(_settings.User, password);
                smtpClient.EnableSsl = _settings.EnableSsl;
                await smtpClient.SendMailAsync(message);
            }
        }

        private string CreateKey()
        {
            var key = new char[32];
            for (int i = 0; i < 32; i++)
            {
                if (_settings.User.Length > i)
                    key[i] = _settings.User[i];
                else
                    key[i] = '0';
            }
            return new string(key);
        }

        private MailMessage CreateMessage(FWMail mail)
        {
            var message = new MailMessage
            {
                Subject = mail.Subject,
                Body = mail.Message,
                IsBodyHtml = mail.IsHtml,
                From = new MailAddress(_settings.From, _settings.FromDisplayName)
            };

            foreach (var to in mail.To)
            {
                message.To.Add(new MailAddress(to.Address, to.DisplayName));
            }

            return message;
        }

        private FWMailSettings _settings;
    }
}

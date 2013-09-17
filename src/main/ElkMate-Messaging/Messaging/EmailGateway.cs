using System;
using System.Net;
using System.Net.Mail;

namespace SmartElk.ElkMate.Messaging.Messaging
{
    public class EmailGateway : IEmailGateway
    {
        public void Send(EmailMessage message, SmtpSettings smtpSettings)
        {
            try
            {
                var client = new SmtpClient(smtpSettings.Server, smtpSettings.Port);

                if (smtpSettings.UseDefaultCredentials)
                {
                    client.UseDefaultCredentials = true;
                }
                else
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpSettings.Username,
                                                               smtpSettings.Password);
                }

                client.EnableSsl = smtpSettings.UseSsl;

                MailAddress from = message.Outgoing.Alias == null
                                       ? new MailAddress(message.Outgoing.Address)
                                       : new MailAddress(message.Outgoing.Address, message.Outgoing.Alias);
                MailAddress to = message.Receiving.Alias == null
                                     ? new MailAddress(message.Receiving.Address)
                                     : new MailAddress(message.Receiving.Address, message.Receiving.Alias);

                var msg = new MailMessage(from, to)
                    {IsBodyHtml = message.IsHtml, Subject = message.Subject, Body = message.Body};

                client.Send(msg);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                throw new EmailSendingFailedException(ex, true);
            }
            catch (Exception ex)
            {
                throw new EmailSendingFailedException(ex);
            }
        }
    }
}
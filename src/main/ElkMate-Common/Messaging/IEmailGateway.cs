namespace ElkMate.Common.Messaging
{
    public interface IEmailGateway
    {
        void Send(EmailMessage messageInfo, SmtpSettings smtpSettings);
    }
}
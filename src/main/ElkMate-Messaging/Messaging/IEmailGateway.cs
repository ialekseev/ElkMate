namespace SmartElk.ElkMate.Messaging.Messaging
{
    public interface IEmailGateway
    {
        void Send(EmailMessage messageInfo, SmtpSettings smtpSettings);
    }
}
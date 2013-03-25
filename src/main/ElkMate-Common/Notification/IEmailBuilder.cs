using ElkMate.Common.Messaging;

namespace ElkMate.Common.Notification
{
    public interface IEmailBuilder
    {        
        EmailMessage BuildMessage(IContact recepient, string template, params object[] templateParams);
    }
}
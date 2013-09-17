using SmartElk.ElkMate.Common;
using SmartElk.ElkMate.Messaging.Messaging;

namespace SmartElk.ElkMate.Messaging.Notification
{
    public interface IEmailBuilder
    {
        EmailMessage BuildMessage(IContact recepient, string template, params object[] templateParams);
    }
}
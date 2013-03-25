using ElkMate.Common.Messaging;
using ElkMate.Common.Template;

namespace ElkMate.Common.Notification
{
    public interface INotificationSender
    {
        IContact DefaultSender { get; }        
        void Notify(IContact recepient, string template, params object[] templateParams);
	    void Notify(string recipientEmail, string recipientAlias, string subject, string text);
	    void Notify(IContact recipient, string subject, string text);
	    void Notify(EmailMessage message, bool async = true);
	    ITemplate DetectTemplate(string name);
		string DetectSubject(string name);
		bool UseHtml { get; }
    }
}
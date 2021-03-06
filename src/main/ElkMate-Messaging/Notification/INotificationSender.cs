﻿using SmartElk.ElkMate.Common;
using SmartElk.ElkMate.Messaging.Messaging;
using SmartElk.ElkMate.Messaging.Template;

namespace SmartElk.ElkMate.Messaging.Notification
{
    public interface INotificationSender
    {
        IContact DefaultSender { get; }
        bool UseHtml { get; }
        void Notify(IContact recepient, string template, params object[] templateParams);
        void Notify(string recipientEmail, string recipientAlias, string subject, string text);
        void Notify(IContact recipient, string subject, string text);
        void Notify(EmailMessage message, bool async = true);
        ITemplate DetectTemplate(string name);
        string DetectSubject(string name);
    }
}
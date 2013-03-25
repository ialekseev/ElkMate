using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Logging;
using ElkMate.Common.Messaging;
using ElkMate.Common.Template;

namespace ElkMate.Common.Notification
{            
        public class EmailNotificationSender : INotificationSender, IEmailBuilder
        {
            private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
            private  IDictionary<string, string> _subjects = new Dictionary<string, string>();
            private readonly ITemplateEngine _templateEngine;
            private readonly IEmailGateway _gateway = new EmailGateway();
            private string _senderAlias = "";
            private string _smtpLogin;
            private string _smtpPwd;
            private string _smtpServer;
            private uint _smtpPort;
            private bool _useSecureConnection;

            public EmailNotificationSender(ITemplateEngine templateEngine, string smtpServer, string smtpLogin = "", string smtpPwd = "", uint smtpPort = 25u, bool useSecureConnection = false, string senderAlias = "")
            {
                _templateEngine = templateEngine;
                _smtpServer = smtpServer;
                _smtpLogin = smtpLogin;
                _smtpPwd = smtpPwd;
                _smtpPort = smtpPort;
                _useSecureConnection = useSecureConnection;
                _senderAlias = senderAlias;
            }

            public bool UseHtml { get; protected set; }

            public virtual string SenderAlias
            {
                get { return _senderAlias; }
                set { _senderAlias = value; }
            }

            public virtual ITemplateEngine TemplateEngine
            {
                get { return _templateEngine; }
            }


            public virtual bool UseSecureConnection
            {
                get { return _useSecureConnection; }
                protected set { _useSecureConnection = value; }
            }

            public virtual string SmtpLogin
            {
                get { return _smtpLogin; }
                protected set { _smtpLogin = value; }
            }

            public virtual string SmtpPwd
            {
                get { return _smtpPwd; }
                protected set { _smtpPwd = value; }
            }

            public virtual string SmtpServer
            {
                get { return _smtpServer; }
                protected set { _smtpServer = value; }
            }

            public virtual uint SmtpPort
            {
                get { return _smtpPort; }
                protected set { _smtpPort = value; }
            }

            public virtual IDictionary<string, string> Subjects
            {
                get { return _subjects; }
                set { _subjects = value; }
            }

            protected virtual SmtpSettings BuildSmtpSettings()
            {
                return new SmtpSettings(SmtpServer, (int)SmtpPort, UseSecureConnection, string.IsNullOrEmpty(SmtpPwd), SmtpLogin, SmtpPwd);
            }
            
            public ITemplate DetectTemplate(string name)
            {
                return TemplateEngine.FindTemplateByName(name);
            }
            
            public string DetectSubject(string name)
            {
                string subject;
                Subjects.TryGetValue(name, out subject);
                if (string.IsNullOrEmpty(subject))
                {
                    Subjects.TryGetValue("*", out subject);
                }
                return subject;
            }

            public IContact DefaultSender
            {
                get { return new Email(SmtpLogin, SenderAlias);}
            }
            
            EmailMessage IEmailBuilder.BuildMessage(IContact recepient, string template, params object[] templateParams)
            {
                var templ = DetectTemplate(template);
                return new EmailMessage(DefaultSender, recepient, DetectSubject(template), templ.Evaluate(templateParams), UseHtml);
            }
            
            public void Notify(IContact recepient, string template, params object[] templateParams)
            {
                Notify(((IEmailBuilder) this).BuildMessage(recepient, template, templateParams));
            }

            public void Notify(string recipientEmail, string recipientAlias, string subject, string text)
            {
                Notify(new Email(recipientEmail, recipientAlias), subject, text);
            }

            public void Notify(IContact recipient, string subject, string text)
            {
                var message = new EmailMessage(DefaultSender, recipient, subject, text, UseHtml);

                Notify(message);
            }

            public void Notify(EmailMessage message, bool async = true)
            {
                Action sendMail = () =>
                {
                    try
                    {
                        _gateway.Send(message, BuildSmtpSettings());
                    }
                    catch (Exception ex)
                    {
                        Logger.Error("Error sending email asynchronously", ex);
                    }
                };

                if (async)
                    Task.Factory.StartNew(sendMail);
                else
                    sendMail();
            }

        }            
}
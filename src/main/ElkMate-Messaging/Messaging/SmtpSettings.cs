using System;

namespace SmartElk.ElkMate.Messaging.Messaging
{
    [Serializable]
    public class SmtpSettings
    {
        private const int DefaultSmtpPort = 25;
        private const string DefaultSmtpServer = "localhost";

        public SmtpSettings(string server, int port, bool useDefaultCredentials, string username = "",
                            string password = "")
        {
            Server = server;
            Port = port;

            UseDefaultCredentials = useDefaultCredentials;

            if (!useDefaultCredentials)
            {
                Username = username;
                Password = password;
            }
        }

        public SmtpSettings(string server, int port, bool useSsl, bool useDefaultCredentials, string username,
                            string password)
        {
            Server = server;
            Port = port;
            UseSsl = useSsl;
            UseDefaultCredentials = useDefaultCredentials;

            if (!useDefaultCredentials)
            {
                Username = username;
                Password = password;
            }
        }

        /// <summary>
        ///     Конструктор со значениями Port = 25, UseDefaultCredentials = true
        /// </summary>
        /// <param name="server">SMTP Server</param>
        public SmtpSettings(string server)
        {
            Server = server;
            Port = DefaultSmtpPort;
            UseSsl = false;
            UseDefaultCredentials = true;
        }

        /// <summary>
        ///     Конструктор со значением Port = 25
        /// </summary>
        /// <param name="server">SMTP Server</param>
        /// <param name="username">SMTP Username</param>
        /// <param name="password">SMTP Passoword</param>
        public SmtpSettings(string server, string username, string password)
        {
            Server = server;
            Port = DefaultSmtpPort;

            UseSsl = false;
            UseDefaultCredentials = false;

            Username = username;
            Password = password;
        }

        /// <summary>
        ///     Конструктор со значениями Server = 'localhost', Port = 25, UseDefaultCredentials = true
        /// </summary>
        public SmtpSettings()
        {
            Server = DefaultSmtpServer;
            Port = DefaultSmtpPort;
            UseSsl = false;
            UseDefaultCredentials = true;
        }

        public string Server { get; private set; }
        public int Port { get; private set; }
        public bool UseSsl { get; private set; }
        public bool UseDefaultCredentials { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}
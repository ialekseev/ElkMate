namespace ElkMate.Common.Messaging
{
    public class EmailMessage
    {
        public IContact Outgoing { get; protected set; }
        public IContact Receiving { get; protected set; }
        public string Subject { get; protected set; }
        public string Body { get; protected set; }
        public bool IsHtml { get; protected set; }

        public EmailMessage(IContact outgoingAddress, IContact receivingAddress, string subject, string body, bool isHtml)
        {
            Outgoing = outgoingAddress;
            Receiving = receivingAddress;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }

        public EmailMessage(string outgoingAddress, IContact receivingAddress, string subject, string body, bool isHtml)
        {
            Outgoing = new Email(outgoingAddress);
            Receiving = receivingAddress;
            Subject = subject;
            Body = body;
            IsHtml = isHtml;
        }
    }
}
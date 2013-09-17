using System;

namespace SmartElk.ElkMate.Messaging.Messaging
{
    [Serializable]
    public class EmailSendingFailedException : Exception
    {
        public EmailSendingFailedException(Exception ex, bool isRecipientFailure)
        {
            InnerException = ex;
            IsRecipientFailure = isRecipientFailure;
        }

        public EmailSendingFailedException(Exception ex)
            : this(ex, false)
        {
        }

        public new Exception InnerException { get; private set; }

        public bool IsRecipientFailure { get; private set; }
    }
}
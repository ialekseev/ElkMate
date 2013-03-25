using System;

namespace ElkMate.Common.Messaging
{
    [Serializable]
    public class EmailSendingFailedException : Exception
    {
        public new Exception InnerException { get; private set; }

        public bool IsRecipientFailure { get; private set; }

        public EmailSendingFailedException(Exception ex, bool isRecipientFailure)
        {
            InnerException = ex;
            IsRecipientFailure = isRecipientFailure;
        }

        public EmailSendingFailedException(Exception ex)
            :this(ex, false)
        {
        }
    }
}

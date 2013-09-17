
namespace SmartElk.ElkMate.Common.Validation
{
    public abstract class BaseValidationCore
    {
        public string ErrorMessage { get; set; }
        
        protected bool FailWithMessage(string message)
        {
            ErrorMessage = message;
            return false;
        }
    }
}

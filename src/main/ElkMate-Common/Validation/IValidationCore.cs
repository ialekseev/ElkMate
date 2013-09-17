namespace SmartElk.ElkMate.Common.Validation
{
    public interface IValidationCore
    {
        bool Validate(object toCheck);
        string ErrorMessage { get; set; }
    }
}

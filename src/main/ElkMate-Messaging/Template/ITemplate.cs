namespace SmartElk.ElkMate.Messaging.Template
{
    public interface ITemplate
    {
        string Name { get; }

        string Evaluate(params object[] parts);
    }
}
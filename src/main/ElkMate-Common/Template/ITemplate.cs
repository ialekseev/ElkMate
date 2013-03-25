namespace ElkMate.Common.Template
{
    public interface ITemplate
    {
        string Name { get; }

        string Evaluate(params object[] parts);
    }
}
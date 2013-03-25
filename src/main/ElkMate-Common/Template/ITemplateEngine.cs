namespace ElkMate.Common.Template
{
    public interface ITemplateEngine
    {
        ITemplate FindTemplateByName(string name);
    }
}
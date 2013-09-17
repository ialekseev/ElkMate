namespace SmartElk.ElkMate.Messaging.Template
{
    public interface ITemplateEngine
    {
        ITemplate FindTemplateByName(string name);
    }
}
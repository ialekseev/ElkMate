namespace ElkMate.Common
{
    public interface IContact
    {
        string Address { get; }
        string Alias { get; }
        IContact WithAlias(string alias);
    }
}
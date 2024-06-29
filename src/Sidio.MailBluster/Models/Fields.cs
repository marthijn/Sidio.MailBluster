namespace Sidio.MailBluster.Models;

public sealed class Fields : Dictionary<string, string>
{
    public void AddField(string key, string value)
    {
        this[key] = value;
    }
}
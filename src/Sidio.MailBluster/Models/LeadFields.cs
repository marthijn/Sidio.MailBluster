namespace Sidio.MailBluster.Models;

/// <summary>
/// The lead fields.
/// </summary>
public sealed class LeadFields : Dictionary<string, string>
{
    /// <summary>
    /// Adds a field.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void AddField(string key, string value)
    {
        this[key] = value;
    }
}
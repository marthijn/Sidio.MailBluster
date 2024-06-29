namespace Sidio.MailBluster;

internal static class LoggingHelpers
{
    public static string? ObfuscateEmailAddress(this string? emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress) || emailAddress.IndexOf('@') < 0)
        {
            return emailAddress;
        }

        return $"{emailAddress[0]}***{emailAddress[(emailAddress.IndexOf('@') - 1)..]}";
    }
}
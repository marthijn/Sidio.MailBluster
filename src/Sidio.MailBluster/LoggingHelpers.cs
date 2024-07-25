namespace Sidio.MailBluster;

internal static class LoggingHelpers
{
    public static string? ObfuscateEmailAddress(this string? emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress) || emailAddress.IndexOf('@') < 0)
        {
            return emailAddress;
        }

        return $"{emailAddress[0]}***{emailAddress.Substring(emailAddress.IndexOf('@') - 1)}";
    }

    public static string? Sanitize(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return input.Trim().Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
    }
}
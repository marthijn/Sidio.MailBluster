using Microsoft.Extensions.Compliance.Redaction;

namespace Sidio.MailBluster.Compliance;

/// <summary>
/// Redacts email addresses.
/// </summary>
internal sealed class EmailRedactor : Redactor
{
    /// <inheritdoc />
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        var length = GetRedactedLength(source);
        if (length < 1)
        {
            return 0;
        }

        var indexOfAt = source.IndexOf('@');
        if (indexOfAt < 0)
        {
            var starRedactor = new StarRedactor();
            starRedactor.Redact(source, destination);
            return length;
        }

        // first character is not redacted
        destination[0] = source[0];

        // redact everything before the '@' character
        for (var i = 1; i < indexOfAt; i++)
        {
            destination[i] = '*';
        }

        // copy the '@' character
        destination[indexOfAt] = '@';

        // copy the redacted part for the domain
        for (var i = indexOfAt + 1; i < source.Length; i++)
        {
            if (source[i] == '.')
            {
                destination[i] = '.';
            }
            else
            {
                destination[i] = '*';
            }
        }

        return length;
    }

    /// <inheritdoc />
    public override int GetRedactedLength(ReadOnlySpan<char> input)
    {
        return input.Length;
    }
}
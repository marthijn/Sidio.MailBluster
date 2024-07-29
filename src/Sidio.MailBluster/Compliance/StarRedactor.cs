using Microsoft.Extensions.Compliance.Redaction;

namespace Sidio.MailBluster.Compliance;

internal sealed class StarRedactor : Redactor
{
    public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
    {
        var length = GetRedactedLength(source);
        if(length < 1)
        {
            return 0;
        }

        // write first character
        destination[0] = source[0];

        for(var i = 1; i < length; i++)
        {
            destination[i] = '*';
        }

        return length;
    }

    public override int GetRedactedLength(ReadOnlySpan<char> input)
    {
        return input.Length;
    }
}
using Sidio.MailBluster.Compliance;

namespace Sidio.MailBluster.Tests.Compliance;

public sealed class StarRedactorTests
{
    [Theory]
    [InlineData("test", "t***")]
    [InlineData("", "")]
    public void Redact_GivenString_ReturnsRedactedString(string input, string expected)
    {
        // arrange
        var redactor = new StarRedactor();

        // act
        var redacted = redactor.Redact(input);

        // assert
        redacted.Should().Be(expected);
    }
}
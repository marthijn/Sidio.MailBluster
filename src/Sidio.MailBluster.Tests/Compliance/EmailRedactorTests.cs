using Sidio.MailBluster.Compliance;

namespace Sidio.MailBluster.Tests.Compliance;

public sealed class EmailRedactorTests
{
    [Theory]
    [InlineData("info@sidio.nl", "i***@*****.**")]
    [InlineData("firstname.lastname@example.org", "f*****************@*******.***")]
    [InlineData("invalidemail", "i***********")]
    public void Redact_GivenEmail_ReturnsRedactedEmail(string email, string expected)
    {
        // arrange
        var redactor = new EmailRedactor();

        // act
        var redacted = redactor.Redact(email);

        // assert
        redacted.Should().Be(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Redact_WhenEmailIsNullOrEmpty_ReturnsEmptyString(string? email)
    {
        // arrange
        var redactor = new EmailRedactor();

        // act
        var redacted = redactor.Redact(email);

        // assert
        redacted.Should().BeNullOrEmpty();
    }
}
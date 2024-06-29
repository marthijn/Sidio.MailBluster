namespace Sidio.MailBluster.Tests;

public sealed class LoggingHelpersTests
{
    [Fact]
    public void ObfuscateEmailAddress_WhenEmailAddressIsNull_ShouldReturnNull()
    {
        // arrange
        string? emailAddress = null;

        // act
        var result = emailAddress.ObfuscateEmailAddress();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public void ObfuscateEmailAddress_WhenEmailAddressIsEmpty_ShouldReturnEmpty()
    {
        // arrange
        var emailAddress = string.Empty;

        // act
        var result = emailAddress.ObfuscateEmailAddress();

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void ObfuscateEmailAddress_WhenEmailAddressIsNotValid_ShouldReturnEmailAddress()
    {
        // arrange
        const string EmailAddress = "richard";

        // act
        var result = EmailAddress.ObfuscateEmailAddress();

        // assert
        result.Should().Be(EmailAddress);
    }

    [Theory]
    [InlineData("info@sidio.nl", "i***o@sidio.nl")]
    [InlineData("ab@sidio.nl", "a***b@sidio.nl")]
    [InlineData("a@sidio.nl", "a***a@sidio.nl")]
    public void ObfuscateEmailAddress_WhenEmailAddressIsValid_ShouldReturnObfuscatedEmailAddress(
        string input,
        string expectedResult)
    {
        // act
        var result = input.ObfuscateEmailAddress();

        // assert
        result.Should().Be(expectedResult);
    }
}
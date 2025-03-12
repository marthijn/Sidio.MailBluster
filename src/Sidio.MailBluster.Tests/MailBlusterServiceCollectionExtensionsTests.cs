using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sidio.MailBluster.Tests;

public sealed class MailBlusterServiceCollectionExtensionsTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void AddMailBluster_WithConfiguration_ServicesRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>()!)
            .Build();

        // Act
        var result = services.AddMailBluster(configuration);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(
            x => x.ServiceType == typeof(IMailBlusterClient) && x.Lifetime == ServiceLifetime.Singleton);
    }

    [Fact]
    public void AddMailBluster_WithConfigurationSection_ServicesRegistered()
    {
        // Arrange
        var apiKey = _fixture.Create<string>();
        var url = _fixture.Create<string>();

        var services = new ServiceCollection();

        var mailBlusterConfiguration = new Dictionary<string, string>
        {
            {$"{MailBlusterOptions.SectionName}:{nameof(MailBlusterOptions.Url)}", url},
            {$"{MailBlusterOptions.SectionName}:{nameof(MailBlusterOptions.ApiKey)}", apiKey},
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(mailBlusterConfiguration!)
            .Build();

        services.Configure<MailBlusterOptions>(configuration.GetSection(MailBlusterOptions.SectionName));

        // Act
        var result = services.AddMailBluster(MailBlusterOptions.SectionName);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(x => x.ServiceType == typeof(IMailBlusterClient));
    }

    [Fact]
    public void AddMailBluster_WithOptions_ServicesRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var result = services.AddMailBluster(options => { options.Url = _fixture.Create<string>(); });

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().Contain(
            x => x.ServiceType == typeof(IMailBlusterClient) && x.Lifetime == ServiceLifetime.Singleton);
    }
}
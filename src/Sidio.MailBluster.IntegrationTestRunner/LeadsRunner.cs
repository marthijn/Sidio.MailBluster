using AutoFixture;
using FluentAssertions;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Logging;
using Sidio.MailBluster.Models;
using Sidio.MailBluster.Requests.Leads;

namespace Sidio.MailBluster.IntegrationTestRunner;

public sealed class LeadsRunner : ITestRunner
{
    private readonly Fixture _fixture = new();
    private readonly ILogger<LeadsRunner> _logger;
    private readonly MailBlusterClient _client;

    public LeadsRunner(IFlurlClientCache flurlClientCache, string apiKey, ILoggerFactory loggerFactory)
    {
        _client = new MailBlusterClient(
            flurlClientCache,
            "https://api.mailbluster.com/api/",
            apiKey,
            loggerFactory.CreateLogger<MailBlusterClient>());

        _logger = loggerFactory.CreateLogger<LeadsRunner>();
    }

    public async Task RunAsync()
    {
        _logger.LogInformation("Running tests for Leads...");

        // create a lead
        var createLeadRequest = new CreateLeadRequest
        {
            Email = $"{_fixture.Create<string>()}@example.com",
            Subscribed = _fixture.Create<bool>(),
            Tags = ["integrationtest"],
            FirstName = _fixture.Create<string>(),
            LastName = _fixture.Create<string>(),
            OverrideExisting = true,
            DoubleOptIn = _fixture.Create<bool>(),
            Fields = new Fields
            {
                { "company", _fixture.Create<string>() },
                { "phone", _fixture.Create<string>() }
            }
        };

        var createLeadResponse = await _client.CreateLeadAsync(createLeadRequest);
        createLeadResponse.Should().NotBeNull();
        createLeadResponse.Lead.Should().NotBeNull();
        createLeadResponse.Lead!.Email.Should().Be(createLeadRequest.Email);

        _logger.LogInformation("Lead {Email} created successfully", createLeadResponse.Lead.Email);

        // get lead
        var lead = await _client.GetLeadAsync(createLeadRequest.Email);
        lead.Should().NotBeNull();

        _logger.LogInformation("Lead {Email} retrieved successfully", lead!.Email);

        // update lead
        var updateLeadRequest = new UpdateLeadRequest
        {
            Email = lead.Email,
            LastName = _fixture.Create<string>(),
            Subscribed = _fixture.Create<bool>(),
        };

        var updateLeadResponse = await _client.UpdateLeadAsync(lead.Email, updateLeadRequest);
        updateLeadResponse.Should().NotBeNull();
        updateLeadResponse.Lead.Should().NotBeNull();
        updateLeadResponse.Lead!.LastName.Should().Be(updateLeadRequest.LastName);

        _logger.LogInformation("Lead {Email} updated successfully", updateLeadResponse.Lead.Email);

        // delete
        var deleteResponse = await _client.DeleteLeadAsync(updateLeadResponse.Lead.Email);
        deleteResponse.Should().NotBeNull();
        deleteResponse.LeadHash.Should().NotBeNullOrEmpty();

        _logger.LogInformation("Lead {Email} deleted successfully", updateLeadResponse.Lead.Email);
    }
}
using Sidio.MailBluster.Models;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks;

internal static class ScenarioContextExtensions
{
    private const string LeadEmail = nameof(LeadEmail);
    private const string Lead = nameof(Lead);
    private const string FieldId = nameof(FieldId);
    private const string Field = nameof(Field);

    public static ITestOutputHelper GetTestOutputHelper(this ScenarioContext scenarioContext) => scenarioContext.ScenarioContainer.Resolve<ITestOutputHelper>();

    public static void SetLead(this ScenarioContext context, Lead lead)
    {
        context.Set(lead, Lead);
    }

    public static Lead? GetLead(this ScenarioContext context) => context.TryGetValue(Lead, out Lead lead) ? lead : null;

    public static void SetLeadEmail(this ScenarioContext context, string email)
    {
        context.Set(email, LeadEmail);
    }

    public static string? GetLeadEmail(this ScenarioContext context) => context.TryGetValue(LeadEmail, out string email) ? email : null;

    public static void SetFieldId(this ScenarioContext context, long id)
    {
        context.Set(id, FieldId);
    }

    public static long? GetFieldId(this ScenarioContext context) => context.TryGetValue(FieldId, out long? id) ? id : null;

    public static void SetField(this ScenarioContext context, Field field)
    {
        context.Set(field, Field);
    }

    public static Field? GetField(this ScenarioContext context) => context.TryGetValue(Field, out Field field) ? field : null;
}
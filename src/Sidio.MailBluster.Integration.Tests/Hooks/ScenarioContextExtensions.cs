using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Hooks;

internal static class ScenarioContextExtensions
{
    public static ITestOutputHelper GetTestOutputHelper(this ScenarioContext scenarioContext) =>
        scenarioContext.ScenarioContainer.Resolve<ITestOutputHelper>();
}
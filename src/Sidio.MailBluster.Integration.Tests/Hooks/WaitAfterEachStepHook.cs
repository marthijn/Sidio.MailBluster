namespace Sidio.MailBluster.Integration.Tests.Hooks;

[Binding]
public sealed class WaitAfterEachStepHook
{
    [AfterStep]
    public Task WaitAfterEachStepAsync()
    {
        // wait 500ms to prevent rate limiting
        return Task.Delay(500);
    }
}
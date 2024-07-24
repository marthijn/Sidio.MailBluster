using Sidio.MailBluster.Integration.Tests.Context;
using Sidio.MailBluster.Integration.Tests.Drivers.Fields;
using Sidio.MailBluster.Integration.Tests.Hooks;
using Xunit.Abstractions;

namespace Sidio.MailBluster.Integration.Tests.Steps;

[Binding]
public sealed class SharedStepDefinitions
{
    private readonly Fixture _fixture = new ();
    private readonly FieldContext _fieldContext;
    private readonly LeadContext _leadContext;
    private readonly ProductContext _productContext;
    private readonly ITestOutputHelper _testOutputHelper;

    public SharedStepDefinitions(
        FieldContext fieldContext,
        LeadContext leadContext,
        ProductContext productContext,
        ScenarioContext scenarioContext)
    {
        _fieldContext = fieldContext;
        _leadContext = leadContext;
        _productContext = productContext;
        _testOutputHelper = scenarioContext.GetTestOutputHelper();
    }

    [Given(@"a random label is created")]
    public void GivenARandomLabelIsCreated()
    {
        _fieldContext.Label = FieldHelper.CreateValidFieldLabel();
        _testOutputHelper.WriteLine($"Label created: {_fieldContext.Label}");
    }

    [Given(@"a random email address is created")]
    public void GivenARandomEmailAddressIsCreated()
    {
        _leadContext.Email = $"{_fixture.Create<string>()}@example.com";
        _testOutputHelper.WriteLine($"Email created: {_leadContext.Email}");
    }

    [Given(@"a random id is created")]
    public void GivenARandomIdIsCreated()
    {
        _productContext.Id = _fixture.Create<string>();
        _testOutputHelper.WriteLine($"Id created: {_productContext.Id}");
    }
}
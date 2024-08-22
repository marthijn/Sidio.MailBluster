Sidio.MailBluster
=============
Sidio.MailBluster is an unofficial C# SDK for the [MailBluster](https://mailbluster.com) API. This package is not yet fully feature complete,
see the feature status below. If you encounter any issues or have recommendations, feel free to create an issue or a 
pull request.

[![build](https://github.com/marthijn/Sidio.MailBluster/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.MailBluster/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/Sidio.MailBluster)](https://www.nuget.org/packages/Sidio.MailBluster/)

⚠️ On the debug log level, sensitive data will be logged. It is highly recommended to
disable the debug log level on production systems and/or configure redaction. See the logging and compliance section
of this readme.

# Installation
Add [the package](https://www.nuget.org/packages/Sidio.MailBluster/) to your project.

# Usage
## Options
```json
{
  "MailBluster:Url": "https://api.mailbluster.com/api/",
  "MailBluster:ApiKey": "your-api-key"
}
```

## Dependency injection
```csharp
services.AddMailBluster();
```

## Using the client
```csharp
public class MyClass
{
    private readonly IMailBlusterClient _client;

    public MyClass(IMailBlusterClient client)
    {
        _client = client;        
    }

     public async Task GetLeads()
     {
          var lead = await _client.GetLeadAsync("noreply@sidio.nl");
    }
}
```

# Logging and compliance
The MailBluster client writes logs on the `Debug` level. The logs contain the request and response data. Sensitive information,
such as names, ip addresses and email addresses, are redacted using the `Microsoft.Extensions.Compliance.Redaction` framework. To use the
default implementation, use:
```csharp
builder.Services.AddRedaction(
    rb =>
    {
        rb.AddMailBlusterCompliance();
    });
```

Currently, there are three types of classifications, which will be redacted as follows:
- Personally identifiable information: values will be replaced with asterisks except for the first character (classification `MailBlusterDataTaxonomy.PersonallyIdentifiableInformation`)
  - Email address: will be redacted from for example `noreply@sidio.nl` to `n******@*****.**` (classification `MailBlusterDataTaxonomy.EmailAddressInformation`)
- Sensitive information: values will be replaced with asterisks except for the first character (classification `MailBlusterDataTaxonomy.SensitiveInformation`)

A fully configured example with JSON logging:
```csharp
// install packages:
// - Microsoft.Extensions.Telemetry
// - Microsoft.Extensions.Compliance.Redaction
builder.Services.AddLogging(
    x =>
    {
        x.EnableRedaction();
        
        // json logging enables logging of the request and response data
        x.ClearProviders();
        x.AddJsonConsole(option => option.JsonWriterOptions = new JsonWriterOptions
        {
            Indented = true
        });
        
        x.Services.AddRedaction(
            rb =>
            {
                rb.AddMailBlusterCompliance();
            });
    });
```


# Feature status
- [x] Manage leads
  - [x] Create
  - [x] Read
  - [x] Update
  - [x] Delete
- [x] Manage fields
    - [x] Create
    - [x] Read
    - [x] Update
    - [x] Delete
- [x] Manage products
    - [x] Create
    - [x] Read
    - [x] Update
    - [x] Delete
- [ ] Manage orders
    - [ ] Create
    - [ ] Read
    - [ ] Update
    - [ ] Delete

# Integration tests
Integration tests are available in the `Sidio.MailBluster.Integration.Tests` project. To run the tests, 
add the following configuration file `local.settings.json`:
```json
{
  "MAILBLUSTER_API_KEY": "your-api-key"
}
```

## Note:
- The integration tests will create, update and delete data in your MailBluster brand associated with the API key.
- In order to run the tests for Fields, a pro account is required.

# API Documentation
- [MailBluster API](https://mailbluster.com/docs/api)

# Troubleshooting
## Sensitive data is not redacted in the logs
Install these packages in your solution:
- `Microsoft.Extensions.Telemetry`
- `Microsoft.Extensions.Compliance.Redaction`

Ensure that the Redaction framework is configured correctly.

# Disclaimer
This package is not affiliated with MailBluster. Although we try to cover the API as much as possible using unit- 
and integration testing, we cannot guarantee that all features or error states have been implemented.
Please use at your own risk.

# Sponsors
Many thanks to [MailBluster](https://mailbluster.com/) for providing us with a pro account.
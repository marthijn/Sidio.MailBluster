Sidio.MailBluster
=============
Sidio.MailBluster is an unofficial C# SDK for the [MailBluster](https://mailbluster.com) API. Currently this package is not feature complete,
and not released as a stable version. Please use at your own risk.

[![build](https://github.com/marthijn/Sidio.MailBluster/actions/workflows/build.yml/badge.svg)](https://github.com/marthijn/Sidio.MailBluster/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/Sidio.MailBluster)](https://www.nuget.org/packages/Sidio.MailBluster/)

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

# Logging
⚠️ Be aware when the log level `Trace` is used, sensitive data (e.g. email address, names) might be logged. It is recommended
not to use trace logging in production environments.

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
- [ ] Manage products
    - [ ] Create
    - [ ] Read
    - [ ] Update
    - [ ] Delete
- [ ] Manage orders
    - [ ] Create
    - [ ] Read
    - [ ] Update
    - [ ] Delete

# Integration tests
Integration tests are available in the `Sidio.MailBluster.Integration.Tests` project. To run the tests, add the following configuration file `local.settings.json`:
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

# Disclaimer
This package is not affiliated with MailBluster. Use at your own risk.

# Sponsors
Many thanks to [MailBluster](https://mailbluster.com/) for providing us with a pro account.
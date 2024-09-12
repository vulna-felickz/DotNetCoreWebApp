# DotNetCoreWebApp

This is a sample repo demonstrating how to integrate [Security-Code-Scan action](https://github.com/marketplace/actions/securitycodescan) into CI/CD process.


# Code Coverage
dotnet new xunit -o DotNetCoreWebApp.Tests
dotnet add ./DotNetCoreWebApp.Tests/DotNetCoreWebApp.Tests.csproj reference ./DotNetCoreWebApp/DotNetCoreWebApp.csproj 

https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=linux


```
cd DotNetCoreWebApp.Tests
dotnet test --collect:"XPlat Code Coverage"
```

Yields

```
Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Attachments:
  /Users/felickz/Repos/vulna-felickz/DotNetCoreWebApp/DotNetCoreWebApp.Tests/TestResults/cdcb23b7-f35a-4ec9-8576-809a2b386f5c/coverage.cobertura.xml
Passed!  - Failed:     0, Passed:    11, Skipped:     0, Total:    11, Duration: 20 ms - /Users/felickz/Repos/vulna-felickz/DotNetCoreWebApp/DotNetCoreWebApp.Tests/bin/Debug/net6.0/DotNetCoreWebApp.Tests.dll (net6.0)
```
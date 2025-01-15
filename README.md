# Setting Up xUnit with Dependency Injection

By micheal / January 15, 2025

Many developers struggle with setting up test frameworks with dependency injection (DI). This often leads to passing object instances around manually, making it harder to introduce new services to existing test fixtures.

Using DI can help manage dependencies more effectively and maintain your tests better.

Let’s walk through how to set up xUnit tests with DI, ensuring your tests are clean and maintainable.

Essentially, you create an interface to an IServiceProvider. In the test fixture setup, you add all your services and assign the built DI provider to this interface. Then, in the tests, each set of tests gets passed a test fixture in the constructor. This fixture is used to access the ServiceProvider interface to call GetService for whatever service you need.

## Creating a Simple Service

First, define a service interface and its implementation:

```c#
public interface IMyService
{
    string GetData();
}
 
public class MyService : IMyService
{
    public string GetData()
    {
        return "Hello, World!";
    }
}
```

## Setting Up the Test Fixture

A test fixture sets up the context for your tests, including configuring the DI container:
```c#
public class TestFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; private set; }
 
    public TestFixture()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                // Register services here
                services.AddSingleton<IMyService, MyService>();
            })
            .Build();
 
        ServiceProvider = host.Services;
    }
 
    public void Dispose()
    {
        // Clean up resources if needed
    }
}
```

Next Step, setup the Nuget Package for xUnit. Add these to your Test csproj files.
The Microsoft.NET.Test.Sdk and xunit.runner.visualstudio packages are required to detect and find the Text Fixtures from the Visual Studio Test Explorer. The Hosting Existing is where we get the Dependency Injection Service.

```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Hosting" Version="9.0.1" />
  <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.12.0" />
  <PackageReference Include="xunit" Version="2.9.3" />
  <PackageReference Include="xunit" Version="2.9.2" />
  <PackageReference Include="xunit.runner.visualstudio" Version="2.8.2">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

In this fixture, we use the Host class to set up our DI container. We register IMyService with its implementation MyService.

## Writing the Test Class

Now, let’s write the test class. We’ll use the fixture to get the service instances:

```c#
public class MyServiceTests : IClassFixture<TestFixture>
{
    private readonly IMyService _myService;
 
    public MyServiceTests(TestFixture fixture)
    {
        _myService = fixture.ServiceProvider.GetService<IMyService>();
    }
 
    [Fact]
    public void GetData_ReturnsHelloWorld()
    {
        // Arrange
 
        // Act
        var result = _myService.GetData();
 
        // Assert
        Assert.Equal("Hello, World!", result);
    }
}
```

## Here’s what’s happening:

* Test Fixture: The TestFixture class sets up the DI container.
* Test Class: The MyServiceTests class uses the IClassFixture interface to indicate it requires the TestFixture.
* Service Resolution: In the constructor, we get the IMyService instance from the ServiceProvider.

## Conclusion

By setting up a test fixture and using DI, you can write clean, maintainable tests that leverage the power of dependency injection. This approach ensures your tests are well-organized and your dependencies are managed effectively.

Happy coding! If you have any questions or need further clarification, feel free to ask.

Keep experimenting and keep learning, and remember: Every day is a school day!


keywords: c# csharp dotnet Dependency Injection, Inversion of Control. Testing.Tests, Unit
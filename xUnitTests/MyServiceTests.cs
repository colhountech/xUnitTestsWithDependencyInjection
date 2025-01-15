using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitTests
{
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
}

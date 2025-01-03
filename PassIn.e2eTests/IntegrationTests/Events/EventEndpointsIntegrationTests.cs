using PassIn.IntegrationsTests.Fixtures;
using PassIn.IntegrationTests.Factories;
using System.Net;
using Testcontainers.MsSql;

namespace PassIn.IntegrationsTests.e2eTests.Events;

public class EventEndpointsIntegrationTests : IClassFixture<DbContextFixture>
{
    private readonly MsSqlContainer _msSqlContainer;
    public EventEndpointsIntegrationTests(DbContextFixture dbContextFixture)
    {
        _msSqlContainer = dbContextFixture._msSqlContainer;  
    }

    [Fact(DisplayName = "Given the endpoint '/api/Events', " +
    "When executed, Then it should return status code 200")]
    public async Task GetEvents_WhenExecuted_ShouldReturnStatusCode200()
    {
        // Arrange
        var app = new PassInWebApplicationFactory(_msSqlContainer);
        using var client = app.CreateClient();

        //Act
        var result = await client.GetAsync("/api/Events");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result?.StatusCode, HttpStatusCode.OK);
    }

}

using PassIn.Communication.Responses;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.IntegrationsTests.DataBuilders;
using PassIn.IntegrationsTests.Fixtures;
using PassIn.IntegrationTests.Factories;
using System.Net;
using System.Net.Http.Json;

namespace PassIn.IntegrationsTests.e2eTests.Events;

public class EventEndpointsIntegrationTests : IClassFixture<DbContextFixture>, IDisposable
{
    private readonly PassInWebApplicationFactory _appFactory;
    private readonly DbContextFixture _dbContextFixture;
    private readonly PassInDbContext _dbContext;
    public EventEndpointsIntegrationTests(DbContextFixture dbContextFixture)
    {
        _appFactory = new PassInWebApplicationFactory(dbContextFixture._msSqlContainer);
        _dbContextFixture = dbContextFixture;
        _dbContext = dbContextFixture.Context;
    }

    public void Dispose()
    {
        _dbContextFixture.ResetDatabase();
    }

    // # TODO  Pagination 

    [Fact(DisplayName = "Given the endpont '/api/Events' and a range of pre-existing Events" +
        "When Executed, Then it should return all the pre-existing Events"
        )]
    public async Task GetEvents_WhenExecuted_ShouldReturnAllEvents()
    {
        //Arrange 
        int quantity = 30;
        using var client = _appFactory.CreateClient();
        IEnumerable<Event> eventsFake = new EventDataBuilder().BuildMany(quantity);
        _dbContext.Events.AddRange(eventsFake);
        _dbContext.SaveChanges();

        //Act
        var result = await client.GetAsync("/api/Events");

        //Assert
        Assert.NotNull(result);
        Assert.Equal(result?.StatusCode, HttpStatusCode.OK);
        var content = await result!.Content.ReadFromJsonAsync<IList<ResponseEventJson>>();
        Assert.NotNull(content);
        Assert.NotEmpty(content);
        Assert.Equal(quantity, content.Count);
    }
}

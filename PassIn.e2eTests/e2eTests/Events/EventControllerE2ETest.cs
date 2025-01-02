using Microsoft.EntityFrameworkCore;
using PassIn.e2eTests.DataBuilders;
using PassIn.e2eTests.Fixtures;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using Xunit.Abstractions;

namespace PassIn.e2eTests.e2eTests.Events;

[Collection(nameof(DbContextCollectionFixture))]
public class EventControllerE2ETest 
{
    private readonly PassInDbContext _dbContext;
    private readonly ITestOutputHelper _output;
    public EventControllerE2ETest(ITestOutputHelper output, DbContextFixture dbContextFixture)
    {
        _dbContext = dbContextFixture.Context;
        _output = output;
    }

    [Fact]
    public void Testeee()
    {
        Event eventFake = new EventDataBuilder().Build();
        _output.WriteLine(eventFake.Title);
        _output.WriteLine(eventFake.Details);
        _output.WriteLine(eventFake.Slug);
        _output.WriteLine(eventFake.Maximum_Attendees.ToString());
        Assert.NotNull(eventFake);
    }

}

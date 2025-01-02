using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events;
using PassIn.Communication.Requests;
using PassIn.Infrastructure;
using PassIn.UnitTests.Fixtures;

namespace PassIn.UnitTests.UnitTests.UseCases.Events;
public class RegisterEventUnitTest : IClassFixture<DbContextFixture>
{
    private readonly PassInDbContext _dbContext;

    public RegisterEventUnitTest(DbContextFixture dbContextFixture)
    {
        _dbContext = dbContextFixture.Context;
    }

    [Fact(DisplayName = "Given a valid event, When executed, Then it should persist the event")]
    public void RegisterEvent_WhenEventIsValid_ShouldPersistEvent()
    {
        //Arrange
        var useCase = new RegisterEventUseCase(_dbContext);
        var requestEventJson = new RequestEventJson
        {
            Title = "Sample Event",
            Details = "Sample Event Details",
            MaximumAttendees = new Random().Next(1, 101)
        };


        //Act
        var result = useCase.Execute(requestEventJson);

        //Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        var persistedEvent = _dbContext.Events.Find(result.Id);
        Assert.NotNull(persistedEvent);
        Assert.Equal(requestEventJson.Title, persistedEvent.Title);
        Assert.Equal(requestEventJson.Details, persistedEvent.Details);
        Assert.Equal(requestEventJson.MaximumAttendees, persistedEvent.Maximum_Attendees);
    }
}

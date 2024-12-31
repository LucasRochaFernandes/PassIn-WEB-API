using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events;
using PassIn.Communication.Requests;
using PassIn.Infrastructure;

namespace PassIn.UnitTests.Events;
public class RegisterEventUnitTest
{
    private PassInDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PassInDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PassInDbContext(options);
    }

    [Fact(DisplayName ="Given a valid event, When executed, Then it should persist the event")]
    public void RegisterEvent_WhenEventIsValid_ShouldPersistEvent()
    {
        //Arrange
        var dbContext = CreateInMemoryDbContext();
        var useCase = new RegisterEventUseCase(dbContext);
        var requestEventJson = new RequestEventJson
        {
            Title = "Sample Event",
            Details = "Sample Event Details",
            MaximumAttendees = (new Random()).Next(1,101)
        };
        
        
        //Act
        var result = useCase.Execute(requestEventJson);

        //Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id); 
        var persistedEvent = dbContext.Events.Find(result.Id);
        Assert.NotNull(persistedEvent);
        Assert.Equal(requestEventJson.Title, persistedEvent.Title);
        Assert.Equal(requestEventJson.Details, persistedEvent.Details);
        Assert.Equal(requestEventJson.MaximumAttendees, persistedEvent.Maximum_Attendees);
    }
}

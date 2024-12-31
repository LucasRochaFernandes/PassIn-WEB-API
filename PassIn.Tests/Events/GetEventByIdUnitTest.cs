using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.UnitTests.Events;
public class GetEventByIdUnitTest
{
    private PassInDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PassInDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

        return new PassInDbContext(options);
    }


    [Fact(DisplayName = "Given a valid event ID, When executed, Then it should return the corresponding event")]
    public void GetEventById_WhenIdIsValid_ShouldReturnEvent()
    {
        //Arrange
        var dbContext = CreateInMemoryDbContext();
        var useCase = new GetEventByIdUseCase(dbContext);
        Guid eventId = Guid.NewGuid();
        dbContext.Events.Add(new Event
        {
            Id = eventId,
            Title = "Sample Event",
            Details = "This is a test event",
            Maximum_Attendees = 100
        });
        dbContext.SaveChanges();

        // Act
        var result = useCase.Execute(eventId);

        //Assert
        Assert.NotNull(result); 
        Assert.Equal(eventId, result.Id); 
        Assert.Equal("Sample Event", result.Title); 
        Assert.Equal("This is a test event", result.Details); 
    }

    [Fact(DisplayName = "Given an invalid id, When executed, Then it should return a type of error not found")]
    public void GetEventById_WhenIdIsInvalid_ShouldReturnNotFoundError()
    {
        //Arrange
        var dbContext = CreateInMemoryDbContext();
        var useCase = new GetEventByIdUseCase(dbContext);
        Guid eventId =  Guid.NewGuid();

        //Act & Assert
        Assert.Throws<NotFoundException>(() => useCase.Execute(eventId));
    }
}

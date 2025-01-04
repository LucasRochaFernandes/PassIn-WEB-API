using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.UnitTests.Fixtures;

namespace PassIn.UnitTests.UnitTests.UseCases.Events;
public class GetEventByIdUnitTest : IClassFixture<DbContextFixture>
{
    private readonly PassInDbContext _dbContext;

    public GetEventByIdUnitTest(DbContextFixture dbContextFixture)
    {
        _dbContext = dbContextFixture.Context;
    }


    [Fact(DisplayName = "Given a valid event ID, When executed, Then it should return the corresponding event")]
    public void GetEventById_WhenIdIsValid_ShouldReturnEvent()
    {
        //Arrange
        var useCase = new GetEventByIdUseCase(_dbContext);
        Guid eventId = Guid.NewGuid();
        _dbContext.Events.Add(new Event
        {
            Id = eventId,
            Title = "Sample Event",
            Details = "This is a test event",
            Maximum_Attendees = 100
        });
        _dbContext.SaveChanges();

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
        var useCase = new GetEventByIdUseCase(_dbContext);
        Guid eventId = Guid.NewGuid();

        //Act & Assert
        Assert.Throws<NotFoundException>(() => useCase.Execute(eventId));
    }
}

using Bogus;
using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.UnitTests.Fixtures;

namespace PassIn.UnitTests.UnitTests.UseCases.Events;
public class GetAllEventsUnitTest : IClassFixture<DbContextFixture>
{
    private readonly PassInDbContext _dbContext;
    public GetAllEventsUnitTest(DbContextFixture dbContextFixture)
    {
        _dbContext = dbContextFixture.Context;
    }

    [Fact(DisplayName = "Given multiple events registered in the database, " +
        "When executed, Then it should return all events")]
    public void GetAllEvents_WhenExecuted_ShouldReturnAllEvents()
    {
        // Arrange
        int count = 30;
        var useCase = new GetAllEventsUseCase(_dbContext);
        var faker = new Faker<Event>()
            .RuleFor(e => e.Title, f => f.Lorem.Sentence(5))
            .RuleFor(e => e.Details, f => f.Lorem.Paragraph())
            .RuleFor(e => e.Maximum_Attendees, f => f.Random.Int(1, 100));
        var events = faker.Generate(count);
        _dbContext.Events.AddRange(events);
        _dbContext.SaveChanges();


        //Act
        var result = useCase.Execute();

        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(count, result.Count);
    }
}

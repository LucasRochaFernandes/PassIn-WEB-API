using Bogus;
using Microsoft.EntityFrameworkCore;
using PassIn.Application.UseCases.Events.GetAll;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.UnitTests.Events;
public class GetAllEventsUnitTest
{
    private PassInDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PassInDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new PassInDbContext(options);
    }

    [Fact(DisplayName = "Given multiple events registered in the database, " +
        "When executed, Then it should return all events")]
    public void GetAllEvents_WhenExecuted_ShouldReturnAllEvents()
    {
        // Arrange
        int count = 30;
        var dbContext = CreateInMemoryDbContext();
        var useCase = new GetAllEventsUseCase(dbContext);
        var faker = new Faker<Event>()
            .RuleFor(e => e.Title, f => f.Lorem.Sentence(5))
            .RuleFor(e => e.Details, f => f.Lorem.Paragraph())
            .RuleFor(e => e.Maximum_Attendees, f => f.Random.Int(1, 100));
        var events = faker.Generate(count);
        dbContext.Events.AddRange(events);
        dbContext.SaveChanges();


        //Act
        var result = useCase.Execute();

        //Assert
        Assert.NotEmpty(result);
        Assert.Equal(count, result.Count); 
    }
}

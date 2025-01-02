using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure;

namespace PassIn.UnitTests.Fixtures;
public class DbContextFixture
{
    public PassInDbContext Context { get; }

    public DbContextFixture()
    {
        var options = new DbContextOptionsBuilder<PassInDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        Context = new PassInDbContext(options);
    }
}

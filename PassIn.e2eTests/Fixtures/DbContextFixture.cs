
using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure;
using Testcontainers.MsSql;

namespace PassIn.IntegrationsTests.Fixtures;

public class DbContextFixture : IAsyncLifetime
{
    public PassInDbContext Context { get; private set; }
    public MsSqlContainer _msSqlContainer { get; private set; } = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
        var options = new DbContextOptionsBuilder<PassInDbContext>()
            .UseSqlServer(_msSqlContainer.GetConnectionString())
            .Options;
        Context = new PassInDbContext(options);
        await Context.Database.MigrateAsync();
    }
    public void ResetDatabase()
    {
        Context.Database.ExecuteSqlRaw(@"
            DELETE FROM Attendees
            ");
        Context.Database.ExecuteSqlRaw(@"
            DELETE FROM Events
            ");
        Context.Database.ExecuteSqlRaw(@"
            DELETE FROM Users
            ");
        Context.SaveChanges();
    }
    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }

}
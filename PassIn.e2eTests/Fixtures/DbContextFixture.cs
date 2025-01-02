
using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure;
using Testcontainers.MsSql;

namespace PassIn.e2eTests.Fixtures;

public class DbContextFixture : IAsyncLifetime
{
    public PassInDbContext Context { get; private set; }
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder()
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
    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }

}
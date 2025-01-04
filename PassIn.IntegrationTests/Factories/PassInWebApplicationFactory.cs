using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PassIn.Infrastructure;
using Testcontainers.MsSql;

namespace PassIn.IntegrationTests.Factories;
public class PassInWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _msSqlContainer;

    public PassInWebApplicationFactory(MsSqlContainer msSqlContainer)
    {
        _msSqlContainer = msSqlContainer;
    }
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<PassInDbContext>));
            services.AddDbContext<PassInDbContext>(options =>
                options.UseSqlServer(_msSqlContainer.GetConnectionString()));
        });

        return base.CreateHost(builder);
    }
}

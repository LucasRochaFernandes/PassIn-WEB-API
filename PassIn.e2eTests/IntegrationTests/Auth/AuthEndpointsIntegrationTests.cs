using Azure;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using PassIn.IntegrationsTests.Fixtures;
using PassIn.IntegrationTests.Factories;
using System.Net;
using System.Net.Http.Json;
using Testcontainers.MsSql;


namespace PassIn.IntegrationTests.IntegrationTests.Auth;
public class AuthEndpointsIntegrationTests : IClassFixture<DbContextFixture>, IDisposable
{
    private readonly PassInDbContext _dbContext;
    private readonly PassInWebApplicationFactory _appFactory;
    private readonly DbContextFixture _dbContextFixture;

    public AuthEndpointsIntegrationTests(DbContextFixture dbContextFixture)
    {
        _dbContext = dbContextFixture.Context;
        _appFactory = new PassInWebApplicationFactory(dbContextFixture._msSqlContainer);
        _dbContextFixture = dbContextFixture;
    }

    public void Dispose()
    {
        _dbContextFixture.ResetDatabase();
    }

    [Fact(DisplayName = "Given valid credentials, When executed, Then it should return a valid token")]
    public async Task PostCredentials_WhenExecuted_ShouldReturnToken()
    {
        //Arrange 
        using var client = _appFactory.CreateClient();
        _dbContext.Users.Add(new User { 
            Username= "ExampleUsername",
            Password = BCrypt.Net.BCrypt.HashPassword("PassWord123#")
        });
        _dbContext.SaveChanges();
        var credentialsRequest = new RequestCredentials
        {
            Username= "ExampleUsername",
            Password= "PassWord123#"
        };

        //Act
        var result = await client.PostAsJsonAsync("/api/Auth", credentialsRequest);


        //Assert
        Assert.NotNull(result);
        Assert.Equal(result?.StatusCode, HttpStatusCode.OK);
        var content = await result!.Content.ReadFromJsonAsync<ResponseLogIn>();
        Assert.NotNull(content); 
        Assert.NotEmpty(content.access_token); 
    }
}

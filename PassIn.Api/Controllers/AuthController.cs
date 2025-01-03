using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Auth;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Infrastructure;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
public class AuthController : Controller
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLogIn), StatusCodes.Status200OK)]
    public IActionResult LogIn([FromBody] RequestCredentials request, 
        [FromServices] PassInDbContext dbContext,
        [FromServices] IConfiguration configuration)
    {
        var useCase = new LogInUseCase(dbContext, configuration);
        var result = useCase.Execute(request);
        return Ok(result);
    }
}

using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Users.Register;
using PassIn.Communication.Requests;
using PassIn.Infrastructure;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
public class UsersController : Controller
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestUserJson request, [FromServices] PassInDbContext dbContext)
    {
        var useCase = new RegisterUserUseCase(dbContext);
        var result = useCase.Execute(request);
        return Created(string.Empty, result);
    }
}

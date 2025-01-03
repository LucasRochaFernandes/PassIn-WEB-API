using PassIn.Application.Helpers;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Users.Register;
public class RegisterUserUseCase
{
    private readonly PassInDbContext _dbContext;
    public RegisterUserUseCase(PassInDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ResponseRegisterUser Execute(RequestUserJson request)
    {
        var usernameExists = _dbContext.Users.FirstOrDefault(usr => usr.Username.Equals(request.Username));
        if (usernameExists is not null)
        {
            throw new ConflictException("Username already exists");
        }
        var entity = new User
        {
            Username = request.Username,
            Password = Cryptography.GetHash(request.Password),
        };
        var userCreated = _dbContext.Users.Add(entity);
        _dbContext.SaveChanges();
        return new ResponseRegisterUser { Id =  entity.Id };
    }
}

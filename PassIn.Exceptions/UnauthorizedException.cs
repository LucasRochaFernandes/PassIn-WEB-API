namespace PassIn.Exceptions;
public class UnauthorizedException : PassInException
{
    public UnauthorizedException(string message) : base(message) { }
}

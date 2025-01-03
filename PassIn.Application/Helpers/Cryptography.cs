namespace PassIn.Application.Helpers;

using BCrypt.Net;

public static class Cryptography
{
    public static string GetHash(string value)
    {
        var passwordHash = BCrypt.HashPassword(value);
        return passwordHash;
    }
    public static bool CheckHashStringMatch(string value, string hash)
    {
        return BCrypt.Verify(value, hash);
    }
}

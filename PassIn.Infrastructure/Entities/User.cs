﻿namespace PassIn.Infrastructure.Entities;
public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; }
    public string Password { get; set; }

}
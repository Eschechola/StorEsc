﻿namespace StorEsc.Application.DTOs;

public abstract class AccountDTO : BaseDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
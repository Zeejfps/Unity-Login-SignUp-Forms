﻿using System.Threading;
using System.Threading.Tasks;

public interface ISignUpService
{
    Task SignUpAsync(string email, string username, string password, CancellationToken cancellationToken = default);
}
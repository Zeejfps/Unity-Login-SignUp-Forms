﻿using System.Threading.Tasks;

namespace Tests
{
    public sealed class TestLoginService : ILoginService
    {
        public Task LoginAsync(string email, string password)
        {
            return Task.Delay(2000);
        }
    }
}
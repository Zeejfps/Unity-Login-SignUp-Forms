using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public sealed class TestSignUpService : ISignUpService
    {
        public Task SignUpAsync(string email, string username, string password)
        {
            return Task.Delay(2000);
        }

        public IEnumerable<IPasswordRequirement> GetPasswordRequirements()
        {
            return new IPasswordRequirement[]
            {
                new MinLengthPasswordRequirement(3),
                new MinDigitsPasswordRequirement(1),
                new MinUpperCaseCharactersPasswordRequirement(1),
                new MinLowerCaseCharactersPasswordRequirement(1),
                new MinSpecialCharactersPasswordRequirement(1)
            };
        }
    }
}
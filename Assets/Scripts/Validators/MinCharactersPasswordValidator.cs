using System;
using System.Linq;

namespace Validators
{
    public abstract class MinCharactersPasswordValidator : IPasswordValidator
    {
        public string Description { get; protected set; }

        protected int Min { get; }
        private Func<char, bool> Predicate { get; }

        protected MinCharactersPasswordValidator(int min, Func<char, bool> predicate)
        {
            Min = min;
            Predicate = predicate;
        }
    
        public bool Validate(string password)
        {
            return password.Count(Predicate.Invoke) >= Min;
        }
    }
}
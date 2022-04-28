using System;

namespace Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
        
        public override string Message => $"Validation message: {base.Message}";
    }
}
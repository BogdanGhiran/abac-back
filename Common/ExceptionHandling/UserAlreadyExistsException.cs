using System;

namespace Common.ExceptionHandling
{
    public class UserAlreadyExistsException : Exception
    {
        public new string Message { get; set; }

        public UserAlreadyExistsException()
        {
        }
    }
}

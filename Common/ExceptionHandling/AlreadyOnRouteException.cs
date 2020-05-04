using System;

namespace Common.ExceptionHandling
{
    public class AlreadyOnRouteException : Exception
    {
        public new string Message { get; set; }

        public AlreadyOnRouteException()
        {
        }
    }
}

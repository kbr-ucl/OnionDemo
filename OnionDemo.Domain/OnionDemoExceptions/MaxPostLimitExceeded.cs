using System;

namespace OnionDemo.Domain.OnionDemoExceptions
{
    public class MaxPostLimitExceeded : Exception
    {
        public MaxPostLimitExceeded(string message) : base(message)
        {
        }
    }
}
using System;

namespace Blog.Domain.BlogExceptions
{
    public class MaxPostLimitExceeded : Exception
    {
        public MaxPostLimitExceeded(string message) : base(message)
        {
        }
    }
}
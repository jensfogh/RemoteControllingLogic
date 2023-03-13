using System;
namespace inCommodities.test.logic.Exceptions
{
    public class InvalidDeltaException : Exception
    {
        public InvalidDeltaException(string message)
            : base(message)
        {
        }
    }
}


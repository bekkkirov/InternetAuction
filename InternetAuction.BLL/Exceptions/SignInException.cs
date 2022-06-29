using System;
using System.Runtime.Serialization;

namespace InternetAuction.BLL.Exceptions
{
    /// <summary>
    /// Represents a sign-in exception
    /// </summary>
    [Serializable]
    public class SignInException : Exception
    {
        public SignInException()
        {
        }

        public SignInException(string message) : base(message)
        {
        }

        public SignInException(string message, Exception inner) : base(message, inner)
        {
        }

        protected SignInException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
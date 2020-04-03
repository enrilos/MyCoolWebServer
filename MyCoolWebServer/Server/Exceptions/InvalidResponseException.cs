namespace MyCoolWebServer.Server.Exceptions
{
    using System;

    public class InvalidResponseException : Exception
    {
        private const string InvalidRequestMessage = "View responses require a status code below 300 and above 400 (inclusive).";

        public static object ThrowFromInvalidResponse()
            => throw new InvalidResponseException(InvalidRequestMessage);

        public InvalidResponseException(string message)
            : base(message)
        {
        }
    }
}

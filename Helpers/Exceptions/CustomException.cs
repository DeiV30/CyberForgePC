namespace  cyberforgepc.Helpers.Exceptions
{
    using System;

    public abstract class CustomException : Exception
    {
        public int ExceptionCode { get; set; }

        public CustomException(string message) : base(message) { }
    }

    #region "Administrative Exceptions"

    public class MessageException : CustomException
    {
        public MessageException(string message) : base(message) => this.ExceptionCode = 4091;
    }

    #endregion
}

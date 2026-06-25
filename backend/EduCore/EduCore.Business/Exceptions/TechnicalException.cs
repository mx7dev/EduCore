namespace EduCore.Business.Exceptions
{
    public class TechnicalException : Exception
    {
        public string Code { get; }
        public string TransactionId { get; }

        public TechnicalException(string code, string message, Exception? innerException = null)
            : base(message, innerException)
        {
            Code = code;
            TransactionId = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }
    }
}

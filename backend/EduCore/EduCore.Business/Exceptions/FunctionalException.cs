namespace EduCore.Business.Exceptions
{
    public class FunctionalException : Exception
    {
        public string Code { get; }
        public string TransactionId { get; }

        public FunctionalException(string code, string message)
            : base(message)
        {
            Code = code;
            TransactionId = DateTime.Now.ToString("ddMMyyyyHHmmssfff");
        }
    }
}

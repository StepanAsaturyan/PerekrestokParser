namespace PerekrestokParser.Common
{
    public class ParserException : Exception
    {
        private readonly string _errorMessage;
        private readonly string _error;

        public string ErrorMessage { get => _errorMessage; }
        public string Error { get => _error; }
        public ParserException(string errorMessage, string errorCode)
        {
            _errorMessage = errorMessage;
            _error = errorCode;
        }
    }
}

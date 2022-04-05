using PerekrestokParser.Interfaces;

namespace PerekrestokParser.ParserSettings
{
    public class PerekrestokSettings : IPerekrestokSettings
    {
        private readonly string _url;
        public string BaseUrl => _url;

        public PerekrestokSettings(string url)
        {
            _url = url;
        }

        public string Prefix => throw new System.NotImplementedException();
    }
}

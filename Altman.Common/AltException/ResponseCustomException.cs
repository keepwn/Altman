namespace Altman.Common.AltException
{
    public class ResponseCustomException : CustomException
    {
        private string _html;
        public string Html
        {
            get { return this._html; }
        }
        public ResponseCustomException(string title,string message, string html) : base(title,message)
        {
            this._html = html;
        }
        public ResponseCustomException(string title,string message, System.Exception inner) : base(title,message, inner) { }
    }
}

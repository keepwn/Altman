namespace Altman.Common.AltException
{
    public class RequestCustomException:CustomException
    {
        private int _statusCode;
        public int StatusCode
        {
            get { return this._statusCode; }
        }
        public RequestCustomException(string title, string message) : base(title,message) { }
        public RequestCustomException(string title, int statusCode,string message): base(title,message)
        {
            this._statusCode = statusCode;
        }
        public RequestCustomException(string title, string message, System.Exception inner) : base(title, message, inner) { }
    }
}

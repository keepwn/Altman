using System;

namespace Altman.Common.AltException
{
    public class CustomException : System.Exception
    {
        private string _title;
        public string Title
        {
            get { return _title; }
        }
        public CustomException() { }
        public CustomException(string title,string message) : base(message)
        {
            _title = title;
        }
        public CustomException(string title, string message, System.Exception inner) : base(message, inner)
        {
            _title = title;
        }
    }
}

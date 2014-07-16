namespace Altman.Common.AltException
{
    public class LocalCustomException:CustomException
    {
        public LocalCustomException(string title,string message) : base(title,message){}
        public LocalCustomException(string title, string message, System.Exception inner) : base(title,message,inner) { }
    }
}

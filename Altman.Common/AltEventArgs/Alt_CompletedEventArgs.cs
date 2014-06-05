using System;

namespace Altman.Common.AltEventArgs
{
    public class AltCompletedEventArgs : EventArgs
    {
        private Exception m_Error;
        private object m_Result;
        private object m_userState;

        public object Result
        {
            get { return this.m_Result; }
        }
        public Exception Error
        {
            get { return this.m_Error; }
        }
        public object UserState
        {
            get { return this.m_userState; }
        }

        public AltCompletedEventArgs(object result, Exception exception): base()
        {
            this.m_Result = result;
            this.m_Error = exception;
        }
        public AltCompletedEventArgs(object result, Exception exception,object userState)
            : base()
        {
            this.m_Result = result;
            this.m_Error = exception;
            this.m_userState = userState;
        }
    }
}
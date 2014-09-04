namespace Altman.Common.AltEventArgs
{
    public class AltProgressChangedEventArgs : System.EventArgs
    {
        private int m_progressPercentage;
        private object m_userState;
        public int ProgressPercentage
        {
            get { return this.m_progressPercentage; }
        }
        public object UserState
        {
            get { return this.m_userState; }
        }

        public AltProgressChangedEventArgs(int progressPercentage): base()
        {
            this.m_progressPercentage = progressPercentage;
        }
        public AltProgressChangedEventArgs(int progressPercentage, object userState)
            : base()
        {
            this.m_progressPercentage = progressPercentage;
            this.m_userState = userState;
        }
    }
}
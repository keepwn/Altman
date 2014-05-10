using System;

namespace Altman.Common.AltEventArgs
{
    public class ProxyReceiveEventArgs : EventArgs
    {
        private string _data;

        public ProxyReceiveEventArgs(string data): base()
        {
            _data = data;
        }
        public string Data
        {
            get { return _data; }
        }
    }
}

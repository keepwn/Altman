using System;

namespace Altman.Common.AltEventArgs
{
    public class AltUploadProgressChangedEventArgs : EventArgs
    {
        private long bytesSent;
        private long totalBytesToSend;
        public long BytesSent
        {
            get
            {
                return this.bytesSent;
            }
        }
        public long TotalBytesToSend
        {
            get
            {
                return this.totalBytesToSend;
            }
        }
        public AltUploadProgressChangedEventArgs(long bytesSent, long totalBytesToSend)
        {
            this.bytesSent = bytesSent;
            this.totalBytesToSend = totalBytesToSend;
        }
    }
}

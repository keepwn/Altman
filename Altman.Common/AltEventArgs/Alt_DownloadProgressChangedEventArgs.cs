
namespace Altman.Common.AltEventArgs
{
    public class AltDownloadProgressChangedEventArgs : System.EventArgs
    {
        private long bytesReceived;
        private long totalBytesToReceive;
        public long BytesReceived
        {
            get
            {
                return this.bytesReceived;
            }
        }
        public long TotalBytesToReceive
        {
            get
            {
                return this.totalBytesToReceive;
            }
        }
        public AltDownloadProgressChangedEventArgs(long bytesReceived, long totalBytesToReceive)
        {
            this.bytesReceived = bytesReceived;
            this.totalBytesToReceive = totalBytesToReceive;
        }
    }
}

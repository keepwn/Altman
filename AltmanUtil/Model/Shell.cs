namespace Altman.Model
{
    public class Shell
    {
        public string Id { get; set; }//保留字段
        public string TargetId { get; set; }
        public string TargetLevel { get; set; }
        public string Status { get; set; }

        public string ShellUrl { get; set; }
        public string ShellPwd { get; set; }
        public string ShellType { get; set; }
        public string ShellExtraString { get; set; }

        public string ServerCoding { get; set; }
        public string WebCoding { get; set; }

        public int TimeOut { get; set; }

        public string Area { get; set; }
        public string Remark { get; set; }
        public string AddTime { get; set; }
    }
}

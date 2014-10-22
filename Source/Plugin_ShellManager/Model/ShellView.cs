using Altman.Webshell.Model;

namespace Plugin_ShellManager.Model
{
	public class ShellView : Shell
	{
		public ShellView(Shell shell)
		{
			this.Id = shell.Id;
			this.TargetId = shell.TargetId;
			this.TargetLevel = shell.TargetLevel;
			this.Status = shell.Status;

			this.ShellUrl = shell.ShellUrl;
			this.ShellPwd = shell.ShellPwd;
			this.ShellType = shell.ShellType;
			this.ShellExtraString = shell.ShellExtraString;

			this.ServerCoding = shell.ServerCoding;
			this.WebCoding = shell.WebCoding;

			this.TimeOut = shell.TimeOut;

			this.Area = shell.Area;
			this.Remark = shell.Remark;
			this.AddTime = shell.AddTime;
		}
	}
}
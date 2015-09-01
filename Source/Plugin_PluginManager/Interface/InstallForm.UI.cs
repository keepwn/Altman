using Eto.Drawing;
using Eto.Forms;

namespace Plugin_PluginManager.Interface
{
	partial class InstallForm
	{
		void Init()
		{
			//_labelDownload
			_labelDownload = new Label();

			//_progressBarDownload
			_progressBarDownload = new ProgressBar();

			var layout = new DynamicLayout {Padding = new Padding(5, 5), Spacing = new Size(5, 5)};
			layout.AddRow(_labelDownload);
			layout.AddRow(_progressBarDownload);

			Content = layout;
			Icon = Application.Instance.MainForm.Icon;
            WindowStyle = WindowStyle.None;
		}

		private ProgressBar _progressBarDownload;
		private Label _labelDownload;
	}
}

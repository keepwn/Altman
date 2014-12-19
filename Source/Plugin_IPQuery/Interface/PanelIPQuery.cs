using System;
using Altman.Plugin;
using Altman.Plugin.Interface;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_IPQuery.Interface
{
	public partial class PanelIPQuery : Panel
	{
		private IHost _host;
		public PanelIPQuery(IHost host, PluginParameter data)
		{
			_host = host;
			Init();
		}

		void _buttonOffline_Click(object sender, System.EventArgs e)
		{
			try
			{
				var ip = _textBoxInput.Text;
				if (!string.IsNullOrWhiteSpace(ip))
				{
					_lableIp.Text = "";
					var query = new IPOfflineQuery();
					var result = query.Find(ip);
					foreach (var s in result)
					{
						_lableIp.Text += s + " ";
					}
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		void _buttonOnline_Click(object sender, System.EventArgs e)
		{
			var ip = _textBoxInput.Text;
			if (!string.IsNullOrWhiteSpace(ip))
			{
				//clear
				_lableIp.Text = "";
				_labelA.Text = "";
				_labelB.Text = "";
				_labelC.Text = "";

				var query = new IPOnlineQuery();
				query.OnQueryCompleted += client_UploadDataCompleted;

				query.GetIpInfo(ip, "17mon");
				query.GetIpInfo(ip, "taobao");
				query.GetIpInfo(ip, "tencent");
				query.GetIpInfo(ip, "sina");
			}
		}

		void client_UploadDataCompleted(object sender, IPOnlineQuery.QueryCompletedEventArgs e)
		{
			var result =  e.Result!= "" ? e.Result : "empty";
			switch (e.Token)
			{
				case "taobao":
					_labelA.Text = result;
					break;
				case "tencent":
					_labelB.Text = result;
					break;
				case "sina":
					_labelC.Text = result;
					break;
				case "17mon":
					_lableIp.Text = result;
					break;
			}
		}
	}
}

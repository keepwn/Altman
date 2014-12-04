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

		async void _buttonOnline_Click(object sender, System.EventArgs e)
		{
			var ip = _textBoxInput.Text;
			if (!string.IsNullOrWhiteSpace(ip))
			{
				Get17mon(ip);
				GetTaobao(ip);
				GetTencent(ip);
				GetSina(ip);
			}
		}

		private async void Get17mon(string ip)
		{
			var query = new IPOnlineQuery();
			var result = await query.GetIpInfo(ip, "17mon");
			_lableIp.Text = result != "" ? result : "empty";
		}

		private async void GetTaobao(string ip)
		{
			var query = new IPOnlineQuery();
			var result = await query.GetIpInfo(ip, "taobao");
			_labelA.Text = result != "" ? result : "empty";
		}

		private async void GetTencent(string ip)
		{
			var query = new IPOnlineQuery();
			var result = await query.GetIpInfo(ip, "tencent");
			_labelB.Text = result != "" ? result : "empty";
		}

		private async void GetSina(string ip)
		{
			var query = new IPOnlineQuery();
			var result = await query.GetIpInfo(ip, "sina");
			_labelC.Text = result != "" ? result : "empty";
		}
	}
}

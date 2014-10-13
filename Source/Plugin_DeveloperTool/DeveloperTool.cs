using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Drawing;
using Eto.Forms;
using PluginFramework;

namespace Plugin_DeveloperTool
{
	public class DeveloperTool : Form
	{
		private IHost _host;
		public DeveloperTool(IHost host, PluginParameter data)
		{
			Init();

			_host = host;

			LoadServices();
		}

		private ComboBox _comboBoxServices;
		private TextArea _textAreaInfo;

		private TextArea _textAreaResult;
		void Init()
		{
			_comboBoxServices = new ComboBox();
			_comboBoxServices.SelectedIndexChanged += _comboBoxServices_SelectedIndexChanged;

			_textAreaInfo = new TextArea{Size=new Size(-1,200)};
			_textAreaInfo.Enabled = true;
			_textAreaResult = new TextArea();

			var layout = new DynamicLayout {Padding = new Padding(10, 10)};
			layout.AddSeparateRow(_comboBoxServices);
			layout.AddSeparateRow(_textAreaInfo);
			layout.AddSeparateRow(_textAreaResult);

			Content = layout;
			Title = "Developer Tool";
			Size = new Size(400, 400);
		}

		void _comboBoxServices_SelectedIndexChanged(object sender, EventArgs e)
		{
			var index = _comboBoxServices.SelectedIndex;
			if (index >= 0)
			{
				/*
				 * void Write(
				 * bool value)
				 */
				var builder = new StringBuilder();
				var selectItem = (_comboBoxServices.Items[index] as ListItem);
				var service = selectItem.Tag;

				var serviceName = selectItem.Text;
				var ret = service;
				var args = service.GetType().GetGenericArguments();

				builder.Append(serviceName+"(\n");

				var arguments = string.Join(
					",\n",
					args.Select((r, i) => "\t" + r.FullName + " func" + (i + 1).ToString()));
				builder.Append(arguments + "\n)");

				_textAreaInfo.Text = builder.ToString();
			}
		}

		private Func<string, bool, string> _function;
		public void LoadServices()
		{
			var names = PluginServiceProvider.GetServiceNames();
			foreach (var name in names)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetService<object>(name)
				};
				_comboBoxServices.Items.Add(item);
			}
		}
	}
}

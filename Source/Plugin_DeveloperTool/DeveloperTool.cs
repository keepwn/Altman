using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
			_textAreaInfo.Enabled = false;
			_textAreaResult = new TextArea();
			_textAreaResult.Text = "If you wanna call one service, you can do like this:\n" +
									"Func<string, bool, string> ToBase64 = null;\n" +
									"ToBase64 = PluginServiceProvider.GetService<Func<Shell, string, string[], byte[]>>(\"ToBase64\");\n" +
									"var result = ToBase64(\"Test\",True);\n" +
									"//result=\"VGVzdA==\"";
			_textAreaResult.Enabled = false;

			var layout = new DynamicLayout {Padding = new Padding(10, 10)};

			layout.AddSeparateRow(
				new Label {Text = "The Registered Services", VerticalAlign = VerticalAlign.Middle},
				_comboBoxServices);
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
				var selectItem = (_comboBoxServices.Items[index] as ListItem);

				var builder = new StringBuilder();
				var serviceStore = (PluginServiceProvider.ServiceStore)selectItem.Tag;

				if (serviceStore.Assembly == null) return;
				var serviceName = selectItem.Text;
				var assembly= serviceStore.Assembly;
				MethodInfo method = null;
				foreach (var type in assembly.GetTypes())
				{
					var mes = type.GetMethods().FirstOrDefault(r => r.Name == serviceName);
					if (mes != null)
					{
						method = mes;
						break;
					}
				}
				if (method != null)
				{
					var returnType = method.ReturnType.ToString();
					var args = method.GetParameters();

					builder.Append(returnType + " " + serviceName + "(\n");

					var arguments = string.Join(
						",\n",
						args.Select((r, i) => "\t" + r.ParameterType +" "+ r.Name));
					builder.Append(arguments + "\n)");

					_textAreaInfo.Text = builder.ToString();
				}
			}
		}

		public void LoadServices()
		{
			var names = PluginServiceProvider.GetServiceNames();
			foreach (var name in names)
			{
				var item = new ListItem
				{
					Text = name,
					Tag = PluginServiceProvider.GetServiceStore(name)
				};
				_comboBoxServices.Items.Add(item);
			}
		}
	}
}

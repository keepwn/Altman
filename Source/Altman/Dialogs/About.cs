using System;
using System.Reflection;
using Altman.Desktop.CustomControls;
using Altman.Desktop.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Dialogs
{
	public class About : Dialog
    {
        public About()
        {
			this.Title = AltStrRes.AboutAltman;
            this.Resizable = true;
	        this.Size = new Size(500, 500);

            var layout = new DynamicLayout { Padding = new Padding(10, 5), Spacing = new Size(5, 5) };

            layout.AddCentered(new ImageView
            {
				Image = Icons.AltmanPng
			}, true, true);

            layout.Add(new Label
            {
                Text = "Altman",
                Font = new Font(SystemFont.Bold, 20),
                HorizontalAlign = HorizontalAlign.Center
            });
			//Version
			layout.Add(new Label
			{
				Text = "Version",
				Font = new Font(SystemFont.Bold, 10),
			});
	        layout.Add(new Label
	        {
		        Text = "    " + AppEnvironment.AppVersion,
		        Font = new Font(SystemFont.Default, 10),
	        });

			//License
			layout.Add(new Label
			{
				Text = "License",
				Font = new Font(SystemFont.Bold, 10),
			});
	        layout.Add(new Label
	        {
		        Text = "    " + "Released under the GNU General Public License v2",
		        Font = new Font(SystemFont.Default, 10),
	        });

			//Copyright
			layout.Add(new Label
			{
				Text = "Copyright",
				Font = new Font(SystemFont.Bold, 10),
			});
            layout.Add(new Label
            {
				Text = "    " + "(C) 2013-2014 by KeePwn",
                Font = new Font(SystemFont.Default, 10),
            });

            //layout.Add(ReadOnly());

            layout.AddCentered(CloseButton());

            Content = layout;
        }

        Control ReadOnly()
        {
            var line = Environment.NewLine;
            string msg =
                   "免责声明:" +line+
                   "本程序仅供学习和研究!" +line+
                   "请使用者遵守国家相关法律法规!" +line+
                   "由于使用不当造成的后果本人不承担任何责任!";
            var control = new TextArea { Text = msg, ReadOnly = true };
            return control;
        }

        Control CloseButton()
        {
            var button = new Button
            {
                Text = AltStrRes.Close
            };
            DefaultButton = button;
            //AbortButton = button;
            button.Click += delegate
            {
                Close();
            };
            return button;
        }
    }
}

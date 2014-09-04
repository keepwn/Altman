using System;
using System.Reflection;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Desktop.Dialogs
{
    public class About : Dialog
    {
        public About()
        {
            this.Title = "About Altman";
            this.Resizable = true;

            var layout = new DynamicLayout { Padding = new Padding(20, 5), Spacing = new Size(10, 10) };

            layout.AddCentered(new ImageView
            {
                Image = Icon.FromResource("Altman.Desktop.altman.ico", GetType())
            }, padding: null, xscale: true, yscale: true);

            layout.Add(new Label
            {
                Text = "Altman",
                Font = new Font(SystemFont.Bold, 20),
                HorizontalAlign = HorizontalAlign.Center
            });

            var version = Assembly.GetEntryAssembly().GetName().Version;
            layout.Add(new Label
            {
                Text = string.Format("Version {0}", version),
                Font = new Font(SystemFont.Default, 10),
                HorizontalAlign = HorizontalAlign.Center
            });

            layout.Add(new Label
            {
                Text = "Copyright 2013-2014 by KeePwn",
                Font = new Font(SystemFont.Default, 10),
                HorizontalAlign = HorizontalAlign.Center
            });

            layout.Add(ReadOnly());

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
                Text = "Close"
            };
            DefaultButton = button;
            AbortButton = button;
            button.Click += delegate
            {
                Close();
            };
            return button;
        }
    }
}

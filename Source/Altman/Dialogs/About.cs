using Altman.Resources;
using Eto.Drawing;
using Eto.Forms;

namespace Altman.Dialogs
{
	public class About : Dialog
    {
        public About()
        {
			this.Title = AltStrRes.AboutAltman;
            this.Resizable = false;
	        //this.Size = new Size(300, 300);

            var layout = new DynamicLayout { Padding = new Padding(0, 0), Spacing = new Size(5, 5) };

	        layout.AddCentered(new ImageView
	        {
		        Image = Icons.AltmanAboutPng
            }, padding: null, xscale: true, yscale: false);

			//Version
			layout.Add(new Label
			{
				Text = "Version",
				Font = new Font(SystemFont.Bold, 10),
			});
	        layout.Add(new Label
	        {
		        Text = "  " + AppEnvironment.AppVersion,
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
		        Text = "  " + "Released under the GNU General Public License v2",
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
				Text = "  " + "(C) 2013-2014 by KeePwn",
                Font = new Font(SystemFont.Default, 10),
            });

            layout.AddCentered(CloseButton());

            Content = layout;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;

namespace Plugin_IPQuery.Interface
{
	partial class PanelIPQuery
	{
		private void Init()
		{
			_textBoxInput = new TextBox { Size = new Size(200, -1) };
			_buttonOnline = new Button { Text = "Online Query", Size = new Size(150, -1) };
			_buttonOnline.Click += _buttonOnline_Click;
			_buttonOffline = new Button { Text = "Offline Query", Size = new Size(150, -1) };
			_buttonOffline.Click += _buttonOffline_Click;

			var cellPadding = new Padding(1, 1, 1, 1);
			var cellPaddingLeft = new Padding(1, 1, 0, 1);
			var cellPaddingRight = new Padding(0, 1, 1, 1);

			_labelA = ColumnContent("");
			_labelB = ColumnContent("");
			_labelC = ColumnContent("");
			_labelD = ColumnContent("");
			_labelE = ColumnContent("");
			_labelF = ColumnContent("");

			_otherTable = new TableLayout(3, 5); //{ BackgroundColor = Colors.Black };
			//row1
			_otherTable.Add(new Panel { Content = ColumnTitle("From Taobao"), Padding = new Padding(1, 0, 0, 0) }, 0, 1, true, false);
			_otherTable.Add(new Panel { Content = ColumnTitle("From Tencent") }, 1, 1, true, false);
			_otherTable.Add(new Panel { Content = ColumnTitle("From Sina"), Padding = new Padding(0, 0, 1, 0) }, 2, 1, true, false);
			//row2
			_otherTable.Add(new Panel { Content = _labelA, Padding = cellPaddingLeft }, 0, 2, true, true);
			_otherTable.Add(new Panel { Content = _labelB, Padding = cellPaddingLeft }, 1, 2, true, true);
			_otherTable.Add(new Panel { Content = _labelC, Padding = cellPadding }, 2, 2, true, true);
			//row3
			//_otherTable.Add(new Panel { Content = ColumnTitle("From IPB"), Padding = new Padding(1, 0, 0, 0) }, 0, 3, true, false);
			//_otherTable.Add(new Panel { Content = ColumnTitle("From Cz88") }, 1, 3, true, false);
			//_otherTable.Add(new Panel { Content = ColumnTitle("From MaxMind GeoLite2"), Padding = new Padding(0, 0, 1, 0) }, 2, 3, true, false);
			//row4
			//_otherTable.Add(new Panel { Content = _labelD, Padding = cellPaddingLeft }, 0, 4, true, true);
			//_otherTable.Add(new Panel { Content = _labelE, Padding = cellPaddingLeft }, 1, 4, true, true);
			//_otherTable.Add(new Panel { Content = _labelF, Padding = cellPadding }, 2, 4, true, true);

			_mainTable = new TableLayout(1, 2) {BackgroundColor = Colors.LightGrey};
			_mainTable.Add(new Panel
			{
				Content = _lableIp = new Label
				{
					Text = "",
					Font = new Font(FontFamilies.Sans, 20),
					VerticalAlign = VerticalAlign.Middle,
					HorizontalAlign = HorizontalAlign.Center,
					Size = new Size(-1, 100),
					BackgroundColor = Colors.White
				},
				Padding = cellPadding
			}, 0, 0, true, false);
			_mainTable.Add(_otherTable, 0, 1, true, true);

			var layout = new DynamicLayout { Padding = new Padding(10, 10), Spacing = new Size(5, 5) };
			layout.BeginVertical();
			layout.BeginHorizontal();
			layout.AddCentered(_textBoxInput);
			layout.Add(_buttonOnline);
			layout.Add(_buttonOffline);
			layout.Add(null);
			layout.AddCentered(new Label { Text = "Data from 17mon.cn" });
			layout.EndHorizontal();
			layout.EndVertical();
			layout.Add(_mainTable);
			Content = layout;
		}

		Label ColumnTitle(string title)
		{
			var control = new Label
			{
				Text = title,
				VerticalAlign = VerticalAlign.Middle,
				HorizontalAlign = HorizontalAlign.Center,
				Size = new Size(50, 20),
				BackgroundColor = Colors.Gray
			};
			return control;
		}
		Label ColumnContent(string title)
		{
			var control = new Label
			{
				Text = title,
				VerticalAlign = VerticalAlign.Middle,
				HorizontalAlign = HorizontalAlign.Center,
				Size = new Size(50, 50),
				BackgroundColor = Colors.White
			};
			return control;
		}

		private TextBox _textBoxInput;
		private Button _buttonOnline;
		private Button _buttonOffline;
		private Label _labelA;
		private Label _labelB;
		private Label _labelC;
		private Label _labelD;
		private Label _labelE;
		private Label _labelF;
		//private Label _lableTitleA;
		//private Label _lableTitleB;
		//private Label _lableTitleC;
		//private Label _lableTitleD;
		//private Label _lableTitleE;
		//private Label _lableTitleF;
		private Label _lableIp;
		private TableLayout _mainTable;
		private TableLayout _otherTable;
	}
}

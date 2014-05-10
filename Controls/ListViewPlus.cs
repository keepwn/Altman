using System;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class ListViewPlus : ListView
    {
        public class EditSubItemEventArgs : EventArgs
        {
            //columns and rows
            private readonly string _oldLabel;
            private readonly string _label;
            private readonly int _itemRowIndex;
            private readonly int _itemColumnIndex;
            private readonly string _userSate;
            private readonly bool _isRemoveIfCancel;
            
            public string Label
            {
                get
                {
                    return this._label;
                }
            }

            public bool IsCancelEdit { get; set; }
            

            public string OldLabel
            {
                get { return _oldLabel; }
            }

            public int ItemRowIndex
            {
                get { return _itemRowIndex; }
            }

            public int ItemColumnIndex
            {
                get { return _itemColumnIndex; }
            }

            public string UserSate
            {
                get { return _userSate; }
            }

            public bool IsRemoveIfCancel
            {
                get { return _isRemoveIfCancel; }
            }

            public EditSubItemEventArgs(string oldLabel, string label,int itemRowIndex, int itemColumnIndex, string userSate, bool isRemoveIfCancel)
            {
                _label = label;
                _oldLabel = oldLabel;
                _itemRowIndex = itemRowIndex;
                _itemColumnIndex = itemColumnIndex;
                _userSate = userSate;
                _isRemoveIfCancel = isRemoveIfCancel;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ListViewPlus()
        {
            InitializeComponent();
            this.View = View.Details;
            this.MultiSelect = false;
            this.GridLines = true;
            this.FullRowSelect = true;
        }

        private TextBox _tmpTextBox;
       
        public event EventHandler<EditSubItemEventArgs> AfterEditSubItem;
        public event EventHandler<EditSubItemEventArgs> EditSubItemCompleted;
        public void EditSubItem(ListViewItem item, int index, string userSate, bool isRemoveIfCancel)
        {
            try
            {
                if (item == null)
                {
                    throw new Exception("待编辑的项为NULL");
                }
                if (item.SubItems.Count < index + 1)
                {
                    throw new Exception("待编辑的子项为NULL");
                }
                CreateOneTextBox(item.Index, index, userSate,isRemoveIfCancel);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreateOneTextBox(int rowIndex, int columnIndex, string userSate, bool isRemoveIfCancel)
        {
            ListViewItem.ListViewSubItem subItem = this.Items[rowIndex].SubItems[columnIndex];
            _tmpTextBox = new TextBox();
            _tmpTextBox.Multiline = true;
            //只能使用标题框的宽度，使用item.SubItems[0]的宽度会有问题
            _tmpTextBox.Bounds = new Rectangle(subItem.Bounds.Location, new Size(this.Columns[columnIndex].Width, subItem.Bounds.Height));
            _tmpTextBox.Text = subItem.Text;
            _tmpTextBox.Tag = new string[] { rowIndex.ToString(), columnIndex.ToString(), userSate,isRemoveIfCancel.ToString()};
            _tmpTextBox.KeyDown += tmpTextBox_KeyDown;
            _tmpTextBox.Leave += tmpTextBox_Leave;
            
            this.Controls.Add(_tmpTextBox);
            _tmpTextBox.BringToFront();
            _tmpTextBox.Select();
        }
        private void tmpTextBox_Leave(object sender, EventArgs e)
        {
            TextBox tmpTb = sender as TextBox;
            string[] tmpIndex = tmpTb.Tag as string[];
            int itemRowIndex = int.Parse(tmpIndex[0]);
            int itemColumnIndex = int.Parse(tmpIndex[1]);
            string userSate = tmpIndex[2];
            bool isRemoveIfCancel = bool.Parse(tmpIndex[3]);
            ListViewItem.ListViewSubItem subItem = this.Items[itemRowIndex].SubItems[itemColumnIndex];
            string oldLabel = subItem.Text;
            string label = tmpTb.Text;

            EditSubItemEventArgs editSubItemArgs = new EditSubItemEventArgs(oldLabel, label, itemRowIndex, itemColumnIndex, userSate, isRemoveIfCancel);
            OnAfterEditSubItem(editSubItemArgs);
            if (editSubItemArgs.IsCancelEdit)
            {
                if (editSubItemArgs.IsRemoveIfCancel)
                {
                    this.Items[itemRowIndex].Remove();
                }
                ClearTextBox();
                return;
            }
            //接受更改
            subItem.Text = label;
            //完成事件
            OnEditSubItemCompleted(editSubItemArgs);
            ClearTextBox();

        }
        private void tmpTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //屏蔽回车事件
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void OnAfterEditSubItem(EditSubItemEventArgs e)
        {
            if (AfterEditSubItem != null)
            {
                AfterEditSubItem(this, e);
            }
        }
        private void OnEditSubItemCompleted(EditSubItemEventArgs e)
        {
            if (EditSubItemCompleted != null)
            {
                EditSubItemCompleted(this, e);
            }
        }
        private void ClearTextBox()
        {
            if (_tmpTextBox != null && !_tmpTextBox.IsDisposed)
            {
                this.Controls.Add(_tmpTextBox);
                _tmpTextBox.Dispose();
            }
        }

        //private void CreateTextBox(ListViewItem item, int startIndex, int count)
        //{
        //    _panel = new Panel();
        //    //定义初始位置大小
        //    Rectangle rt = new Rectangle(0, 0, 0, item.Bounds.Height);
        //    for (int j = startIndex; j < startIndex + count; j++)
        //    {
        //        TextBox tmpTextBox = new TextBox
        //            {
        //                Multiline = true,
        //                Bounds = new Rectangle(
        //                    rt.X,
        //                    rt.Y,
        //                    this.Columns[j].Width, //只能使用标题框的宽度，使用item.SubItems[0]的宽度会有问题
        //                    rt.Height),
        //                Name = "TextBox" + j,
        //                Tag = item.SubItems[j],
        //                Text = item.SubItems[j].Text
        //            };
        //        tmpTextBox.KeyDown += tmpTextBox_KeyDown;
        //        rt.X += tmpTextBox.Width;//移动X坐标，长度为新增的textbox的宽度
        //        _panel.Controls.Add(tmpTextBox);
        //    }
        //    _panel.Location = item.SubItems[startIndex].Bounds.Location;
        //    _panel.Size = new Size(rt.X, item.Bounds.Height);
        //    _panel.Bounds = new Rectangle(
        //        item.SubItems[startIndex].Bounds.Location,
        //        new Size(rt.X, item.Bounds.Height));
        //    _panel.Tag = item;
        //    _panel.Leave += panel_Leave;
        //    this.Controls.Add(_panel);
        //    _panel.Controls[0].Select();
        //}
        //protected override void OnDoubleClick(EventArgs e)
        //{
        //    Point tmpPoint = this.PointToClient(Cursor.Position);
        //    ListViewItem.ListViewSubItem subitem = this.HitTest(tmpPoint).SubItem;
        //    ListViewItem item = this.HitTest(tmpPoint).Item;
        //    if (subitem != null)
        //    {
        //        if (item.SubItems[0].Equals(subitem))
        //        {
        //            EditItem(subitem, new Rectangle(item.Bounds.Left, item.Bounds.Top, this.Columns[0].Width, item.Bounds.Height - 2));
        //        }
        //        else
        //        {
        //            EditItem(subitem);
        //        }
        //    }
        //    base.OnDoubleClick(e);
        //}
    }

    //public class ListViewEx : ListView
    //{
    //    protected override void OnKeyDown(KeyEventArgs e)
    //    {
    //        if (e.KeyCode == Keys.F2)
    //        {
    //            if (this.SelectedItems.Count > 0)
    //            {
    //                //this.SelectedItems[0].BeginEdit();
    //                ListViewItem lvi = this.SelectedItems[0];
    //                EditItem(lvi.SubItems[0], new Rectangle(lvi.Bounds.Left, lvi.Bounds.Top, this.Columns[0].Width, lvi.Bounds.Height - 2));
    //            }
    //        }
    //        base.OnKeyDown(e);
    //    }
    //    protected override void OnSelectedIndexChanged(EventArgs e)
    //    {
    //        this._textBox.Visible = false;
    //        this.m_cb.Visible = false;
    //        base.OnSelectedIndexChanged(e);
    //    }
    //    //protected override void OnDoubleClick(EventArgs e)
    //    //{
    //    //    Point tmpPoint = this.PointToClient(Cursor.Position);
    //    //    ListViewItem item = this.GetItemAt(tmpPoint.X, tmpPoint.Y);
    //    //    if (item != null)
    //    //    {
    //    //        if (tmpPoint.X > this.Columns[0].Width && tmpPoint.X < this.Width)
    //    //        {
    //    //            EditItem(1);
    //    //        }
    //    //    }
    //    //    base.OnDoubleClick(e);
    //    //}
    //    protected override void OnDoubleClick(EventArgs e)
    //    {
    //        Point tmpPoint = this.PointToClient(Cursor.Position);
    //        ListViewItem.ListViewSubItem subitem = this.HitTest(tmpPoint).SubItem;
    //        ListViewItem item = this.HitTest(tmpPoint).Item;
    //        if (subitem != null)
    //        {
    //            if (item.SubItems[0].Equals(subitem))
    //            {
    //                EditItem(subitem, new Rectangle(item.Bounds.Left, item.Bounds.Top, this.Columns[0].Width, item.Bounds.Height - 2));
    //            }
    //            else
    //            {
    //                EditItem(subitem);
    //            }
    //        }
    //        base.OnDoubleClick(e);
    //    }
    //    protected override void WndProc(ref   Message m)
    //    {
    //        if (m.Msg == 0x115 || m.Msg == 0x114)
    //        {
    //            this._textBox.Visible = false;
    //        }
    //        base.WndProc(ref   m);
    //    }
    //}
}

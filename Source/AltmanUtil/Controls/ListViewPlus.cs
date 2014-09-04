using System;
using System.Drawing;
using System.Windows.Forms;

namespace Altman.Controls
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
                    throw new Exception("edited item is null");
                }
                if (item.SubItems.Count < index + 1)
                {
                    throw new Exception("edited subitem is null");
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
    }
}

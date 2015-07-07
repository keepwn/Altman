using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eto.Forms;

namespace Plugin_FileManager.Interface
{
    public partial class FileEditerFindForm : Form
    {
        public FileEditerFindForm()
        {
            Init();
        }

        public EventHandler<FindContentEventArgs> FindNextClick;
        void _buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void _buttonFindNext_Click(object sender, EventArgs e)
        {
            if (FindNextClick != null)
            {
                var content = _textBoxFindText.Text;
                var direction = _radioButtonUp.Checked
                    ? FindContentEventArgs.Direction.Up
                    : FindContentEventArgs.Direction.Down;
                var caseSensitive = _checkBoxCaseSensitive.Checked == true;

                FindNextClick(this, new FindContentEventArgs(content, direction, caseSensitive));
            }
        }
    }

    public class FindContentEventArgs : EventArgs
    {
        public string FindContent;
        public Direction FindDirection;
        public bool CaseSensitive;
        public enum Direction
        {
            Up,
            Down
        }
        public FindContentEventArgs(string findContent, Direction findDirection, bool caseSensitive)
        {
            FindContent = findContent;
            FindDirection = findDirection;
            CaseSensitive = caseSensitive;
        }
    }
}

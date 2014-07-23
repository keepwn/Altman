using System;
using System.Collections.Generic;
using System.Text;

namespace Altman.Logic
{
    internal class FuncTreeNode
    {
        private string _name;
        private string _info;
        private FuncTreeNode _parent;
        private Dictionary<string, CustomShellType.FuncCode> _funcs;
        private Dictionary<string,FuncTreeNode> _nodes;

        #region 属性
        public string FullPath
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                GetFullPath(stringBuilder, "/");
                return stringBuilder.ToString();
            }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Info
        {
            get { return _info; }
            set { _info = value; }
        }

        public FuncTreeNode Parent
        {
            get { return _parent; }
        }

        public Dictionary<string, CustomShellType.FuncCode> Funcs
        {
            get { return _funcs; }
        }

        public Dictionary<string, FuncTreeNode> Nodes
        {
            get { return _nodes; }
        }
        
        #endregion

        public FuncTreeNode(string name)
        {
            this._name = name;
            this._parent = null;
            this._funcs = new Dictionary<string, CustomShellType.FuncCode>();
            this._nodes = new Dictionary<string, FuncTreeNode>();
        }

        private void GetFullPath(StringBuilder path, string pathSeparator)
        {
            if (Parent != null)
            {
                Parent.GetFullPath(path, pathSeparator);
                if (Parent.Parent != null)
                {
                    path.Append(pathSeparator);
                }
                path.Append(Name);
            }
        }
        public FuncTreeNode AddNode(string nodeName)
        {
            if (!this.Nodes.ContainsKey(nodeName))
            {
                FuncTreeNode newNode = new FuncTreeNode(nodeName);
                newNode._parent = this;
                this.Nodes.Add(newNode.Name, newNode);
                return newNode;
            }
            return this.Nodes[nodeName];
        }
        public FuncTreeNode AddNodes(string xpath)
        {
            string[] paths = xpath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (paths.Length == 0)
            {
                throw new Exception("Unable to add the root node");
            }
            int index = 0;
            FuncTreeNode tmp = this;
            while (index < paths.Length)
            {
                string tmpNodeName = paths[index];
                if (!tmp.Nodes.ContainsKey(tmpNodeName))
                {
                    tmp = tmp.AddNode(tmpNodeName);
                }
                else
                {
                    tmp = tmp.Nodes[tmpNodeName];
                }
                index++;
            }
            return tmp;
        }
        public FuncTreeNode FindNode(string nodeName)
        {
            if (this.Nodes.ContainsKey(nodeName))
            {
                return this.Nodes[nodeName];
            }
            else
            {
                return null;
            }
        }
        public FuncTreeNode FindNodes(string xpath)
        {
            string[] paths = xpath.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (paths.Length == 0)
            {
                return this;
            }
            int index = 0;
            FuncTreeNode tmp = this;
            while (index < paths.Length)
            {
                tmp = tmp.FindNode(paths[index]);
                if (tmp != null)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }
            return tmp;
        }
    }
}

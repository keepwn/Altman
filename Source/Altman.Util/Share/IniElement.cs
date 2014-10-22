using System;
using System.Text;

namespace Altman.Util.Share
{
	public class IniElement
	{
		protected string formatting = "";
		private string _line;

		protected IniElement()
		{
			_line = "";
		}

		public IniElement(string content)
		{
			_line = content.TrimEnd();
		}

		/// <summary>
		/// 格式化
		/// </summary>
		public string Formatting
		{
			get { return formatting; }
			set { formatting = value; }
		}

		/// <summary>
		/// 缩进
		/// </summary>
		public string Indentation
		{
			get
			{
				var indent = new StringBuilder();
				foreach (var c in formatting)
				{
					if (!char.IsWhiteSpace(c)) break;
					indent.Append(c);
				}
				return indent.ToString();
			}
			set
			{
				if (value.TrimStart().Length > 0)
					throw new ArgumentException("Indentation property cannot contain any characters which are not condsidered as white ones.");
				if (IniSettings.TabReplacement != null)
					value = value.Replace("\t", IniSettings.TabReplacement);
				formatting = value + formatting.TrimStart();
				_line = value + _line.TrimStart();
			}
		}

		/// <summary>
		/// 正文内容（不包括缩进）
		/// </summary>
		public string Content
		{
			get { return _line.TrimStart(); }
			protected set { _line = value; }
		}

		/// <summary>
		/// 行内容（包括缩进）
		/// </summary>
		public string Line
		{
			get
			{
				var indent = Indentation;
				if (_line.Contains(IniSettings.NewLine))
				{
					string[] lines = _line.Split(new[] { IniSettings.NewLine }, StringSplitOptions.None);
					var ret = new StringBuilder();
					ret.Append(lines[0]);
					for (int i = 1; i < lines.Length; i++)
						ret.Append(IniSettings.NewLine + indent + lines[i]);
					return ret.ToString();
				}
				return _line;
			}
		}

		public override string ToString()
		{
			return "Line: \"" + _line + "\"";
		}

		public virtual void FormatDefault()
		{
			Indentation = "";
		}
	}

	/// <summary>
	/// 节的开始段，如"[SectionName]"
	/// </summary>
	public class IniSectionStart : IniElement
	{
		private readonly string textOnTheRight; // e.g.  "[SectionName] some text"
		private string inlineComment, inlineCommentChar;
		private string sectionName;

		private IniSectionStart()
		{
		}

		public IniSectionStart(string content): base(content)
		{
			//content = Content;
			formatting = ExtractFormat(content);
			content = content.TrimStart();
			if (IniSettings.AllowInlineComments)
			{
				IniSettings.IndexOfAnyResult result = IniSettings.IndexOfAny(content, IniSettings.CommentChars);
				if (result.index > content.IndexOf(IniSettings.SectionCloseBracket))
				{
					inlineComment = content.Substring(result.index + result.any.Length);
					inlineCommentChar = result.any;
					content = content.Substring(0, result.index);
				}
			}
			if (IniSettings.AllowTextOnTheRight)
			{
				int closeBracketPos = content.LastIndexOf(IniSettings.SectionCloseBracket);
				if (closeBracketPos != content.Length - 1)
				{
					textOnTheRight = content.Substring(closeBracketPos + 1);
					content = content.Substring(0, closeBracketPos);
				}
			}
			sectionName =
				content.Substring(IniSettings.SectionOpenBracket.Length,
					content.Length - IniSettings.SectionCloseBracket.Length - IniSettings.SectionOpenBracket.Length).Trim();
			Content = content;
			Format();
		}

		public string SectionName
		{
			get { return sectionName; }
			set
			{
				sectionName = value;
				Format();
			}
		}

		public string InlineComment
		{
			get { return inlineComment; }
			set
			{
				if (!IniSettings.AllowInlineComments || IniSettings.CommentChars.Length == 0)
					throw new NotSupportedException("Inline comments are disabled.");
				inlineComment = value;
				Format();
			}
		}

		public static bool IsLineValid(string testString)
		{
			return testString.StartsWith(IniSettings.SectionOpenBracket) &&
					testString.EndsWith(IniSettings.SectionCloseBracket);
		}

		public override string ToString()
		{
			return "Section: \"" + sectionName + "\"";
		}

		public IniSectionStart CreateNew(string sectName)
		{
			var ret = new IniSectionStart();
			ret.sectionName = sectName;
			if (IniSettings.PreserveFormatting)
			{
				ret.formatting = formatting;
				ret.Format();
			}
			else
				ret.Format();
			return ret;
		}

		public static string ExtractFormat(string content)
		{
			bool beforeS = false;
			bool afterS = false;
			bool beforeEvery = true;
			char currC;
			string comChar;
			string insideWhiteChars = "";
			var form = new StringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				currC = content[i];
				if (char.IsLetterOrDigit(currC) && beforeS)
				{
					afterS = true;
					beforeS = false;
					form.Append('$');
				}
				else if (afterS && char.IsLetterOrDigit(currC))
				{
					insideWhiteChars = "";
				}
				else if (content.Length - i >= IniSettings.SectionOpenBracket.Length &&
						content.Substring(i, IniSettings.SectionOpenBracket.Length) == IniSettings.SectionOpenBracket &&
						beforeEvery)
				{
					beforeS = true;
					beforeEvery = false;
					form.Append('[');
				}
				else if (content.Length - i >= IniSettings.SectionCloseBracket.Length &&
						content.Substring(i, IniSettings.SectionOpenBracket.Length) == IniSettings.SectionCloseBracket && afterS)
				{
					form.Append(insideWhiteChars);
					afterS = false;
					form.Append(IniSettings.SectionCloseBracket);
				}
				else if ((comChar = IniSettings.OfAny(i, content, IniSettings.CommentChars)) != null)
				{
					form.Append(';');
				}
				else if (char.IsWhiteSpace(currC))
				{
					if (afterS) insideWhiteChars += currC;
					else form.Append(currC);
				}
			}
			string ret = form.ToString();
			if (ret.IndexOf(';') == -1)
				ret += "   ;";
			return ret;
		}

		public override void FormatDefault()
		{
			Formatting = IniSettings.DefaultSectionFormatting;
			Format();
		}

		public void Format()
		{
			Format(formatting);
		}

		public void Format(string formatting)
		{
			var build = new StringBuilder();
			foreach (char t in formatting)
			{
				var currC = t;
				if (currC == '$')
					build.Append(sectionName);
				else if (currC == '[')
					build.Append(IniSettings.SectionOpenBracket);
				else if (currC == ']')
					build.Append(IniSettings.SectionCloseBracket);
				else if (currC == ';' && IniSettings.CommentChars.Length > 0 && inlineComment != null)
					build.Append(IniSettings.CommentChars[0]).Append(inlineComment);
				else if (char.IsWhiteSpace(t))
					build.Append(t);
			}
			Content = build.ToString().TrimEnd() + (IniSettings.AllowTextOnTheRight ? textOnTheRight : "");
		}

		public static IniSectionStart FromName(string sectionName)
		{
			var ret = new IniSectionStart();
			ret.SectionName = sectionName;
			ret.FormatDefault();
			return ret;
		}
	}

	/// <summary>
	/// 空白行
	/// </summary>
	public class IniBlankLine : IniElement
	{
		public IniBlankLine(int linesCount): base("")
		{
			LinesCount = linesCount;
		}

		public int LinesCount
		{
			get { return Line.Length / IniSettings.NewLine.Length + 1; }
			set
			{
				if (value < 1)
					throw new ArgumentOutOfRangeException("Cannot set LinesCount to less than 1.");
				var build = new StringBuilder();
				for (var i = 1; i < value; i++)
					build.Append(IniSettings.NewLine);
				Content = build.ToString();
			}
		}

		public static bool IsLineValid(string testString)
		{
			return testString == "";
		}

		public override string ToString()
		{
			return LinesCount + " blank line(s)";
		}

		public override void FormatDefault()
		{
			LinesCount = 1;
			base.FormatDefault();
		}
	}

	/// <summary>
	/// 注释行
	/// </summary>
	public class IniCommentary : IniElement
	{
		private string _comment;
		private string _commentChar;

		private IniCommentary()
		{
		}

		public IniCommentary(string content): base(content)
		{
			if (IniSettings.CommentChars.Length == 0)
				throw new NotSupportedException("Comments are disabled. Set the IniSettings.CommentChars property to turn them on.");
			_commentChar = IniSettings.StartsWith(Content, IniSettings.CommentChars);
			_comment = Content.Length > _commentChar.Length ? Content.Substring(_commentChar.Length) : "";
		}

		public string CommentChar
		{
			get { return _commentChar; }
			set
			{
				if (_commentChar != value)
				{
					_commentChar = value;
					Rewrite();
				}
			}
		}

		public string Comment
		{
			get { return _comment; }
			set
			{
				if (_comment != value)
				{
					_comment = value;
					Rewrite();
				}
			}
		}

		private void Rewrite()
		{
			var newContent = new StringBuilder();
			var lines = _comment.Split(new[] {IniSettings.NewLine}, StringSplitOptions.None);
			newContent.Append(_commentChar + lines[0]);
			for (var i = 1; i < lines.Length; i++)
				newContent.Append(IniSettings.NewLine + _commentChar + lines[i]);
			Content = newContent.ToString();
		}

		public static bool IsLineValid(string testString)
		{
			return IniSettings.StartsWith(testString.TrimStart(), IniSettings.CommentChars) != null;
		}

		public override string ToString()
		{
			return "Comment: \"" + _comment + "\"";
		}

		public static IniCommentary FromComment(string comment)
		{
			if (IniSettings.CommentChars.Length == 0)
				throw new NotSupportedException("Comments are disabled. Set the IniSettings.CommentChars property to turn them on.");
			var ret = new IniCommentary();
			ret._comment = comment;
			ret.CommentChar = IniSettings.CommentChars[0];
			return ret;
		}

		public override void FormatDefault()
		{
			base.FormatDefault();
			CommentChar = IniSettings.CommentChars[0];
			Rewrite();
		}
	}

	/// <summary>
	/// 键值对
	/// </summary>
	public class IniValue : IniElement
	{
		private readonly string _textOnTheRight; // only if qoutes are on, e.g. "Name = 'Jack' text-on-the-right"
		private string _inlineComment, _inlineCommentChar;
		private string _key;
		private string _value;

		private IniValue()
		{
		}

		public IniValue(string content)
			: base(content)
		{
			string[] split = Content.Split(new[] {IniSettings.EqualsString}, 2, StringSplitOptions.None);
			formatting = ExtractFormat(content);
			string split0 = split[0].Trim();
			string split1 = split.Length >= 1
				? split[1].Trim()
				: "";

			if (split0.Length > 0)
			{
				if (IniSettings.AllowInlineComments)
				{
					IniSettings.IndexOfAnyResult result = IniSettings.IndexOfAny(split1, IniSettings.CommentChars);
					if (result.index != -1)
					{
						_inlineComment = split1.Substring(result.index + result.any.Length);
						split1 = split1.Substring(0, result.index).TrimEnd();
						_inlineCommentChar = result.any;
					}
				}
				if (IniSettings.QuoteChar != null && split1.Length >= 2)
				{
					var quoteChar = (char) IniSettings.QuoteChar;
					if (split1[0] == quoteChar)
					{
						int lastQuotePos;
						if (IniSettings.AllowTextOnTheRight)
						{
							lastQuotePos = split1.LastIndexOf(quoteChar);
							if (lastQuotePos != split1.Length - 1)
								_textOnTheRight = split1.Substring(lastQuotePos + 1);
						}
						else
							lastQuotePos = split1.Length - 1;
						if (lastQuotePos > 0)
						{
							if (split1.Length == 2)
								split1 = "";
							else
								split1 = split1.Substring(1, lastQuotePos - 1);
						}
					}
				}
				_key = split0;
				_value = split1;
			}
			Format();
		}

		public string Key
		{
			get { return _key; }
			set
			{
				_key = value;
				Format();
			}
		}

		public string Value
		{
			get { return _value; }
			set
			{
				this._value = value;
				Format();
			}
		}

		public string InlineComment
		{
			get { return _inlineComment; }
			set
			{
				if (!IniSettings.AllowInlineComments || IniSettings.CommentChars.Length == 0)
					throw new NotSupportedException("Inline comments are disabled.");
				if (_inlineCommentChar == null)
					_inlineCommentChar = IniSettings.CommentChars[0];
				_inlineComment = value;
				Format();
			}
		}

		public string ExtractFormat(string content)
		{
			//IniSettings.DefaultValueFormatting
			var pos = FeState.BeforeEvery;
			var insideWhiteChars = "";

			var form = new StringBuilder();
			for (int i = 0; i < content.Length; i++)
			{
				var currC = content[i];
				if (char.IsLetterOrDigit(currC))
				{
					if (pos == FeState.BeforeEvery)
					{
						form.Append('?');
						pos = FeState.AfterKey;
						//afterKey = true; beforeEvery = false; ;
					}
					else if (pos == FeState.BeforeVal)
					{
						form.Append('$');
						pos = FeState.AfterVal;
					}
				}

				else if (pos == FeState.AfterKey && content.Length - i >= IniSettings.EqualsString.Length &&
						content.Substring(i, IniSettings.EqualsString.Length) == IniSettings.EqualsString)
				{
					form.Append(insideWhiteChars);
					pos = FeState.BeforeVal;
					//afterKey = false; beforeVal = true; 
					form.Append('=');
				}
				else if ((IniSettings.OfAny(i, content, IniSettings.CommentChars)) != null)
				{
					form.Append(insideWhiteChars);
					form.Append(';');
				}
				else if (char.IsWhiteSpace(currC))
				{
					string theWhiteChar;
					if (currC == '\t' && IniSettings.TabReplacement != null)
						theWhiteChar = IniSettings.TabReplacement;
					else
						theWhiteChar = currC.ToString();
					if (pos == FeState.AfterKey || pos == FeState.AfterVal)
					{
						insideWhiteChars += theWhiteChar;
						continue;
					}
					form.Append(theWhiteChar);
				}
				insideWhiteChars = "";
			}
			if (pos == FeState.BeforeVal)
			{
				form.Append('$');
				pos = FeState.AfterVal;
			}
			var ret = form.ToString();
			if (ret.IndexOf(';') == -1)
				ret += "   ;";
			return ret;
		}

		public void Format()
		{
			Format(formatting);
		}

		public void Format(string formatting)
		{
			var build = new StringBuilder();
			for (var i = 0; i < formatting.Length; i++)
			{
				char currC = formatting[i];
				if (currC == '?')
					build.Append(_key);
				else if (currC == '$')
				{
					if (IniSettings.QuoteChar != null)
					{
						var quoteChar = (char) IniSettings.QuoteChar;
						build.Append(quoteChar).Append(_value).Append(quoteChar);
					}
					else
						build.Append(_value);
				}
				else if (currC == '=')
					build.Append(IniSettings.EqualsString);
				else if (currC == ';')
					build.Append(_inlineCommentChar + _inlineComment);
				else if (char.IsWhiteSpace(formatting[i]))
					build.Append(currC);
			}
			Content = build.ToString().TrimEnd() + (IniSettings.AllowTextOnTheRight ? _textOnTheRight : "");
		}


		public override void FormatDefault()
		{
			Formatting = IniSettings.DefaultValueFormatting;
			Format();
		}

		public IniValue CreateNew(string key, string value)
		{
			var ret = new IniValue();
			ret._key = key;
			ret._value = value;
			if (IniSettings.PreserveFormatting)
			{
				ret.formatting = formatting;
				if (IniSettings.AllowInlineComments)
					ret._inlineCommentChar = _inlineCommentChar;
				ret.Format();
			}
			else
				ret.FormatDefault();
			return ret;
		}

		public static bool IsLineValid(string testString)
		{
			int index = testString.IndexOf(IniSettings.EqualsString);
			return index > 0;
		}

		public void Set(string key, string value)
		{
			this._key = key;
			this._value = value;
			Format();
		}

		public override string ToString()
		{
			return "Value: \"" + _key + " = " + _value + "\"";
		}

		public static IniValue FromData(string key, string value)
		{
			var ret = new IniValue();
			ret._key = key;
			ret._value = value;
			ret.FormatDefault();
			return ret;
		}

		private enum FeState // stare of format extractor (ExtractFormat method)
		{
			BeforeEvery,
			AfterKey,
			BeforeVal,
			AfterVal
		}
	}
}
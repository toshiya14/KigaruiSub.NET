using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RMEGo.Kigaruisub.Controls
{
    /// <summary>
    /// TextEditor.xaml 的互動邏輯
    /// </summary>
    public partial class TextEditor : UserControl
    {
        public TextEditor()
        {
            InitializeComponent();

            var input = @"{\rtf1\ansi\deff0\fs24\b Bold \b0\iItalic\i0\ul UnderLine \ulnone\strike StrikeLine \strike0\fs36 BigFont \fs24}";
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(input));
            text.SelectAll();
            text.Selection.Load(ms, DataFormats.Rtf);
        }

        public static string Escape(string s)
        {
            if (s == null) return s;

            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (c >= 0x20 && c < 0x80)
                {
                    if (c == '\\' || c == '{' || c == '}')
                    {
                        sb.Append('\\');
                    }
                    sb.Append(c);
                }
                else if (c < 0x20 || (c >= 0x80 && c <= 0xFF))
                {
                    sb.Append($"\\'{((byte)c).ToString("X")}");
                }
                else
                {
                    sb.Append($"\\u{(short)c}?");
                }
            }
            return sb.ToString();
        }
    }
}

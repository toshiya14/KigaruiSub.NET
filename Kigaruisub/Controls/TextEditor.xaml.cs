using RMEGo.Kigaruisub.Helpers;
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

            var converter = new AssToRTF();
            var intext = @"{\fn微軟雅黑}{\t(\r(36,74),10)}{\fs24\b1}内容";
            var textRange = new TextRange(text.Document.ContentStart, text.Document.ContentEnd);
            var endPoint = text.Document.ContentEnd;
            var result = converter.Divide(intext);
            foreach(var section in result)
            {
                switch (section[0])
                {
                    case AssToRTF.SECTION_CMD:
                        var lastLetter = section[1] + 1;
                        for(var i = section[1] + 1; i <= section[2]; i++)
                        {
                            var @char = intext[i];
                            if ((@char>='a' && @char<='z') || (@char >= 'A' && @char <= 'Z'))
                            {
                                lastLetter = i;
                            }
                            else
                            {
                                break;
                            }
                        }
                        var start = textRange.Start.GetPositionAtOffset(section[1], LogicalDirection.Forward);
                        var end = start.GetPositionAtOffset(1, LogicalDirection.Forward);
                        text.Selection.Select(start, end);
                        text.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Color.FromRgb(0xEF, 0x34, 0x56)));
                        break;
                }
            }

            //var input = @"{\rtf1\ansi\ansicpg950\deff0\fs24\b \'b7\'73\'b2\'d3\'a9\'fa\'c5\'e9 \b0\iItalic\i0\ul UnderLine \ulnone\strike StrikeLine \strike0\fs36 BigFont \fs24}";
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(input));
            //text.SelectAll();
            //text.Selection.Load(ms, DataFormats.Rtf);
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

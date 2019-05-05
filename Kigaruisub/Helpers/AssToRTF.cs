using ASSLoader.NET;
using RMEGo.Kigaruisub.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMEGo.Kigaruisub.Helpers
{
    public class AssToRTF
    {
        public const int SECTION_PLAINTEXT = 0x1000;
        public const int SECTION_CODEWRAP = 0x2000;
        public const int SECTION_CMD = 0x2100;
        public const int SECTION_PARAMETERS = 0x2110;

        private List<TextFormat> _formats = new List<TextFormat>();

        public AssToRTF()
        {

        }

        public void LoadStyle(ASSStyle style)
        {
            _formats.Add(TextFormat.GetFormat(style));
        }

        public string Convert(string input)
        {
            // Processing hart replacement.
            var replacedText = input.Replace("\n", "").Replace("\\N", "\\N\n");

            var pattern = Divide(replacedText);
            return "";
        }

        public IList<int[]> Divide(string input)
        {

            var sectionDef = new List<int[]>();
            var currentSection = SECTION_PLAINTEXT;
            var bracketLevel = 0;
            for (var i = 0; i < input.Length; i++)
            {
                var inchr = input[i];
                switch (currentSection)
                {
                    case SECTION_PLAINTEXT:
                        // end of a text section.
                        if (inchr == '{')
                        {
                            var lastText = FindFromLast(sectionDef, SECTION_PLAINTEXT);
                            if (lastText != null)
                            {
                                lastText[2] = i - 1;
                                if(lastText[2] < lastText[1])
                                {
                                    sectionDef.Remove(lastText);
                                }
                            }
                            currentSection = SECTION_CODEWRAP;
                            sectionDef.Add(new[] { SECTION_CODEWRAP, i, -1 });
                        }
                        else
                        {
                            if (i == input.Length - 1)
                            {
                                var lastText = FindFromLast(sectionDef, SECTION_PLAINTEXT);
                                if (lastText != null)
                                {
                                    lastText[2] = i;
                                }
                            }
                        }
                        break;

                    case SECTION_CODEWRAP:
                        // end of a code wrap section.
                        if(inchr == '}')
                        {
                            var lastCode = FindFromLast(sectionDef, SECTION_CODEWRAP);
                            if(lastCode != null)
                            {
                                lastCode[2] = i;
                            }
                            currentSection = SECTION_PLAINTEXT;
                            sectionDef.Add(new[] { SECTION_PLAINTEXT, i + 1, -1 });
                        }
                        else if(inchr == '\\')
                        {
                            var last = sectionDef.Last();
                            if (last[0] == SECTION_CMD) {
                                last[2] = i - 1;
                            }
                            currentSection = SECTION_CMD;
                            sectionDef.Add(new[] { SECTION_CMD, i, -1 });
                        }
                        else
                        {
                            throw new BadFormatException(i, input, "Invalid characters in Effect Command.");
                        }
                        break;

                    case SECTION_CMD:
                        if (inchr == '\\')
                        {
                            var last = sectionDef.Last();
                            if (last[0] == SECTION_CMD || last[0] == SECTION_PARAMETERS)
                            {
                                var lastcmd = FindFromLast(sectionDef, SECTION_CMD);
                                lastcmd[2] = i - 1;
                            }
                            currentSection = SECTION_CMD;
                            sectionDef.Add(new[] { SECTION_CMD, i, -1 });
                        }
                        else if(inchr == '(')
                        {
                            currentSection = SECTION_PARAMETERS;
                            sectionDef.Add(new[] { SECTION_PARAMETERS, i, -1 });
                            bracketLevel = 0;
                        }else if (inchr == '}')
                        {
                            var last = sectionDef.Last();
                            if (last[0] == SECTION_CMD || last[0] == SECTION_PARAMETERS)
                            {
                                var lastcmd = FindFromLast(sectionDef, SECTION_CMD);
                                lastcmd[2] = i - 1;
                            }
                            currentSection = SECTION_CODEWRAP;
                            i--;
                        }
                        break;

                    case SECTION_PARAMETERS:
                        if(inchr == '(')
                        {
                            bracketLevel++;
                        }else if(inchr == ')')
                        {
                            bracketLevel--;
                            if (bracketLevel < 0)
                            {
                                var lastpar = FindFromLast(sectionDef, SECTION_PARAMETERS);
                                if (lastpar != null)
                                {
                                    lastpar[2] = i;
                                }
                                currentSection = SECTION_CMD;
                                i--;
                            }
                        }
                        break;
                }
            }

            return sectionDef;
        }

        private int[] FindFromLast(IList<int[]> cols, int section)
        {
            for(var i = cols.Count-1; i >= 0; i--)
            {
                var e = cols[i];
                if(e[0] == section)
                {
                    return e;
                }
            }
            return null;
        }
    }

    public class TextFormat
    {
        public Dictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();
        public string FontName { get; set; } = "Vernada";
        public double FontSize { get; set; } = 16.0;
        public bool IsBold { get; set; } = false;
        public bool IsItalic { get; set; } = false;
        public bool HasUnderLine { get; set; } = false;
        public bool HasStrikeLine { get; set; } = false;

        public static TextFormat GetFormat(ASSStyle style)
        {
            var output =  new TextFormat
            {
                FontName = style.Fontname,
                FontSize = style.Fontsize,
                IsBold = style.Bold,
                IsItalic = style.Italic,
                HasUnderLine = style.Underline,
                HasStrikeLine = style.StrikeOut
            };
            output.Meta["defName"] = style.Name;
            return output;
        }
    }
}

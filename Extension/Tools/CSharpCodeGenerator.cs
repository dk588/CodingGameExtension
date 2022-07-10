using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodinGameExtension.Tools
{
    public class CSharpCodeGenerator:CodeGeneratorBase, ICodeGenerator
    {
      

        public String GetCode()
        {
            var sb = new StringBuilder();
            sb.AppendLine("using System;\r\nusing System.Linq;\r\nusing System.IO;\r\nusing System.Text;\r\nusing System.Collections;\r\nusing System.Collections.Generic;");

            foreach (FileInfo file in files)
            {
               

                using (var reader = file.OpenText())
                {
                    string s;
                    var isWaitingNameSpace = true;
                    var isWaitingFirstBraket = true;                    
                    var programIsEncloseWithBraket = false;
                    int lastBraketLinePosition = 0;
                    int lastBraketLineSize = 0;
                    while ((s = reader.ReadLine()) != null)
                    {
                        var l = new Line(s);

                        if (isWaitingNameSpace)
                        {
                            if (l.IsNameSpaceLine())
                            {
                                isWaitingNameSpace = false;

                                if (l.EndWithSemiColon())
                                    isWaitingFirstBraket = false;
                                else
                                    programIsEncloseWithBraket = true;
                            }
                        }
                        else if(isWaitingFirstBraket)
                        {
                            if (l.IsOpenBraket())
                            {
                                isWaitingFirstBraket = false;                                
                            }
                        }
                        else
                        {
                            

                            if (programIsEncloseWithBraket && l.IsCloseBraket())
                            {
                                lastBraketLinePosition = sb.Length;
                                lastBraketLineSize = l.Length;
                            }
                            sb.AppendLine(s);
                        }
                    }

                    if (programIsEncloseWithBraket && lastBraketLinePosition > 0)
                        sb.Remove(lastBraketLinePosition, lastBraketLineSize+2);
                }
            }


            return sb.ToString();
        }


    }

    internal class Line
    {
        private string s;

        public int Length { get { return s.Length; } }

        public Line(string s)
        {
            this.s = s;
        }

        public bool IsNameSpaceLine()
        {
            return s.TrimStart(' ', '\t').StartsWith("namespace");
        }

        public override string ToString() { return s; }

        internal bool EndWithSemiColon()
        {
            return s.TrimEnd(' ').EndsWith(";");
        }

        internal bool IsOpenBraket()
        {

            return s.TrimStart(' ', '\t').TrimEnd(' ') == "{";
        }
        internal bool IsCloseBraket()
        {

            return s.TrimStart(' ', '\t').TrimEnd(' ') == "}";
        }

        internal bool NotEmpty()
        {
            return s.TrimStart(' ', '\t').Length != 0;
        }

        internal Line Clone()
        {
           return new Line(s);
        }
    }
}
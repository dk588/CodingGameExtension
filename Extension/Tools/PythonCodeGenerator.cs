using System.IO;
using System.Text;

namespace CodinGameExtension.Tools
{
    public class PythonCodeGenerator : CodeGeneratorBase, ICodeGenerator
    {
        public string GetCode()
        {
            var sb = new StringBuilder();
            sb.AppendLine("import sys\r\nimport math\r\n");

            foreach (FileInfo file in files)
            {
                using (var reader = file.OpenText())
                {
                    string s;
                    var isCopyStart = false;
                    while ((s = reader.ReadLine()) != null)
                    {
                        if (!isCopyStart && !s.TrimStart(' ').StartsWith("import") && s.Length > 0)
                            isCopyStart = true;

                        if (isCopyStart)
                        {
                            sb.AppendLine(s);
                        }
                    }
                }
            }

            return sb.ToString();
        }
    }
}
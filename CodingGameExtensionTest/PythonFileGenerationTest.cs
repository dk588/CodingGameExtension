using CodinGameExtension.Tools;
using System.IO;
using Xunit;

namespace CodingGameExtensionTest
{
    public class PythonFileGenerationTest
    {

        const string HEADER = "import sys\r\nimport math\r\n\r\n";

        [Fact]
        public void Header()
        {
            var cSharpProject = new PythonCodeGenerator();

           Assert.Equal(HEADER , cSharpProject.GetCode());

        }


        [Fact]
        public void PythonTestFile()
        {
            var gen = new PythonCodeGenerator();
            gen.AddFile(new FileInfo(@"TestFiles\PythonTestFile.py"));

            string Content = "Ok\r\n";

            Assert.Equal(HEADER + Content, gen.GetCode());
        }

    }
}
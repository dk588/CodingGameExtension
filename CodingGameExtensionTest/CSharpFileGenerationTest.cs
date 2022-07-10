using CodinGameExtension.Tools;
using System.IO;
using Xunit;

namespace CodingGameExtensionTest
{
    public class CSharpFileGenerationTest
    {

        const string HEADER = "using System;\r\nusing System.Linq;\r\nusing System.IO;\r\nusing System.Text;\r\nusing System.Collections;\r\nusing System.Collections.Generic;\r\n";

        [Fact]
        public void Header()
        {
            var cSharpProject = new CSharpCodeGenerator();

           Assert.Equal(HEADER , cSharpProject.GetCode());

        }

        [Fact]
        public void SimpleFile()
        {
            var cSharpProject = new CSharpCodeGenerator();
            cSharpProject.AddFile(new FileInfo(@"TestFiles\SimpleTestFile.cs"));

            string Content = "Content\r\n";

            Assert.Equal(HEADER + Content, cSharpProject.GetCode());
        }

       [Fact]
        public void SimpleFileWithSpace()
        {
            var cSharpProject = new CSharpCodeGenerator();
            cSharpProject.AddFile(new FileInfo(@"TestFiles\SimpleTestFileWithSpace.cs"));

            string Content = "Content\r\n";

            Assert.Equal(HEADER + Content, cSharpProject.GetCode());
        }

        [Fact]
        public void SimpleFileWithDifferentUsing()
        {
            var cSharpProject = new CSharpCodeGenerator();
            cSharpProject.AddFile(new FileInfo(@"TestFiles\SimpleTestFile2.cs"));

            string Content = "Content\r\n";

            Assert.Equal(HEADER + Content, cSharpProject.GetCode());
        }
        
        [Fact]
        public void WithBraketTestFile()
        {
            var cSharpProject = new CSharpCodeGenerator();
            cSharpProject.AddFile(new FileInfo(@"TestFiles\WithBraketTestFile.cs"));

            string Content = "    {\r\n\r\n        Content\r\n    }\r\n\r\n    }\r\n\r\n";

            Assert.Equal(HEADER + Content, cSharpProject.GetCode());
        }



    }
}
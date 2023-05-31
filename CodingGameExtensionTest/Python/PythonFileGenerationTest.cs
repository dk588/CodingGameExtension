using CodinGameExtension.Tools;
using Xunit;

namespace CodingGameExtensionTest.Python;

public class PythonFileGenerationTest : CodeGeneratorTestBase<PythonCodeGenerator>
{
    protected override string LanguageName => "Python";

    protected override string HEADER => "import sys\r\nimport math\r\n\r\n";

    [Fact]
    public void Header()
    {
        Assert.Equal(HEADER, new PythonCodeGenerator().GetCode());
    }

    [Fact]
    public void PythonTestFile()
    {
        PerformFileTest("PythonTestFile");
    }
}
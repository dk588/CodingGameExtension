using CodinGameExtension.Tools;
using Xunit;

namespace CodingGameExtensionTest.Csharp;

public class CSharpFileGenerationTest : CodeGeneratorTestBase<CSharpCodeGenerator>
{
    protected override string LanguageName => "CSharp";

    protected override string HEADER => "using System;\r\nusing System.Linq;\r\nusing System.IO;\r\nusing System.Text;\r\nusing System.Collections;\r\nusing System.Collections.Generic;\r\n";

    [Fact]
    public void MixedNamespacesGeneration()
    {
        PerformFileTest("MixedNamespaces");
    }

    [Fact]
    public void GenerateWithNoNamespace()
    {
        PerformFileTest("NoNamespace");
    }

    [Fact]
    public void MoveUsingsInsideFileScopedNamespace()
    {
        PerformFileTest("FileScopedUsings");
    }

    [Fact]
    public void MoveUsingsInsideBlockScopedNamespace()
    {
        PerformFileTest("BlockScopedWithUsings");
    }

    [Fact]
    public void FileScopedToBracketedNamespaceConversion()
    {
        PerformFileTest("FileScopedNamespace");
    }

    [Fact]
    public void Header()
    {
        Assert.Equal(HEADER, new CSharpCodeGenerator().GetCode());
    }

    [Fact]
    public void SimpleFile()
    {
        PerformFileTest("SimpleTestFile");
    }

    [Fact]
    public void SimpleFileWithSpace()
    {
        PerformFileTest("SimpleTestFileWithSpace");
    }

    [Fact]
    public void SimpleFileWithDifferentUsing()
    {
        PerformFileTest("SimpleTestFile2");
    }

    [Fact]
    public void WithBracketTestFile()
    {
        PerformFileTest("WithBracketTestFile");
    }
}
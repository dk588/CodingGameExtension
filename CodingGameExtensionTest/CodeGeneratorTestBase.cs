namespace CodingGameExtensionTest;

using CodinGameExtension.Tools;
using System;
using System.IO;
using System.Linq;
using Xunit;

public abstract class CodeGeneratorTestBase<T> where T : ICodeGenerator, new()
{
    protected abstract string HEADER { get; }

    protected abstract string LanguageName { get; }

    protected void PerformFileTest(string testName)
    {
        T generator = new();
        var filesForTest = Directory.GetFiles(Path.Combine(LanguageName, testName));
        foreach (var file in filesForTest.Where(x => Path.GetFileNameWithoutExtension(x).StartsWith("input")))
        {
            generator.AddFile(new FileInfo(file));
        }

        Assert.Equal(HEADER + File.ReadAllText(filesForTest.Single(x => Path.GetFileNameWithoutExtension(x).StartsWith("output"))) + Environment.NewLine, generator.GetCode());
    }
}

using System.Collections.Generic;
using System.IO;

namespace CodinGameExtension.Tools
{
    public interface ICodeGenerator
    {
        string GetCode();

        void AddFile(FileInfo fileInfo);

        void AddFiles(IEnumerable<FileInfo> fileInfos);
    }
}
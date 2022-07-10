using System.Collections.Generic;
using System.IO;

namespace CodinGameExtension.Tools
{
    public abstract class CodeGeneratorBase
    {

        internal List<FileInfo> files = new List<FileInfo>();


        public void AddFile(FileInfo fileInfo)
        {
            files.Add(fileInfo);
        }

        public void AddFiles(IEnumerable<FileInfo> fileInfos)
        {
            files.AddRange(fileInfos);
        }

    }
}
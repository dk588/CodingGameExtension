using System;
using System.Collections.Generic;
using System.IO;

namespace CodinGameExtension.Tools
{
    public interface ICodeGenerator
    {

        String GetCode();

        void AddFile(FileInfo fileInfo);

         void AddFiles(IEnumerable<FileInfo> fileInfos);


    }
}
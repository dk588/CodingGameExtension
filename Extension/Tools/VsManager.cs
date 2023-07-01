using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace CodinGameExtension.Tools
{
    internal class VsManager
    {
        private readonly DTE2 dte;

        public VsManager()
        {
            dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }

        private List<string> projectFiles;

        [SuppressMessage("Usage", "VSTHRD108:Assert thread affinity unconditionally", Justification = "Lazy initialisation of the property")]
        public List<string> ProjectFiles
        {
            get
            {
                if (projectFiles is null)
                {
                    ThreadHelper.ThrowIfNotOnUIThread();

                    projectFiles = new List<string>();

                    // Unloaded projects will just have null ProjectItems
                    foreach (Project prj in dte.Solution.Projects.OfType<Project>())
                    {
                        ExtractFiles(projectFiles, prj.ProjectItems);
                    }
                }

                return projectFiles;
            }
        }

        public ICodeGenerator GetCodeGenerator()
        {
            foreach (var file in ProjectFiles)
            {
                if (file.EndsWith(".cs"))
                    return new CSharpCodeGenerator();
                if (file.EndsWith(".py"))
                    return new PythonCodeGenerator();
            }
            throw new Exception("cs or py files not found in project");
        }

        /// <summary>
        /// Recursive !
        /// </summary>
        /// <param name="result"></param>
        /// <param name="prj"></param>
        private static void ExtractFiles(List<string> result, ProjectItems items)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (items != null)
            {
                foreach (var projectItem in items.OfType<ProjectItem>())
                {
                    if (projectItem.Properties.PropertyExist("FullPath"))
                    {
                        string fullpath = projectItem.Properties.GetProperty("FullPath");
                        if (fullpath.EndsWith("\\")) //Folder
                            ExtractFiles(result, projectItem.ProjectItems);
                        else
                            result.Add(fullpath);
                    }
                }
            }
        }

        public void SaveAllDocument()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            dte.Documents.SaveAll();
        }
    }
}

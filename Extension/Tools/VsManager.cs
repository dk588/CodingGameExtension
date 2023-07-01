using CodingBrowser;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using MessageBox = Community.VisualStudio.Toolkit.MessageBox;

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

                    Project prj = ((object[])dte.ActiveSolutionProjects)[0] as Project;
                    ExtractFiles(projectFiles, prj.ProjectItems);
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

        public string GetStartupUrl()
        {
            const string CONFIGURATION_KEY = "CodingameExtension.StartupUrl";
            ThreadHelper.ThrowIfNotOnUIThread();
            var startupProjects = dte.Solution.SolutionBuild.StartupProjects;
            if (startupProjects is IEnumerable e)
            {
                foreach (object obj in e)
                {
                    if (obj is string str)
                    {
                        foreach (Project prj in dte.Solution.Projects)
                        {
                            if (prj.UniqueName == str)
                            {
                                if (prj.Globals.VariableExists[CONFIGURATION_KEY])
                                {
                                    return prj.Globals[CONFIGURATION_KEY]?.ToString();
                                }
                                else
                                {
                                    prj.Globals[CONFIGURATION_KEY] = Browser.DefaultStartupUrl;
                                    prj.Globals.VariablePersists[CONFIGURATION_KEY] = true;
                                    prj.Save();
                                }
                            }
                        }
                    }
                }
            }

            new MessageBox().Show("No startup url found, starting on home page");
            return Browser.DefaultStartupUrl;
        }

        /// <summary>
        /// Recursive !
        /// </summary>
        /// <param name="result"></param>
        /// <param name="prj"></param>
        private static void ExtractFiles(List<string> result, ProjectItems items)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (var item in items)
            {
                var projectItem = item as ProjectItem;

                if (!projectItem.Properties.PropertyExist("FullPath"))
                    continue;

                var fullpath = projectItem.Properties.getProperty("FullPath");

                if (fullpath.EndsWith("\\")) //Folder
                    ExtractFiles(result, projectItem.ProjectItems);
                else
                    result.Add(fullpath);
            }
        }

        public void SaveAllDocument()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            dte.Documents.SaveAll();
        }
    }
}

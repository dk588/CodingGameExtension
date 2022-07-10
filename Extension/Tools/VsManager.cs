using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodinGameExtension.Tools
{
    internal class VsManager
    {
        EnvDTE80.DTE2 dte;

   public  VsManager()
        {
            dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }


        private List<String> projectFiles;
        public List<String> ProjectFiles { get { 
                if (projectFiles is null)
                {
                    ThreadHelper.ThrowIfNotOnUIThread();

                    projectFiles = new List<String>();

                    Project prj = ((object[])dte.ActiveSolutionProjects)[0] as Project;

                    ExtractFiles(projectFiles, prj.ProjectItems);

                }
                return projectFiles; } }

     
    
        public ICodeGenerator GetCodeGenerator()
        {
            foreach (var file in ProjectFiles) {
                if (file.EndsWith(".cs"))
                    return new CSharpCodeGenerator();
                if (file.EndsWith(".py"))
                    return new PythonCodeGenerator();
            }
             throw new Exception("cs or pi files not found in project");
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

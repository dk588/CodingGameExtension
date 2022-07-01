using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingGameExtension.Tools
{
    internal class VsManager
    {
        EnvDTE80.DTE2 dte;

        public  VsManager()
        {
            dte = Package.GetGlobalService(typeof(DTE)) as DTE2;
        }

        public List<String> getProjetFiles()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var result = new List<String>();

            Project prj = ((object[])dte.ActiveSolutionProjects)[0] as Project;

            ExtractFiles(result, prj.ProjectItems);

            return result;

        
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

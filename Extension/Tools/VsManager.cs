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

            prj.FullName.Substring(0, prj.FullName.LastIndexOf('\\')+1);

            foreach (var projectItem in prj.ProjectItems)
            {
                var file = projectItem as ProjectItem;

                var fullpath = file.Properties.getProperty("FullPath");

                result.Add(fullpath);

            }
            return result;
        }

        public void SaveAllDocument()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            dte.Documents.SaveAll();

        }

  

    }
}

using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingGameExtension.Tools
{
    internal static class Extension
    {


        public static string getProperty(this Properties properties, String name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (Property p in properties)
            {
                if (p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)) 
                    return p.Value.ToString();

            }

            throw new Exception($"property [{name}] not found");
        }
    }
}

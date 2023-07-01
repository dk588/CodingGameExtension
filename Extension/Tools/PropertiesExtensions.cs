using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;

namespace CodinGameExtension.Tools
{
    public static class PropertiesExtensions
    {
        public static bool PropertyExist(this Properties properties, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (properties != null)
            {
                foreach (Property p in properties)
                {
                    if (p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                        return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public static string GetProperty(this Properties properties, string name)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            foreach (Property p in properties)
            {
                if (p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
                    return p.Value.ToString();
            }

            throw new ArgumentException($"property [{name}] not found", nameof(name));
        }
    }
}
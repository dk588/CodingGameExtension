using CodingBrowser;
using CodingGameExtension;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace CodinGameExtension.Command
{
    [Command(PackageIds.CommandLoad)]
    internal sealed class CommandLoad : BaseCommand<CommandLoad>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            Browser.Start();
        }
    } 
}

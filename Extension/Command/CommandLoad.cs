
using CodingBrowser;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;
using Community.VisualStudio.Toolkit;
using CodingGameExtension;

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

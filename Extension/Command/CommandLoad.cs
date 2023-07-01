using CodinGameExtension.Tools;
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
        protected override Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            Browser.Start(new VsManager().GetStartupUrl());
            return Task.CompletedTask;
        }
    } 
}

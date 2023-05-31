using CodinGameExtension.Tools;
using CodingBrowser;
using CodingGameExtension;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Linq;
using Task = System.Threading.Tasks.Task;

namespace CodinGameExtension.Command
{
    [Command(PackageIds.CommandPush)]
    internal sealed class CommandPush : BaseCommand<CommandPush>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var vs = new VsManager();

            vs.SaveAllDocument();

            var generator = vs.GetCodeGenerator();

            generator.AddFiles(vs.ProjectFiles.Select(f => new FileInfo(f)));

            var b = Browser.Start();

            if (b.CanSendCode())
                b.SendCode(generator.GetCode());
            else
            {
                var m = new MessageBox();
                await m.ShowAsync("Can't find element to send code");
            }
        }
    }
}

using CodingBrowser;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using Interop = Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio;
using EnvDTE80;
using EnvDTE;
using CodinGameExtension.Tools;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CodingGameExtension;
using Community.VisualStudio.Toolkit;

namespace CodinGameExtension.Command
{

    [Command(PackageIds.CommandPushNPlay)]
    internal sealed class CommandPushNPlay : BaseCommand<CommandPushNPlay>
    {

        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var vs = new VsManager();

            vs.SaveAllDocument();

            var generator = vs.GetCodeGenerator();

            generator.AddFiles(vs.ProjectFiles.Select(f => new FileInfo(f)));

            var b = Browser.Start();


            if (b.CanSendCode())
            {
                b.SendCode(generator.GetCode());
                if (b.CanLaunchTest())
                    b.LaunchTest();
                else
                {
                    var m = new Community.VisualStudio.Toolkit.MessageBox();
                    await m.ShowAsync("Can't find element to launch test");
                }
            }
            else
            {
                var m = new Community.VisualStudio.Toolkit.MessageBox();
                await m.ShowAsync("Can't find element to send code");
            }

        }
    }

}

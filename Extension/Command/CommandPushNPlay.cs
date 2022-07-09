
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

namespace CodinGameExtension.Command
{
    internal sealed class CommandPushNPlay
    {

        public const int CommandId = 0x0102;

        public static readonly Guid CommandSet = new Guid("24fae27f-5144-4741-b6fb-f2f8821376e1");

        private readonly AsyncPackage package;


        private CommandPushNPlay(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static CommandPushNPlay Instance
        {
            get;
            private set;
        }

        private IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandPushNPlay(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            var vs = new VsManager();

            vs.SaveAllDocument();

            var cSharpProject = new CSharpProject();

            cSharpProject.AddFiles(vs.getProjetFiles().Select(f => new FileInfo(f)));

            var b = Browser.Start();


            if (b.CanSendCode())
            {
                b.SendCode(cSharpProject.GetCode());
                if (b.CanLaunchTest())
                    b.LaunchTest();
                else
                    MessageBox.Show("Can't find element to launch test");
            }
            else
                MessageBox.Show("Can't find element to send code");

        }
    }

}

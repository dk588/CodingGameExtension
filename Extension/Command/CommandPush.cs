
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
    /// <summary>
    /// Command handler
    /// 
    /// https://docs.microsoft.com/en-us/visualstudio/extensibility/saving-data-in-project-files?view=vs-2022
    /// 
    /// </summary>
    internal sealed class CommandPush
    {

        public const int CommandId = 0x0101;

        public static readonly Guid CommandSet = new Guid("24fae27f-5144-4741-b6fb-f2f8821376e1");

        private readonly AsyncPackage package;


        private CommandPush(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static CommandPush Instance
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
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandPush(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            var vs = new VsManager();

            vs.SaveAllDocument();

            var cSharpProject = new CSharpProject();

            cSharpProject.AddFiles(vs.getProjetFiles().Select(f => new FileInfo(f)));

            var b = Browser.Start();

            if (b.CanSendCode())
                b.SendCode(cSharpProject.GetCode());
            else
                MessageBox.Show("Can't find element to send code");
        }
    }

}

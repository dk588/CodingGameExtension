
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

namespace CodingGameExtension.Command
{
    /// <summary>
    /// Command handler
    /// 
    /// https://docs.microsoft.com/en-us/visualstudio/extensibility/saving-data-in-project-files?view=vs-2022
    /// 
    /// </summary>
    internal sealed class CommandRetrieve
    {
        public const int CommandId = 0x0101;

        public static readonly Guid CommandSet = new Guid("24fae27f-5144-4741-b6fb-f2f8821376e1");

        private readonly AsyncPackage package;


        private CommandRetrieve(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        public static CommandRetrieve Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new CommandRetrieve(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            // var b = Browser.Start();

            package.GetServiceAsync(typeof(EnvDTE.DTE));
            DTE2 dte = (DTE2)Package.GetGlobalService(typeof(EnvDTE.DTE));
            var xx = dte.ActiveDocument.Type;
            var tt = (EnvDTE.TextDocument) dte.ActiveDocument.Object();

            // tt.Selection.SelectAll();
            //tt.Selection.Insert(b.RetrieveCode());

        }
 }

}

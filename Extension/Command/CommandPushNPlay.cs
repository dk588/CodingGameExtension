﻿using CodinGameExtension.Tools;
using CodingBrowser;
using CodingGameExtension;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Linq;
using Task = System.Threading.Tasks.Task;

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

            var b = Browser.Start(vs.GetStartupUrl());

            if (b.CanSendCode())
            {
                b.SendCode(generator.GetCode());
                if (b.CanLaunchTest())
                    b.LaunchTest();
                else
                {
                    await new MessageBox().ShowErrorAsync("Can't find element to launch test");
                }
            }
            else
            {
                await new MessageBox().ShowAsync("Can't find element to send code");
            }
        }
    }
}
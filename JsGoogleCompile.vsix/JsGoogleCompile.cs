//------------------------------------------------------------------------------
// <copyright file="JsGoogleCompile.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace JsGoogleCompile.vsix
{
    using System;
    using System.ComponentModel.Design;
    using Microsoft.VisualStudio.Shell;
    using EnvDTE;

    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class JsGoogleCompile
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("8b2c61b8-73ec-406f-82b5-8dae60b0b39b");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        private static ErrorListHelper errorListHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsGoogleCompile"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private JsGoogleCompile(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            if (errorListHelper == null)
            {
                errorListHelper = new ErrorListHelper();
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static JsGoogleCompile Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
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
        public static void Initialize(Package package)
        {
            Instance = new JsGoogleCompile(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            const string CompilerUrl = @"http://closure-compiler.appspot.com/compile";
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            Document doc = dte.ActiveDocument;
            
            TextDocument txt = doc.Object() as TextDocument;

            var requestCompile = new RequestCompile(
                doc.FullName,
                CompilationLevelHelper.AdvancedOptimizations,
                CompilerUrl);

            errorListHelper.Remove();

            var compilerResults = requestCompile.Run();
            var errorCount = compilerResults.Errors == null ? 0 : compilerResults.Errors.Count;
            if (errorCount > 0)
            {
                foreach (var compilerError in compilerResults.Errors)
                {
                    errorListHelper.Write(
                        TaskCategory.Misc,
                        TaskErrorCategory.Error,
                        "Some Context",
                        compilerError.Error,
                        "sample.js",
                        compilerError.Lineno,
                        compilerError.Charno);
                }
            }

            var warningCount = compilerResults.Warnings == null ? 0 : compilerResults.Warnings.Count;
            if (warningCount > 0)
            {
                foreach (var compilerWarning in compilerResults.Warnings)
                {
                    errorListHelper.Write(
                        TaskCategory.Misc,
                        TaskErrorCategory.Warning,
                        "Some Context",
                        compilerWarning.Warning,
                        "sample.js",
                        compilerWarning.Lineno,
                        compilerWarning.Charno);
                }
            }

            // Show a message box to prove we were here
            ////string message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", this.GetType().FullName);
            ////string title = "JsGoogleCompile";
            ////VsShellUtilities.ShowMessageBox(
            ////    this.ServiceProvider,
            ////    message,
            ////    title,
            ////    OLEMSGICON.OLEMSGICON_INFO,
            ////    OLEMSGBUTTON.OLEMSGBUTTON_OK,
            ////    OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);

            ////helper.Remove();
        }
    }
}

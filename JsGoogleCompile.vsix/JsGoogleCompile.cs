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
    using System.Windows.Forms;
    using System.IO;
    using Microsoft.VisualStudio;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.Shell.Interop;/// <summary>
                                               /// Command handler
                                               /// </summary>
    internal sealed class JsGoogleCompile
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int IdeActiveDocContextMenuCommandId = 0x0100;

        public const int ProjectFileContextMenuCommandId = 0x0101;

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
                var menuCommandID = new CommandID(CommandSet, IdeActiveDocContextMenuCommandId);

                var menuItem = new OleMenuCommand(MenuItemCallback, menuCommandID);
                menuItem.BeforeQueryStatus += menuCommand_BeforeQueryStatus;

                commandService.AddCommand(menuItem);
            }
        }

        private void menuCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand != null)
            {
                // start by assuming that the menu will not be shown
                menuCommand.Visible = false;
                menuCommand.Enabled = false;

                var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
                var doc = dte.ActiveDocument;

                if (doc == null)
                {
                    return;
                }

                if (Path.GetExtension(doc.FullName).ToUpper() != ".JS")
                {
                    return;
                }

                menuCommand.Visible = true;
                menuCommand.Enabled = true;
            }
        }

        public static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;
            int hr = VSConstants.S_OK;

            var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            if (monitorSelection == null || solution == null)
            {
                return false;
            }

            IVsMultiItemSelect multiItemSelect = null;
            IntPtr hierarchyPtr = IntPtr.Zero;
            IntPtr selectionContainerPtr = IntPtr.Zero;

            try
            {
                hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);

                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemid == VSConstants.VSITEMID_NIL)
                {
                    // there is no selection
                    return false;
                }

                // multiple items are selected
                if (multiItemSelect != null) return false;

                // there is a hierarchy root node selected, thus it is not a single item inside a project

                if (itemid == VSConstants.VSITEMID_ROOT) return false;

                hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
                if (hierarchy == null) return false;

                Guid guidProjectID = Guid.Empty;

                if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectID)))
                {
                    return false; // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid
                }

                // if we got this far then there is a single project item selected
                return true;
            }
            finally
            {
                if (selectionContainerPtr != IntPtr.Zero)
                {
                    Marshal.Release(selectionContainerPtr);
                }

                if (hierarchyPtr != IntPtr.Zero)
                {
                    Marshal.Release(hierarchyPtr);
                }
            }
        }
        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static JsGoogleCompile Instance { get; private set; }

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
            var dte = Package.GetGlobalService(typeof(DTE)) as DTE;
            var doc = dte.ActiveDocument;

            if (doc == null)
            {
                MessageBox.Show("Please open a JavaScript file", "JS Google Closure Compiler");
                return;
            }

            errorListHelper.Remove();

            const string CompilerUrl = @"http://closure-compiler.appspot.com/compile";
            var requestCompile = new RequestCompile(
                doc.FullName,
                CompilationLevelHelper.AdvancedOptimizations,
                CompilerUrl);

            Action asyncRunner = () =>
            {
                var compilerResults = requestCompile.Run();
                var resultsWriter = new ResultsWriter(errorListHelper);
                resultsWriter.EmitErrors(compilerResults);
                resultsWriter.EmitWarnings(compilerResults);
            };

            var task1 = new System.Threading.Tasks.Task(asyncRunner);
            task1.Start();
        }

        private void EmitResults(ICompilerResults compilerResults)
        {
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
        }
    }
}

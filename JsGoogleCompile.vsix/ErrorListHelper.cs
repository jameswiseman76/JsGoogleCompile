namespace JsGoogleCompile.vsix
{
    using System;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio;
    using Microsoft.VisualStudio.TextManager.Interop;
    using System.Diagnostics;

    public class ErrorListHelper : IServiceProvider
    {
        public static ErrorListProvider errorListProvider { get; set; }

        public ErrorListHelper()
        {
            if (errorListProvider == null)
            {
                errorListProvider = GetErrorListProvider();
            }
        }

        public object GetService(Type serviceType)
        {
            return Package.GetGlobalService(serviceType);
        }

        public ErrorListProvider GetErrorListProvider()
        {
            ErrorListProvider provider = new ErrorListProvider(this); //this implementing IServiceProvider
            provider.ProviderName = "JS Google Closure Compiler";
            provider.ProviderGuid = Guid.Parse("DD9ACCF5-3445-452F-92A1-0649774B6E32");
            return provider;
        }

        public void Write(
                    TaskCategory category,
                    TaskErrorCategory errorCategory,
                    string context, //used as an indicator when removing
                    string text,
                    string document,
                    int line,
                    int column)
        {
            ErrorTask task = new ErrorTask();
            task.Text = text;
            task.ErrorCategory = errorCategory;

            //The task list does +1 before showing this numbers
            task.Line = line - 1;
            task.Column = column - 1;
            task.Document = document;
            task.Category = category;
            
            if (!string.IsNullOrEmpty(document))
            {
                //attach to the navigate event
                task.Navigate += NavigateDocument;
            }

            //add it to the errorlistprovider
            errorListProvider.Tasks.Add(task);

            errorListProvider.BringToFront();
        }

        public void Remove()
        {
            errorListProvider.Tasks.Clear();
        }

        public static void OpenDocumentAndNavigateTo(string path, int line, int column)
        {
            IVsUIShellOpenDocument openDoc = Package.GetGlobalService(typeof(IVsUIShellOpenDocument)) as IVsUIShellOpenDocument;

            if (openDoc == null)
            {
                return;
            }

            IVsWindowFrame frame;
            Microsoft.VisualStudio.OLE.Interop.IServiceProvider sp;
            IVsUIHierarchy hier;
            uint itemid;
            Guid logicalView = VSConstants.LOGVIEWID_Code;

            if (ErrorHandler.Failed(
                openDoc.OpenDocumentViaProject(path, ref logicalView, out sp, out hier, out itemid, out frame))
                || frame == null)
            {
                return;
            }

            object docData;
            frame.GetProperty((int)__VSFPROPID.VSFPROPID_DocData, out docData);

            // Get the VsTextBuffer  
            VsTextBuffer buffer = docData as VsTextBuffer;
            if (buffer == null)
            {
                IVsTextBufferProvider bufferProvider = docData as IVsTextBufferProvider;
                if (bufferProvider != null)
                {
                    IVsTextLines lines;
                    ErrorHandler.ThrowOnFailure(bufferProvider.GetTextBuffer(out lines));
                    buffer = lines as VsTextBuffer;
                    Debug.Assert(buffer != null, "IVsTextLines does not implement IVsTextBuffer");
                    if (buffer == null)
                    {
                        return;
                    }
                }
            }

            // Finally, perform the navigation.  
            IVsTextManager mgr = Package.GetGlobalService(typeof(VsTextManagerClass))
                 as IVsTextManager;
            if (mgr == null)
            {
                return;
            }
            mgr.NavigateToLineAndColumn(buffer, ref logicalView, line, column, line, column);
        }

        void NavigateDocument(object sender, EventArgs e)
        {
            Task task = sender as Task;
            if (task == null)
            {
                throw new ArgumentException("sender");
            }
            //use the helper class to handle the navigation
            OpenDocumentAndNavigateTo(task.Document, task.Line, task.Column);
        }
    }
}

namespace JsGoogleCompile.vsix
{
    using Microsoft.VisualStudio.Shell;

    public class ResultsWriter : IResultsOutput
    {
        private readonly ErrorListHelper errorListHelper;

        public ResultsWriter(ErrorListHelper errorListHelper)
        {
            Guard.ArgumentNotNull(() => errorListHelper, errorListHelper);

            this.errorListHelper = errorListHelper;
        }

        public void EmitWarnings(ICompilerResults compilerResults)
        {
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

        public void EmitErrors(ICompilerResults compilerResults)
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

        }

        public void EmitSummary(ICompilerResults compilerResults)
        {
            // do nothing
        }
    }
}

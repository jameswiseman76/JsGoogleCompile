using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsGoogleCompile.JSON
{
    [Serializable]
    class CompilerResults
    {
        public string compiledCode { get; set; }
        public List<CompilerErrors> errors { get; set; }
        public List<CompilerErrors> warnings { get; set; }
        public CompilerStatistics statistics { get; set; }
        public string outputFilePath { get; set; }
    }
}

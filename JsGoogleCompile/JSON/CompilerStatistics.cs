using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsGoogleCompile.JSON
{
    [Serializable]
    class CompilerStatistics
    {
        public int originalSize { get; set; }
        public int originalGzipSize { get; set; }
        public int compressedSize { get; set; }
        public int compressedGzipSize { get; set; }
        public int compileTime { get; set; }
    }
}

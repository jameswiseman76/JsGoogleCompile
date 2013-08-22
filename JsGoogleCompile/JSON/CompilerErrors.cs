using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsGoogleCompile.JSON
{
    [Serializable]
    public class CompilerErrors
    {
        public string type { get; set; }
        public string file { get; set; }
        public int lineno { get; set; }
        public int charno { get; set; }
        public string error { get; set; }
        public string warning { get; set; }
        public string line { get; set; }
    }
}

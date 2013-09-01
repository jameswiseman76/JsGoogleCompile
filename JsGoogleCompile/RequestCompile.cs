using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Web.Script.Serialization;
using JsGoogleCompile.JSON;

namespace JsGoogleCompile
{
    class RequestCompile
    {
        static string fileName = string.Empty;
        static string compileLevel = string.Empty;

        static void Main(string[] args)
        {
            try
            {
                Compiler compiler = new Compiler();

                if (!RequestCompile.CheckUsageInstructions(args))
                {
                    return;
                }

                Console.WriteLine("Requesting compile from http://closure-compiler.appspot.com/compile...");
                Console.WriteLine();

                fileName = args[0];

                var responseFromServer = compiler.CompileJsFile(fileName, compileLevel);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                CompilerResults cr = jss.Deserialize<CompilerResults>(responseFromServer);

                //cr.errors.c
                int errorCount = 0, warningCount = 0;
                if (cr.errors != null)
                {
                    errorCount = cr.errors.Count;
                }

                if (cr.warnings != null)
                {
                    warningCount = cr.warnings.Count;
                }


                if (errorCount > 0)
                {
                    foreach (CompilerErrors ce in cr.errors)
                    {
                        Console.WriteLine(fileName + "(" + ce.lineno + "): ERROR " + ce.error);
                        Console.WriteLine(ce.line.TrimStart());
                    }
                }

                if (warningCount > 0)
                {
                    foreach (CompilerErrors cw in cr.warnings)
                    {
                        Console.WriteLine(fileName + "(" + cw.lineno + "): WARNING " + cw.warning);
                        Console.WriteLine(cw.line.TrimStart());
                    }
                }

                Console.WriteLine("----------------------------");
                Console.WriteLine("Completed Scan");

                if (warningCount > 0 || errorCount > 0)
                {
                    Console.WriteLine("Found " + errorCount + " Errors, " + warningCount + " Warnings");
                }
                else
                {
                    Console.WriteLine("No Errors or Warnings Found!");
                }

                if (errorCount <= 0)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine("Code Emitted:");
                    Console.WriteLine(cr.compiledCode);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Occurred Running GoogleCC:");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
        public static bool CheckUsageInstructions(string[] args)
        {
            if ( (args.Length == 2) && (args[1].Substring(1, 1).ToUpper() == "C"))
            {
                fileName = args[0];
                compileLevel = args[1].Substring(2, 1);
                return true;
            }
            if ( (args.Length == 1) && (args[0]!= "/?"))
            {
                compileLevel = "a";
                fileName = args[0];
                return true;
            }
            else
            {
                EmitUsageInstructions();
                return false;
            } 
        }

        private static void EmitUsageInstructions()
        {
            Console.WriteLine("JsGoogleCompile: Request a compile from the Google Closure Compiler service");
            Console.WriteLine("(http://closure-compiler.appspot.com/compile)");
            Console.WriteLine();
            Console.WriteLine("Usage:\t\tJsGoogleCompile.exe FileName [/c[attribute]]");
            Console.WriteLine("FileName:\tThe full filename and path of the file on disk to compress");
            Console.WriteLine("/c: \t\tSpecify compilation level. (If omitted 'advanced' is assumed)");
            Console.WriteLine("attribute: \t w: Whitespace only");
            Console.WriteLine("\t\t s: Simple optimisations");
            Console.WriteLine("\t\t a: Advianced optimisations");
        }
    }
}

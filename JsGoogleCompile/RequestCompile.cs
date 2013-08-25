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
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Requesting compile from http://closure-compiler.appspot.com/compile...");
                Console.WriteLine();

                Compiler compiler = new Compiler();

                var fileName = string.Empty;
                if (args.Length > 0)
                {
                    fileName = args[0];
                }
                else
                {
                    //for testing 
                    fileName = @"..\..\sample.js";
                    
                    //for release
                    // Console.WriteLine("Usage: JsGoogleCompile.exe [FileName]");
                    // return;
                }


                var responseFromServer = compiler.CompileJsFile(fileName);

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
    }
}

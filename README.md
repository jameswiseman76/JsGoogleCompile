JsGoogleCompile
===============

Compile Your JavaScript with Google Closure Compiler

This tool can be plugged into almost all versions of Visual Studio that support "External Tools". 

It has also been shown to work in other IDEs tht support a similar functionality, including NetBeans and Eclipse. 

All you need for this to work in your IDE is for it to visualise text written to the standard output stream, and for it to pass the fully qualified file path and name of the currently-open file.

For exmaple, in Visual Studio:

1) In your Visual Studio IDE, navigate to Tools->External Tools. 

2) Add a menu item like so with:

 - Title: Whatever you want  - I called it &GCC (the ampersand allows you to navigate to it via the keyboard)
 - Command: This is the full path of JsGoogleCompile.exe
 - Arguments: $(ItemPath)
 - Initial directory: $(BinDir)
 - Tick "Use Output Window"

3) Open up your favourite JavaScript file

4) Select the new menu option

5) JsGoogleCompile.exe will emit to the IDE output window.

(Check out the screenshots under the "About" folder of the project)

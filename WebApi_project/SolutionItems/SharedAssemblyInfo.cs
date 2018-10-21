using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Adam S. Grzonkowski")]
[assembly: AssemblyProduct("Example WebApi application.")]
[assembly: AssemblyCopyright("Copyright © Adam S. Grzonkowski 2018. Licensed under GPL.")]
[assembly: AssemblyTrademark("")]


// Make it easy to distinguish Debug and Release builds;
// for example, through the file properties window.

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
[assembly: AssemblyDescription("Built with Debug settings.")]

#else

[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyDescription("Built with Release settings.")]

#endif

[assembly: CLSCompliant(true)]


// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.

[assembly: ComVisible(false)]


// The AssemblyFileVersionAttribute is incremented with every build in order
// to distinguish one build from another. AssemblyFileVersion is specified
// in AssemblyVersionInfo.cs so that it can be easily incremented by the
// automated build process.

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
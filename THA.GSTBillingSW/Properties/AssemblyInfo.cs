using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("THA.GSTBillingSW")]
[assembly: AssemblyDescription("GST Billing and Inventroy Management SW")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("THAsoft")]
[assembly: AssemblyProduct("THA.GSTBillingSW")]
[assembly: AssemblyCopyright("THAsoft ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("78d39b09-561e-4e79-bde3-388bc1f346bb")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
// Let log4net know that it can look for configuration in the default application config file
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
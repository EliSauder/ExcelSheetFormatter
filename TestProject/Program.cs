using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace TestProject {
    class Program {
        static void Main(string[] args) {
            string appdata = Environment.ExpandEnvironmentVariables(@"%APPDATA%\%ASSEMBLY%\config.xml");
            appdata = Regex.Replace(appdata, "%PRGMNAME%", Assembly.GetExecutingAssembly().GetName().Name, RegexOptions.IgnoreCase); //appdata.Replace("%ASSEMBLY%", Assembly.GetExecutingAssembly().GetName().Name, StringComparison.OrdinalIgnoreCase);

            Console.WriteLine(appdata);
        }
    }
}

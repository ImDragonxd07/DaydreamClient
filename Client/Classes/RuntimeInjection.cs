using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daydream.Client.Classes
{
    internal class RuntimeInjection
    {
        public static void load(string code)
        {
            Utility.Logger.externallog("Running external code");
            var script = CSharpScript.Create<int>(code);
            Compilation compilation = script.GetCompilation();
        }
    }
}

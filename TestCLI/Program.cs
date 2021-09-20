using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FoxHollow.TerminalUI;
using FoxHollow.TerminalUI.Elements;
using FoxHollow.TerminalUI.Types;

namespace TestCLI
{
    class Program
    {
        /// <summary>
        ///     Simple wrapper for Task.Delay that returns a boolean indicating whether 
        ///     the request was canceled or not.
        /// </summary>
        /// <param name="milliseconds">Milliseconds to delay</param>
        /// <param name="cToken">Token that allows for cancellation of the delay</param>
        /// <returns>
        ///     Booleaning indicating cancel status.
        /// 
        ///     false = Delay was canceled prematurely
        ///      true = Delay executed to the end without being canceled
        /// </returns>
        public static async Task<bool> Delay(int milliseconds, CancellationToken cToken)
        {
            try {
                await Task.Delay(milliseconds, cToken);

                return true;
            }
            catch(TaskCanceledException)
            {
                 return false;
            }
        }
        
        static void Main(string[] args)
        {
            Terminal.Run("TestCLI", "TerminalUI", Entry);
        }

        private static Text _text;
        private static KeyValueText _kvt;
        private static SplitLine _split;
        private static ProgressBar _progress;
        // private static Pager _pager;

        static async Task Entry(CancellationTokenSource cts)
        {
            _text = new Text("This is a test...", foregroundColor: ConsoleColor.Cyan, area: TerminalArea.RightHalf, show: true);
            Terminal.NextLine();

            TerminalColor.PagerLineNumberBackground = TerminalColor.DefaultBackground;

            List<string> lines = new List<string>();

            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            lines.Add("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");

            // _pager = new Pager(lines: lines, headerText: "meow", showLineNumbers: true, highlightText: "System", area: TerminalArea.RightHalf, show: true);
            // _pager = new Pager(headerText: "meow", showLineNumbers: true, highlightText: "snap", show: false);

            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TestCLI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/home/flip/code/TerminalUI/TestCLI/bin/Debug/net5.0/TerminalUI.dll'. Symbols loaded.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/Microsoft.Win32.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Memory.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.RuntimeInformation.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");
            // _pager.AppendLine("Loaded '/snap/dotnet-sdk/120/shared/Microsoft.NETCore.App/5.0.5/System.Runtime.InteropServices.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.");

            // await _pager.RunAsync(cts.Token);

            Terminal.NextLine();

            _kvt = new KeyValueText("test", "meow", -10, area: TerminalArea.RightHalf, show: true);
            Terminal.NextLine();

            _split = new SplitLine("meow", "hey", area: TerminalArea.RightHalf, show: true); 
            NotificationBox _box = new NotificationBox(height: 25, borderBackgroundColor: ConsoleColor.DarkBlue, defaultBackgroundColor: ConsoleColor.DarkBlue, area: TerminalArea.RightHalf);
            Terminal.NextLine();

            _progress = new ProgressBar(area: TerminalArea.RightHalf, show: true);
            Terminal.NextLine();
            Terminal.NextLine();

            Header _header = new Header("test left", area: TerminalArea.RightHalf, show: true);
            Terminal.NextLine();
            Terminal.NextLine();
            Terminal.NextLine();

            if (!await Delay(2000, cts.Token))
                return;

            _box.UpdateLine(0, "meow", TextJustify.Center, ConsoleColor.Cyan);
            _box.Show();

            if (!await Delay(3000, cts.Token))
                return;

            _box.Hide();

            // List<DataTableColumn> columns = new List<DataTableColumn>()
            // {
            //     new DataTableColumn("Name", "Person Name"),
            //     new DataTableColumn("Age", "Age")
            // };

            // List<Person> people = new List<Person>()
            // {
            //     new Person("Steve Smith", "Male", 48, null),
            //     new Person("John Smith", "Male", 37, null),
            //     new Person("Jane Doe", "Female", 28, "Smith"),
            //     new Person("Janet Brown", "Female", 2, "Smith"),
            //     new Person("James", "Male", 12, null)
            // };
            
            // DataTable _dataTable = new DataTable(people, columns, showHeader: true, area: TerminalArea.LeftHalf, show: false);

            await Loop(cts);
        }

        static async Task Loop(CancellationTokenSource cts)
        {
            int i = 0;

            while (!cts.IsCancellationRequested)
            {
                try {
                    await Task.Delay(1000, cts.Token);
                }
                catch (TaskCanceledException) { }

                i++;

                // _text.UpdateValue($"interval {i}");
                // _kvt.UpdateValue($"interval {i}");

                // if (i <= 10)
                    // _progress.UpdateProgress((double)i / 10.0);
                
                // Terminal.WriteLine("meow");
            }

        }

        public class Person
        {
            public string Name { get; set; }
            public string Gender { get; set; }
            public int Age { get; set; }
            public string MaidenName { get; set; }

            public Person (string name, string gender, int age, string maidenName)
            {
                this.Name = name;
                this.Gender = gender;
                this.Age = age;
                this.MaidenName = maidenName;
            }
        }
    }
}

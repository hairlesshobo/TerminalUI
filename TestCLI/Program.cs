﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerminalUI;
using TerminalUI.Elements;
using TerminalUI.Types;

namespace TestCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Terminal.Run("TestCLI", "TerminalUI", Entry);
        }

        private static Text _text;
        private static KeyValueText _kvt;
        private static SplitLine _split;
        private static ProgressBar _progress;
        private static Pager _pager;

        static async Task Entry(CancellationTokenSource cts)
        {
            _text = new Text("This is a test...", foregroundColor: ConsoleColor.Cyan, show: true);
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

            _kvt = new KeyValueText("test", "meow", -10, show: false);
            Terminal.NextLine();

            _split = new SplitLine("meow", "hey", area: TerminalArea.RightHalf, show: true);
            Terminal.NextLine();

            _progress = new ProgressBar(area: TerminalArea.LeftHalf, show: true);
            Terminal.NextLine();
            Terminal.NextLine();

            List<DataTableColumn> columns = new List<DataTableColumn>();
            
            // DataTable _dataTable = new DataTable() show: true);

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
    }
}

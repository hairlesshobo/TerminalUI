﻿/**
 *  TerminalUI - Simple terminal widgets for C#
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation; either version 2.1 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TerminalUI.Elements;

namespace TerminalUI
{
    public static class Terminal
    {
        private static TextWriter debugWriter = TextWriter.Null;
        public static Header Header { get; private set; } = null;
        public static StatusBar StatusBar { get; private set; } = null;
        public static TerminalPoint RootPoint { get; private set; } = TerminalPoint.GetCurrent();

        public static int UsableHeight => Terminal.Height - (Header == null ? 0 : 2) - (StatusBar == null ? 0 : 1);
        public static int UsableWidth => Terminal.Width;
        public static int Width => Console.WindowWidth;
        public static int Height => Console.WindowHeight;
        public static int Left => Console.CursorLeft;
        public static int Top => Console.CursorTop;
        public static int UsableBottomn => Console.WindowHeight - 1 - (StatusBar == null ? 0 : 1);

        public static ConsoleColor BackgroundColor
        {
            get => Console.BackgroundColor;
            set => Console.BackgroundColor = value;
        }

        public static ConsoleColor ForegroundColor
        {
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
        }

        private static bool _cursorVisible = true;

        public static bool CursorVisible
        {
            get => (OperatingSystem.IsWindows() ? Console.CursorVisible : _cursorVisible);
            set => Console.CursorVisible = _cursorVisible = value;
        }

        static Terminal()
        {
            string debugLogPath = Environment.GetEnvironmentVariable("TUI_DEBUG_LOG");

            if (!String.IsNullOrWhiteSpace(debugLogPath))
                debugWriter = File.CreateText(debugLogPath);
        }

        /// <summary>
        ///     Proxy method for <see cref="Console.Write(char)" />. Reserved for future use
        /// </summary>
        /// <param name="input">Character to write to the terminal</param>
        public static void Write(char input) => Console.Write(input);

        /// <summary>
        ///     Proxy method for <see cref="Console.Write(string)" />. Reserved for future use
        /// </summary>
        /// <param name="input">String to write to the terminal</param>
        public static void Write(string input) => Console.Write(input);


        /// <summary>
        ///     Parses a string for color control codes and then writes the string to the terminal
        /// </summary>
        /// <param name="input">String to parse and write to the terminal</param>
        public static void WriteParsed(string input) => Console.Write(input);
        
        public static void WriteColor(ConsoleColor color, char inputChar)
            => WriteColor(color, new string(new char[] { inputChar }));

        public static void WriteColor(ConsoleColor color, string inputString)
        {
            Terminal.ForegroundColor = color;
            Terminal.Write(inputString);
            Terminal.ResetForeground();
        }

        public static void WriteColorBG(ConsoleColor color, string inputString)
        {
            Terminal.BackgroundColor = color;
            Terminal.Write(inputString);
            Terminal.ResetBackground();
        }

        public static void WriteLine() 
            => Terminal.NextLine();

        public static void WriteLine(string inputString)
        {
            Terminal.Write(inputString);
            Terminal.NextLine();
        }

        public static void WriteLineColor(ConsoleColor color, char inputChar)
            => WriteLineColor(color, new string(new char[] { inputChar }));

        public static void WriteLineColor(ConsoleColor color, string inputString)
        {
            WriteColor(color, inputString);
            Terminal.NextLine();
        }

        public static void WriteLineColorBG(ConsoleColor color, string inputString)
        {
            WriteColorBG(color, inputString);
            Terminal.NextLine();
        }

        public static void WriteDebugLine(string inputString)
        {
            debugWriter.WriteLine(inputString);
            debugWriter.Flush();
        }


        public static void NextLine()
            => Console.SetCursorPosition(0, Terminal.Top+1);

        public static void ResetColor()
        {
            ResetForeground();
            ResetBackground();
        }

        public static TerminalPoint MoveX(int x)
            => TerminalPoint.GetCurrent().AddX(x).MoveTo();

        public static void ResetForeground()
            => Console.ForegroundColor = TerminalColor.DefaultForeground;

        public static void ResetBackground()
            => Console.BackgroundColor = TerminalColor.DefaultBackground;

        public static void Clear(bool rawClear = false)
        {
            if (rawClear)
                Console.Clear();
            else
            {
                RootPoint.MoveTo();
                
                int height = Height;

                if (Header != null)
                    height -= 2;

                if (StatusBar != null)
                    height -= 1;

                StringBuilder sb = new StringBuilder();
                    
                for (int w = 0; w < Width; w++)
                    sb.Append(' ');

                string wideString = sb.ToString();

                for (int h = 0; h < height; h++)
                {
                    Console.CursorLeft = 0;
                    Terminal.Write(wideString);
                }

                RootPoint.MoveTo();

                // Console.Clear();

                // Header?.Redraw();
                // StatusBar?.Redraw();

                // RootPoint.MoveTo();
            }
        }

        public static Header InitHeader(string left, string right)
        {
            if (Header == null)
            {
                Header = new Header(left, right);
                RootPoint = new TerminalPoint(0, 2);
            }
            else
                Header.UpdateHeader(left, right);

            return Header;
        }

        public static StatusBar InitStatusBar(params StatusBarItem[] items)
        {
            if (StatusBar == null)
                StatusBar = new StatusBar(items);
            else
                StatusBar.ShowItems(items);

            return StatusBar;
        }

        public static void SetCursorPosition(int left, int top)
            => Console.SetCursorPosition(left, top);

        public static Task Start()
            => KeyInput.StartLoop();

        public static void Stop()
            => KeyInput.StopListening();

        public static void WaitForStop()
            => KeyInput.WaitForStop();
    }
}

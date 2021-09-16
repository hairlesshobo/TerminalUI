/**
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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TerminalUI.Elements;

namespace TerminalUI
{
    /// <summary>
    ///     Static helper class for simplifying a console-based, interactive user interface
    /// </summary>
    public static class Terminal
    {
        /// <summary>
        ///     Header object. Will be null until the first time <see cref="InitHeader" /> is called
        /// </summary>
        public static Header Header { get; private set; } = null;

        /// <summary>
        ///     StatusBar object. Will be null until the first time <see cref="InitStatusBar"/> is called
        /// </summary>
        public static StatusBar StatusBar { get; private set; } = null;

        /// <summary>
        ///     The root point of the terminal, that is, the Top-leftmost position that is 
        ///     not part of the header
        /// </summary>
        public static TerminalPoint RootPoint { get; private set; } = TerminalPoint.GetCurrent();

        /// <summary>
        ///     Number of lines that are usable by the application. This accounts for and 
        ///     deducts any lines that are in use by the header and/or status bar
        /// </summary>
        public static int UsableHeight => Terminal.Height - (Header == null ? 0 : 2) - (StatusBar == null ? 0 : 1);

        /// <summary>
        ///     Number of colums that are usable by the application
        /// </summary>
        public static int UsableWidth => Terminal.Width;

        /// <summary>
        ///     Width of the wonsole window
        /// </summary>
        internal static int Width => Console.WindowWidth;

        /// <summary>
        ///     Height of the console window
        /// </summary>
        internal static int Height => Console.WindowHeight;

        /// <summary>
        ///     Current left-position of the cursor
        /// </summary>
        internal static int Left
        {
            get => Console.CursorLeft;
            set => Console.CursorLeft = value;
        }

        /// <summary>
        ///     Current top-position of the cursor
        /// </summary>
        internal static int Top
        {
            get => Console.CursorTop;
            set => Console.CursorTop = value;
        }
        
        /// <summary>
        ///     Row number of the bottom-most usable row that is not a status bar
        /// </summary>
        internal static int UsableBottomn => Terminal.Height - 1 - (StatusBar == null ? 0 : 1);

        /// <summary>
        ///     Background color of the terminal. Only makes the call to change the 
        ///     color if the background is different from the requested color
        /// </summary>
        public static ConsoleColor BackgroundColor
        {
            get => _backgroundColor = Console.BackgroundColor;
            set
            {
                // only set the background color if it is different from the cached background color
                if (_backgroundColor != value)
                    Console.BackgroundColor = _backgroundColor = value;
            }
        }
        private static ConsoleColor _backgroundColor = Console.BackgroundColor;


        /// <summary>
        ///     Foreground color of the terminal. Only makes the call to change the 
        ///     color if the foreground is different from the requested color
        /// </summary>
        public static ConsoleColor ForegroundColor
        {
            get => _foregroundColor = Console.ForegroundColor;
            set
            {
                // only set the background color if it is different from the cached background color
                if (_foregroundColor != value)
                    Console.ForegroundColor = _foregroundColor = value;
            }
        }
        private static ConsoleColor _foregroundColor = Console.ForegroundColor;

        /// <summary>
        ///     Get or set the status of the cursor visibility
        /// </summary>
        public static bool CursorVisible
        {
            get => (OperatingSystem.IsWindows() ? Console.CursorVisible : _cursorVisible);
            set => Console.CursorVisible = _cursorVisible = value;
        }
        private static bool _cursorVisible = true;

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
        ///     Write a character to the terminal with the specified foreground color
        /// </summary>
        /// <param name="color">Color to write</param>
        /// <param name="inputChar">char to write</param>
        public static void WriteColor(ConsoleColor color, char inputChar)
            => WriteColor(color, new string(new char[] { inputChar }));

        /// <summary>
        ///     Write a string to the terminal using the specified foreground color
        /// </summary>
        /// <param name="color">Color to write</param>
        /// <param name="inputString">string to write</param>
        public static void WriteColor(ConsoleColor color, string inputString)
        {
            Terminal.ForegroundColor = color;
            Terminal.Write(inputString);
            Terminal.ResetForeground();
        }

        /// <summary>
        ///     Write a string to the terminal using the specified background color
        /// </summary>
        /// <param name="backgroundColor">Background color to use</param>
        /// <param name="inputString">String to write</param>
        public static void WriteColorBG(ConsoleColor backgroundColor, string inputString)
        {
            Terminal.BackgroundColor = backgroundColor;
            Terminal.Write(inputString);
            Terminal.ResetBackground();
        }

        /// <summary>
        ///     Write a blank line to the terminal (in other words, just move to the next line)
        /// </summary>
        public static void WriteLine() 
            => Terminal.NextLine();

        /// <summary>
        ///     Write a string to the terminal then move to the next line
        /// </summary>
        /// <param name="inputString">string to write</param>
        public static void WriteLine(string inputString)
        {
            Terminal.Write(inputString);
            Terminal.NextLine();
        }

        /// <summary>
        ///     Write a character to the terminal using the specified foreground color 
        ///     then move to the next line
        /// </summary>
        /// <param name="foregroundColor">Color to write</param>
        /// <param name="inputChar">character to write</param>
        public static void WriteLineColor(ConsoleColor foregroundColor, char inputChar)
            => WriteLineColor(foregroundColor, new string(new char[] { inputChar }));

        /// <summary>
        ///     Write a string to the terminal using the specified foreground
        ///     color then move to the next line
        /// </summary>
        /// <param name="color">Color to write</param>
        /// <param name="inputString">string to write</param>
        public static void WriteLineColor(ConsoleColor color, string inputString)
        {
            WriteColor(color, inputString);
            Terminal.NextLine();
        }

        /// <summary>
        ///     Write a string to the terminal using the specified background color
        ///     then move to the next line
        /// </summary>
        /// <param name="backgroundColor">background color to use</param>
        /// <param name="inputString">string to write</param>
        public static void WriteLineColorBG(ConsoleColor backgroundColor, string inputString)
        {
            WriteColorBG(backgroundColor, inputString);
            Terminal.NextLine();
        }

        /// <summary>
        ///     Move to the next line and position the cursor all the way to the left
        /// </summary>
        public static void NextLine()
            => Console.SetCursorPosition(0, Terminal.Top+1);

        /// <summary>
        ///     Move to the next line and position the cursor all the weay to the left 
        ///     of the specified area
        /// </summary>
        /// <param name="area">area to use for determining left position</param>
        public static void NextLine(TerminalArea area)
            => throw new NotImplementedException();

        /// <summary>
        ///     Reset both the foreground and the background color of the terminal
        /// </summary>
        public static void ResetColor()
        {
            ResetForeground();
            ResetBackground();
        }

        /// <summary>
        ///     Reset the foreground color to the default specified by 
        ///     <see cref="TerminalColor.DefaultForeground" />
        /// </summary>
        public static void ResetForeground()
            => Terminal.ForegroundColor = TerminalColor.DefaultForeground;

        /// <summary>
        ///     Reset the background color to the default specified by 
        ///     <see cref="TerminalColor.DefaultBackground" />
        /// </summary>
        public static void ResetBackground()
            => Terminal.BackgroundColor = TerminalColor.DefaultBackground;

        /// <summary>
        ///     Clear the terminal
        /// </summary>
        /// <param name="rawClear">if true, the entire console, header and status bar included, will be erased</param>
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

        /// <summary>
        ///     Initialize the header
        /// </summary>
        /// <param name="left">Text to show on the left of the header</param>
        /// <param name="right">Text to show on the right of the header</param>
        /// <returns>Header object</returns>
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

        /// <summary>
        ///     Initialize the status bar
        /// </summary>
        /// <param name="items">Items to include in the status bar</param>
        /// <returns>Status bar object</returns>
        public static StatusBar InitStatusBar(params StatusBarItem[] items)
        {
            if (StatusBar == null)
                StatusBar = new StatusBar(items);
            else
                StatusBar.ShowItems(items);

            return StatusBar;
        }

        /// <summary>
        ///     Move the cursor to the specified position
        /// </summary>
        /// <param name="left">left position</param>
        /// <param name="top">top position</param>
        public static void SetCursorPosition(int left, int top)
            => Console.SetCursorPosition(left, top);

        /// <summary>
        ///     Start the main loop.. this starts listening for input keys
        /// </summary>
        /// <returns>Task</returns>
        public static Task StartAsync()
            => KeyInput.StartLoop();

        /// <summary>
        ///     Shut down the main loop
        /// </summary>
        public static void Stop()
            => KeyInput.StopListening();

        /// <summary>
        ///     Wait for the main loop to stop processing
        /// </summary>
        public static void WaitForStop()
            => KeyInput.WaitForStop();
    }
}

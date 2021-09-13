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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TerminalUI;

namespace TerminalUI.Elements
{
    public class Pager : Element, IDisposable
    {
        #region Constants
        private const string _statusLineLeft = "Navigate: <up>/<down> or <pageUp>/<pageDown>  Exit: q";
        // private const string _statusLineLeft = "Navigate: <up>/<down> or <pageUp>/<pageDown>  Save To File: <ctrl>+s  Exit: q or <esc>";
        #endregion Constants
        
        #region Public Properties
        public bool AutoScroll { get; set; } = false;
        public bool ShowLineNumbers { get; set; } = false;
        public bool ShowHeader { get; set; } = false;
        public string HeaderText 
        { 
            get => _headerText;
            set
            {
                this.ShowHeader = !String.IsNullOrWhiteSpace(value);
                _headerText = value;
            }
        }
        public string HighlightText 
        { 
            get => _highlightText;
            set
            {
                this.Highlight = !String.IsNullOrWhiteSpace(value);
                _highlightText = value;
            }
        }

        public bool Highlight { get; set; } = false;
        public ConsoleColor HighlightColor { get; set; } = TerminalColor.PagerHighlightColorForeground;
        public int StartLine { get; set; } = 2;

        public int FirstLine => this.StartLine + (this.ShowHeader ? 1 : 0);
        public int WindowHeight => (Terminal.Height - this.StartLine - (this.ShowHeader ? 1 : 0));
        public int WindowWidth => Terminal.Width;
        public int LineNumberWidth => (this.ShowLineNumbers ? (_topLineIndexPointer + this.MaxLines).ToString().Length : 0);
        public int MaxWidth => (this.ShowLineNumbers ? this.WindowWidth - this.LineNumberWidth : this.WindowWidth);
        public int MaxLines => this.WindowHeight - 1;
        public bool DeferDraw => _deferDraw;
        public int BottomLine => Terminal.Height - 1;
        public bool Started => _started;
        public CancellationToken CancellationToken => _cts.Token;

        public TerminalPoint FirstTextLinePoint { get; private set; }
        #endregion Public Properties

        #region Private Fields
        private string _headerText = String.Empty;
        private string _highlightText = null;
        private List<string> _lines;
        private int _topLineIndexPointer = 0;

        private int _totalLines = 0;
        private bool _drawn = false;

        private bool _deferDraw = true;
        private bool _started = false;
        private TaskCompletionSource _tcs = null;
        private CancellationTokenSource _cts = null;

        #endregion Private Fields

        #region Constructor
        public Pager()
        {
            _lines = new List<string>();
            _cts = new CancellationTokenSource();

            this.TopLeftPoint = new TerminalPoint(0, 2);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, 2);
            this.BottomLeftPoint = new TerminalPoint(0, Terminal.Height - 1);
            this.BottomRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height - 1);

            this.FirstTextLinePoint = new TerminalPoint(0, this.TopLeftPoint.Top+1);
        }

        public Task RunAsync()
        {
            if (_tcs != null)
                return Task.CompletedTask;

            _tcs = new TaskCompletionSource();
            _started = true;
            
            Setup();

            if (!_drawn)
                Redraw();

            return _tcs.Task;
        }

        public void Stop()
            => _tcs?.TrySetResult();

        public void Start()
        {
            _deferDraw = false;            
            RunAsync();
            WriteHeader();
        }

        public static Pager StartNew()
        {
            Pager pager = new Pager();
            pager.Start();

            return pager;
        }

        public async Task WaitForQuitAsync()
        {
            if (_tcs != null)
                await _tcs.Task;
        }
        #endregion Constructor

        public void AppendLine()
            => AppendLine(string.Empty);

        public void AppendLine(string line)
        {
            if (line == null)
                line = String.Empty;
                
            _lines.Add(line);
            _totalLines++;

            if (_started)
            {
                if (this.AutoScroll == true)
                    ScrollToBottom();
                else
                {
                    if (DeferDraw == true)
                    {
                        if (_totalLines >= this.MaxLines)
                        {
                            if (_drawn == false)
                                Redraw();
                            // else
                            //     WriteStatusBar();
                        }
                    }
                    else
                    {
                        // we haven't filled the screen yet, so instead of redrawing the whole thing, 
                        // lets just append the line. this should prove to be immensely faster than a 
                        // complete redraw each time
                        if (_totalLines <= this.MaxLines)
                        {
                            DrawLine(_totalLines-1, line);
                            // this.FirstTextLinePoint.AddY(_totalLines).MoveTo();

                        }
                        else if (_totalLines - _topLineIndexPointer <= this.MaxLines)
                            Redraw();
                        // here we only update the status bar because the line that is being added is
                        // below the visible point on the screen
                        // else
                        //     WriteStatusBar();
                    }
                }
            }
        }

        public void ScrollToTop()
        {
            _topLineIndexPointer = 0;

            Redraw();
        }

        public void ScrollToBottom()
        {
            _topLineIndexPointer = _totalLines - this.MaxLines;

            Redraw();
        }

        public void UpLine()
        {
            _topLineIndexPointer--;

            if (_topLineIndexPointer < 0)
                _topLineIndexPointer = 0;

            Redraw();
        }

        public void UpPage()
        {
            _topLineIndexPointer -= this.MaxLines;

            if (_topLineIndexPointer < 0)
                _topLineIndexPointer = 0;

            Redraw();
        }


        public void DownLine()
        {
            _topLineIndexPointer++;

            if (_topLineIndexPointer + this.MaxLines >= _totalLines)
                _topLineIndexPointer = _totalLines - (this.MaxLines > _totalLines ? _totalLines : this.MaxLines);

            Redraw();
        }

        public void DownPage()
        {
            _topLineIndexPointer += this.MaxLines;

            if (_topLineIndexPointer + this.MaxLines >= _totalLines)
                _topLineIndexPointer = _totalLines - (this.MaxLines > _totalLines ? _totalLines : this.MaxLines);

            Redraw();
        }

        // public void SaveFile()
        // {
        //     lock (_consoleLock)
        //     {
        //         Terminal.SetCursorPosition(0, this.BottomLine);
        //         Terminal.BackgroundColor = ConsoleColor.DarkGreen;
        //         Terminal.ForegroundColor = ConsoleColor.Black;
        //         Terminal.CursorVisible = true;
        //         Terminal.Write(String.Empty.PadRight(this.WindowWidth));

        //         Terminal.SetCursorPosition(0, this.BottomLine);
        //         Terminal.Write("Enter file name (blank to abort): ");
        //         string fileName = KeyInput.ReadLine();
        //         Terminal.ResetColor();
        //         Terminal.CursorVisible = false;

        //         Redraw();

        //         fileName = fileName.Trim();

        //         if (fileName != String.Empty)
        //         {
        //             if (File.Exists(fileName))
        //             {
        //                 Terminal.SetCursorPosition(0, this.BottomLine);
        //                 Terminal.BackgroundColor = ConsoleColor.DarkRed;
        //                 Terminal.ForegroundColor = ConsoleColor.Black;
        //                 Terminal.Write(String.Empty.PadRight(this.WindowWidth));

        //                 Terminal.SetCursorPosition(0, this.BottomLine);
        //                 Terminal.Write("ERROR: File already exists, try again.");
        //                 Terminal.ResetColor();

        //                 Thread.Sleep(2000);
        //                 Redraw();
        //             }
        //             else
        //             {
        //                 using (StreamWriter stream = File.CreateText(fileName))
        //                 {
        //                     foreach (string line in _lines)
        //                         stream.WriteLine(line);
        //                 }

        //                 Terminal.SetCursorPosition(0, this.BottomLine);
        //                 Terminal.BackgroundColor = ConsoleColor.DarkGreen;
        //                 Terminal.ForegroundColor = ConsoleColor.Black;
        //                 Terminal.Write(String.Empty.PadRight(this.WindowWidth));

        //                 Terminal.SetCursorPosition(0, this.BottomLine);
        //                 Terminal.Write($"Success! Text saved to {fileName}");
        //                 Terminal.ResetColor();

        //                 Thread.Sleep(2000);
        //                 Redraw();    
        //             }
        //         }
        //     }
        // }

        public void Dispose()
            => Cleanup();

        #region Private Methods
        private void Setup()
        {
            Terminal.CursorVisible = false;

            Terminal.InitStatusBar(
                new StatusBarItem(
                    "Main Menu",
                    (key) => {
                        _cts.Cancel();
                        _tcs.TrySetResult();
                        return Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.Q)
                ),
                new StatusBarItem(
                    "Navigate",
                    (key) => {
                        if (key.RootKey == ConsoleKey.DownArrow)
                            DownLine();
                        else if (key.RootKey == ConsoleKey.UpArrow)
                            UpLine();
                        else if (key.RootKey == ConsoleKey.PageDown)
                            DownPage();
                        else if (key.RootKey == ConsoleKey.PageUp)
                            UpPage();
                        else if (key.RootKey == ConsoleKey.Home)
                            ScrollToTop();
                        else if (key.RootKey == ConsoleKey.End)
                            ScrollToBottom();

                        return Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.DownArrow),
                    Key.MakeKey(ConsoleKey.UpArrow),
                    Key.MakeKey(ConsoleKey.PageUp),
                    Key.MakeKey(ConsoleKey.PageDown),
                    Key.MakeKey(ConsoleKey.Home),
                    Key.MakeKey(ConsoleKey.End)
                )
            );
        }

        private void Cleanup()
        {
            Terminal.Clear();
            Terminal.ResetColor();

            _lines.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public override void Redraw()
        {
            _drawn = true;

            WriteHeader();

            IEnumerable<string> linesToShow = _lines.Skip(_topLineIndexPointer).Take(this.MaxLines);
            int i = 0;

            foreach (string line in linesToShow)
            {
                DrawLine(i, line);

                i++;
            }
        }

        private void DrawLine(int index, string line)
        {
            Terminal.SetCursorPosition(0, this.FirstLine + index);

            if (ShowLineNumbers == true)
            {
                Terminal.BackgroundColor = ConsoleColor.DarkBlue;
                Terminal.Write((_topLineIndexPointer + index + 1).ToString().PadLeft(this.LineNumberWidth));
                Terminal.ResetColor();
                Terminal.Write(" ");
            }

            int lineWidth = (line.Length > this.MaxWidth ? this.MaxWidth : line.Length);

            if (this.Highlight == true && this.HighlightText != null)
            {
                for (int s = 0; s < lineWidth; s++)
                {
                    if (line.Substring(s).ToLower().StartsWith(this.HighlightText.ToLower()))
                    {
                        Terminal.ForegroundColor = this.HighlightColor;
                        Terminal.Write(line.Substring(s, this.HighlightText.Length));
                        Terminal.ResetColor();

                        s += this.HighlightText.Length-1;
                    }
                    else
                        Terminal.Write(line[s]);
                }

                // erase the rest of the line
                if (line.Length < this.MaxWidth)
                    Terminal.Write(String.Empty.PadRight(this.MaxWidth - line.Length));
            }
            else
                Terminal.Write(line.Substring(0, lineWidth).PadRight(this.MaxWidth));
        }

        private void WriteHeader()
        {
            if (this.ShowHeader)
            {
                string line = this.HeaderText;

                if (line == null)
                    line = String.Empty;

                Terminal.SetCursorPosition(0, this.StartLine);
                Terminal.BackgroundColor = ConsoleColor.DarkBlue;
                Terminal.Write(line.PadRight(this.MaxWidth));
                Terminal.ResetColor();
            }
        }

        private void WriteStatusBar()
        {
            string line = _statusLineLeft;

            int startLine = _topLineIndexPointer + 1;
            int endLine = _topLineIndexPointer + this.MaxLines;

            if (endLine > _totalLines)
                endLine = _totalLines;

            double linePct = Math.Round(((double)(endLine) / (double)_totalLines) * 100.0, 0);
            string lineIndex = $"Line: {startLine}-{endLine} / {_totalLines}   {linePct.ToString("##0").PadLeft(3)}%";

            int leftPad = this.WindowWidth - lineIndex.Length;

            Terminal.SetCursorPosition(0, this.BottomLine);
            Terminal.BackgroundColor = ConsoleColor.DarkBlue;
            Terminal.Write(line.PadRight(leftPad) + lineIndex);
            Terminal.ResetColor();
        }

        #endregion Private Methods
    }
}
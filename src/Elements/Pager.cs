/*
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     Pager element that is used for displaying multi-line text that overflows
    ///     beyond what the terminal is able to display. This element can be scrolled
    ///     to allow for easy viewing of the content.
    /// </summary>
    public class Pager : Element, IDisposable
    {
        #region Public Properties
        /// <summary>
        ///     If true, the display will automatically scroll to the bottom as new lines are added
        ///
        ///     Note: this only works when the pager has been started, otherwise scrolling must be done
        ///           manually from outside the element
        /// </summary>
        public bool AutoScroll { get; set; } = false;

        /// <summary>
        ///     If true, show line numbers for each line.
        /// 
        ///     Note: This will slow down rendering slightly if the background color is different from that
        ///           of the default terminal background color
        /// </summary>
        public bool ShowLineNumbers { get; set; } = false;

        /// <summary>
        ///     Background color to use for line numbers. If null, the default defined in
        ///     <see cref="TerminalColor"/> will be used
        /// </summary>
        public Nullable<ConsoleColor> LineNumberBackground 
        { 
            get => _lineNumberBackground ?? TerminalColor.PagerLineNumberBackground;
            set => _lineNumberBackground = value;
        }
        private Nullable<ConsoleColor> _lineNumberBackground = null;

        /// <summary>
        ///     Foreground color to use for line numbers. If null, the default defined in
        ///     <see cref="TerminalColor"/> will be used
        /// </summary>
        public Nullable<ConsoleColor> LineNumberForeground 
        { 
            get => _lineNumberForeground ?? TerminalColor.PagerLineNumberForeground;
            set => _lineNumberForeground = value;
        }
        private Nullable<ConsoleColor> _lineNumberForeground = null;

        /// <summary>
        ///     If true, the header provided in <see cref="HeaderText" /> will be displayed
        /// </summary>
        public bool ShowHeader { get; set; } = false;

        /// <summary>
        ///     Header text to display, if <see cref="ShowHeader" /> is enabled
        /// </summary>
        public string HeaderText 
        { 
            get => _headerText;
            set
            {
                this.ShowHeader = !String.IsNullOrWhiteSpace(value);
                _headerText = value ?? String.Empty;;
            }
        }
        private string _headerText = String.Empty;


        /// <summary>
        ///     Text that is to be highlighted in the output
        /// </summary>
        public string HighlightText 
        { 
            get => _highlightText;
            set
            {
                this.Highlight = !String.IsNullOrWhiteSpace(value);
                _highlightText = value ?? string.Empty;
            }
        }
        private string _highlightText = null;


        /// <summary>
        ///     Flag indicating whether to activate text highlighting
        /// </summary>
        public bool Highlight { get; set; } = false;


        /// <summary>
        ///     Foreground color to use when highlighting text
        /// </summary>
        public Nullable<ConsoleColor> HighlightForegroundColor 
        { 
            get => _highlightColor ?? TerminalColor.PagerHighlightColorForeground;
            set => _highlightColor = value;
        }
        private Nullable<ConsoleColor> _highlightColor = null;


        /// <summary>
        ///     Width of the line number column (does NOT include the extra space between line number column and start of lines)
        /// </summary>
        public int LineNumberWidth => (this.ShowLineNumbers ? (_topLineIndexPointer + this.MaxLines).ToString().Length : 0);

        /// <summary>
        ///     Total height of the pager
        /// </summary>
        public int PagerHeight => this.MaxHeight;

        /// <summary>
        ///     Total width of the pager
        /// </summary>
        public int PagerWidth => (this.ShowLineNumbers ? this.MaxWidth - (this.LineNumberWidth+1) : this.MaxWidth);

        /// <summary>
        ///     Maximum number of lines to display on the pager
        /// </summary>
        public int MaxLines => this.PagerHeight - (this.ShowHeader ? 1 : 0);

        /// <summary>
        ///     Defer drawing until all data is received
        /// </summary>
        public bool DeferDraw => _deferDraw;

        /// <summary>
        ///     Terminal point that indicates the first line of text
        /// </summary>
        public TerminalPoint FirstTextLinePoint { get; private set; }

        /// <summary>
        ///     Terminal point that indicates where the header, if present, begins
        /// </summary>
        public TerminalPoint HeaderLinePoint { get; private set; }
        #endregion Public Properties

        #region Private Fields
        private List<string> _lines;
        private int _topLineIndexPointer = 0;

        private int _totalLines = 0;
        private bool _drawn = false;

        private bool _deferDraw = true;
        private bool _started = false;
        private TaskCompletionSource<bool> _tcs = null;
        private CancellationTokenSource _cts = null;
        private CancellationTokenSource _watchdogCts = null;

        #endregion Private Fields

        #region Constructor
        /// <summary>
        ///     Constuct a new pager
        /// </summary>
        /// <param name="autoScroll">If true, automatically scroll output</param>
        /// <param name="showLineNumbers">Show the line numbers</param>
        /// <param name="headerText">Text to display as a header</param>
        /// <param name="highlightText">Text to highlight</param>
        /// <param name="lines">Lines to start pager with</param>
        /// <param name="area">TerminalArea to consume</param>
        /// <param name="show">If true, element will be shown upon construction</param>
        public Pager(bool autoScroll = false,
                     bool showLineNumbers = false,
                     string headerText = null,
                     string highlightText = null,
                     List<string> lines = null,
                     TerminalArea area = TerminalArea.Default,
                     bool show = false)
            : base(area, show) 
        {

            this.AutoScroll = autoScroll;
            this.ShowLineNumbers = showLineNumbers;
            this.HeaderText = headerText;
            this.HighlightText = highlightText;

            _lines = lines ?? new List<string>();

            this.RecalculateAndRedraw();
        }

        /// <summary>
        ///     Recalculate the lauout and redraw the entire element
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                // TODO: factor in TerminalArea
                (this.TopLeftPoint, this.TopRightPoint, this.BottomLeftPoint, this.BottomRightPoint) 
                    = TerminalPoint.GetAreaBounds(this.Area);

                this.Width = this.TopRightPoint.Left - this.TopLeftPoint.Left;
                this.Height = this.BottomLeftPoint.Top - this.TopLeftPoint.Top;

                this.HeaderLinePoint = (this.ShowHeader ? this.TopLeftPoint.Clone() : null);
                this.FirstTextLinePoint = (this.ShowHeader ? this.TopLeftPoint.AddY(1) : this.TopLeftPoint.Clone());
            }

            this.RedrawAll();
        }

        /// <summary>
        ///     Run the pager asynchronously. This automatically sets up the status bar for navigation 
        ///     and will continue running until the user cancels it, or the passed cToken is canceled
        /// </summary>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Task</returns>
        public Task RunAsync(CancellationToken cToken = default)
        {
            if (_tcs != null)
                return Task.CompletedTask;
            
            _cts = CancellationTokenSource.CreateLinkedTokenSource(cToken);
            _tcs = new TaskCompletionSource<bool>();
            _started = true;

            _watchdogCts = Helpers.SetupTaskWatchdog(cToken, () => {
                _tcs?.TrySetResult(false);
            });
            
            SetupStatusBar();

            this.Visible = true;

            if (!_drawn)
                Redraw();

            return _tcs.Task;
        }

        /// <summary>
        ///     Immediately stop the pager, if it is running
        /// </summary>
        public void Stop()
        {
            _cts?.Cancel();
            _watchdogCts?.Cancel();
            
            _tcs?.TrySetResult(true);

            Terminal.StatusBar?.Reset();
        }

        /// <summary>
        ///     Start the pager as a background task
        /// </summary>
        public void Start()
        {
            _deferDraw = false;            
            RunAsync();
            DrawHeader();
        }

        /// <summary>
        ///     Shortcut method to start a create and start a new pager
        /// </summary>
        /// <returns>New pager instance with default config</returns>
        public static Pager StartNew()
        {
            Pager pager = new Pager();
            pager.Start();

            return pager;
        }

        /// <summary>
        ///     Wait until the pager is finished
        /// </summary>
        /// <returns>Task</returns>
        public async Task WaitForQuitAsync()
        {
            if (_tcs != null)
                await _tcs.Task;
        }
        #endregion Constructor

        /// <summary>
        ///     Redraw the entire pager
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;
                
            _drawn = true;

            DrawHeader();

            List<string> linesToShow = _lines.Skip(_topLineIndexPointer).Take(this.MaxLines).ToList();

            if (this.ShowLineNumbers)
            {
                if (this.LineNumberBackground != TerminalColor.DefaultBackground)
                    Terminal.BackgroundColor = this.LineNumberBackground.Value;

                if (this.LineNumberForeground != TerminalColor.DefaultForeground)
                    Terminal.ForegroundColor = this.LineNumberForeground.Value;
                
                for (int i = 0; i < linesToShow.Count; i++)
                    DrawLineNumber(i);

                if (this.LineNumberBackground != TerminalColor.DefaultBackground)
                    Terminal.ResetBackground();

                if (this.LineNumberForeground != TerminalColor.DefaultForeground)
                    Terminal.ResetForeground();
            }

            for (int i = 0; i < linesToShow.Count; i++)
                DrawLine(i, linesToShow[i]);
        }

        /// <summary>
        ///     Append a blank line to the pager
        /// </summary>
        public void AppendLine()
            => AppendLine(string.Empty);

        /// <summary>
        ///     Append a line to the pager 
        /// </summary>
        /// <param name="line">Line to append</param>
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

        /// <summary>
        ///     Scroll to the top line
        /// </summary>
        public void ScrollToTop()
        {
            _topLineIndexPointer = 0;

            Redraw();
        }

        /// <summary>
        ///     Scroll to the bottom line
        /// </summary>
        public void ScrollToBottom()
        {
            _topLineIndexPointer = _totalLines - this.MaxLines;

            Redraw();
        }

        /// <summary>
        ///     Move up one line
        /// </summary>
        public void UpLine()
        {
            _topLineIndexPointer--;

            if (_topLineIndexPointer < 0)
                _topLineIndexPointer = 0;

            Redraw();
        }

        /// <summary>
        ///     Move up one page
        /// </summary>
        public void UpPage()
        {
            _topLineIndexPointer -= this.MaxLines;

            if (_topLineIndexPointer < 0)
                _topLineIndexPointer = 0;

            Redraw();
        }


        /// <summary>
        ///     Move down one line
        /// </summary>
        public void DownLine()
        {
            _topLineIndexPointer++;

            if (_topLineIndexPointer + this.MaxLines >= _totalLines)
                _topLineIndexPointer = _totalLines - (this.MaxLines > _totalLines ? _totalLines : this.MaxLines);

            Redraw();
        }

        /// <summary>
        ///     Move down one page
        /// </summary>
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

        /// <summary>
        ///     Cleanup the pager resources
        /// </summary>
        public void Dispose()
            => Cleanup();

        #region Private Methods
        /// <summary>
        ///     Setup the status bar for interactive user interface
        /// </summary>
        private void SetupStatusBar()
        {
            Terminal.InitStatusBar(
                new StatusBarItem(
                    "Quit Pager",
                    (key) => {
                        this.Stop();

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

        /// <summary>
        ///     Cleanup any resources 
        /// </summary>
        private void Cleanup()
        {
            // TODO: we shouldn't do a complete clear of the terminal, instead just erase ourself
            Terminal.Clear();
            Terminal.ResetColor();

            // TODO: is any of this actually necessary? is IDisposable necessary?
            // _lines.Clear();
            // GC.Collect();
            // GC.WaitForPendingFinalizers();
        }

        /// <summary>
        ///     Draw a single line
        /// </summary>
        /// <param name="index">Index to write the line to</param>
        /// <param name="line">Line to write</param>
        private void DrawLine(int index, string line)
        {
            int xOffset = 0;

            if (ShowLineNumbers == true)
                xOffset = this.LineNumberWidth + 1;

            this.FirstTextLinePoint.AddY(index).AddX(xOffset).MoveTo();

            // if (ShowLineNumbers == true)
            // {
            //     Terminal.WriteColorBG(ConsoleColor.DarkBlue, (_topLineIndexPointer + index + 1).ToString().PadLeft(this.LineNumberWidth));
            //     Terminal.Write(" ");
            // }

            int lineWidth = (line.Length > this.PagerWidth ? this.PagerWidth : line.Length);

            if (this.Highlight == true && this.HighlightText != null)
            {
                for (int s = 0; s < lineWidth; s++)
                {
                    if (line.Substring(s).ToLower().StartsWith(this.HighlightText.ToLower()))
                    {
                        int remainingChars = lineWidth - s;

                        int substringLength = this.HighlightText.Length;

                        if (remainingChars < substringLength)
                            substringLength = remainingChars;

                        if (this.HighlightForegroundColor != TerminalColor.DefaultForeground)
                            Terminal.WriteColor(this.HighlightForegroundColor.Value, line.Substring(s, substringLength));
                        else
                            Terminal.Write(line.Substring(s, substringLength));

                        s += this.HighlightText.Length-1;
                    }
                    else
                        Terminal.Write(line[s]);
                }

                // erase the rest of the line
                if (line.Length < this.PagerWidth)
                    Terminal.Write(String.Empty.PadRight(this.PagerWidth - line.Length));
            }
            else
                Terminal.Write(line.Substring(0, lineWidth).PadRight(this.PagerWidth));
        }

        /// <summary>
        ///     Draw the line number at the specified index
        /// </summary>
        /// <param name="index">index to draw line number for</param>
        private void DrawLineNumber(int index)
        {
            if (!ShowLineNumbers)
                return;

            this.FirstTextLinePoint.AddY(index).MoveTo();

            Terminal.Write((_topLineIndexPointer + index + 1).ToString().PadLeft(this.LineNumberWidth));
        }

        /// <summary>
        ///     Draw the header
        /// </summary>
        private void DrawHeader()
        {
            if (!this.ShowHeader)
                return;
         
            this.HeaderLinePoint.MoveTo();

            string completeHeaderLine = String.Empty;

            if (this.ShowLineNumbers)
                completeHeaderLine = String.Empty.PadLeft(this.LineNumberWidth+1);

            completeHeaderLine += this.HeaderText.PadRight(this.PagerWidth);

            Terminal.WriteColorBG(TerminalColor.PagerHeaderBackground, completeHeaderLine);
        }

        // private void WriteStatusBar()
        // {
        //     int startLine = _topLineIndexPointer + 1;
        //     int endLine = _topLineIndexPointer + this.MaxLines;

        //     if (endLine > _totalLines)
        //         endLine = _totalLines;

        //     double linePct = Math.Round(((double)(endLine) / (double)_totalLines) * 100.0, 0);
        //     string lineIndex = $"Line: {startLine}-{endLine} / {_totalLines}   {linePct.ToString("##0").PadLeft(3)}%";

        //     int leftPad = this.MaxWidth - lineIndex.Length;

        //     Terminal.SetCursorPosition(0, this.BottomLine);
        //     Terminal.BackgroundColor = ConsoleColor.DarkBlue;
        //     Terminal.Write(lineIndex.PadLeft(leftPad));
        //     Terminal.ResetColor();
        // }

        #endregion Private Methods
    }
}
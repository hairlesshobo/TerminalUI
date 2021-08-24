using System;
using System.Collections.Generic;
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
        public string HeaderText { get; set; } = String.Empty;
        public string HighlightText { get; set; } = null;
        public bool Highlight { get; set; } = false;
        public ConsoleColor HighlightColor { get; set; } = ConsoleColor.DarkYellow;
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

        public TerminalPoint FirstTextLinePoint { get; private set; }
        #endregion Public Properties

        #region Private Fields
        private object _consoleLock = new object();
        
        private List<string> _lines;
        private int _topLineIndexPointer = 0;

        private int _totalLines = 0;
        private volatile bool _drawn = false;

        private Thread _thread;
        private volatile bool _abort = false;
        private bool _deferDraw = true;
        private volatile bool _started = false;
        #endregion Private Fields

        #region Constructor
        public Pager()
        {
            _lines = new List<string>();

            this.TopLeftPoint = new TerminalPoint(0, 2);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, 2);
            this.BottomLeftPoint = new TerminalPoint(0, Terminal.Height - 1);
            this.BottomRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height - 1);

            this.FirstTextLinePoint = new TerminalPoint(0, this.TopLeftPoint.Top+1);
        }

        public void Start()
        {
            _thread = new Thread(Run);
            _thread.Start();

            _started = true;
        }
        #endregion Constructor

        public void AppendLine()
            => AppendLine(string.Empty);

        public void AppendLine(string Line)
        {
            _lines.Add(Line);
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
                        if (_totalLines - _topLineIndexPointer <= this.MaxLines)
                            Redraw();
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

        public void WaitForExit()
        {
            if (!_drawn)
                Redraw();

            _thread.Join();
        }

        public void Dispose()
        {
            if (!_abort)
                _abort = true;

            WaitForExit();
        }


        #region Private Methods
        private void Run()
        {
            Setup();
            MainLoop();
            Cleanup();
        }

        private void Setup()
        {
            lock (_consoleLock)
            {
                Terminal.CursorVisible = false;

                Terminal.InitStatusBar(
                    new StatusBarItem(
                        "Main Menu",
                        (key) => {
                            _abort = true;
                            return Task.Delay(0);
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

                            return Task.Delay(0);
                        },
                        Key.MakeKey(ConsoleKey.DownArrow),
                        Key.MakeKey(ConsoleKey.UpArrow),
                        Key.MakeKey(ConsoleKey.PageUp),
                        Key.MakeKey(ConsoleKey.PageDown),
                        Key.MakeKey(ConsoleKey.Home),
                        Key.MakeKey(ConsoleKey.End)
                    )
                );

                // if (!_deferDraw)
                //     WriteStatusBar();

                // Console.CancelKeyPress += (sender, e) => {
                //     _abort = true;
                // };
            }
        }

        private void MainLoop()
        {
            while (!_abort)
            {
                Thread.Sleep(10);
                // ConsoleKeyInfo key = Console.ReadKey(true);

                // if (key.Key == ConsoleKey.Q || key.Key == ConsoleKey.Escape || (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control))
                //     _abort = true;
                // else if (key.Key == ConsoleKey.DownArrow)
                //     DownLine();
                // else if (key.Key == ConsoleKey.UpArrow)
                //     UpLine();
                // else if (key.Key == ConsoleKey.PageDown)
                //     DownPage();
                // else if (key.Key == ConsoleKey.PageUp)
                //     UpPage();
                // else if (key.Key == ConsoleKey.Home)
                //     ScrollToTop();
                // else if (key.Key == ConsoleKey.End)
                //     ScrollToBottom();
                // else if (key.Key == ConsoleKey.S && key.Modifiers == ConsoleModifiers.Control)
                //     SaveFile();
            }
        }

        private void Cleanup()
        {
            lock (_consoleLock)
            {
                Terminal.Clear();
                // Terminal.SetCursorPosition(0, 0);
                // Terminal.CursorVisible = true;
                Terminal.ResetColor();
            }

            _lines.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public override void Redraw()
        {
            if (!_abort)
            {
                _drawn = true;

                WriteHeader();
                // WriteStatusBar();

                IEnumerable<string> linesToShow = _lines.Skip(_topLineIndexPointer).Take(this.MaxLines);
                int i = 0;

                foreach (string line in linesToShow)
                {
                    lock (_consoleLock)
                    {
                        Terminal.SetCursorPosition(0, this.FirstLine + i);

                        if (ShowLineNumbers == true)
                        {
                            Terminal.BackgroundColor = ConsoleColor.DarkBlue;
                            Terminal.Write((_topLineIndexPointer + i + 1).ToString().PadLeft(this.LineNumberWidth));
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

                    i++;
                }
            }
        }

        private void WriteHeader()
        {
            if (this.ShowHeader)
            {
                string line = this.HeaderText;

                if (line == null)
                    line = String.Empty;

                lock (_consoleLock)
                {
                    Terminal.SetCursorPosition(0, this.StartLine);
                    Terminal.BackgroundColor = ConsoleColor.DarkBlue;
                    Terminal.Write(line.PadRight(this.MaxWidth));
                    Terminal.ResetColor();
                }
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

            lock (_consoleLock)
            {
                Terminal.SetCursorPosition(0, this.BottomLine);
                Terminal.BackgroundColor = ConsoleColor.DarkBlue;
                Terminal.Write(line.PadRight(leftPad) + lineIndex);
                Terminal.ResetColor();
            }
        }

        #endregion Private Methods
    }
}
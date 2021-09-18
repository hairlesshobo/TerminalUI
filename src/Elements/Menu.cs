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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     Basic menu element that allows listing multiple options and allows the 
    ///     user to interactively select one or more (multiple select not yet implemented)
    ///     items
    /// </summary>
    /// <typeparam name="TKey">Type returned by the menu</typeparam>
    public class Menu : Element
    {
        #region Public Properties
        [Obsolete]
        public string MenuLabel { get; set; } = null;
        public bool EnableCancel { get; set; } = false;
        public bool MultiSelect { get; private set; } = false;
        public Func<Task> QuitCallback { get; set; } = null;
        public List<object> SelectedValues => _entries.Where(x => x.Selected).Select(x => x.SelectedValue).ToList();
        public IReadOnlyList<object> SelectedEntries => (IReadOnlyList<object>)_entries;
        TaskCompletionSource<List<object>> _tcs;
        public int MaxLines { get; private set; } // => Terminal.UsableHeight - 1;
        public int LeftPad { 
            get => _leftPad; 
            set 
            {
                if (value < 0 || value > 8)
                    throw new ArgumentOutOfRangeException();

                _leftPad = value;
                _leftPadStr = String.Empty;

                for (int i = 0; i < LeftPad; i++)
                    _leftPadStr += " ";
            }
        }

        #endregion Public Properties

        #region Private Fields
        private List<MenuEntry> _entries;
        private int _cursorIndex = -1;
        private string _leftPadStr = "    ";
        private int _leftPad = 4;
        private int _entryOffset = 0;
        #endregion Private Fields

        #region Constructors
        public Menu(List<MenuEntry> entries, bool multiSelect)
            : base() => Initalize(entries, multiSelect);

        public Menu(List<MenuEntry> Entries)
            : base() => Initalize(Entries, false);

        public Menu()
            : base() => Initalize(null, false);

        public void Initalize(List<MenuEntry> entries, bool multiSelect)
        {
            this.MultiSelect = multiSelect;

            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = this.TopLeftPoint.AddX(Terminal.UsableWidth);

            this.BottomLeftPoint = new TerminalPoint(this.TopLeftPoint.Left, Terminal.UsableBottom);
            this.BottomRightPoint = this.BottomLeftPoint.AddX(Terminal.UsableWidth);

            this.MaxLines = this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 1;

            SetMenuItems(entries);
        }
        #endregion Constructors

        #region Public Methods
        public void SetMenuItems(List<MenuEntry> entries)
        {
            _entries = entries;
            _cursorIndex = 0;

            if (_entries != null && _entries.Count > 0)
            {
                _cursorIndex = _entries.IndexOf(_entries.First(x => !x.Disabled && !x.Header));

                if (this.MultiSelect == false && (!_entries.Any(x => x.Selected && !x.Disabled && !x.Header)))
                    _entries.First(x => !x.Disabled && !x.Header).Selected = true;

                // if this isn't a multiselect menu, make sure there is only one selected by default
                if (this.MultiSelect == false && _entries.Count(x => x.Selected == true) > 1)
                {
                    _entries.ForEach(x => x.Selected = false);
                    _entries.First().Selected = true;
                }
            }
        }

        private CancellationToken _cToken = default;
        private CancellationTokenSource _watchdogCts = null;

        public async Task<List<TResult>> ShowAsync<TResult>(CancellationToken cToken = default)
            => (await ShowAsync(cToken: cToken))?.Cast<TResult>()?.ToList();

        // TODO: remove clearScreen
        public Task<List<object>> ShowAsync(CancellationToken cToken = default)
        {
            _cToken = cToken;
            _tcs = new TaskCompletionSource<List<object>>();

            this.MaxLines = this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 1;

            this.Redraw();

            // string message = "to select item";

            // if (this.MultiSelect == true)
            // {
            //     message = "when finished";
            //     Terminal.Write("Press ");
            //     Terminal.WriteColor(this.KeyColor, "<space>");
            //     Terminal.Write(" to select entry, ");
            //     Terminal.WriteColor(this.KeyColor, "<Shift>-A");
            //     Terminal.Write(" to select all, ");
            //     Terminal.WriteColor(this.KeyColor, "<Shift>-D");
            //     Terminal.Write(" to deselect all");
            //     Terminal.WriteLine();
            // }

            this.SetupStatusBar();

            _watchdogCts = Helpers.SetupTaskWatchdog(cToken, this.AbortMenu);

            return _tcs.Task;
        }

        public override void Redraw()
        {
            this.Clear();

            foreach (MenuEntry entry in _entries.Skip(_entryOffset).Take(MaxLines))
                WriteMenuEntry(entry);
        }

        private void Clear()
        {
            this.TopLeftPoint.MoveTo();
            
            StringBuilder sb = new StringBuilder();
                
            for (int w = 0; w < Width; w++)
                sb.Append(' ');

            string wideString = sb.ToString();

            for (int h = 0; h < this.MaxLines; h++)
            {
                Console.CursorLeft = 0;
                Terminal.Write(wideString);
            }

            this.TopLeftPoint.MoveTo();
        }

        private void SetupStatusBar()
        {
            List<StatusBarItem> statusBarItems = new List<StatusBarItem>();

            if (this.EnableCancel)
            {
                statusBarItems.Add(new StatusBarItem(
                    "Cancel",
                    async (key) => {
                        this.AbortMenu();
                        await Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.C, ConsoleModifiers.Control)
                ));
            } 
            else
            {
                // TOOD: add callback here for quit that will allow me to use the global TCS to close out the application
                statusBarItems.Add(new StatusBarItem(
                    "Quit Application",
                    async (key) => 
                    {
                        if (this.QuitCallback != null)
                            await this.QuitCallback();
                        else
                            Environment.Exit(0);
                    },
                    Key.MakeKey(ConsoleKey.Q)
                ));
            }

            statusBarItems.Add(new StatusBarItem(
                "Accept",
                async (key) =>
                {
                    _watchdogCts?.Cancel();

                    if ( this.MultiSelect == false)
                    {
                        MenuEntry finalEntry = _entries.First(x => x.Selected);

                        this.Clear();

                        try
                        {
                            if (finalEntry != null && finalEntry.Task != null)
                                await finalEntry.Task(_cToken);

                            _tcs.SetResult(new List<object>() {
                                finalEntry.SelectedValue
                            });
                        }
                        catch (Exception e)
                        {
                            _tcs.SetException(e);
                        }
                    }
                    else
                        _tcs.SetResult(this.SelectedValues);
                },
                Key.MakeKey(ConsoleKey.Enter)
            ));

            statusBarItems.Add(new StatusBarItem(
                "Nav",
                (key) =>
                {
                    MenuEntry selectedEntry = _entries[_cursorIndex];
                    MenuEntry previousEntry = selectedEntry;

                    bool down = (key.RootKey == ConsoleKey.DownArrow);

                    if (this.MultiSelect == false)
                        selectedEntry.Selected = false;

                    if (key.RootKey == ConsoleKey.DownArrow)
                        MoveCursor(selectedEntry, true);
                    else if (key.RootKey == ConsoleKey.UpArrow)
                        MoveCursor(selectedEntry, false);


                    if (this.MultiSelect == false && (key.RootKey == ConsoleKey.UpArrow || key.RootKey == ConsoleKey.DownArrow))
                    {
                        WriteMenuEntry(previousEntry);
                        selectedEntry = _entries[_cursorIndex];
                        selectedEntry.Selected = true;
                        WriteMenuEntry(selectedEntry);
                    }
                    else
                    {
                        WriteMenuEntry(_entries[_cursorIndex]);

                        if (down)
                        {
                            if (_cursorIndex > 0)
                                WriteMenuEntry(previousEntry);
                        }
                        else
                        {
                            if (_entryOffset == 0 && _cursorIndex < this.MaxLines-1)
                                WriteMenuEntry(previousEntry);
                        }
                    }

                    return Task.CompletedTask;
                },
                Key.MakeKey(ConsoleKey.UpArrow),
                Key.MakeKey(ConsoleKey.DownArrow)
            ));

            if (this.MultiSelect == true)
            {
                statusBarItems.Add(new StatusBarItem(
                    "Mark Item",
                    (key) => {
                        if (key.RootKey == ConsoleKey.Spacebar)
                        {
                            MenuEntry selectedEntry = _entries[_cursorIndex];
                
                            selectedEntry.Selected = !selectedEntry.Selected;
                            WriteMenuEntry(selectedEntry);
                        }

                        return Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.Spacebar)
                ));

                statusBarItems.Add(new StatusBarItem(
                    "Select All",
                    (key) => {
                        _entries.ForEach(x => x.Selected = true);
                        this.Redraw();

                        return Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.A, ConsoleModifiers.Control)
                ));

                statusBarItems.Add(new StatusBarItem(
                    "Deselect All",
                    (key) => {
                        _entries.ForEach(x => x.Selected = false);
                        this.Redraw();

                        return Task.CompletedTask;
                    },
                    Key.MakeKey(ConsoleKey.D, ConsoleModifiers.Control)
                ));
            }

            Terminal.StatusBar.ShowItems(statusBarItems.ToArray());
        }

        public void AbortMenu()
        {
            _watchdogCts?.Cancel();
            _tcs.TrySetResult(null);

            // Terminal.StatusBar?.Reset();
        }

        #endregion Public Methods

        #region Private Methods
        private void MoveCursor(MenuEntry entry, bool down)
        {
            if (down == true)
            {
                if (_cursorIndex < (_entries.Count() - 1))
                {
                    _cursorIndex++;

                    if ((_cursorIndex-_entryOffset) == this.MaxLines)
                    {
                        _entryOffset += 1;

                        if (this._entries.Count >= this.MaxLines)
                            this.Redraw();
                    }
                }
                else
                {
                    _cursorIndex = 0;
                    _entryOffset = 0;

                    if (this._entries.Count >= this.MaxLines)
                        this.Redraw();
                }

                if (_entries[_cursorIndex].Disabled || _entries[_cursorIndex].Header)
                    MoveCursor(_entries[_cursorIndex], down);
            }
            else
            {
                if (_cursorIndex > 0)
                {
                    _cursorIndex--;

                    if (_entryOffset > 0)
                    {
                        _entryOffset--;

                        if (this._entries.Count >= this.MaxLines)
                            this.Redraw();
                    }
                }
                else
                {
                    _cursorIndex = _entries.Count - 1;
                    _entryOffset = (_entries.Count > this.MaxLines ? _entries.Count - this.MaxLines : 0);

                    if (this._entries.Count >= this.MaxLines)
                        this.Redraw();
                }

                if (_entries[_cursorIndex].Disabled || _entries[_cursorIndex].Header)
                    MoveCursor(_entries[_cursorIndex], down);
            }
        }

        private void MoveCursor(MenuEntry currentEntry, MenuEntry newEntry)
        {
            int newIndex = _entries.IndexOf(newEntry);
            currentEntry.Selected = false;
            newEntry.Selected = true;

            _cursorIndex = newIndex;
        }

        private void WriteMenuEntry(MenuEntry entry)
        {
            int entryIndex = _entries.IndexOf(entry);

            this.TopLeftPoint.AddY(entryIndex-_entryOffset).MoveTo();

            // print the header
            if (entry.Header == true && entry.Name != null)
            {
                Terminal.ForegroundColor = TerminalColor.MenuHeaderForeground;
                Terminal.Write($"----- {entry.Name} -----");
                Terminal.ResetForeground();
            }
            else if (entry.Disabled == true)
            {
                Terminal.ForegroundColor = TerminalColor.MenuDisabledForeground;
                Terminal.Write($"{_leftPadStr}  {entry.Name}");
                Terminal.ResetForeground();
            }
            else
            {
                string multiSelectChcekbox = String.Empty;
                
                if (this.MultiSelect)
                    multiSelectChcekbox = "[" + (entry.Selected == true? "X" : " ") + "]";

                if (entryIndex == _cursorIndex)
                {
                    Terminal.Write(_leftPadStr);
                    Terminal.BackgroundColor = TerminalColor.MenuCursorBackground;
                    Terminal.ForegroundColor = TerminalColor.MenuCursorArrow;
                    Terminal.Write(">");
                    if (this.MultiSelect == true)
                        Terminal.Write(multiSelectChcekbox);
                    Terminal.ForegroundColor = TerminalColor.MenuCursorForeground;
                    Terminal.Write($" {entry.Name}");
                    Terminal.ResetColor();
                }
                else
                {
                    if (entry.ForegroundColor != TerminalColor.DefaultForeground)
                        Terminal.ForegroundColor = entry.ForegroundColor;

                    Terminal.Write($"{_leftPadStr} ");  

                    if (this.MultiSelect == true)
                        Terminal.Write(multiSelectChcekbox);

                    Terminal.Write($" {entry.Name}");

                    if (entry.ForegroundColor != TerminalColor.DefaultForeground)
                        Terminal.ResetColor();
                }
            }
        }
        #endregion Private Methods
    }
}
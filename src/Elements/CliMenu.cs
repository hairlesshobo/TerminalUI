using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TerminalUI.Elements
{
    public class CliMenuEntry : CliMenuEntry<string>
    {
        public CliMenuEntry() : base()
        { }
    }

    public class CliMenuEntry<TKey>
    {
        public string Name { get; set; }
        public Func<Task> Task { get; set; }
        public bool Disabled { get; set; } = false;
        public bool Header { get; set; } = false;
        public TKey SelectedValue { get; set; }
        public ConsoleKey ShortcutKey { get; set; }
        public ConsoleColor ForegroundColor { get; set; } = Terminal.ForegroundColor;
        public ConsoleColor BackgroundColor { get; set; } = Terminal.BackgroundColor;
        public bool Selected { get; internal set; }
    }

    public class CliMenu<TKey>
    {
        #region Public Properties
        public string MenuLabel { get; set; } = null;
        public ConsoleColor HeaderColor { get; set; } = ConsoleColor.Magenta;
        public ConsoleColor KeyColor { get; set; } = ConsoleColor.DarkYellow;
        public ConsoleColor CursorBackgroundColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor CursorForegroundColor { get; set; } = ConsoleColor.DarkGreen;
        public ConsoleColor CursorArrowColor { get; set; } = ConsoleColor.Green;
        public ConsoleColor DisabledForegroundColor { get; set; } = ConsoleColor.DarkGray;
        public bool EnableCancel { get; set; } = false;
        public bool MultiSelect { get; private set; } = false;
        public List<TKey> SelectedValues => _entries.Where(x => x.Selected).Select(x => x.SelectedValue).ToList();
        public IReadOnlyList<TKey> SelectedEntries => (IReadOnlyList<TKey>)_entries;
        #endregion Public Properties

        #region Private Fields
        private List<CliMenuEntry<TKey>> _entries;
        private ConsoleColor _foregroundColor;
        private ConsoleColor _backgroundColor;
        private int _cursorIndex = -1;
        private bool _canceled = false;
        private List<TKey> _choosenItems = null;

        private int _startLine;
        #endregion Private Fields

        #region Constructors
        public CliMenu(List<CliMenuEntry<TKey>> entries, bool multiSelect)
            => Initalize(entries, multiSelect);

        public CliMenu(List<CliMenuEntry<TKey>> Entries)
            => Initalize(Entries, false);

        public CliMenu()
            => Initalize(null, false);

        public void Initalize(List<CliMenuEntry<TKey>> entries, bool multiSelect)
        {
            this.MultiSelect = multiSelect;

            SetMenuItems(entries);
        }
        #endregion Constructors

        #region Public Methods
        public void SetMenuItems(List<CliMenuEntry<TKey>> entries)
        {
            _entries = entries;
            _cursorIndex = 0;

            if (_entries != null)
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

        public Task<List<TKey>> Show()
            => ShowAsync(true);

        public async Task<List<TKey>> ShowAsync(bool clearScreen)
        {
            _choosenItems = null;

            Terminal.CursorVisible = false;
            _foregroundColor = Terminal.ForegroundColor;
            _backgroundColor = Terminal.BackgroundColor;

            if (clearScreen)
                Terminal.Clear();

            Terminal.WriteLine();

            if (this.MenuLabel != null)
            {
                Terminal.WriteLineColor(this.HeaderColor, this.MenuLabel);
                Terminal.WriteLine();
            }

            _startLine = Terminal. Top;

            foreach (CliMenuEntry<TKey> entry in _entries)
                WriteMenuEntry(entry);

            Terminal.SetCursorPosition(0, _startLine + _entries.Count() + 1);

            string message = "to select item";

            if (this.MultiSelect == true)
            {
                message = "when finished";
                Terminal.Write("Press ");
                Terminal.WriteColor(this.KeyColor, "<space>");
                Terminal.Write(" to select entry, ");
                Terminal.WriteColor(this.KeyColor, "<Shift>-A");
                Terminal.Write(" to select all, ");
                Terminal.WriteColor(this.KeyColor, "<Shift>-D");
                Terminal.Write(" to deselect all");
                Terminal.WriteLine();
            }

            // Terminal.WriteLine();
            // Terminal.Write("Press ");
            // Formatting.WriteC(this.KeyColor, "<enter>");
            // Terminal.Write($" {message}, ");
            // Formatting.WriteC(this.KeyColor, "<esc>");
            // Terminal.Write(" or ");
            // Formatting.WriteC(this.KeyColor, "q");
            // Terminal.Write(" to cancel");
            // Terminal.WriteLine();

            List<StatusBarItem> statusBarItems = new List<StatusBarItem>();

            if (this.EnableCancel)
            {
                statusBarItems.Add(new StatusBarItem(
                    "Cancel",
                    async (key) => {
                        this.AbortMenu();
                        await Task.Delay(0);
                    },
                    Key.MakeKey(ConsoleKey.C, ConsoleModifiers.Control)
                ));
            } 
            else
            {
                statusBarItems.Add(new StatusBarItem(
                    "Quit Application",
                    (key) => 
                    {
                        Environment.Exit(0);
                        return Task.Delay(0);
                    },
                    Key.MakeKey(ConsoleKey.Q)
                ));
            }

            statusBarItems.Add(new StatusBarItem(
                "Select Item",
                async (key) =>
                {
                    // Terminal.CursorVisible = true;
                    // Terminal.SetCursorPosition(0, _startLine+_entries.Count()+1);

                    if (this.MultiSelect == false)
                    {
                        CliMenuEntry<TKey> finalEntry = _entries.First(x => x.Selected);

                        if (clearScreen)
                            Terminal.Clear();

                        if (finalEntry != null && finalEntry.Task != null)
                            await finalEntry.Task();

                        _choosenItems = new List<TKey>() {
                            finalEntry.SelectedValue
                        };
                    }
                    else
                        _choosenItems = this.SelectedValues;
                },
                Key.MakeKey(ConsoleKey.Enter)
            ));

            statusBarItems.Add(new StatusBarItem(
                "Navigate",
                (key) =>
                {
                    CliMenuEntry<TKey> selectedEntry = _entries[_cursorIndex];
                    CliMenuEntry<TKey> previousEntry = selectedEntry;

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
                        WriteMenuEntry(previousEntry);
                        WriteMenuEntry(_entries[_cursorIndex]);
                    }

                    return Task.Delay(0);

                    // if (key.Key == ConsoleKey.Enter)
                    //     break;
                },
                Key.MakeKey(ConsoleKey.UpArrow),
                Key.MakeKey(ConsoleKey.DownArrow)
            ));

            statusBarItems.Add(new StatusBarItem(
                "Select Item",
                async (key) => {
                    if (key.RootKey == ConsoleKey.Spacebar)
                    {
                        CliMenuEntry<TKey> selectedEntry = _entries[_cursorIndex];
            
                        selectedEntry.Selected = !selectedEntry.Selected;
                        WriteMenuEntry(selectedEntry);

                        await Task.Delay(0);
                    }
                },
                Key.MakeKey(ConsoleKey.Spacebar)
            ));

            Terminal.StatusBar.ShowItems(statusBarItems.ToArray());


            // while (1 == 1)
            // {
            //     ConsoleKeyInfo key = Terminal.ReadKey(true);

            //     CliMenuEntry<TKey> selectedEntry = _entries[_cursorIndex];
            //     CliMenuEntry<TKey> previousEntry = selectedEntry;

            //     if (_multiSelect == false && (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow))
            //         selectedEntry.Selected = false;

            //     // if (_multiSelect == true)
            //     // {
            //     //     if (key.Key == ConsoleKey.Spacebar)
            //     //     {
            //     //         selectedEntry.Selected = !selectedEntry.Selected;
            //     //         WriteMenuEntry(selectedEntry);
            //     //     }
            //     //     else if (key.Modifiers == ConsoleModifiers.Shift)
            //     //     {
            //     //         if (key.Key == ConsoleKey.A)
            //     //         {
            //     //             foreach (CliMenuEntry<TKey> entry in _entries)
            //     //             {
            //     //                 entry.Selected = true;
            //     //                 WriteMenuEntry(entry);
            //     //             }
            //     //         }
            //     //         else if (key.Key == ConsoleKey.D)
            //     //         {
            //     //             foreach (CliMenuEntry<TKey> entry in _entries)
            //     //             {
            //     //                 entry.Selected = false;
            //     //                 WriteMenuEntry(entry);
            //     //             }
            //     //         }
            //     //     }
            //     // }

            //     // if (key.Key == ConsoleKey.Escape || key.Key == ConsoleKey.Q || (key.Key == ConsoleKey.C && key.Modifiers == ConsoleModifiers.Control))
            //     // {
            //     //     _canceled = true;
            //     //     break;
            //     // }
            //     // else if
            //      if (key.Key == ConsoleKey.DownArrow)
            //         MoveCursor(selectedEntry, true);
            //     else if (key.Key == ConsoleKey.UpArrow)
            //         MoveCursor(selectedEntry, false);


            //     if (_multiSelect == false && (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.DownArrow))
            //     {
            //         WriteMenuEntry(previousEntry);
            //         selectedEntry = _entries[_cursorIndex];
            //         selectedEntry.Selected = true;
            //         WriteMenuEntry(selectedEntry);
            //     }
            //     else
            //     {
            //         WriteMenuEntry(previousEntry);
            //         WriteMenuEntry(_entries[_cursorIndex]);
            //     }

            //     if (key.Key == ConsoleKey.Enter)
            //         break;
            // }



            // // Terminal.CursorVisible = true;
            // // Terminal.SetCursorPosition(0, _startLine+_entries.Count()+1);

            // if (_canceled == false)
            // {
            //     if (_multiSelect == false)
            //     {
            //         CliMenuEntry<TKey> finalEntry = _entries.First(x => x.Selected);

            //         if (ClearScreen)
            //             Terminal.Clear();

            //         if (finalEntry != null && finalEntry.Action != null)
            //             finalEntry.Action();

            //         return new List<TKey>() {
            //             finalEntry.SelectedValue
            //         };
            //     }
            //     else
            //         return this.SelectedEntries;
            // }
            // else
            // {
            //     this.OnCancel();
            //     return null;
            // }

            return await Task.Run(async () => 
            {
                // delay until the user does something
                while (_choosenItems == null && _canceled == false)
                    await Task.Delay(10);

                if (_canceled)
                    return null;

                return _choosenItems;
            });
        }

        public void AbortMenu()
            => _canceled = true;

        #endregion Public Methods

        #region Private Methods
        private void MoveCursor(CliMenuEntry<TKey> entry, bool down)
        {
            if (down == true)
            {
                if (_cursorIndex < (_entries.Count() - 1))
                    _cursorIndex++;
                else
                    _cursorIndex = 0;

                if (_entries[_cursorIndex].Disabled || _entries[_cursorIndex].Header)
                    MoveCursor(_entries[_cursorIndex], down);
            }
            else
            {
                if (_cursorIndex > 0)
                    _cursorIndex--;
                else
                    _cursorIndex = _entries.Count() - 1;

                if (_entries[_cursorIndex].Disabled || _entries[_cursorIndex].Header)
                    MoveCursor(_entries[_cursorIndex], down);
            }
        }

        private void MoveCursor(CliMenuEntry<TKey> currentEntry, CliMenuEntry<TKey> newEntry)
        {
            int newIndex = _entries.IndexOf(newEntry);
            currentEntry.Selected = false;
            newEntry.Selected = true;

            _cursorIndex = newIndex;
        }

        private void WriteMenuEntry(CliMenuEntry<TKey> Entry)
        {
            int entryIndex = _entries.IndexOf(Entry);

            Terminal.SetCursorPosition(0, _startLine + entryIndex);

            if (Entry.Header)
            {
                Terminal.ForegroundColor = ConsoleColor.Cyan;
                Terminal.BackgroundColor = _backgroundColor;
            }

            else
            {
                Terminal.BackgroundColor = _backgroundColor;
                Terminal.Write("    ");

                if (entryIndex == _cursorIndex)
                {
                    Terminal.BackgroundColor = this.CursorBackgroundColor;
                    Terminal.ForegroundColor = this.CursorArrowColor;
                    Terminal.Write("> ");
                    Terminal.ForegroundColor = this.CursorForegroundColor;
                }
                else if (Entry.Disabled)
                {
                    Terminal.ForegroundColor = this.DisabledForegroundColor;
                    Terminal.BackgroundColor = _backgroundColor;
                    Terminal.Write("  ");
                }
                else
                {
                    Terminal.BackgroundColor = _backgroundColor;
                    Terminal.Write("  ");
                }
            }

            if (this.MultiSelect == true)
            {
                Terminal.Write("[");

                if (Entry.Selected == true)
                    Terminal.Write("X");
                else
                    Terminal.Write(" ");

                Terminal.Write("] ");
            }


            if (Entry.Header && Entry.Name != null)
                Terminal.Write($"----- {Entry.Name} -----");
            else
            {
                if (!Entry.Disabled)
                    Terminal.ForegroundColor = Entry.ForegroundColor;

                char[] letters = (Entry.Name != null ? Entry.Name.ToCharArray() : new char[0]);

                ConsoleColor defaultForeground = Terminal.ForegroundColor;

                for (int i = 0; i < letters.Length; i++)
                {
                    char letter = letters[i];

                    if (letter == '`' && i < letters.Length)
                    {
                        char nextLetter = letters[i + 1];
                        bool setColor = false;

                        switch (nextLetter)
                        {
                            case 'R':
                                Terminal.ForegroundColor = ConsoleColor.Red;
                                setColor = true;
                                break;

                            case 'G':
                                Terminal.ForegroundColor = ConsoleColor.Green;
                                setColor = true;
                                break;

                            case 'B':
                                Terminal.ForegroundColor = ConsoleColor.Blue;
                                setColor = true;
                                break;

                            case 'C':
                                Terminal.ForegroundColor = ConsoleColor.Cyan;
                                setColor = true;
                                break;

                            case 'N':
                                Terminal.ForegroundColor = defaultForeground;
                                setColor = true;
                                break;
                        }

                        if (setColor)
                        {
                            i++;
                            continue;
                        }
                    }

                    Terminal.Write(letter);
                }
            }

            Terminal.SetCursorPosition(0, Terminal.Top);

            Terminal.ForegroundColor = _foregroundColor;
            Terminal.BackgroundColor = _backgroundColor;
        }
        #endregion Private Methods
    }
}
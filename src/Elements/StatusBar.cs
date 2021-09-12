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
using System.Linq;
using System.Threading.Tasks;

namespace TerminalUI.Elements
{
    public class StatusBarItem
    {
        public string Name { get; set; }
        public Key[] Keys { get; set; }
        public Func<Key, Task> Task { get; set; }
        public bool ShowKey { get; set; } = true;

        private Action removeCallback = () => { };

        public StatusBarItem()
        {

        }

        public StatusBarItem(string name)
        {
            this.Name = name;
        }

        public StatusBarItem(string name, Func<Key, Task> task, params Key[] keys)
        {
            this.Name = name;
            this.Task = task;
            this.Keys = keys;
        }

        public void Remove()
            => removeCallback();

        internal void AddRemoveCallback(Action callback)
            => removeCallback = callback;
    }

    public class StatusBar : Element
    {
        private List<StatusBarItem> _items = new List<StatusBarItem>();
        private List<StatusBarItem> _prevItems = new List<StatusBarItem>();
        private static StatusBar _instance;

        public StatusBar(params StatusBarItem[] items) => Init(items);
        public StatusBar() => Init(new StatusBarItem[]{ });

        private void Init(StatusBarItem[] items)
        {
            this.Height = 1;
            this.Width = Terminal.Width;

            // OK so i'm not sure why this behaves differently on windows than it does on windows
            // and to be honest, i didn't feel like spending more than 30 seconds addressing it.
            // Basically, if you write all the way to the last character of the line, the console
            // moves down one line (i assume it's just word wrap).. the side effect of this on windows
            // is that, when the status bar is drawn on the very last line, to the very last column.. it 
            // pushes the entire window up one line and therefore cuts off the first header line of the
            // terminal. This behavior doesn't happen on linux, so for now.. we just subtract one from the 
            // status bar width on windows and call it a quick fix instead of a lazy hack
            if (OperatingSystem.IsWindows())
                this.Width -= 1;
            
            this.TopLeftPoint = new TerminalPoint(0, Terminal.Height-1);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height-1);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            ShowItems(items);
        }

        public void ShowItems(params StatusBarItem[] items)
        {
            // stash the current items so that, during redraw, we can remove any key bindings we have
            _prevItems = _items;

            _items = items.ToList();

            Redraw();
        }

        public override void Redraw()
        {
            this.RemovePreviousKeyBindings();
            
            TerminalPoint prevPoint = TerminalPoint.GetCurrent();
            ConsoleColor prevBackgroundColor = Console.BackgroundColor;

            this.TopLeftPoint.MoveTo();

            Console.BackgroundColor = ConsoleColor.DarkBlue;

            for (int i = 0; i < _items.Count; i++)
            {
                StatusBarItem item = _items[i];

                Terminal.Write(' ');

                if (item.Keys != null && item.Task != null)
                {
                    for (int k = 0; k < item.Keys.Length; k++)
                    {
                        Key key = item.Keys[k];

                        // TODO: Convert to task
                        KeyInput.RegisterKey(key, item.Task);

                        if (item.ShowKey)
                            Terminal.WriteColor(ConsoleColor.Magenta, $"{key.ToString()}");

                        if (k < (item.Keys.Length-1))
                            Terminal.WriteColor(ConsoleColor.DarkGray, "/");
                    }

                    if (item.ShowKey)
                        Terminal.Write(' ');
                }

                Terminal.Write(item.Name);
                Terminal.Write(' ');

                item.AddRemoveCallback(() => {
                    if (item.Keys != null)
                    {
                        foreach (Key key in item.Keys)
                            KeyInput.UnregisterKey(key);
                    }

                    _items.Remove(item);

                    this.Redraw();
                });

                if (i < (_items.Count-1))
                    Terminal.Write((char)BoxChars.ThinVertical);
            }

            int charsToBlank = this.Width - Terminal.Left;

            for (int i = 0; i < charsToBlank; i++)
                Terminal.Write(' ');

            Console.BackgroundColor = prevBackgroundColor;
            prevPoint.MoveTo();
        }

        private void RemovePreviousKeyBindings()
        {
            foreach (StatusBarItem prevItem in _prevItems)
            {
                if (prevItem.Keys != null)
                {
                    foreach (Key key in prevItem.Keys)
                        KeyInput.UnregisterKey(key);
                }
            }

            _prevItems.Clear();
        }

        public void RemoveItemByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var item = _items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
                item.Remove();
        }

        public static StatusBar GetInstance()
        {
            if (_instance == null)
                _instance = new StatusBar();

            return _instance;
        }
    }
}
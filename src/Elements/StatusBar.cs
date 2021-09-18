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
using System.Runtime.InteropServices;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     A status bar element is a special element that can only exist once
    ///     and will be shown on the very bottom line of the screen. The status
    ///     bar is also the entry point for any terminal key input
    /// </summary>
    public class StatusBar : Element
    {
        private List<StatusBarItem> _items = new List<StatusBarItem>();
        private List<StatusBarItem> _prevItems = new List<StatusBarItem>();
        private static StatusBar _instance;

        private List<StatusBarItem> _defaultItems = new List<StatusBarItem>();

        private StatusBar(bool show = false) 
            : base(show) => this.RecalculateAndRedraw();

        /// <summary>
        ///     Recalculate and redraw the status bar
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            this.Height = 1;
            this.Width = this.MaxWidth;

            // OK so i'm not sure why this behaves differently on windows than it does on windows
            // and to be honest, i didn't feel like spending more than 30 seconds addressing it.
            // Basically, if you write all the way to the last character of the line, the console
            // moves down one line (i assume it's just word wrap).. the side effect of this on windows
            // is that, when the status bar is drawn on the very last line, to the very last column.. it 
            // pushes the entire window up one line and therefore cuts off the first header line of the
            // terminal. This behavior doesn't happen on linux, so for now.. we just subtract one from the 
            // status bar width on windows and call it a quick fix instead of a lazy hack
            // if (OperatingSystem.IsWindows())
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                this.Width -= 1;
            
            this.TopLeftPoint = new TerminalPoint(0, Terminal.Height-1);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, Terminal.Height-1);

            this.RedrawAll();
        }

        internal void SetDefaultItems(params StatusBarItem[] items)
            => _defaultItems = items.ToList();

        internal void SetItems(params StatusBarItem[] items)
        {
            // stash the current items so that, during redraw, we can remove any key bindings we have
            _prevItems = _items;

            _items = items.ToList();
        }

        /// <summary>
        ///     Show the provided list of items on the status bar
        /// </summary>
        /// <param name="items">Items to display</param>
        public void ShowItems(params StatusBarItem[] items)
        {
            this.SetItems(items);

            this.Redraw();
        }

        /// <summary>
        ///     Redraw the status bar
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            this.RemovePreviousKeyBindings();

            using (this.TopLeftPoint.GetMove())
            {
                ConsoleColor prevBackgroundColor = Console.BackgroundColor;

                this.TopLeftPoint.MoveTo();

                Console.BackgroundColor = TerminalColor.StatusBarBackgroundColor;

                List<StatusBarItem> items = _items;
                bool isDefaultList = false;
                
                if (items == null || items.Count == 0) 
                {
                    items = _defaultItems;
                    isDefaultList = true;
                }
                else
                    this.RemoveDefaultKeyBindings();

                for (int i = 0; i < items.Count; i++)
                {
                    StatusBarItem item = items[i];

                    Terminal.Write(' ');

                    if (item.Keys != null && item.Task != null)
                    {
                        for (int k = 0; k < item.Keys.Length; k++)
                        {
                            Key key = item.Keys[k];

                            // TODO: Convert to task
                            KeyInput.RegisterKey(key, item.Task);

                            if (item.ShowKey)
                                Terminal.WriteColor(TerminalColor.StatusBarKeyForegroundColor, $"{key.ToString()}");

                            if (k < (item.Keys.Length-1))
                                Terminal.WriteColor(TerminalColor.StatusBarSeparatorForegroundColor, "/");
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

                        if (!isDefaultList)
                            _items.Remove(item);

                        this.Redraw();
                    });

                    if (i < (items.Count-1))
                        Terminal.WriteColor(TerminalColor.StatusBarSeparatorForegroundColor, (char)BoxChars.ThinVertical);
                }

                int charsToBlank = this.Width - Terminal.Left;

                for (int i = 0; i < charsToBlank; i++)
                    Terminal.Write(' ');

                Console.BackgroundColor = prevBackgroundColor;
            }
        }

        private void RemoveDefaultKeyBindings()
        {
            if (_defaultItems == null)
                return;

            foreach (StatusBarItem defaultItem in _defaultItems)
            {
                if (defaultItem.Keys != null)
                {
                    foreach (Key key in defaultItem.Keys)
                        KeyInput.UnregisterKey(key);
                }
            }
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

        /// <summary>
        ///     Remove an item from the status bar by the provided name 
        /// </summary>
        /// <param name="name">name of the item to remove</param>
        public void RemoveItemByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var item = _items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (item != null)
                item.Remove();
        }

        /// <summary>
        ///     Reset the status bar back to the default items
        /// </summary>
        public void Reset()
            => this.ShowItems();

        /// <summary>
        ///     Get the existing instance of the StatusBar or create a new one
        /// </summary>
        /// <returns>StatusBar instance</returns>
        public static StatusBar GetInstance(bool show = false)
        {
            if (_instance == null)
                _instance = new StatusBar(show: show);

            return _instance;
        }
    }
}
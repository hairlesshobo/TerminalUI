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
using System.Threading.Tasks;

namespace TerminalUI.Types
{
    /// <summary>
    ///     Describes an item that is to be displayed on the status bar
    /// </summary>
    public class StatusBarItem
    {
        /// <summary>
        ///     Name of the status bar item. This is also the text that is shown
        ///     on the status bar
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Keys that are to be associated with this status bar item. If any of the 
        ///     provided keys are pressed, the task associated with this item is executed
        /// </summary>
        public Key[] Keys { get; private set; }

        /// <summary>
        ///     Task to run when any of the provided keys are pressed
        /// </summary>
        /// <value></value>
        public Func<Key, Task> Task { get; private set; }

        /// <summary>
        ///     Flag indicating whether to show the key information in the status bar
        /// </summary>
        /// <value></value>
        public bool ShowKey { get; set; } = true;

        private Action removeCallback = () => { };

        /// <summary>
        ///     Construct a text-only status bar item
        /// </summary>
        /// <param name="name">Text to show</param>
        public StatusBarItem(string name)
        {
            this.Name = name;
        }

        /// <summary>
        ///     Constuct a new status bar item
        /// </summary>
        /// <param name="name">Text to show</param>
        /// <param name="task">Task to perform</param>
        /// <param name="keys">Key(s) that will trigger this task</param>
        public StatusBarItem(string name, Func<Key, Task> task, params Key[] keys)
        {
            this.Name = name;
            this.Task = task;
            this.Keys = keys;
        }

        /// <summary>
        ///     Remove this item from the status bar
        /// </summary>
        public void Remove()
            => removeCallback();

        /// <summary>
        ///     Used to bolt the status bar with the status bar item remove callback
        /// </summary>
        /// <param name="callback"></param>
        internal void AddRemoveCallback(Action callback)
            => removeCallback = callback;
    }
}
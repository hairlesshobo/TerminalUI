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

namespace TerminalUI
{
    public class StatusBarItem
    {
        public string Name { get; private set; }
        public Key[] Keys { get; private set; }
        public Func<Key, Task> Task { get; private set; }
        public bool ShowKey { get; set; } = true;

        private Action removeCallback = () => { };

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
}
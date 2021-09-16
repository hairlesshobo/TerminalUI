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
}
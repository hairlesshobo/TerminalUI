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
using System.Threading;
using System.Threading.Tasks;

namespace TerminalUI.Types
{
    /// <summary>
    ///     Represents a single menu entry that is to be displayed on a Menu element
    /// </summary>
    public class MenuEntry
    {
        /// <summary>
        ///     Name of the menu entry (the text that is displayed to the end user)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Task to perform if this is the menu entry that is selected.
        /// 
        ///     Note: This task is only executed if the menu is in single-select mode.
        ///           It will not be selected if the menu is in multi-select mode
        /// </summary>
        public Func<CancellationToken, Task> Task { get; set; }

        /// <summary>
        ///     If true, the menu entry is disabled and cannot be selected by the end user
        /// </summary>
        public bool Disabled { get; set; } = false;

        /// <summary>
        ///     If true, the menu entry is a header 
        /// </summary>
        public bool Header { get; set; } = false;

        /// <summary>
        ///     Value that is to be returned if this menu entry is selected
        /// </summary>
        public object SelectedValue { get; set; }
        
        /// <summary>
        ///     Foreground color for this entry
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; } = Terminal.ForegroundColor;

        /// <summary>
        ///     Background color for this entry
        /// </summary>
        public ConsoleColor BackgroundColor { get; set; } = Terminal.BackgroundColor;

        /// <summary>
        ///     Flag indicating whether this entry is selected
        /// </summary>
        public bool Selected { get; internal set; }
    }
}
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

namespace FoxHollow.TerminalUI.Types
{
    /// <summary>
    ///     Enum that allows specifying which "area" an element should reside in
    ///     For example, if "RightHalf" is specified, that will have the left side
    ///     of the element be placed at the center of the terminal
    /// </summary>
    public enum TerminalArea
    {
        /// <summary>
        ///     No special constraints are placed on the element and the 
        ///     current cursor position is used
        /// </summary>
        Default,

        /// <summary>
        ///     The entire terminal is available for use and, for most elements,
        ///     the cursor will be returned to the left-most position of the line
        ///     during positioning
        /// </summary>
        EntireTerminal,

        /// <summary>
        ///     Right half of terminal is available for use
        /// </summary>
        LeftHalf,
        
        /// <summary>
        ///     Left half of terminal is available for use
        /// </summary>
        RightHalf
    }
}
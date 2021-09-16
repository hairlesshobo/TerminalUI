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
 
namespace TerminalUI.Types
{
    /// <summary>
    ///     Enum that specifies how text is positioned on the line
    /// </summary>
    public enum TextJustify
    {
        /// <summary>
        ///     Text is positioned to the left side of the line
        /// </summary>
        Left = 0,

        /// <summary>
        ///     Text is positioned in the center portion of the line
        /// </summary>
        Center = 1,

        /// <summary>
        ///     Text is positioned to the right side of the line
        /// </summary>
        Right = 2
    }
}
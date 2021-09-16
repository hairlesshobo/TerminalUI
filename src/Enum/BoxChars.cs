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
    ///     Characters used for producing boxes
    /// </summary>
    public enum BoxChars
    {
        ThinTopLeft = '\u250C',
        ThinBottomLeft = '\u2514',
        ThinTopRight = '\u2510',
        ThinBottomRight = '\u2518',
        ThinVertical = '\u2502',
        ThinHorizontal = '\u2500',
        ThickHorizontal = '\u2501',
        ThinDashHorizontal = '\u2504',
        ThickDashHorizontal = '\u2505'
    }
}
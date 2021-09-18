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

namespace TerminalUI.Types
{
    /// <summary>
    ///     Enum that describes the row selection mode of a data table
    /// </summary>
    public enum DataTableSelectType
    {
        /// <summary>
        ///     No selection will be allowed
        /// </summary>
        None = 0,
        
        /// <summary>
        ///     Single row selection will be allowed
        /// </summary>
        Single = 1,
        
        /// <summary>
        ///     Multiple rows will be selectable
        /// </summary>
        Multiple = 2
    }
}
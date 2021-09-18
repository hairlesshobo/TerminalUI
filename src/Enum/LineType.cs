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
    ///     Characters used for drawing horizontal lines
    /// </summary>
    public enum LineType
    {
        /// <summary>
        ///     Thin horizontal line
        /// </summary>
        Thin = '\u2500',
        
        /// <summary>
        ///     Thick horizontal line
        /// </summary>
        Thick = '\u2501',
        
        /// <summary>
        ///     Thin triple dash horizontal line
        /// </summary>
        ThinTripleDash = '\u2504',
        
        /// <summary>
        ///     Thick triple dash horizontal line
        /// </summary>
        ThickTripleDash ='\u2505',
        
        /// <summary>
        ///     Double horizontal line
        /// </summary>
        Double = '\u2550',
        
        /// <summary>
        ///     Thin double horizontal line
        /// </summary>
        ThinDoubleDash = '\u254C',
        
        /// <summary>
        ///     Thick double horizontal line
        /// </summary>
        ThickDoubleDash ='\u254D',
    }
}
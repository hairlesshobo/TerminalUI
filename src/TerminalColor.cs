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

namespace TerminalUI
{
    /// <summary>
    ///     Allow to specify the default colors for the terminal
    /// </summary>
    public static class TerminalColor
    {
        /// <summary>
        ///     Default background color
        /// </summary>
        public static ConsoleColor DefaultBackground { get; set; } = ConsoleColor.Black;

        /// <summary>
        ///     Default foreground color
        /// </summary>
        public static ConsoleColor DefaultForeground { get; set; } = ConsoleColor.White;
        
        /// <summary>
        ///     Color of filled progress bar segment
        /// </summary>
        public static ConsoleColor ProgressBarFilled { get; set; } = ConsoleColor.DarkYellow;
        
        /// <summary>
        ///     Color of unfilled progress bar segment
        /// </summary>
        public static ConsoleColor ProgressBarUnfilled { get; set; } = ConsoleColor.DarkGray;

        
        /// <summary>
        ///     Foreground color of left-side header text
        /// </summary>
        public static ConsoleColor HeaderLeft { get; set; } = ConsoleColor.Magenta;
        
        /// <summary>
        ///     Foreground color of right-side header text
        /// </summary>
        public static ConsoleColor HeaderRight { get; set; } = ConsoleColor.White;
        
        /// <summary>
        ///     Background color of header
        /// </summary>
        public static ConsoleColor HeaderBackground { get; set; } = ConsoleColor.Black;

        
        /// <summary>
        ///     Default foreground color of left side of KeyValueText
        /// </summary>
        public static ConsoleColor KeyValueTextKeyColor { get; set; } = ConsoleColor.Blue;
        
        /// <summary>
        ///     Default foreground color of right side of KeyValueText
        /// </summary>
        public static ConsoleColor KeyValueTextValueColor { get; set; } = ConsoleColor.White;

        
        /// <summary>
        ///     Default background color of the CliMenu cursor
        /// </summary>
        public static ConsoleColor CliMenuCursorBackground { get; set; } = ConsoleColor.DarkGray;
        
        /// <summary>
        ///     Default foreground color of the CliMenu cursor
        /// </summary>
        public static ConsoleColor CliMenuCursorForeground { get; set; } = ConsoleColor.DarkGreen;
        
        /// <summary>
        ///     Default foreground color of the CliMenu cursor arrow
        /// </summary>
        public static ConsoleColor CliMenuCursorArrow { get; set; } = ConsoleColor.Green;
        
        /// <summary>
        ///     Default foreground color for a disabled CliMenu entry
        /// </summary>
        public static ConsoleColor CliMenuDisabledForeground { get; set; } = ConsoleColor.DarkGray;
        
        /// <summary>
        ///     Default foreground for a CliMenu header entry
        /// </summary>
        public static ConsoleColor CliMenuHeaderForeground { get; set; } = ConsoleColor.Cyan;

        
        /// <summary>
        ///     Foreground color of the highlighted text of a Pager
        /// </summary>
        public static ConsoleColor PagerHighlightColorForeground { get; set; } = ConsoleColor.DarkYellow;

        /// <summary>
        ///     Foreground color of the pager line numbers
        /// </summary>
        public static ConsoleColor PagerLineNumberForeground { get; set; } = ConsoleColor.White;

        /// <summary>
        ///     Background color of the pager line numbers
        /// </summary>
        public static ConsoleColor PagerLineNumberBackground { get; set; } = ConsoleColor.DarkBlue;

        /// <summary>
        ///     Background color of the pager header
        /// </summary>
        public static ConsoleColor PagerHeaderBackground { get; set; } = ConsoleColor.DarkBlue;

        /// <summary>
        ///     Foreground color of a line
        /// </summary>
        public static ConsoleColor LineForegroundColor { get; set; } = ConsoleColor.White;
    }
}
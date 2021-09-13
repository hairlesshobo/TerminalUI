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

namespace TerminalUI
{
    public static class TerminalColor
    {
        public static ConsoleColor DefaultBackground { get; set; } = ConsoleColor.Black;
        public static ConsoleColor DefaultForeground { get; set; } = ConsoleColor.White;
        
        // DarkCyan, DarkYellow
        public static ConsoleColor ProgressBarFilled { get; set; } = ConsoleColor.DarkYellow;
        public static ConsoleColor ProgressBarUnfilled { get; set; } = ConsoleColor.DarkGray;

        public static ConsoleColor HeaderLeft { get; set; } = ConsoleColor.Magenta;
        public static ConsoleColor HeaderRight { get; set; } = ConsoleColor.White;
        public static ConsoleColor HeaderBackground { get; set; } = ConsoleColor.Black;

        public static ConsoleColor KeyValueTextKeyColor { get; set; } = ConsoleColor.Blue;
        public static ConsoleColor KeyValueTextValueColor { get; set; } = ConsoleColor.White;

        public static ConsoleColor CliMenuCursorBackground { get; set; } = ConsoleColor.DarkGray;
        public static ConsoleColor CliMenuCursorForeground { get; set; } = ConsoleColor.DarkGreen;
        public static ConsoleColor CliMenuCursorArrow { get; set; } = ConsoleColor.Green;
        public static ConsoleColor CliMenuDisabledForeground { get; set; } = ConsoleColor.DarkGray;
        public static ConsoleColor CliMenuHeaderForeground { get; set; } = ConsoleColor.Cyan;

        public static ConsoleColor PagerHighlightColorForeground { get; set; } = ConsoleColor.DarkYellow;
    }
}
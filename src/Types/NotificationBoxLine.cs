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

namespace FoxHollow.TerminalUI.Types
{
    /// <summary>
    ///     Class that defines a line that is to be displayed in a notification box
    /// </summary>
    public class NotificationBoxLine
    {
        /// <summary>
        ///     Text to display on this line
        /// </summary>
        public string Text
        {
            get => _text ?? String.Empty;
            internal set => _text = value;
        }
        private string _text;

        /// <summary>
        ///     Foreground color to use when drawing this line
        /// </summary>
        public Nullable<ConsoleColor> ForegroundColor
        { 
            get => _foregroundColor ?? TerminalColor.DefaultForeground;
            internal set => _foregroundColor = value; 
        }
        private Nullable<ConsoleColor> _foregroundColor { get; set; }

        /// <summary>
        ///     Background color to use when drawing this line
        /// </summary>
        public Nullable<ConsoleColor> BackgroundColor
        { 
            get => _backgroundColor ?? TerminalColor.DefaultBackground;
            internal set => _backgroundColor = value; 
        }
        private Nullable<ConsoleColor> _backgroundColor { get; set; }

        /// <summary>
        ///     How the text should be justified when drawing
        /// </summary>
        public TextJustify Justify { get; internal set; } = TextJustify.Left;

        /// <summary>
        ///     TerminalPoint where the text line begins
        /// </summary>
        public TerminalPoint RootPoint { get; internal set; }
    }
}
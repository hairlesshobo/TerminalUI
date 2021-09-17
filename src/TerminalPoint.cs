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
using TerminalUI.Types;

namespace TerminalUI
{
    /// <summary>
    ///     Class used to make interacting with terminal positioning much easier
    /// </summary>
    public class TerminalPoint
    {
        /// <summary>
        ///     Left index of cursor
        /// </summary>
        public int Left { get; private set; }

        /// <summary>
        ///     Top index of cursor
        /// </summary>
        public int Top { get; private set; }

        /// <summary>
        ///     Construct a new TerminalPoint from the provided left and top coordinates
        /// </summary>
        /// <param name="left">left index</param>
        /// <param name="top">top index</param>
        public TerminalPoint(int left, int top)
        {
            Top = top;
            Left = left;
        }

        /// <summary>
        ///     Create a copy of the current terminal point and add the specified number of columns to it
        /// </summary>
        /// <param name="amount">Number of columns to move</param>
        /// <returns>New TerminalPoint object</returns>
        public TerminalPoint AddX(int amount)
            => new TerminalPoint(this.Left + amount, this.Top);

        /// <summary>
        ///     Create a copy of the current terminal point and add the specified number of row to it
        /// </summary>
        /// <param name="amount">Number of rows to move</param>
        /// <returns>New TerminalPoint object</returns>
        public TerminalPoint AddY(int amount)
            => new TerminalPoint(this.Left, this.Top + amount);

                /// <summary>
        ///     Create a copy of the current terminal point and add the specified number of columns to it
        /// </summary>
        /// <param name="amountX">Number of columns to move</param>
        /// <param name="amountY">Number of rows to move</param>
        /// <returns>New TerminalPoint object</returns>
        public TerminalPoint Add(int amountX, int amountY)
            => new TerminalPoint(this.Left + amountX, this.Top + amountY);

        /// <summary>
        ///     Get the left-most index of the specified terminal area while staying
        ///     on the same line
        /// </summary>
        /// <param name="area">Terminal area to calculate left point for</param>
        /// <returns>New terminal point that is all the way to the left of the current area and on the same line</returns>
        internal static TerminalPoint GetLeftPoint(TerminalArea area)
        {
            TerminalPoint point = TerminalPoint.GetCurrent();

            if (area == TerminalArea.LeftHalf)
                point.Left = 0;
            else if (area == TerminalArea.RightHalf)
                point.Left = Terminal.UsableWidth / 2;

            return point;
        }

        internal static (TerminalPoint, TerminalPoint, TerminalPoint, TerminalPoint) GetAreaBounds(TerminalArea area)
        {
            int usableHeight = Terminal.UsableHeight;
            int usableWidth = Terminal.UsableWidth;

            if (area == TerminalArea.Default)
            {
                return (Terminal.RootPoint, 
                        Terminal.RootPoint.AddX(usableWidth), 
                        Terminal.RootPoint.AddY(usableHeight), 
                        Terminal.RootPoint.AddX(usableWidth).AddY(usableHeight)
                    );
            }

            int halfWidth = usableWidth / 2;
            int halfHeight = usableHeight / 2;

            if (area == TerminalArea.LeftHalf)
            {
                return (Terminal.RootPoint, 
                        Terminal.RootPoint.AddX(halfWidth), 
                        Terminal.RootPoint.AddY(usableHeight), 
                        Terminal.RootPoint.AddX(halfWidth).AddY(usableHeight)
                    );
            }

            if (area == TerminalArea.RightHalf)
            {
                return (Terminal.RootPoint.AddX(halfWidth), 
                        Terminal.RootPoint.AddX(usableWidth), 
                        Terminal.RootPoint.AddY(usableHeight), 
                        Terminal.RootPoint.AddX(usableWidth).AddY(usableHeight)
                    );
            }

            throw new NotImplementedException();
            
        }

        public TerminalPoint Clone()
            => new TerminalPoint(this.Left, this.Top);

        /// <summary>
        ///     Get the current cursor positoin as a new TerminalPoint
        /// </summary>
        /// <returns>New terminal point representing current cursor position</returns>
        public static TerminalPoint GetCurrent()
            => new TerminalPoint(Console.CursorLeft, Console.CursorTop);

        /// <summary>
        ///     Move to the terminal point described by the the TerminalPoint object
        /// </summary>
        /// <returns>itself</returns>
        public TerminalPoint MoveTo()
        {
            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;

            return this;
        }

        public TerminalPointMove GetMove()
            => new TerminalPointMove(this);

        /// <summary>
        ///     Move to the terminal point described by the the TerminalPoint object
        /// </summary>
        /// <returns>Current terminal point, prior to the move</returns>
        public TerminalPoint MoveToWithCurrent()
        {
            TerminalPoint currentPoint = TerminalPoint.GetCurrent();

            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;

            return currentPoint;
        }
    }
}
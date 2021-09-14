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
    public class TerminalPoint
    {
        public int Left { get; private set; }
        public int Top { get; private set; }

        public TerminalPoint(int left, int top)
        {
            Top = top;
            Left = left;
        }

        public TerminalPoint AddX(int amount)
            => new TerminalPoint(this.Left + amount, this.Top);

        public TerminalPoint AddY(int amount)
            => new TerminalPoint(this.Left, this.Top + amount);

        public TerminalPoint Add(int amountX, int amountY)
            => new TerminalPoint(this.Left + amountX, this.Top + amountY);

        public static TerminalPoint GetLeftPoint(Area area)
        {
            TerminalPoint point = TerminalPoint.GetCurrent();

            if (area == Area.LeftHalf)
                point.Left = 0;
            else if (area == Area.RightHalf)
                point.Left = Terminal.UsableWidth / 2;

            return point;
        }

        public static TerminalPoint GetCurrent()
            => new TerminalPoint(Console.CursorLeft, Console.CursorTop);

        public TerminalPoint MoveTo()
        {
            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;

            return this;
        }
    }
}
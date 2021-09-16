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

namespace TerminalUI.Elements
{
    public class HorizontalLine : Element
    {
        private LineType lineType;
        private ConsoleColor color;

        public HorizontalLine(ConsoleColor color = ConsoleColor.White, LineType lineType = LineType.Thin, int width = 0, TerminalArea area = TerminalArea.Default)
            : base (area)
        {
            this.lineType = lineType;
            this.color = color;
            this.Width = width;

            if (this.Width == 0)
                this.Width = this.MaxWidth;
            else if (this.Width < 0)
                this.Width = this.MaxWidth + this.Width;

            this.TopLeftPoint = TerminalPoint.GetLeftPoint(area);
            this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);

            this.Redraw();
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                TerminalPoint prevPoint = this.TopLeftPoint.MoveToWithCurrent();

                for (int i = 0; i < this.Width; i++)
                    Terminal.WriteColor(color, (char)lineType);

                prevPoint.MoveTo();
            }
        }
    }
}
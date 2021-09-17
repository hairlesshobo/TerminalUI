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

namespace TerminalUI.Elements
{
    public class HorizontalLine : Element
    {
        private int _configuredWidth = 0;
        public LineType LineType { get; private set; }
        public ConsoleColor ForegroundColor { get; private set; }

        public HorizontalLine(
            ConsoleColor color = ConsoleColor.White, 
            LineType lineType = LineType.Thin, 
            int width = 0, 
            TerminalArea area = TerminalArea.Default,
            bool show = false)
            : base (area, show)
        {
            _configuredWidth = width;

            this.LineType = lineType;
            this.ForegroundColor = color;
            
            this.RecalculateAndRedraw();
        }

        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            this.Width = _configuredWidth;

            if (this.Width == 0)
                this.Width = this.MaxWidth;
            else if (this.Width < 0)
                this.Width = this.MaxWidth + this.Width;

            using (this.OriginalPoint.GetMove())
            {
                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
                this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);
            }

            this.RedrawAll();
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                TerminalPoint prevPoint = this.TopLeftPoint.MoveToWithCurrent();

                for (int i = 0; i < this.Width; i++)
                    Terminal.WriteColor(ForegroundColor, (char)LineType);

                prevPoint.MoveTo();
            }
        }
    }
}
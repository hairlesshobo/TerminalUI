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
using FoxHollow.TerminalUI.Types;

namespace FoxHollow.TerminalUI.Elements
{
    /// <summary>
    ///     A horizontal line element is exactly what it sounds like.. a horizontal
    ///     line that is used to separate a line above and below it
    /// </summary>
    public class HorizontalLine : Element
    {
        private int _configuredWidth = 0;

        /// <summary>
        ///     Type of line to draw
        /// </summary>
        public LineType LineType { get; private set; }
        
        /// <summary>
        ///     Foreground color to use when drawing the line
        /// </summary>
        public Nullable<ConsoleColor> ForegroundColor 
        { 
            get => _foregoundColor ?? TerminalColor.LineForegroundColor;
            private set => _foregoundColor = value; 
        }
        private Nullable<ConsoleColor> _foregoundColor;

        /// <summary>
        ///     Construct a new horizontal line
        /// </summary>
        /// <param name="foregroundColor">Color to use when drawing the line</param>
        /// <param name="lineType">Type of line to draw</param>
        /// <param name="width">
        ///     Width to draw the line to.. 
        ///     if 0, it will automatically fill the provided area
        ///     if below 0, it will be the area width - the absolute value provided
        ///     if above 0, it will be a fixed width as provided
        /// </param>
        /// <param name="area">Area to fill</param>
        /// <param name="show">If true, element will be displayed immediately upon construction</param>
        public HorizontalLine(Nullable<ConsoleColor> foregroundColor = null, 
                              LineType lineType = LineType.Thin, 
                              int width = 0, 
                              TerminalArea area = TerminalArea.Default,
                              bool show = false)
            : base (area, show)
        {
            _configuredWidth = width;

            this.LineType = lineType;
            this.ForegroundColor = foregroundColor;
            
            this.RecalculateAndRedraw();
        }


        /// <summary>
        ///     Recalculate and redraw the entire element
        /// </summary>
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

        /// <summary>
        ///     Redraw the element, if visible
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                if (this.ForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.ForegroundColor = this.ForegroundColor.Value;

                for (int i = 0; i < this.Width; i++)
                    Terminal.Write((char)LineType);

                if (this.ForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.ResetForeground();
            }
        }
    }
}
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
    public class SplitLine : Element
    {
        private ConsoleColor? _leftColor = null;
        private ConsoleColor? _rightColor = null;

        public ConsoleColor LeftColor => (_leftColor != null ? _leftColor.Value : TerminalColor.HeaderLeft);
        public ConsoleColor RightColor => (_rightColor != null ? _rightColor.Value : TerminalColor.HeaderRight);

        public string LeftText { get; private set; }
        public string RightText { get; private set; }


        public SplitLine(
            string leftText, 
            string rightText, 
            ConsoleColor? leftColor = null, 
            ConsoleColor? rightColor = null, 
            bool show = false
            ) : base(show)
        {
            this._leftColor = leftColor;
            this._rightColor = rightColor;

            this.Update(leftText, rightText, true);

            this.RecalculateAndRedraw();
        }

        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.TopLeftPoint = TerminalPoint.GetCurrent();
                this.TopRightPoint = new TerminalPoint(Terminal.Width, this.TopLeftPoint.Top);
            }

            this.Height = 1;
            this.Width = TopRightPoint.Left - TopLeftPoint.Left;

            this.RedrawAll();
        }

        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                Terminal.WriteColor(this.LeftColor, LeftText);

                int splitChars = this.Width - LeftText.Length - RightText.Length;

                for (int i = 0; i < splitChars; i++)
                    Terminal.Write(' ');

                Terminal.WriteColor(this.RightColor, RightText);
            }
        }

        public void Update(string left, string right, bool noRedraw = false)
        {
            LeftText = left;
            RightText = right;

            if (!noRedraw)
                this.Redraw();
        }

        public void UpdateLeft(string text, bool noRedraw = false)
        {
            LeftText = text;

            if (!noRedraw)
                this.Redraw();
        }

        public void UpdateRight(string text, bool noRedraw = false)
        {
            RightText = text;

            if (!noRedraw)
                this.Redraw();
        }
    }
}
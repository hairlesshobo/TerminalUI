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

        private ConsoleColor LeftColor => (_leftColor != null ? _leftColor.Value : TerminalColor.HeaderLeft);
        private ConsoleColor RightColor => (_rightColor != null ? _rightColor.Value : TerminalColor.HeaderRight);

        private string leftText;
        private string rightText;


        public SplitLine(
            string leftText, 
            string rightText, 
            ConsoleColor? leftColor = null, 
            ConsoleColor? rightColor = null, 
            bool show = false
            ) : base(TerminalArea.Default, show)
        {
            this._leftColor = leftColor;
            this._rightColor = rightColor;

            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(Terminal.Width, this.TopLeftPoint.Top);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            this.Height = 1;
            this.Width = TopRightPoint.Left - TopLeftPoint.Left;

            this.Update(leftText, rightText);
        }

        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                Terminal.WriteColor(this.LeftColor, leftText);

                int splitChars = this.Width - leftText.Length - rightText.Length;

                for (int i = 0; i < splitChars; i++)
                    Terminal.Write(' ');

                Terminal.WriteColor(this.RightColor, rightText);
            }
        }

        public void Update(string left, string right)
        {
            leftText = left;
            rightText = right;

            this.Redraw();
        }

        public void UpdateLeft(string text)
        {
            leftText = text;

            this.Redraw();
        }

        public void UpdateRight(string text)
        {
            rightText = text;

            this.Redraw();
        }
    }
}
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
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     Split-line element. This is used for displaying two pieces of text on a line, 
    ///     one is left-justified and the other is right justified
    /// </summary>
    public class SplitLine : Element
    {
        /// <summary>
        ///     Color used for the foreground of the left piece of text
        /// </summary>
        public Nullable<ConsoleColor> LeftForegroundColor
        {
            get => _leftColor ?? TerminalColor.HeaderLeft;
            set => _leftColor = value;
        }
        private ConsoleColor? _leftColor = null;
        
        /// <summary>
        ///     Color used for the foreground of the right piece of text
        /// </summary>
        public Nullable<ConsoleColor> RightForegroundColor
        {
            get => _rightColor ?? TerminalColor.HeaderRight;
            set => _rightColor = value;
        }

        private ConsoleColor? _rightColor = null;

        /// <summary>
        ///     Text that is displayed on the left
        /// </summary>
        public string LeftText 
        { 
            get => _leftText ?? String.Empty;
            private set => _leftText = value;
        }
        private string _leftText = null;

        /// <summary>
        ///     Text that is displayed on the right
        /// </summary>
        public string RightText 
        { 
            get => _rightText ?? String.Empty;
            private set => _rightText = value;
        }
        private string _rightText = null;


        /// <summary>
        ///     Construct a new instance of the split-line element
        /// </summary>
        /// <param name="leftText">Text to show on the left</param>
        /// <param name="rightText">Text to show on the right</param>
        /// <param name="leftColor">Color to use for the left</param>
        /// <param name="rightColor">Color to use for the right</param>
        /// <param name="area">TerminalArea to be confined to</param>
        /// <param name="show">if true, the element will be shown immediately</param>
        public SplitLine(string leftText, 
                         string rightText = null, 
                         ConsoleColor? leftColor = null, 
                         ConsoleColor? rightColor = null, 
                         TerminalArea area = TerminalArea.Default,
                         bool show = false) 
            : base(area, show)
        {
            this.LeftForegroundColor = leftColor;
            this.RightForegroundColor = rightColor;

            this.Update(leftText, rightText, true);

            this.RecalculateAndRedraw();
        }

        /// <summary>
        ///     Recalculate the layout and redraw the entire element
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
                this.TopRightPoint = TerminalPoint.GetRightPoint(this.Area);
            }

            this.Height = 1;
            this.Width = TopRightPoint.Left - TopLeftPoint.Left;

            this.RedrawAll();
        }

        /// <summary>
        ///     Redraw the element
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                if (this.LeftForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.LeftForegroundColor.Value, this.LeftText);
                else
                    Terminal.Write(this.LeftText);

                int splitChars = this.Width - LeftText.Length - RightText.Length;

                for (int i = 0; i < splitChars; i++)
                    Terminal.Write(' ');

                if (this.RightForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.RightForegroundColor.Value, this.RightText);
                else
                    Terminal.Write(this.RightText);
            }
        }

        /// <summary>
        ///     Update the left and right text and redraw the element
        /// </summary>
        /// <param name="leftText">Text to draw on the left</param>
        /// <param name="rightText">Text to draw on the right</param>
        /// <param name="noRedraw">If true, the element will not be redrawn</param>
        public void Update(string leftText, string rightText, bool noRedraw = false)
        {
            this.LeftText = leftText;
            this.RightText = rightText;

            if (!noRedraw)
                this.Redraw();
        }

        /// <summary>
        ///     Update the left text and redraw the element
        /// </summary>
        /// <param name="leftText">Text to draw on the left</param>
        /// <param name="noRedraw">If true, the element will not be redrawn</param>   
        public void UpdateLeft(string leftText, bool noRedraw = false)
        {
            this.LeftText = leftText;

            if (!noRedraw)
                this.Redraw();
        }

        /// <summary>
        ///     Update the right text and redraw the element
        /// </summary>
        /// <param name="rightText">Text to draw on the right</param>
        /// <param name="noRedraw">If true, the element will not be redrawn</param>
        public void UpdateRight(string rightText, bool noRedraw = false)
        {
            RightText = rightText;

            if (!noRedraw)
                this.Redraw();
        }
    }
}
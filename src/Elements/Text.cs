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
    /// <summary>
    ///     Text element
    ///     This is a very simple element that just displays text by itself
    /// </summary>
    public class Text : Element
    {
        private int prevWidth = 0;
        private string text = String.Empty;
        private ConsoleColor? color;

        /// <summary>
        ///     Construct a new Text element, using the provided text as the initial 
        ///     text to be displayed
        /// </summary>
        /// <param name="valueText">Initial text to display</param>
        /// <param name="area">Terminal area to constrain the element to</param>
        public Text(string valueText, TerminalArea area = TerminalArea.Default)
            : base (area)
        {
            this.TopLeftPoint = TerminalPoint.GetLeftPoint(area);

            this.UpdateValue(valueText);
        }

        /// <summary>
        ///     Construct a new Text element, using the provided text and foreground color
        ///     as the initial text to be displayed
        /// </summary>
        /// <param name="foregroundColor">Color to use drawing the text element</param>
        /// <param name="valueText">Initial text to display</param>
        /// <param name="area">Terminal area to constrain the element to</param>
        public Text(ConsoleColor foregroundColor, string valueText, TerminalArea area = TerminalArea.Default)
            : base (area)
        {
            this.TopLeftPoint = TerminalPoint.GetLeftPoint(area);

            this.UpdateValue(foregroundColor, valueText);
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                TerminalPoint previousPoint = TerminalPoint.GetCurrent();
                TopLeftPoint.MoveTo();

                if (this.text == null)
                    this.text = String.Empty;

                if (this.color != null)
                    Terminal.ForegroundColor = this.color.Value;

                Terminal.Write(this.text);
                
                if (this.text.Length < prevWidth)
                {
                    int spacesToClear = prevWidth - this.text.Length;

                    for (int i = 0; i < spacesToClear; i++)
                        Terminal.Write(' ');
                }

                if (this.color != null)
                    Terminal.ResetForeground();

                prevWidth = this.text.Length;

                this.TopRightPoint = TerminalPoint.GetCurrent();

                previousPoint.MoveTo();
            }
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            this.color = color;

            this.Redraw();
        }

        public void UpdateValue(ConsoleColor color, string newText)
            => UpdateValue(newText, color);

        public void UpdateValue(string newText)
            => UpdateValue(newText, null);

        private void UpdateValue(string newText, ConsoleColor? color = null)
        {
            this.text = newText;

            if (color != null)
                this.color = color;

            this.Redraw();
        }
    }
}
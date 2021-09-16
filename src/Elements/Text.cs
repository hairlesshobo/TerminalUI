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
        private int _prevWidth = 0;

        /// <summary>
        ///     Current foreground color in use by the text element. Null if the default
        ///     terminal foreground color should be used, as defined in
        ///     <see cref="TerminalColor.DefaultForeground" />
        /// </summary>
        public ConsoleColor? ForegroundColor { get; private set; } = null;

        /// <summary>
        ///     Current text value being displayed by the text element
        /// </summary>
        public string TextValue { get; private set; } = String.Empty;

        /// <summary>
        ///     Construct a new Text element, using the provided text as the initial 
        ///     text to be displayed
        /// </summary>
        /// <param name="valueText">Initial text to display</param>
        /// <param name="area">Terminal area to constrain the element to</param>
        public Text(string valueText, TerminalArea area = TerminalArea.Default, bool show = false)
            : base (area, show)
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

        /// <summary>
        ///     If visible, redraw the text element
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;
             
            using (this.TopLeftPoint.GetMove())
            {
                if (this.TextValue == null)
                    this.TextValue = String.Empty;

                if (this.ForegroundColor != null)
                    Terminal.ForegroundColor = this.ForegroundColor.Value;

                Terminal.Write(this.TextValue);
                
                if (this.TextValue.Length < _prevWidth)
                {
                    int spacesToClear = _prevWidth - this.TextValue.Length;

                    for (int i = 0; i < spacesToClear; i++)
                        Terminal.Write(' ');
                }

                if (this.ForegroundColor != null)
                    Terminal.ResetForeground();

                _prevWidth = this.TextValue.Length;

                this.TopRightPoint = TerminalPoint.GetCurrent();
            }
        }

        /// <summary>
        ///     Set the foreground color of the text element
        /// </summary>
        /// <param name="foregroundColor">
        ///     Foreground color, null if the element should use the default foreground color
        /// </param>
        public void SetForegroundColor(ConsoleColor? foregroundColor)
        {
            this.ForegroundColor = foregroundColor;

            this.Redraw();
        }

        /// <summary>
        ///     Update the text value and foreground color then redraw the text element
        /// </summary>
        /// <param name="foregroundColor">New foreground color to use</param>
        /// <param name="newText">New text to display</param>
        public void UpdateValue(ConsoleColor foregroundColor, string newText)
            => UpdateValue(newText, foregroundColor);

        /// <summary>
        ///     Update the text value and redraw the text element
        /// </summary>
        /// <param name="newText">New text to display</param>
        public void UpdateValue(string newText)
            => UpdateValue(newText, null);

        /// <summary>
        ///     Private: Update the text value and optionally the color, then redraw the text element
        /// </summary>
        /// <param name="newText">New text to display</param>
        /// <param name="color">Optional color to update to</param>
        private void UpdateValue(string newText, ConsoleColor? color = null)
        {
            this.TextValue = newText;

            if (color != null)
                this.ForegroundColor = color;

            this.Redraw();
        }
    }
}
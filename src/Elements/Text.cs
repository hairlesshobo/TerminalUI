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
    ///     Text element
    ///     This is a very simple element that just displays text by itself
    /// </summary>
    public class Text : Element
    {
        private int _configuredMaxLength = 0;
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
        public string TextValue 
        { 
            get => _textValue;
            private set => _textValue = value ?? String.Empty;
        }
        private string _textValue = String.Empty;

        /// <summary>
        ///     The effective width of the text
        /// </summary>
        public int TextWidth => this.TextValue.Length > this.MaxLength ? this.MaxLength : this.TextValue.Length;

        /// <summary>
        ///     Maximum length the string may be
        /// </summary>
        public int MaxLength { get; private set; } = 0;

        /// <summary>
        ///     Construct a new Text element, using the provided text as the initial 
        ///     text to be displayed
        /// </summary>
        /// <param name="text">Initial text to display</param>
        /// <param name="maxLength">Maximum number of characters the text field may contain</param>
        /// <param name="foregroundColor">Color to use when drawing the text</param>
        /// <param name="area">Terminal area to constrain the element to</param>
        /// <param name="show">If true, element will automatically be shown once constructed</param>
        public Text(string text = null, 
                    int maxLength = 0, 
                    Nullable<ConsoleColor> foregroundColor = null, 
                    TerminalArea area = TerminalArea.Default, 
                    bool show = false)
            : base (area, show)
        {
            this.TextValue = text;
            this.ForegroundColor = foregroundColor;

            _configuredMaxLength = maxLength;

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
                this.TopRightPoint = this.TopLeftPoint.AddX(this.TextValue.Length);

                this.MaxLength = _configuredMaxLength;

                // if less than 0, use that many characters less than MaxWidth
                if (this.MaxLength < 0)
                    this.MaxLength = this.MaxWidth + this.MaxLength;
                else if (this.MaxWidth < this.MaxLength || this.MaxLength == 0)
                    this.MaxLength = this.MaxWidth;
            }

            this.RedrawAll();
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
                if (this.ForegroundColor != null)
                    Terminal.WriteColor(this.ForegroundColor.Value, this.TextValue.Substring(0, this.TextWidth));
                else
                    Terminal.Write(this.TextValue.Substring(0, this.TextWidth));

                this.TopRightPoint = TerminalPoint.GetCurrent();
                
                if (this.TextValue.Length < _prevWidth)
                {
                    int spacesToClear = _prevWidth - this.TextValue.Length;

                    for (int i = 0; i < spacesToClear; i++)
                        Terminal.Write(' ');
                }

                _prevWidth = this.TextWidth;
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
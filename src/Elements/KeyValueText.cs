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
    ///     A class to display a KeyValue textual item. A KeyValueText element
    ///     consists of two pieces of text.. a "Key", which is a "static" string,
    ///     and a "Value" which is a dynamic and frequently changing string
    /// </summary>
    public class KeyValueText : Element
    {
        private TerminalPoint _kvtRightPoint;
        private int _prevRightWidth = 0;
        private int _configuredLeftWidth = 0;
        private int _configuredRightMaxLength = 0;
        
        /// <summary>
        ///     Text to use for the key, that is, on the left side of the element
        /// </summary>
        public string KeyText 
        { 
            get => _keyText;
            private set => _keyText = value ?? String.Empty;
        }
        private string _keyText = String.Empty;

        /// <summary>
        ///     Text to display on the right side of the element
        /// </summary>
        public string ValueText 
        { 
            get => _valueText;
            private set => _valueText = value ?? String.Empty;
        }
        private string _valueText = String.Empty;

        /// <summary>
        ///     Foreground color to use for the "Key" string
        /// </summary>
        public ConsoleColor KeyColor { get; set; } = TerminalColor.KeyValueTextKeyColor;

        /// <summary>
        ///     Foreground color to use for the "Value" string
        /// </summary>
        public ConsoleColor ValueColor { get; set; } = TerminalColor.KeyValueTextValueColor;

        /// <summary>
        ///     Maximum length the value may be
        /// </summary>
        public int MaxValueLength { get; private set; } = 0;

        /// <summary>
        ///     How wide the left side of the element is 
        /// </summary>
        public int LeftWidth { get; private set; }

        /// <summary>
        ///     Width of the right part of the element
        /// </summary>
        public int RightWidth => this.ValueText.Length > this.MaxValueLength ? this.MaxValueLength : this.ValueText.Length;

        /// <summary>
        ///     String that is used as the separator between the left and the right
        /// </summary>
        public string Separator
        {
            get => _separator;
            set => _separator = value ?? ": ";
        }
        private string _separator;
        
        /// <summary>
        ///     Construct a new KeyValueText element
        /// </summary>
        /// <param name="keyText">Text to use for the "key"</param>
        /// <param name="valueText">Text to use for the "value"</param>
        /// <param name="leftWidth">
        ///     The optional fixed width to use for the "key" side of the element.
        /// 
        ///     - If the provided value is 0, no padding and therefore no fixed width is applied
        ///     - If the provided value is less than 0, the value text is to be right-justified 
        ///       with a fixed width using the absolute width as provided
        ///     - If the provided value is greater than 0, the value text is to be left-justified
        ///       with a fixed width using the absolutely width as provided
        /// </param>
        /// <param name="rightMaxLength">Maximum string length the right part of the element may contain</param>
        /// <param name="separator">String used to separate the left and the right sides</param>
        /// <param name="area">TerminalArea the element should be constrainted to</param>
        /// <param name="show">If true, the element will be shown immediately</param>
        public KeyValueText(string keyText, 
                            string valueText = null, 
                            int leftWidth = 0,
                            int rightMaxLength = 0,
                            string separator = ": ",
                            TerminalArea area = TerminalArea.Default,
                            bool show = false)
            : base (area, show)
        {
            this.KeyText = keyText;
            this.ValueText = valueText;
            this.Separator = separator;
            
            _configuredRightMaxLength = rightMaxLength;
            _configuredLeftWidth =  leftWidth;

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
                this.LeftWidth = this.ValueText.Length;

                if (_configuredLeftWidth != 0)
                    this.LeftWidth = Math.Abs(_configuredLeftWidth);

                // add space for the left and right separator
                this.LeftWidth += this.Separator.Length;

                this.MaxValueLength = _configuredRightMaxLength;

                if (this.MaxValueLength == 0)
                    this.MaxValueLength = this.MaxWidth - this.LeftWidth;
                else if (this.MaxValueLength < 0)
                    this.MaxValueLength = (this.MaxWidth + this.MaxValueLength) - this.LeftWidth;
                
                this.TopRightPoint = this.TopLeftPoint.AddX(LeftWidth + this.RightWidth);
                _kvtRightPoint = this.TopLeftPoint.AddX(LeftWidth);
            }

            this.RedrawAll();
        }

        /// <summary>
        ///     Change the separator that is used between left and right
        /// </summary>
        /// <param name="separator">new separator string</param>
        public void SetSeparator(string separator)
        {
            this.Separator = separator;

            this.RecalculateAndRedraw();
        }

        /// <summary>
        ///     Redraw the entire element, both the static "Key" and the dynamic "Value" strings
        /// </summary>
        public override void RedrawAll()
        {
            if (!this.Visible)
                return;
            
            using (this.TopLeftPoint.GetMove())
            {
                if (_configuredLeftWidth < 0)
                    this.KeyText = this.KeyText.PadLeft(this.LeftWidth - this.Separator.Length);
                else if (_configuredLeftWidth > 0)
                    this.KeyText = this.KeyText.PadRight(this.LeftWidth - this.Separator.Length);

                if (this.KeyColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.KeyColor, $"{KeyText}{this.Separator}");
                else
                    Terminal.Write($"{KeyText}{this.Separator}");

                this.Redraw();
            }
        }

        /// <summary>
        ///     Redraw only the "value" side of the element
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this._kvtRightPoint.GetMove())
            {
                if (this.ValueColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.ValueColor, this.ValueText.Substring(0, this.RightWidth));
                else
                    Terminal.Write(this.ValueText.Substring(0, this.RightWidth));
                
                this.TopRightPoint = TerminalPoint.GetCurrent();

                if (this.RightWidth < _prevRightWidth)
                {
                    int spacesToClear = _prevRightWidth - this.RightWidth;

                    for (int i = 0; i < spacesToClear; i++)
                        Terminal.Write(' ');
                }

                _prevRightWidth = this.RightWidth;
            }
        }

        /// <summary>
        ///     Update the value displayed by this element
        /// </summary>
        /// <param name="newText">New text to display</param>
        public void UpdateValue(string newText)
        {
            this.ValueText = newText;

            this.Redraw();
        }
    }
}
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
    /// <summary>
    ///     A class to display a KeyValue textual item. A KeyValueText element
    ///     consists of two pieces of text.. a "Key", which is a "static" string,
    ///     and a "Value" which is a dynamic and frequently changing string
    /// </summary>
    public class KeyValueText : Element
    {
        private TerminalPoint kvtRightPoint;
        private int leftWidth = 0;
        private int prevRightWidth = 0;
        private string keyName = String.Empty;
        private string valueText = String.Empty;

        /// <summary>
        ///     Foreground color to use for the "Key" string
        /// </summary>
        public ConsoleColor KeyColor { get; set; } = TerminalColor.KeyValueTextKeyColor;

        /// <summary>
        ///     Foreground color to use for the "Value" string
        /// </summary>
        /// <value></value>
        public ConsoleColor ValueColor { get; set; } = TerminalColor.KeyValueTextValueColor;

        
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
        /// <param name="area">TerminalArea the element should be constrainted to</param>
        public KeyValueText(
            string keyText, 
            string valueText = null, 
            int leftWidth = 0, 
            TerminalArea area = TerminalArea.Default,
            bool show = false)
            : base (area, show)
        {
            this.TopLeftPoint = TerminalPoint.GetLeftPoint(area);
            this.keyName = keyText;
            this.leftWidth = (leftWidth != 0 ? Math.Abs(leftWidth) : keyText.Length) + 2;
            this.kvtRightPoint = this.TopLeftPoint.AddX(this.leftWidth);
            this.valueText = valueText;

            if (leftWidth < 0)
                this.keyName = this.keyName.PadLeft(leftWidth * -1);
            else if (leftWidth > 0)
                this.keyName = this.keyName.PadRight(leftWidth);

            this.RedrawAll();
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
                if (this.KeyColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.KeyColor, $"{keyName}: ");
                else
                    Terminal.Write($"{keyName}: ");

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

            using (this.kvtRightPoint.GetMove())
            {
                // TODO: if total length > this.MaxWidth.. truncate the text

                if (this.valueText == null)
                    this.valueText = String.Empty;

                if (this.ValueColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.ValueColor, $"{keyName}: ");
                else
                    Terminal.Write(this.valueText);
                
                if (this.valueText.Length < prevRightWidth)
                {
                    int spacesToClear = prevRightWidth - this.valueText.Length;

                    for (int i = 0; i < spacesToClear; i++)
                        Terminal.Write(' ');
                }

                prevRightWidth = this.valueText.Length;

                this.TopRightPoint = TerminalPoint.GetCurrent();
            }
        }

        public void UpdateValue(string newText)
        {
            this.valueText = newText;

            this.Redraw();
        }
    }
}
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
    public class KeyValueText : Element
    {
        public ConsoleColor KeyColor { get; set; } = TerminalColor.KeyValueTextKeyColor;
        public ConsoleColor ValueColor { get; set; } = TerminalColor.KeyValueTextValueColor;

        private TerminalPoint kvtRightPoint;
        private int leftWidth = 0;
        private int prevRightWidth = 0;
        private string keyName = String.Empty;
        private string valueText = String.Empty;

        public KeyValueText(string keyName, string valueText = null, int leftWidth = 0, TerminalArea area = TerminalArea.Default)
            : base (area)
        {
            this.TopLeftPoint = TerminalPoint.GetLeftPoint(area);
            this.keyName = keyName;
            this.leftWidth = (leftWidth != 0 ? Math.Abs(leftWidth) : keyName.Length) + 2;
            this.kvtRightPoint = this.TopLeftPoint.AddX(this.leftWidth);
            this.valueText = valueText;

            if (leftWidth < 0)
                this.keyName = this.keyName.PadLeft(leftWidth * -1);
            else if (leftWidth > 0)
                this.keyName = this.keyName.PadRight(leftWidth);
        }

        public override void RedrawAll()
        {
            if (this.Visible)
            {
                TerminalPoint prevPoint = TerminalPoint.GetCurrent();
                this.TopLeftPoint.MoveTo();

                if (this.KeyColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.KeyColor, $"{keyName}: ");
                else
                    Terminal.Write($"{keyName}: ");

                this.Redraw();

                prevPoint.MoveTo();
            }
        }

        public override void Redraw()
        {
            // TODO: if total length > this.MaxWidth.. truncate the text
            if (this.Visible)
            {
                TerminalPoint previousPoint = TerminalPoint.GetCurrent();
                kvtRightPoint.MoveTo();

                if (this.valueText == null)
                    this.valueText = String.Empty;

                if (this.ValueColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.ValueColor, $"{keyName}: ");
                else
                    Console.Write(this.valueText);
                
                if (this.valueText.Length < prevRightWidth)
                {
                    int spacesToClear = prevRightWidth - this.valueText.Length;

                    for (int i = 0; i < spacesToClear; i++)
                        Console.Write(' ');
                }

                prevRightWidth = this.valueText.Length;

                this.TopRightPoint = TerminalPoint.GetCurrent();

                previousPoint.MoveTo();
            }
        }

        public void UpdateValue(string newText)
        {
            this.valueText = newText;

            this.Redraw();
        }
    }
}
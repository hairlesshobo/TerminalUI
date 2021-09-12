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
    public class Text : Element
    {
        private int prevWidth = 0;
        private string text = String.Empty;
        private ConsoleColor? color;

        public Text()
            => Init(null);

        public Text(string valueText)
            => Init(valueText);

        public Text(ConsoleColor color, string valueText)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(color, valueText);
        }

        private void Init(string valueText)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(valueText);
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
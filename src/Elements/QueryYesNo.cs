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
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalUI.Elements
{
    public class QueryYesNo : Element
    {
        private string text = String.Empty;
        private ConsoleColor color = TerminalColor.DefaultForeground;

        public QueryYesNo(string queryText)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(queryText);
        }

        public QueryYesNo(ConsoleColor color, string valueText)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();

            this.UpdateValue(valueText, color);
        }

        public override void Redraw()
            => throw new NotSupportedException();

        public void SetForegroundColor(ConsoleColor? color)
            => this.color = (color == null ? TerminalColor.DefaultForeground : color.Value);

        public async Task<Nullable<bool>> QueryAsync(CancellationToken cToken = default)
        {
            this.Visible = true;

            using (this.TopLeftPoint.GetMove())
            {
                if (this.text == null)
                    this.text = String.Empty;

                int queryLength = this.text.Length + 7;

                if (this.color != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.color, this.text);
                else
                    Terminal.Write(this.text);
                
                Terminal.Write(" (yes/");
                Terminal.WriteColor(TerminalColor.PagerHighlightColorForeground, "NO");
                Terminal.Write(") ");

                TerminalPoint textInputPoint = TerminalPoint.GetCurrent();
                int prevLength = 0;

                bool? responseVal = null;

                while (!cToken.IsCancellationRequested)
                {
                    textInputPoint.MoveTo();

                    // erase any previous characters that were entered, if any exist
                    if (prevLength > 0)
                    {
                        for (int i = 0; i < prevLength; i++)
                            Terminal.Write(' ');

                        textInputPoint.MoveTo();
                    }

                    string response = await KeyInput.ReadStringAsync(cToken);

                    this.TopRightPoint = TerminalPoint.GetCurrent();

                    if (response == null)
                        break;

                    prevLength = response.Length;

                    response = response.Trim().ToLower();

                    if (response == String.Empty || response == "no")
                    {
                        responseVal = false;
                        break;
                    }
                    else if (response == "yes")
                    {
                        responseVal = true;
                        break;
                    }
                }

                return responseVal;
            }    
        }

        public override void Show()
            => throw new NotSupportedException();

        private void UpdateValue(string newText, ConsoleColor? color = null)
        {
            if (newText is null)
                newText = String.Empty;

            this.text = newText.Trim();

            if (!text.EndsWith("?"))
                text += "?";

            if (color != null)
                this.color = color.Value;
        }
    }
}
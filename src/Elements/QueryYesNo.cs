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
using System.Threading;
using System.Threading.Tasks;
using FoxHollow.TerminalUI.Types;

namespace FoxHollow.TerminalUI.Elements
{
    /// <summary>
    ///     Simple element that is used to await a yes/no answer from the user
    /// </summary>
    public class QueryYesNo : Element
    {
        /// <summary>
        ///     Text to display to end user
        /// </summary>
        public string QueryText { get; private set; } = String.Empty;


        /// <summary>
        ///     Foreground color to display the query as. Uses default terminal foreground 
        ///     if no color is provided
        /// </summary>
        /// <value></value>
        public Nullable<ConsoleColor> ForegroundColor
        {
            get => _foregroundColor ?? TerminalColor.DefaultForeground;
            private set => _foregroundColor = value;
        }
        private Nullable<ConsoleColor> _foregroundColor = null;

        /// <summary>
        ///     Construct a new isntance of the query element
        /// </summary>
        /// <param name="queryText">Text to show</param>
        /// <param name="foregroundColor">Foreground color to use when drawing the message</param>
        /// <param name="area">Area to constrain the query box to</param>
        public QueryYesNo(string queryText, 
                          Nullable<ConsoleColor> foregroundColor = null,
                          TerminalArea area = TerminalArea.Default)
            : base(area, false) 
        {
            this.SetQueryText(queryText);
            this.ForegroundColor = foregroundColor;

            this.RecalculateAndRedraw();
        }

        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
            }

            // this.RedrawAll();
        }

        /// <summary>
        ///     Not supported by this element
        /// </summary>
        public override void Redraw()
            => throw new NotSupportedException();

        /// <summary>
        ///     Set the foreground color
        /// </summary>
        /// <param name="foregroundColor">New color to use</param>
        public void SetForegroundColor(ConsoleColor? foregroundColor)
            => this.ForegroundColor = foregroundColor;

        /// <summary>
        ///     Asynchronously query the user for a response
        /// </summary>
        /// <param name="cToken">Token used to cancel the query</param>
        /// <returns>
        ///     The result of the task can be one of three states:
        ///
        ///     true  = user entered yes
        ///     false = user entered no
        ///     null  = query was canceled before a valid answer was provided
        /// </returns>
        public async Task<Nullable<bool>> QueryAsync(CancellationToken cToken = default)
        {
            this.Visible = true;

            using (this.TopLeftPoint.GetMove())
            {
                if (this.QueryText == null)
                    this.QueryText = String.Empty;

                int queryLength = this.QueryText.Length + 7;

                if (this.ForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.WriteColor(this.ForegroundColor.Value, this.QueryText);
                else
                    Terminal.Write(this.QueryText);
                
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

        /// <summary>
        ///     Not supported
        /// </summary>
        public override void Show()
            => throw new NotSupportedException();

        /// <summary>
        ///     Set query text to the provided text
        /// </summary>
        /// <param name="newText">text to use</param>
        private void SetQueryText(string newText)
        {
            if (newText is null)
                newText = String.Empty;

            this.QueryText = newText.Trim();

            if (!QueryText.EndsWith("?"))
                QueryText += "?";
        }
    }
}
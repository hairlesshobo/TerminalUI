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

using FoxHollow.TerminalUI.Types;

namespace FoxHollow.TerminalUI.Elements
{
    /// <summary>
    ///     A header element is a two-line element that is comprised of two 
    ///     other elements. The first line is a <see cref="SplitLine" /> element
    ///     the second element is a <see cref="HorizontalLine" />  
    /// </summary>
    public class Header : Element
    {
        /// <summary>
        ///     Text to display on the left side of the SplitLine
        /// </summary>
        public string LeftText { get; private set; }

        /// <summary>
        ///     Text to display on the right side of the SplitLine
        /// </summary>
        /// <value></value>
        public string RightText { get; private set; }

        private SplitLine splText = null;
        private HorizontalLine hl = null;
        private bool _initialized = false;

        /// <summary>
        ///     Construct a new header
        /// </summary>
        /// <param name="leftText">Text to display on left side of the header</param>
        /// <param name="rightText">Text to display on the right side of the header</param>
        /// <param name="area">TerminalArea to use when calcualting the layout</param>
        /// <param name="show">If true, the header will display automatically upon construction</param>
        public Header(string leftText, 
                      string rightText = null,
                      TerminalArea area = TerminalArea.Default,
                      bool show = false) 
            : base (area, show)
        {
            this.LeftText = leftText;
            this.RightText = rightText;
            
            this.RecalculateAndRedraw();
        }

        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.Height = 2;
                this.Width = this.MaxWidth;

                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
                this.TopRightPoint = TerminalPoint.GetRightPoint(this.Area);
                this.BottomLeftPoint = this.TopLeftPoint.AddY(1);
                this.BottomRightPoint = this.TopRightPoint.AddY(1);

                if (!_initialized)
                {
                    using (this.TopLeftPoint.GetMove())
                        splText = new SplitLine(this.LeftText, this.RightText, area: this.Area, show: this.AutoShow);

                    using (this.BottomLeftPoint.GetMove())
                        hl = new HorizontalLine(TerminalColor.DefaultForeground, LineType.Thin, area: this.Area, show: this.AutoShow);
                    
                    _initialized = true;
                }
                else
                {
                    splText.RecalculateAndRedraw();
                    hl.RecalculateAndRedraw();
                }
            }
            // nothing needs to be done here because the child elements automatically handle it

            this.RedrawAll();
        }

        /// <summary>
        ///     Update the text on the left
        /// </summary>
        /// <param name="leftText">text on the left</param>
        public void UpdateLeft(string leftText)
        {
            this.LeftText = leftText;

            this.Redraw();
        }

        /// <summary>
        ///     Update the text on the right
        /// </summary>
        /// <param name="right">Text on the right</param>
        public void UpdateRight(string right)
        {
            this.RightText = right;

            this.Redraw();
        }

        /// <summary>
        ///     Update both left and right text in the header
        /// </summary>
        /// <param name="leftText">Text on the left side of the header</param>
        /// <param name="rightText">Text on the right side of the header</param>
        public void UpdateHeader(string leftText, string rightText)
        {
            this.LeftText = leftText;
            this.RightText = rightText;

            this.Redraw();
        }

        /// <summary>
        ///     Redraw the text portion of the header
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                Terminal.BackgroundColor = TerminalColor.HeaderBackground;

                splText.Update(this.LeftText, this.RightText);

                Terminal.ResetBackground();
            }
        }

        /// <summary>
        ///     Redraw the entire element
        /// </summary>
        public override void RedrawAll()
        {
            if (!this.Visible)
                return;

            this.Redraw();

            // draw the header line
        }

        /// <summary>
        ///     Show the header
        /// </summary>
        public override void Show()
        {
            base.Show();

            this.splText?.Show();
            this.hl?.Show();
        }

        /// <summary>
        ///     Hide the header
        /// </summary>
        public override void Hide()
        {
            this.splText?.Hide();
            this.hl?.Hide();

            base.Hide();
        }
    }
}
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

namespace TerminalUI.Elements
{
    public class Header : Element
    {
        public string Left { get; private set; }
        public string Right { get; private set; }

        private SplitLine splText = null;
        private HorizontalLine hl = null;

        public Header(
            string left, 
            string right,
            bool show = false
            ) : base (TerminalArea.Default, show)
        {
            this.Height = 1;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = new TerminalPoint(0, 0);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, 0);
            this.BottomLeftPoint = new TerminalPoint(0, 1);
            this.BottomRightPoint = new TerminalPoint(Terminal.Width, 1);

            this.UpdateHeader(left, right);
        }

        public void UpdateLeft(string left)
        {
            this.Left = left;

            this.Redraw();
        }

        public void UpdateRight(string right)
        {
            this.Right = right;

            this.Redraw();
        }

        public void UpdateHeader(string left, string right)
        {
            this.Left = left;
            this.Right = right;

            this.Redraw();
        }

        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                Terminal.BackgroundColor = TerminalColor.HeaderBackground;

                if (splText == null)
                    splText = new SplitLine(this.Left, this.Right, show: true);
                else
                    splText.Update(this.Left, this.Right);

                this.BottomLeftPoint.MoveTo();

                if (hl == null)
                    hl = new HorizontalLine(TerminalColor.DefaultForeground, LineType.Thin, show: true);

                Terminal.ResetBackground();
            }
        }

        public override void Show()
        {
            this.splText?.Show();
            this.hl?.Show();

            base.Show();
        }

        public override void Hide()
        {
            this.splText?.Hide();
            this.hl?.Hide();

            base.Hide();
        }
    }
}
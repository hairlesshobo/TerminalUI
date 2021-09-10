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
    public class ProgressBar : Element
    {
        private ProgressDisplay display;
        private ProgressOptions options;
        private int barWidth;
        private double currentPercent;

        public ProgressBar(
            int width = 0,
            ProgressDisplay display = ProgressDisplay.Right,
            ProgressOptions options = ProgressOptions.None,
            double startPercent = 0.0
            )
        {
            this.Height = width;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);

            this.display = display;
            this.options = options;

            if (this.Width == 0)
                this.Width = Terminal.Width;

            this.barWidth = this.Width;

            if (this.display != ProgressDisplay.NoPercent)
                this.barWidth = this.Width - 5;

            this.UpdateProgress(startPercent);
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                TerminalPoint previousPoint = TerminalPoint.GetCurrent();
                this.TopLeftPoint.MoveTo();

                if (currentPercent > 1)
                    currentPercent /= 100.0;

                int filled = (int)Math.Round(this.barWidth * currentPercent);
                int unfilled = this.barWidth - filled;

                string pctString = String.Format("{0,3:N0}%", (currentPercent * 100.0));

                if (display == ProgressDisplay.Left)
                    Terminal.Write(String.Format("{0,5}", pctString));

                Terminal.ForegroundColor = TerminalColor.ProgressBarFilled;

                for (int i = 0; i < filled; i++)
                    Terminal.Write((char)BlockChars.Solid);
                
                Terminal.ForegroundColor = TerminalColor.ProgressBarUnfilled;

                for (int i = filled; i < this.barWidth; i++)
                    Terminal.Write((char)BlockChars.MediumShade);

                Terminal.ResetForeground();

                if (display == ProgressDisplay.Right)
                    Terminal.Write(String.Format("{0,-5}", pctString));

                previousPoint.MoveTo();
            }
        }

        public void UpdateProgress(double newPercent)
        {
            this.currentPercent = newPercent;

            this.Redraw();
        }
    }
}
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
using System.Linq;

namespace TerminalUI.Elements
{
    public class ProgressBar : Element
    {
        public ProgressDisplay Display { get; private set; }
        public ProgressMode Mode { get; private set; }
        public double CurrentPercent { get; private set; }
        public int ExplicitWidth { get; private set; }

        private long value;
        private long divisor;

        public ProgressBar(
            int width = 0,
            ProgressDisplay display = ProgressDisplay.Right,
            ProgressMode mode = ProgressMode.Default,
            double startPercent = 0.0,
            int explicitWidth = 0
            )
        {
           Init(width, display, mode, startPercent, explicitWidth);
        }

        public ProgressBar(ProgressMode mode)
        {
            Init(mode: mode);
        }

        private void Init(
            int width = 0,
            ProgressDisplay display = ProgressDisplay.Right,
            ProgressMode mode = ProgressMode.Default,
            double startPercent = 0.0,
            int explicitWidth = 0
            )
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Value cannot be less than 0");

            this.SetExplicitWidth(explicitWidth);

            if (startPercent < 0)
                startPercent = 0;

            this.Height = width;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);

            this.Display = display;
            this.Mode = mode;

            if (this.Width == 0)
                this.Width = Terminal.Width;

            this.CurrentPercent = startPercent;

            // this.UpdateProgress(startPercent);
        }

        public void SetExplicitWidth(int width)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Value cannot be less than 0");

            this.ExplicitWidth = width;
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                TerminalPoint previousPoint = TerminalPoint.GetCurrent();
                this.TopLeftPoint.MoveTo();

                if (CurrentPercent > 1)
                    CurrentPercent /= 100.0;

                int barWidth = this.GetBarWidth();

                int filled = (int)Math.Round(barWidth * CurrentPercent);
                int unfilled = barWidth - filled;

                string pctString = String.Format("{0,3:N0}%", (CurrentPercent * 100.0));
                string explicitCountString = $"[ {this.value.ToString().PadLeft(this.ExplicitWidth)} / {this.divisor.ToString().PadLeft(this.ExplicitWidth)} ]";
                int explicitCountWidth = explicitCountString.Length + 1;

                if (this.Mode == ProgressMode.ExplicitCountLeft)
                    Terminal.Write(String.Format("{0, -" + explicitCountWidth + "}", explicitCountString));

                if (Display == ProgressDisplay.Left)
                    Terminal.Write(String.Format("{0,-5}", pctString));

                Terminal.ForegroundColor = TerminalColor.ProgressBarFilled;

                for (int i = 0; i < filled; i++)
                    Terminal.Write((char)BlockChars.Solid);
                
                Terminal.ForegroundColor = TerminalColor.ProgressBarUnfilled;

                for (int i = filled; i < barWidth; i++)
                    Terminal.Write((char)BlockChars.MediumShade);

                Terminal.ResetForeground();

                if (this.Mode == ProgressMode.ExplicitCountRight)
                    Terminal.Write(String.Format("{0, " + explicitCountWidth + "}", explicitCountString));

                if (Display == ProgressDisplay.Right)
                    Terminal.Write(String.Format("{0,5}", pctString));

                previousPoint.MoveTo();
            }
        }

        public void UpdateProgress(double newPercent)
        {
            this.CurrentPercent = newPercent;

            this.Visible = true;
            this.Redraw();
        }

        public void UpdateProgress(long value, long divisor)
        {
            this.value = value;
            this.divisor = divisor;

            if (divisor == 0)
            {
                this.value = 0;
                UpdateProgress(0);
            }

            UpdateProgress((double)value / (double)divisor);
        }

        public void UpdateProgress(long value, long divisor, int explicitWidth)
        {
            this.SetExplicitWidth(explicitWidth);

            UpdateProgress(value, divisor);
        }

        public void UpdateProgress(long value, long divisor, bool calcWidth)
        {
            if (calcWidth)
            {
                int newWidth = new int[] { value.ToString().Length, divisor.ToString().Length }.Max();

                SetExplicitWidth(newWidth);
            }

            UpdateProgress(value, divisor);
        }

        private int GetBarWidth()
        {
            int barWidth = this.Width;

            // if we are showing a percentage, we need to account for the width of that text
            if (this.Display != ProgressDisplay.NoPercent)
                barWidth -= 5;

            if (this.Mode != ProgressMode.Default)
            {
                int neededChars = 8;

                neededChars += this.value.ToString().PadLeft(this.ExplicitWidth).Length;
                neededChars += this.divisor.ToString().PadLeft(this.ExplicitWidth).Length;

                barWidth -= neededChars;
            }

            return barWidth;
        }
    }
}
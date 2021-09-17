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
using System.Linq;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     Progress bar element is designed for quickly representing a progress
    ///     as a percentage on a line bar
    /// </summary>
    public class ProgressBar : Element
    {
        private int _configuredWidth = 0;

        /// <summary>
        ///     Where to display the progress percent as text
        /// </summary>
        public ProgressDisplay Display { get; private set; }

        /// <summary>
        ///     Additional mode that the progress bar is operating in
        /// </summary>
        public ProgressMode Mode { get; private set; }
        
        /// <summary>
        ///     Current percentage represented by the progress bar. 
        /// 
        ///     Note: value will be between 0 and 1 as a decimal
        /// </summary>
        public double CurrentPercent 
        { 
            get => _currentPercent;
            private set
            {
                if (value < 0 || value > 1)
                    throw new ArgumentOutOfRangeException("Provided percentage must be a decimal value between 0 and 1");

                _currentPercent = value;
            }
        }

        private double _currentPercent = 0.0;

        /// <summary>
        ///     If specified, this will be the fixed width of the progress bar values 
        ///     when operating in "explicit" mode
        /// </summary>
        public int ExplicitWidth { get; private set; }

        /// <summary>
        ///     The numerator to use when operating in explicit mode
        /// </summary>
        public long Numerator { get; private set; }

        /// <summary>
        ///     The divisor to use when operating in explicit mode
        /// </summary>
        /// <value></value>
        public long Divisor { get; private set; }


        /// <summary>
        ///     Obsolete
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="show"></param>
        [Obsolete]
        public ProgressBar(ProgressMode mode, bool show = false)
            : this(width: 0, mode: mode, show: show) 
        {
        }


        /// <summary>
        ///     Constuct a new instance of the progress bar element
        /// </summary>
        /// <param name="width">
        ///     Width to use for the progress bar.
        /// 
        ///           0 = use max width available in the specified area
        ///     below 0 = use max width minus the absolute value provided
        ///     above 0 = set the entire element to a fixed width
        /// </param>
        /// <param name="display">
        ///     An enum that specifies if and where to display the progress as text
        /// </param>
        /// <param name="mode">Additional mode to operate under</param>
        /// <param name="startPercent">Initial percentage to use when first constructing the progress bar</param>
        /// <param name="explicitWidth">Width to pad the explicit values to, if displayed</param>
        /// <param name="area">Area to constrain the progress bar to</param>
        /// <param name="show">If true, immediately show the progress bar upon creation</param>
        public ProgressBar(int width = 0,
                           ProgressDisplay display = ProgressDisplay.Right,
                           ProgressMode mode = ProgressMode.Default,
                           double startPercent = 0.0,
                           int explicitWidth = 0,
                           TerminalArea area = TerminalArea.Default,
                           bool show = false) 
            : base(area, show) 
        {
            this.SetExplicitWidth(explicitWidth);

            this.CurrentPercent = startPercent;
            this.Display = display;
            this.Mode = mode;
            _configuredWidth = width;
            
            this.RecalculateAndRedraw();
        }

        /// <summary>
        ///     Recalculate and redraw the entire element
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.Height = 1;
                this.Width = this._configuredWidth;

                // if the provided width value is negative, then we subtract that number
                // from the max width we are allowed to work with. For example, if -3
                // is specified, than our total width ends up being 3 less than the 
                // max usable TerminalArea width
                if (this.Width < 0)
                    this.Width = this.MaxWidth + this.Width;
                else if (this.Width == 0)
                    this.Width = this.MaxWidth;
                
                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
                this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);
            }

            this.RedrawAll();
        }

        /// <summary>
        ///     Set the fixed width of the explicit count display to the value provided
        /// </summary>
        /// <param name="width">Number of characters to pad explicit values to</param>
        public void SetExplicitWidth(int width)
        {
            if (width < 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Value cannot be less than 0");

            this.ExplicitWidth = width;
        }

        /// <summary>
        ///     Set the progress mode
        /// </summary>
        /// <param name="mode">new mode to use</param>
        public void SetMode(ProgressMode mode)
            => this.Mode = mode;

        /// <summary>
        ///     Redraw the progress bar
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            using (this.TopLeftPoint.GetMove())
            {
                int barWidth = this.GetBarWidth();

                int filled = (int)Math.Round(barWidth * CurrentPercent);
                int unfilled = barWidth - filled;

                string pctString = String.Format("{0,3:N0}%", (CurrentPercent * 100.0));
                string explicitCountString = $"[ {this.Numerator.ToString().PadLeft(this.ExplicitWidth)} / {this.Divisor.ToString().PadLeft(this.ExplicitWidth)} ]";
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
            }
        }

        /// <summary>
        ///     Update the progress bar using the provided percentage.
        /// 
        ///     Note: The percentage must a decimal value between 0 and 1
        /// </summary>
        /// <param name="newPercent">Percentage value to be displayed</param>
        public void UpdateProgress(double newPercent)
        {
            this.CurrentPercent = newPercent;
            this.Visible = true;
            this.Redraw();
        }

        /// <summary>
        ///     Update the progress bar using the provided explicit values and automatically
        ///     calculate the percentage
        /// </summary>
        /// <param name="numerator">Top number of the fraction</param>
        /// <param name="divisor">Bottom number of the fraction</param>
        public void UpdateProgress(long numerator, long divisor)
        {
            this.Numerator = numerator;
            this.Divisor = divisor;

            if (divisor == 0)
            {
                this.Numerator = 0;
                UpdateProgress(0);
            }

            UpdateProgress((double)numerator / (double)divisor);
        }

        /// <summary>
        ///     Update the progress. The provided currentPercent value is used for the progress bar
        ///     and the value and divisor are only updated for display purposes
        /// </summary>
        /// <param name="numerator">The top value of the fraction</param>
        /// <param name="divisor">The bottom value of the fraction</param>
        /// <param name="newPercent">The progress current percentage</param>
        public void UpdateProgress(long numerator, long divisor, double newPercent)
        {
            this.Numerator = numerator;
            this.Divisor = divisor;

            UpdateProgress(newPercent);
        }

        /// <summary>
        ///     Update the progress using the provided explicit values, and optionally
        ///     recalculate the fixed-width of the explicit display
        /// </summary>
        /// <param name="numerator">Top value of the fraction</param>
        /// <param name="divisor">Bottom number of the fraction</param>
        /// <param name="calcWidth">
        ///     If true, set the new fixed width to the length of the divisor
        /// </param>
        public void UpdateProgress(long numerator, long divisor, bool calcWidth)
        {
            if (calcWidth)
            {
                int newWidth = divisor.ToString().Length;

                SetExplicitWidth(newWidth);
            }

            UpdateProgress(numerator, divisor);
        }

        /// <summary>
        ///     Calculate the width of the bar, taking into account any text that is 
        ///     to be displayed on either side of the bar
        /// </summary>
        private int GetBarWidth()
        {
            int barWidth = this.Width;

            // if we are showing a percentage, we need to account for the width of that text
            if (this.Display != ProgressDisplay.NoPercent)
                barWidth -= 5;

            if (this.Mode != ProgressMode.Default)
            {
                int neededChars = 8;

                neededChars += this.Numerator.ToString().PadLeft(this.ExplicitWidth).Length;
                neededChars += this.Divisor.ToString().PadLeft(this.ExplicitWidth).Length;

                barWidth -= neededChars;
            }

            return barWidth;
        }
    }
}
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
            TerminalPoint previousPoint = TerminalPoint.GetCurrent();
            this.TopLeftPoint.MoveTo();

            if (currentPercent > 1)
                currentPercent /= 100.0;

            int filled = (int)Math.Round(this.barWidth * currentPercent);
            int unfilled = this.barWidth - filled;

            string pctString = String.Format("{0,3:N0}%", (currentPercent * 100.0));

            if (display == ProgressDisplay.Left)
                Terminal.Write(String.Format("{0,5}", pctString));

            for (int i = 0; i < this.barWidth; i++)
            {
                if (i < filled)
                    Terminal.WriteColor(TerminalColor.ProgressBarFilled, (char)BlockChars.Solid);
                else
                    Terminal.Write((char)BlockChars.MediumShade);
            }

            if (display == ProgressDisplay.Right)
                Terminal.Write(String.Format("{0,-5}", pctString));

            previousPoint.MoveTo();
        }

        public void UpdateProgress(double newPercent)
        {
            this.currentPercent = newPercent;

            this.Redraw();
        }
    }
}
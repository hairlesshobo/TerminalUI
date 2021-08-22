using System;

namespace TerminalUI.Elements
{
    public class ProgressBar
    {
        private TerminalPoint progressBarPoint;
        private ProgressDisplay display;
        private ProgressOptions options;
        private int width;
        private int barWidth;

        public ProgressBar(
            int width = 0,
            ProgressDisplay display = ProgressDisplay.Right,
            ProgressOptions options = ProgressOptions.None,
            double startPercent = 0.0
            )
        {
            this.progressBarPoint = TerminalPoint.GetCurrent();
            this.width = width;
            this.display = display;
            this.options = options;

            if (this.width == 0)
                this.width = Console.WindowWidth;

            this.barWidth = this.width;

            if (this.display != ProgressDisplay.NoPercent)
                this.barWidth = this.width - 5;

            this.UpdateProgress(startPercent);
        }

        public void UpdateProgress(double currentPercent)
        {
            TerminalPoint previousPoint = TerminalPoint.GetCurrent();
            this.progressBarPoint.MoveTo();

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
                    Terminal.WriteColor(ConsoleColor.Yellow, (char)BlockChars.Solid);
                else
                    Terminal.Write((char)BlockChars.MediumShade);
            }

            if (display == ProgressDisplay.Right)
                Terminal.Write(String.Format("{0,-5}", pctString));

            previousPoint.MoveTo();
        }
    }
}
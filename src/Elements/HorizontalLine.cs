using System;

namespace TerminalUI.Elements
{
    public class HorizontalLine : Element
    {
        private LineType lineType;
        private ConsoleColor color;

        public HorizontalLine(ConsoleColor color = ConsoleColor.White, LineType lineType = LineType.Thin, int width = 0)
        {
            this.lineType = lineType;
            this.color = color;
            this.Width = width;

            if (this.Width <= 0)
                this.Width = Console.WindowWidth;

            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(this.TopLeftPoint.Left + this.Width, this.TopLeftPoint.Top);

            this.Redraw();
        }

        public override void Redraw()
        {
            for (int i = 0; i < this.Width; i++)
                Terminal.WriteColor(color, (char)lineType);
        }
    }
}
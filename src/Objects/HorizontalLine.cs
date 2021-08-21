using System;

namespace TerminalUI.Objects
{
    public class HorizontalLine
    {
        public HorizontalLine(LineType lineType = LineType.Thin, int width = 0)
        {
            if (width <= 0)
                width = Console.WindowWidth;

            for (int i = 0; i < width; i++)
                Terminal.Write((char)lineType);
        }
    }
}
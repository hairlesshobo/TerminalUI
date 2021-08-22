using System;

namespace TerminalUI.Elements
{
    public class HorizontalLine
    {
        public HorizontalLine(ConsoleColor color = ConsoleColor.White, LineType lineType = LineType.Thin, int width = 0)
        {
            if (width <= 0)
                width = Console.WindowWidth;

            for (int i = 0; i < width; i++)
                Terminal.WriteColor(color, (char)lineType);
        }
    }
}
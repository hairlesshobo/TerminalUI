using System;

namespace TerminalUI
{
    public class TerminalPoint
    {
        public int Left { get; private set; }
        public int Top { get; private set; }

        public TerminalPoint(int left, int top)
        {
            Top = top;
            Left = left;
        }

        public static TerminalPoint GetCurrent()
            => new TerminalPoint(Console.CursorLeft, Console.CursorTop);

        public void MoveTo()
        {
            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;
        }
    }
}
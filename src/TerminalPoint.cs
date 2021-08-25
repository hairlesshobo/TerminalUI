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

        public TerminalPoint AddX(int amount)
            => new TerminalPoint(this.Left + amount, this.Top);

        public TerminalPoint AddY(int amount)
            => new TerminalPoint(this.Left, this.Top + amount);

        public TerminalPoint Add(int amountX, int amountY)
            => new TerminalPoint(this.Left + amountX, this.Top + amountY);

        public static TerminalPoint GetCurrent()
            => new TerminalPoint(Console.CursorLeft, Console.CursorTop);

        public void MoveTo()
        {
            Console.CursorLeft = this.Left;
            Console.CursorTop = this.Top;
        }
    }
}
using System;

namespace TerminalUI.Elements
{
    public class SplitLine : Element
    {
        private static string _leftText;
        private static string _rightText;


        public SplitLine(string leftText, string rightText)
        {
            _leftText = leftText;
            _rightText = rightText;

            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(Terminal.Width, this.TopLeftPoint.Top);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            this.Height = 1;
            this.Width = TopRightPoint.Left - TopLeftPoint.Left;

            this.Redraw();
        }

        public override void Redraw()
        {
            Terminal.WriteColor(ConsoleColor.White, _leftText);

            Console.CursorLeft = Terminal.Width - _rightText.Length;
            Terminal.WriteColor(ConsoleColor.Magenta, _rightText);

            this.TopLeftPoint.MoveTo();
        }

        public string UpdateLeft(string text)
        {
            throw new NotImplementedException();
        }

        public string UpdateRight(string text)
        {
            throw new NotImplementedException();
        }
    }
}
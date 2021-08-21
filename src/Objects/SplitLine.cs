using System;

namespace TerminalUI.Objects
{
    public class SplitLine
    {
        private TerminalPoint linePoint;

        public SplitLine(string leftText, string rightText)
        {
            linePoint = TerminalPoint.GetCurrent();

            // Console.CursorLeft = 0;

            Console.Write(leftText);

            Console.CursorLeft = Console.WindowWidth - rightText.Length;
            Console.Write(rightText);

            linePoint.MoveTo();
            
            // Console.CursorLeft = 0;

            // I don't need to drop down a line because writing all the way to the end of the line,
            // it seems to automatically drop down a line for me
            // Console.CursorTop -= 1;
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
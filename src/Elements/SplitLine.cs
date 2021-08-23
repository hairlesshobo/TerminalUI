using System;

namespace TerminalUI.Elements
{
    public class SplitLine : Element
    {
        private string leftText;
        private string rightText;


        public SplitLine(string leftText, string rightText)
        {
            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = new TerminalPoint(Terminal.Width, this.TopLeftPoint.Top);
            this.BottomLeftPoint = null;
            this.BottomRightPoint = null;

            this.Height = 1;
            this.Width = TopRightPoint.Left - TopLeftPoint.Left;

            this.Update(leftText, rightText);
        }

        public override void Redraw()
        {
            Terminal.Write(leftText);

            int splitChars = this.Width - leftText.Length - rightText.Length;

            if (splitChars > 0)
            {
                for (int i = 0; i < splitChars; i++)
                    Terminal.Write(' ');
            }

            Terminal.Write(rightText);

            this.TopLeftPoint.MoveTo();
        }

        public void Update(string left, string right)
        {
            leftText = left;
            rightText = right;

            this.Redraw();
        }

        public void UpdateLeft(string text)
        {
            leftText = text;

            this.Redraw();
        }

        public void UpdateRight(string text)
        {
            rightText = text;

            this.Redraw();
        }
    }
}
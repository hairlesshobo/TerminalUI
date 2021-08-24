using System;

namespace TerminalUI.Elements
{
    public class Header : Element
    {
        public string Left { get; private set; }
        public string Right { get; private set; }

        private SplitLine splText = null;
        private HorizontalLine hl = null;

        public Header(string left, string right)
        {
            this.Height = 1;
            this.Width = Terminal.Width;
            
            this.TopLeftPoint = new TerminalPoint(0, 0);
            this.TopRightPoint = new TerminalPoint(Terminal.Width, 0);
            this.BottomLeftPoint = new TerminalPoint(0, 1);
            this.BottomRightPoint = new TerminalPoint(Terminal.Width, 1);

            this.UpdateHeader(left, right);
        }

        public void UpdateLeft(string left)
        {
            this.Left = left;

            this.Redraw();
        }

        public void UpdateRight(string right)
        {
            this.Right = right;

            this.Redraw();
        }

        public void UpdateHeader(string left, string right)
        {
            this.Left = left;
            this.Right = right;

            this.Redraw();
        }

        public override void Redraw()
        {
            TerminalPoint prevPoint = TerminalPoint.GetCurrent();

            this.TopLeftPoint.MoveTo();
            Terminal.BackgroundColor = TerminalColor.HeaderBackground;

            if (splText == null)
                splText = new SplitLine(this.Left, this.Right);
            else
                splText.Update(this.Left, this.Right);

            this.BottomLeftPoint.MoveTo();

            if (hl == null)
                hl = new HorizontalLine(TerminalColor.DefaultForeground, LineType.Thin);
            else
                hl.Redraw();

            Terminal.ResetBackground();
            prevPoint.MoveTo();
        }
    }
}
using System;

namespace TerminalUI.Elements
{
    public abstract class Element
    {
        public int Width { get; private protected set; }
        public int Height { get; private protected set; }
        
        public TerminalPoint TopLeftPoint { get; private protected set; }
        public TerminalPoint TopRightPoint { get; private protected set; }
        public TerminalPoint BottomLeftPoint { get; private protected set; } = null;
        public TerminalPoint BottomRightPoint { get; private protected set; } = null;

        public abstract void Redraw();

        public void Erase()
        {
            throw new NotImplementedException();
        }
    }
}
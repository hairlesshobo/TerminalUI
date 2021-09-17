using System;

namespace TerminalUI
{
    public class TerminalPointMove : IDisposable
    {
        public TerminalPoint PreviousPoint { get; private set; } 
        public TerminalPoint DestinationPoint { get; private set; }

        internal TerminalPointMove(TerminalPoint destPoint)
        {
            this.DestinationPoint = destPoint;
            this.PreviousPoint = this.DestinationPoint.MoveToWithCurrent();
        }

        public void Dispose()
            => this.PreviousPoint.MoveTo();
    }
}
using System;

namespace TerminalUI
{
    /// <summary>
    ///     TerminalPoint helper class that allows to wrap a TerminalPoint
    ///     move action into a using() { } block so that, upon exiting the 
    ///     block, the cursor is automatically returned to the previous location
    /// </summary>
    public class TerminalPointMove : IDisposable
    {
        /// <summary>
        ///     Previous point where the cursor was location prior to the move
        /// </summary>
        public TerminalPoint PreviousPoint { get; private set; } 

        /// <summary>
        ///     Point where the cursor is temporarily being moved to
        /// </summary>
        public TerminalPoint DestinationPoint { get; private set; }

        /// <summary>
        ///     Constuct a new temporary movement
        /// </summary>
        /// <param name="destPoint">The point we are to temporarily move to</param>
        internal TerminalPointMove(TerminalPoint destPoint)
        {
            this.DestinationPoint = destPoint;
            this.PreviousPoint = this.DestinationPoint.MoveToWithCurrent();
        }

        /// <summary>
        ///     Dispose of the object and move back to the previous cursor point
        /// </summary>
        public void Dispose()
            => this.PreviousPoint.MoveTo();
    }
}
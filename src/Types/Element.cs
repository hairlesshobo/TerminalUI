/**
 *  TerminalUI - Simple terminal widgets for C#
 * 
 *  Copyright (c) 2021 Steve Cross <flip@foxhollow.cc>
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU Lesser General Public License as published by
 *  the Free Software Foundation; either version 2.1 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Text;

namespace TerminalUI.Types
{
    /// <summary>
    ///     Abstract class that defines all terminal elements
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        ///     Width of the element
        /// </summary>
        public int Width { get; private protected set; }

        /// <summary>
        ///     Height of the element
        /// </summary>
        public int Height { get; private protected set; }

        /// <summary>
        ///     Maximum width that the element may used, optionally constrained by the
        ///     specified TerminalArea
        /// </summary>
        public int MaxWidth { get; private set; }

        /// <summary>
        ///     Flag indicating whether the element is currently visible
        /// </summary>
        public bool Visible { get; private protected set; } = false;
        
        /// <summary>
        ///     TerminalPoint that represents the top-left-most point of the current element
        /// </summary>
        public TerminalPoint TopLeftPoint { get; private protected set; }

        /// <summary>
        ///     TerminalPoint that represents the top-right-most point of the current element
        /// </summary>
        public TerminalPoint TopRightPoint { get; private protected set; }

        /// <summary>
        ///     If the element spans multiple lines, this will be a TerminalPoint that represents 
        ///     the bottom-left-most point of the current element. If the element is a single line
        ///     element, this value will be null
        /// </summary>
        public TerminalPoint BottomLeftPoint { get; private protected set; } = null;

        /// <summary>
        ///     If the element spans multiple lines, this will be a TerminalPoint that represents 
        ///     the bottom-right-most point of the current element. If the element is a single line
        ///     element, this value will be null
        /// </summary>
        public TerminalPoint BottomRightPoint { get; private protected set; } = null;

        /// <summary>
        ///     TerminalArea that this element is constrainted to
        /// </summary>
        /// <value></value>
        public TerminalArea Area { get; private protected set; } = TerminalArea.Default;

        /// <summary>
        ///     This represents the point the cursor was at when the element was first initialized
        /// </summary>
        protected TerminalPoint OriginalPoint { get; private protected set; }

        /// <summary>
        ///     Default constructor used by all elements
        /// </summary>
        protected Element(bool show = false)
        {
            this.Visible = show;
            this.OriginalPoint = TerminalPoint.GetCurrent();

            this.CalculateLayout();

            Terminal.RegisterElement(this);
        }

        /// <summary>
        ///     Constructor used by all elements when specifing a TerminalArea
        /// </summary>
        protected Element(TerminalArea area, bool show = false)
            : this(show)
        {
            this.Area = area;
        }

        /// <summary>
        ///     Calculate the layout bounds based on the provided TerminalArea
        /// </summary>
        protected void CalculateLayout()
        {
            if (this.Area == TerminalArea.LeftHalf || this.Area == TerminalArea.RightHalf)
                this.MaxWidth = Terminal.UsableWidth / 2;
            else if (this.Area == TerminalArea.Default)
                this.MaxWidth = Terminal.UsableWidth;
        }

        /// <summary>
        ///     Recalculates the layout and then re-draws the entire thing.
        ///     This is really only needed after a terminal resize event
        /// </summary>
        // internal abstract void RecalculateAndRedraw();
        internal virtual void RecalculateAndRedraw() => this.RedrawAll();

        /// <summary>
        ///     Redraw the entire element. For most elements, this is the same as calling
        ///     Redraw(), but some elements have "static" content that would only need
        ///     to be redrawn on certain occasions
        /// </summary>
        public virtual void RedrawAll()
            => Redraw();

        /// <summary>
        ///     Redraw the "dynamic" portion of the element
        /// </summary>
        public abstract void Redraw();

        /// <summary>
        ///     Erase the element from the terminal. 
        /// </summary>
        public virtual void Erase()
        {
            if (this.TopLeftPoint == null)
                throw new ArgumentNullException(nameof(this.TopLeftPoint));

            if (this.TopRightPoint == null)
                throw new ArgumentNullException(nameof(this.TopRightPoint));

            TerminalPoint prevPoint = TerminalPoint.GetCurrent();

            int width = this.TopRightPoint.Left - this.TopLeftPoint.Left;
            int lines = (this.BottomLeftPoint == null ? 1 : this.BottomLeftPoint.Top - this.TopLeftPoint.Top);
            
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < width; i++)
                sb.Append(' ');

            string wideBlank = sb.ToString();

            for (int i = 0; i < lines; i++)
            {
                this.TopLeftPoint.AddY(i).MoveTo();
                Terminal.Write(wideBlank);
            }

            prevPoint.MoveTo();
        }

        /// <summary>
        ///     Erase the element and mark it as not visible
        /// </summary>
        public virtual void Hide()
        {
            if (this.Visible == false)
                return;

            this.Visible = false;
            this.Erase();
        }

        /// <summary>
        ///     Mark the element as visible and immediately draw it on the terminal
        /// </summary>
        public virtual void Show()
        {
            if (this.Visible == true)
                return;

            this.Visible = true;
            this.RedrawAll();
        }
    }
}
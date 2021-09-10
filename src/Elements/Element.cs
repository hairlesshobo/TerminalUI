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

namespace TerminalUI.Elements
{
    public abstract class Element
    {
        public int Width { get; private protected set; }
        public int Height { get; private protected set; }
        public bool Visible { get; private protected set; } = false;
        
        public TerminalPoint TopLeftPoint { get; private protected set; }
        public TerminalPoint TopRightPoint { get; private protected set; }
        public TerminalPoint BottomLeftPoint { get; private protected set; } = null;
        public TerminalPoint BottomRightPoint { get; private protected set; } = null;

        public virtual void RedrawAll()
            => Redraw();

        public abstract void Redraw();

        public void Erase()
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

        public void Hide()
        {
            if (this.Visible == false)
                return;

            this.Visible = false;
            this.Erase();
        }

        public void Show()
        {
            if (this.Visible == true)
                return;

            this.Visible = true;
            this.RedrawAll();
        }
    }
}
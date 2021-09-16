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
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    public class NotificationBox : Element
    {
        private string[] text;
        public ConsoleColor? BorderColor { get; set; } = null;
        private Nullable<ConsoleColor>[] textColor;
        private TextJustify[] textJustify;
        private TerminalPoint[] textLines;
        public int TextLines => this.Height - 2;

        public NotificationBox(int height = 5, int width = 0)
        {
            this.Height = height;
            this.Width = width;

            textLines = new TerminalPoint[this.TextLines];
            text = new string[this.TextLines];
            textColor = new Nullable<ConsoleColor>[this.TextLines];
            textJustify = new TextJustify[this.TextLines];

            for (int i = 0; i < this.TextLines; i++)
            {
                text[i] = String.Empty;
                textJustify[i] = TextJustify.Left;
            }

            if (this.Width == 0)
                this.Width = (int)Math.Round((double)Terminal.Width * 0.80, 0);

            int outsideChars = (Terminal.Width - this.Width)/2;

            this.TopLeftPoint = TerminalPoint.GetCurrent().AddX(outsideChars);
            this.TopRightPoint = this.TopLeftPoint.AddX(this.Width);
            this.BottomLeftPoint = this.TopLeftPoint.AddY(this.Height);
            this.BottomRightPoint = this.BottomLeftPoint.AddX(this.Width);

            int verticalLines = this.Height - 2;
            TerminalPoint rootTp = this.TopLeftPoint.AddX(1);

            for (int i = 0; i < verticalLines; i++)
                textLines[i] = rootTp.AddY(i+1);
            
            if (this.Width < 3 || this.Height < 3)
                throw new InvalidOperationException();
        }

        public void SetLineColor(int index, ConsoleColor? color = null)
        {
            if (index > this.text.Length)
                throw new IndexOutOfRangeException();

            textColor[index] = color;
        }

        public void SetTextJustify(int index, TextJustify justify)
        {
            if (index > this.text.Length)
                throw new IndexOutOfRangeException();

            textJustify[index] = justify;
        }

        public void UpdateText(int index, string newText)
        {
            if (index > this.text.Length)
                throw new IndexOutOfRangeException();

            if (newText.Length > this.Width - 2)
                newText = newText.Substring(0, this.Width - 2);

            this.text[index] = newText;

            this.DrawLine(index);
        }

        public override void RedrawAll()
        {
            if (this.Visible)
            {
                TerminalPoint prevPoint = TerminalPoint.GetCurrent();

                TerminalPoint currentPoint = this.TopLeftPoint;
                currentPoint.MoveTo();

                if (this.BorderColor.HasValue)
                    Terminal.ForegroundColor = this.BorderColor.Value;

                Terminal.Write((char)BoxChars.ThinTopLeft);

                for (int curWidth = 1; curWidth < (this.Width-1); curWidth++)
                    Terminal.Write((char)BoxChars.ThinHorizontal);

                Terminal.Write((char)BoxChars.ThinTopRight);

                if (this.BorderColor.HasValue)
                    Terminal.ResetForeground();


                int verticalLines = this.Height - 2;

                for (int i = 0; i < verticalLines; i++)
                {
                    currentPoint = currentPoint.AddY(1);
                    currentPoint.MoveTo();
                    
                    if (this.BorderColor.HasValue)
                        Terminal.ForegroundColor = this.BorderColor.Value;

                    Terminal.Write((char)BoxChars.ThinVertical);

                    currentPoint.AddX(this.Width-1).MoveTo();
                    
                    Terminal.Write((char)BoxChars.ThinVertical);

                    if (this.BorderColor.HasValue)
                        Terminal.ResetForeground();
                }

                currentPoint = currentPoint.AddY(1);
                currentPoint.MoveTo();

                if (this.BorderColor.HasValue)
                    Terminal.ForegroundColor = this.BorderColor.Value;

                Terminal.Write((char)BoxChars.ThinBottomLeft);

                for (int curWidth = 1; curWidth < (this.Width-1); curWidth++)
                    Terminal.Write((char)BoxChars.ThinHorizontal);

                Terminal.Write((char)BoxChars.ThinBottomRight);

                if (this.BorderColor.HasValue)
                    Terminal.ResetForeground();

                prevPoint.MoveTo();
            }

            this.Redraw();
        }

        public override void Redraw()
        {
            if (this.Visible)
            {
                for (int i = 0; i < this.TextLines; i++)
                    DrawLine(i);
            }
        }

        private void DrawLine(int index)
        {
            if (index > this.text.Length)
                throw new IndexOutOfRangeException();

            if (this.Visible)
            {
                textLines[index].MoveTo();
                
                if (this.textColor[index].HasValue)
                    Terminal.ForegroundColor = this.textColor[index].Value;

                Terminal.Write(GetJustifiedText(index));

                if (this.textColor[index].HasValue)
                    Terminal.ResetForeground();
            }
        }

        private string GetJustifiedText(int index)
        {
            int width = this.Width-2;

            if (textJustify[index] == TextJustify.Left)
                return text[index];

            if (textJustify[index] == TextJustify.Right)
                return text[index].PadLeft(width);

            return text[index].PadLeft((int)Math.Floor((double)width / 2.0) + (int)Math.Ceiling((double)text[index].Length / 2.0));
        }
    }
}
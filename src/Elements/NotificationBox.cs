/*
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
    /// <summary>
    ///     A notification box is meant to be displayed above the rest of the 
    ///     elements for a notification or, at some point in the future, maybe
    ///     used to query the user for information of some sort
    /// </summary>
    public class NotificationBox : Element
    {
        private NotificationBoxLine[] _lines;

        /// <summary>
        ///     Default background to use for text lines when no other color is specified
        /// </summary>
        public Nullable<ConsoleColor> DefaultBackgroundColor { get; private set; } = null;

        /// <summary>
        ///     Foreground color to use when drawing the border
        /// </summary>
        public Nullable<ConsoleColor> BorderForegroundColor 
        { 
            get => _borderForegroundColor ?? TerminalColor.DefaultForeground;
            private set => _borderForegroundColor = value;
        }
        private Nullable<ConsoleColor> _borderForegroundColor = null;

        /// <summary>
        ///     Background color to use when drawing the border
        /// </summary>
        public Nullable<ConsoleColor> BorderBackgroundColor 
        { 
            get => _borderBackgroundColor ?? TerminalColor.DefaultBackground;
            private set => _borderBackgroundColor = value;
        }
        private Nullable<ConsoleColor> _borderBackgroundColor = null;

        /// <summary>
        ///     Value indicates the number of text lines available in this notification box
        /// </summary>
        public int TextLineCount { get; private set; }

        private int _configuredHeight = 0;
        private int _configuredWidth = 0;

        /// <summary>
        ///     Construct a new notificatiom box element
        /// </summary>
        /// <param name="height">
        ///     Height of the new box. 
        /// 
        ///     Note: 2 lines will be used for the border
        /// </param>
        /// <param name="width">
        ///     Width of the new box.
        /// 
        ///     Noite: 2  of the columns will be used for the border
        /// </param>
        /// <param name="borderForegroundColor">Foreground color to use when drawing the border of the box</param>
        /// <param name="borderBackgroundColor">Background color to use when drawing the border of the box</param>
        /// <param name="defaultBackgroundColor">Background color to use by default when drawing the background of any line</param>
        /// <param name="area">Area to constrain the notification box to</param>
        /// <param name="show">If true, the element will be shown immediately upon creation</param>
        public NotificationBox(int height = 5, 
                               int width = 0,
                               Nullable<ConsoleColor> borderForegroundColor = null,
                               Nullable<ConsoleColor> borderBackgroundColor = null,
                               Nullable<ConsoleColor> defaultBackgroundColor = null,
                               TerminalArea area = TerminalArea.Default,
                               bool show = false)
            : base(area, show)
        {
            this.BorderForegroundColor = borderForegroundColor;
            this.BorderBackgroundColor = borderBackgroundColor;
            this.DefaultBackgroundColor = defaultBackgroundColor;

            _configuredHeight = height;
            _configuredWidth = width;

            this.RecalculateAndRedraw();
        }

        /// <summary>
        ///     Recalculate the layout and position, then redraw the entire element
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.Height = _configuredHeight;
                this.TextLineCount = this.Height - 2;

                this.Width = _configuredWidth;

                if (this.Width == 0)
                    this.Width = (int)Math.Round((double)this.MaxWidth * 0.80, 0);

                _lines = new NotificationBoxLine[this.TextLineCount];

                for (int i = 0; i < this.TextLineCount; i++)
                    _lines[i] = new NotificationBoxLine() { BackgroundColor = this.DefaultBackgroundColor };

                int outsideChars = (this.MaxWidth - this.Width)/2;

                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area).AddX(outsideChars);
                this.TopRightPoint = this.TopLeftPoint.AddX(this.Width);
                this.BottomLeftPoint = this.TopLeftPoint.AddY(this.Height);
                this.BottomRightPoint = this.BottomLeftPoint.AddX(this.Width);

                int verticalLines = this.Height - 2;
                TerminalPoint rootTp = this.TopLeftPoint.AddX(1);

                for (int i = 0; i < verticalLines; i++)
                    _lines[i].RootPoint = rootTp.AddY(i+1);
                
                if (this.Width < 3 || this.Height < 3)
                    throw new InvalidOperationException();
            }

            this.RedrawAll();
        }

        /// <summary>
        ///     Set the specified line to the provided color
        /// </summary>
        /// <param name="index">Line index to change</param>
        /// <param name="color">New color</param>
        public void SetLineColor(int index, ConsoleColor? color = null)
        {
            if (index > _lines.Length)
                throw new IndexOutOfRangeException();

            _lines[index].ForegroundColor = color;
        }

        /// <summary>
        ///     Set the specified line to the provided justification
        /// </summary>
        /// <param name="index">Line index to change</param>
        /// <param name="justify">New text justification to use</param>
        public void SetTextJustify(int index, TextJustify justify)
        {
            if (index > _lines.Length)
                throw new IndexOutOfRangeException();

            _lines[index].Justify = justify;
        }

        /// <summary>
        ///     Change the foreground and background color of the box border
        /// </summary>
        /// <param name="foreground">New foreground color</param>
        /// <param name="background">New background color</param>
        public void SetBorderColors(Nullable<ConsoleColor> foreground, Nullable<ConsoleColor> background)
        {
            this.BorderForegroundColor = foreground;
            this.BorderBackgroundColor = background;

            this.DrawBox();
        }

        /// <summary>
        ///     Update the text at the specified line index
        /// </summary>
        /// <param name="index">Index to change</param>
        /// <param name="newText">New text to display</param>
        public void UpdateText(int index, string newText)
        {
            if (index > _lines.Length)
                throw new IndexOutOfRangeException();

            if (newText.Length > this.Width - 2)
                newText = newText.Substring(0, this.Width - 2);

            _lines[index].Text = newText;

            this.DrawLine(index);
        }

        /// <summary>
        ///     Update one or more aspect of a single line. All values are only updated 
        ///     if the value provided is not null
        /// </summary>
        /// <param name="index">Index to change</param>
        /// <param name="text">New text to display, if not null</param>
        /// <param name="justify">New justify to set</param>
        /// <param name="foregroundColor">New foreground color to set</param>
        /// <param name="backgroundColor">New background color to set</param>
        public void UpdateLine(int index,
                               string text = null, 
                               Nullable<TextJustify> justify = null,
                               Nullable<ConsoleColor> foregroundColor = null,
                               Nullable<ConsoleColor> backgroundColor = null)
        {
            if (index > _lines.Length)
                throw new IndexOutOfRangeException();

            NotificationBoxLine line = _lines[index];

            if (text != null)
                line.Text = text;

            if (justify.HasValue)
                line.Justify = justify.Value;

            if (foregroundColor != null)
                line.ForegroundColor = foregroundColor.Value;

            if (backgroundColor != null)
                line.BackgroundColor = backgroundColor.Value;

            this.DrawLine(index);
        }

        /// <summary>
        ///     Redraw the entire element.. that is, redraw the border and the text lines
        /// </summary>
        public override void RedrawAll()
        {
            if (!this.Visible)
                return;

            this.DrawBox();

            this.Redraw();
        }

        /// <summary>
        ///     Redraw the text lines
        /// </summary>
        public override void Redraw()
        {
            if (!this.Visible)
                return;

            for (int i = 0; i < this.TextLineCount; i++)
                DrawLine(i);
        }

        /// <summary>
        ///     Hide the notification box from the terminal
        /// </summary>
        public override void Hide()
        {
            if (!this.Visible)
                return;

            this.Visible = false;

            this.Erase();

            Terminal.RedrawAllElements();
        }

        /// <summary>
        ///     Draw the box border
        /// </summary>
        private void DrawBox()
        {
            if (!this.Visible)
                return;
                 
            TerminalPoint currentPoint = this.TopLeftPoint.Clone();

            using (currentPoint.GetMove())
            {
                // If the border color isn't terminal default foreground.. we set the color
                if (this.BorderForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.ForegroundColor = this.BorderForegroundColor.Value;

                if (this.BorderBackgroundColor != TerminalColor.DefaultBackground)
                    Terminal.BackgroundColor = this.BorderBackgroundColor.Value;

                // draw the top line
                Terminal.Write((char)BoxChars.ThinTopLeft);

                for (int curWidth = 1; curWidth < (this.Width-1); curWidth++)
                    Terminal.Write((char)BoxChars.ThinHorizontal);

                Terminal.Write((char)BoxChars.ThinTopRight);

                // draw the sides

                int verticalLines = this.Height - 2;

                for (int i = 0; i < verticalLines; i++)
                {
                    currentPoint = currentPoint.AddY(1);
                    currentPoint.MoveTo();
                    
                    Terminal.Write((char)BoxChars.ThinVertical);

                    currentPoint.AddX(this.Width-1).MoveTo();
                    
                    Terminal.Write((char)BoxChars.ThinVertical);
                }

                currentPoint = currentPoint.AddY(1);
                currentPoint.MoveTo();

                Terminal.Write((char)BoxChars.ThinBottomLeft);

                for (int curWidth = 1; curWidth < (this.Width-1); curWidth++)
                    Terminal.Write((char)BoxChars.ThinHorizontal);

                Terminal.Write((char)BoxChars.ThinBottomRight);

                if (this.BorderForegroundColor != TerminalColor.DefaultForeground)
                    Terminal.ResetForeground();

                if (this.BorderBackgroundColor != TerminalColor.DefaultBackground)
                    Terminal.ResetBackground();
            }
        }

        /// <summary>
        ///     Draw a single line
        /// </summary>
        /// <param name="index">Index to draw</param>
        private void DrawLine(int index)
        {
            if (index > _lines.Length)
                throw new IndexOutOfRangeException();

            if (!this.Visible)
                return;
                
            using (_lines[index].RootPoint.GetMove())
            {
                if (_lines[index].BackgroundColor != TerminalColor.DefaultBackground)
                    Terminal.BackgroundColor = _lines[index].BackgroundColor.Value;

                // TODO: make sure this works and is fast
                Terminal.WriteColor(_lines[index].ForegroundColor, GetJustifiedText(index));

                // if (_lines[index].ForegroundColor != TerminalColor.DefaultForeground)
                //     Terminal.ForegroundColor = this._lines[index].ForegroundColor.Value;

                // Terminal.Write(GetJustifiedText(index));

                // if (_lines[index].ForegroundColor != TerminalColor.DefaultForeground)
                //     Terminal.ResetForeground();

                if (_lines[index].BackgroundColor != TerminalColor.DefaultBackground)
                    Terminal.ResetBackground();
            }
        }

        /// <summary>
        ///     Get the text as a justified string
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Justified text</returns>
        private string GetJustifiedText(int index)
        {
            int width = this.Width-2;

            if (_lines[index].Justify == TextJustify.Left)
                return _lines[index].Text.PadRight(width);

            if (_lines[index].Justify == TextJustify.Right)
                return _lines[index].Text.PadLeft(width);

            int leftPad = (int)Math.Floor((double)width / 2.0) + (int)Math.Ceiling((double)_lines[index].Text.Length / 2.0);
            int rightPad = width - leftPad;
            return _lines[index].Text.PadLeft(leftPad) + string.Empty.PadRight(rightPad);
        }
    }
}
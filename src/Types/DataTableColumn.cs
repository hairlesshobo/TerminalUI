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

namespace TerminalUI.Types
{
    public class DataTableColumn
    {
        private string label = null;
        private int width = 0;

        public string Name { get; set; } = null;
        public string Label 
        { 
            get => (label == null ? (this.Name == null ? String.Empty : this.Name) : label);
            set => label = value;
        }
        public string LabelFormatted {
            get
            {
                if (width < 0)
                    return this.Label.PadLeft(width * -1);
                else if (width > 0)
                    return this.Label.PadRight(width);

                return this.Label;
            }
        }

        public int Width { 
            get => (width == 0 ? this.Label.Length : width);
            set => width = value;
        }
        public bool AllowEdit { get; set; }
        public ConsoleColor ForegroundColor { get; set; } = Terminal.ForegroundColor;
        public ConsoleColor BackgroundColor { get; set; } = Terminal.BackgroundColor;
        public Func<object, string> Format = null;

        public DataTableColumn()
        { }

        public DataTableColumn(string name, string label, int width = 0)
        {
            this.Name = name;
            this.Label = label;
            this.width = width;
        }

        public DataTableColumn(string label, int width = 0)
        {
            this.Label = label;
            this.width = width;
        }
    }
}
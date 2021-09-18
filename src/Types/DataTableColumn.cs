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

namespace TerminalUI.Types
{
    /// <summary>
    ///     Describes a column that is to be displayed on a DataTable
    /// </summary>
    public class DataTableColumn
    {
        /// <summary>
        ///     Name of the column. This needs to be the name of the property in the list
        ///     that is being passed. It is best to use `nameof()`
        /// </summary>
        public string Name { get; set; } = null;

        /// <summary>
        ///     Label to show in the header, if enabled
        /// </summary>
        public string Label 
        { 
            get => (_label == null ? (this.Name == null ? String.Empty : this.Name) : _label);
            set => _label = value;
        }
        private string _label = null;


        /// <summary>
        ///     Label that has been formatted with any alignment and padding
        /// </summary>
        public string LabelFormatted 
        {
            get
            {
                if (width < 0)
                    return this.Label.PadLeft(width * -1);
                else if (width > 0)
                    return this.Label.PadRight(width);

                return this.Label;
            }
        }

        /// <summary>
        ///     Width of the column
        /// </summary>
        public int Width { 
            get => (width == 0 ? this.Label.Length : width);
            set => width = value;
        }
        private int width = 0;

        /// <summary>
        ///     If true, this will allow values in this column to be edited
        ///     !! NOT IMPLEMENTED YET !!
        /// </summary>
        public bool AllowEdit { get; private set; }

        /// <summary>
        ///     Foreground color to use when drawing this column
        /// </summary>
        public ConsoleColor ForegroundColor { get; set; } = Terminal.ForegroundColor;

        /// <summary>
        ///     Background color to use when drawing this column
        /// </summary>
        /// <value></value>
        public ConsoleColor BackgroundColor { get; set; } = Terminal.BackgroundColor;

        /// <summary>
        ///     Callback function that allows for custom formatting of the cell contents
        /// </summary>
        public Func<object, string> Format = null;

        /// <summary>
        ///     Construct a new DataTableColumn object
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="label">Optional label to use in the header, if enabled</param>
        /// <param name="width">Width of the column</param>
        public DataTableColumn(string name, string label = null, int width = 0)
        {
            this.Name = name;
            this.Label = label;
            this.width = width;
        }
    }
}
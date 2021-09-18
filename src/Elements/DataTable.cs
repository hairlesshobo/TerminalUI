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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    /// <summary>
    ///     The data table element is meant for displaying data in a table format
    /// 
    ///     At some point in the future, it will also be able to be used for editing 
    ///     data in a table, but we aren't there yet
    /// </summary>
    public class DataTable : Element
    {
        // TODO: Add ability to define columns and format as a table view
        // TODO: Add ability to edit entries in table mode 
        // TODO: Add a new "theme" that allows you to select between a header with and without a line
        // TODO: add ability to specify colors and such for header
        // TODO: dynamically calculate column width based on provided data (maybe need to defer drawing until all cells are calculated)
        #region Public Properties
        /// <summary>
        ///     Columns that are to be visible in the table
        /// </summary>
        public List<DataTableColumn> Columns { get; private set; } = null;

        /// <summary>
        ///     Data store to populate the table with
        /// </summary>
        public IList DataStore { get; private set; } = null;

        /// <summary>
        ///     Row selection mode
        /// </summary>
        public DataTableSelectType SelectType { get; private set; }

        /// <summary>
        ///     Maximum number of lines
        /// </summary>
        public int MaxLines { get; private set; }

        /// <summary>
        ///     TerminalPoint that describes the header
        /// </summary>
        public TerminalPoint HeaderPoint { get; private set; } = null;

        /// <summary>
        ///     Terminal point that describes where the data rows start
        /// </summary>
        public TerminalPoint DataPoint { get; private set; } = null;

        /// <summary>
        ///     Flag indicating whether to show the header
        /// </summary>
        public bool ShowHeader 
        { 
            get => _showHeader;
            set 
            {
                _showHeader = value;
                this.RecalculateAndRedraw();
            }
        }
        private bool _showHeader = true;


        #endregion Public Properties

        #region Private Fields
        private HorizontalLine _headerLine = null;
        private int _configuredRows = 0;

        // private Dictionary<int, bool> _selectedIndexes = new Dictionary<int, bool>();
        // private bool _canceled = false;
        // private List<object> _choosenItems = null;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        ///     Constuct a new instance of the data table element
        /// </summary>
        /// <param name="dataStore">Data store to pull from</param>
        /// <param name="columns">Column definitions used to generate the table</param>
        /// <param name="selectType">Row selection type to use</param>
        /// <param name="showHeader">Flag indicating whether to display a header</param>
        /// <param name="rows">
        ///     Specifies how many terminal rows should be consumed by the data table
        /// 
        ///          0 = all remaining rows of the area
        ///    above 0 = fixed number
        ///    below 0 = reminaing rows - absolute value
        /// </param>
        /// <param name="area">Are to constrain the element to</param>
        /// <param name="show">if true, immediately show the element upon creation</param>
        public DataTable(IList dataStore,
                         List<DataTableColumn> columns,
                         DataTableSelectType selectType = DataTableSelectType.None, 
                         bool showHeader = true,
                         int rows = 0,
                         TerminalArea area = TerminalArea.Default,
                         bool show = false)
            : base(area, show)
        {
            this.DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
            this.Columns = columns ?? throw new ArgumentNullException(nameof(columns));
            this.SelectType = selectType;
            this.ShowHeader = showHeader;

            _configuredRows = rows;
        }
        #endregion Constructors

        /// <summary>
        ///     Recalculate and redraw the entire element
        /// </summary>
        internal override void RecalculateAndRedraw()
        {
            base.CalculateLayout();

            using (this.OriginalPoint.GetMove())
            {
                this.TopLeftPoint = TerminalPoint.GetLeftPoint(this.Area);
                this.TopRightPoint = TerminalPoint.GetRightPoint(this.Area);
                
                TerminalPoint bottomPoint = TerminalPoint.GetBottomPoint(this.Area);
                int maxRows = bottomPoint.Top - this.TopLeftPoint.Top;

                this.Height = _configuredRows;

                if (this.Height <= 0)
                {
                    this.Height = maxRows;

                    if (_configuredRows < 0)
                        this.Height += _configuredRows;
                }
                
                if (this.Height > maxRows)
                    this.Height = maxRows;

                this.BottomLeftPoint = this.TopLeftPoint.AddY(this.Height);
                this.BottomRightPoint = this.TopRightPoint.AddY(this.Height);

                this.HeaderPoint = (this.ShowHeader ? this.TopLeftPoint : null);
                this.DataPoint = (this.ShowHeader ? this.TopLeftPoint.AddY(2) : this.TopLeftPoint);

                this.MaxLines = this.Height; // this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 3;
                this.Width = this.TopRightPoint.Left - this.TopLeftPoint.Left;
            }

            this.RedrawAll();
        }

        #region Public Methods

        /// <summary>
        ///     Not implemented yet
        /// </summary>
        /// <param name="clearScreen"></param>
        /// <returns></returns>
        public Task<List<object>> ShowAsync(bool clearScreen = true)
        {
            throw new NotImplementedException();

            // TODO: this does nothing right now....
            // _choosenItems = null;

            // this.MaxLines = this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 1;

            // this.Redraw();

            // // this.SetupStatusBar();

            // return await Task.Run(async () => 
            // {
            //     // delay until the user does something
            //     while (_choosenItems == null && _canceled == false)
            //         await Task.Delay(10);

            //     if (_canceled)
            //         return null;

            //     return _choosenItems;
            // });
        }

        /// <summary>
        ///     Redraw the entire data table
        /// </summary>
        public override void RedrawAll()
        {
            if (!this.Visible)
                return;

            if (this.ShowHeader == true)
                this.DrawHeader();

            this.Redraw();
        }

        
        /// <summary>
        ///     Redraw the data rows
        /// </summary>
        public override void Redraw()
        {
            this.Clear();

            this.DrawRows();
        }

        /// <summary>
        ///     Not implemented yet
        /// </summary>
        public void AbortMenu()
            => throw new NotImplementedException();
            // => _canceled = true;

        #endregion

        #region Private Methods
        /// <summary>
        ///     Draw the header
        /// </summary>
        private void DrawHeader()
        {            
            if (!this.Visible | !this.ShowHeader)
                return;

            using (this.HeaderPoint.GetMove())
            {
                int remainingChars = this.Width;

                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataTableColumn column = this.Columns[i];
                    
                    Terminal.Write(column.LabelFormatted);

                    remainingChars -= column.Label.Length;

                    if (i < (this.Columns.Count-1))
                        Terminal.Write("   ");
                }
                
                if (_headerLine == null)
                {
                    Terminal.NextLine();
                    _headerLine = new HorizontalLine(ConsoleColor.White, LineType.ThinTripleDash, this.Width, area: this.Area, show: true);
                }
            }
        }

        /// <summary>
        ///     Draw all rows of the table
        /// </summary>
        private void DrawRows()
        {
            if (!this.Visible)
                return;


            using (this.DataPoint.GetMove())
            {
                for (int i = 0; i < this.DataStore.Count; i++)
                {
                    this.DataPoint.AddY(i).MoveTo();
                    object rowObj = this.DataStore[i];

                    foreach (DataTableColumn column in this.Columns)
                    {
                        string columnValue = String.Empty;
                        
                        if (column.Name != null)
                            columnValue = GetValue(rowObj.GetType(), rowObj, column);
                        else if (column.Format != null)
                            columnValue = column.Format(rowObj);

                        if (columnValue.Length > column.Width)
                            columnValue = columnValue.Substring(0, column.Width);
                        else
                            columnValue = columnValue.PadRight(column.Width);

                        Console.Write(columnValue);
                        Console.Write("   ");
                    }

                    if (i >= (this.MaxLines))
                        break;
                }
            }
        }

        /// <summary>
        ///     Get a value for a single cell of the table
        /// </summary>
        /// <param name="dataType">Data type being fetched</param>
        /// <param name="inObj">object to read the value from</param>
        /// <param name="column">Column definition to use</param>
        /// <returns>found value as a string</returns>
        private string GetValue(Type dataType, object inObj, DataTableColumn column)
        {
            if (inObj == null)
                return String.Empty;

            PropertyInfo propInfo = dataType.GetProperty(column.Name);

            if (propInfo == null || !propInfo.CanRead)
                return String.Empty;

            object objValue = propInfo.GetValue(inObj); 

            if (column.Format != null)
                return column.Format(objValue);
            
            return objValue.ToString();
        }

        /// <summary>
        ///     Clear all data table rows
        /// </summary>
        private void Clear()
        {
            this.DataPoint.MoveTo();
            
            StringBuilder sb = new StringBuilder();
                
            for (int w = 0; w < Width; w++)
                sb.Append(' ');

            string wideString = sb.ToString();

            for (int h = 0; h < this.MaxLines; h++)
            {
                Console.CursorLeft = 0;
                Terminal.Write(wideString);
            }

            this.TopLeftPoint.MoveTo();
        }

        #endregion
    }
}
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TerminalUI.Types;

namespace TerminalUI.Elements
{
    public class DataTable : Element
    {
        // TODO: Add ability to define columns and format as a table view
        // TODO: Add ability to edit entries in table mode 
        #region Public Properties
        public List<DataTableColumn> Columns { get; set; } = null;
        public IList DataStore { get; set; } = null;
        public DataTableSelectType SelectType { get; private set; }
        public int MaxLines { get; private set; }
        public int TableWidth => this.TopRightPoint.Left - this.TopLeftPoint.Left;
        public TerminalPoint HeaderPoint { get; private set; } = null;
        public TerminalPoint DataPoint { get; private set; } = null;
        public bool ShowHeader 
        { 
            get => _showHeader;
            set 
            {
                if (value == true)
                {
                    this.HeaderPoint = this.TopLeftPoint;
                    this.DataPoint = this.TopLeftPoint.AddY(2);
                }
                else
                {
                    this.HeaderPoint = null;
                    this.DataPoint = this.TopLeftPoint;
                }

                _showHeader = value;
            }
        }

        #endregion Public Properties

        #region Private Fields
        private bool _showHeader = true;
        // private bool _canceled = false;
        private Dictionary<int, bool> _selectedIndexes = new Dictionary<int, bool>();
        // private List<object> _choosenItems = null;
        private HorizontalLine _headerLine = null;
        #endregion Private Fields

        #region Constructors
        public DataTable(DataTableSelectType selectType, bool show = false)
            : base(show) => Initalize(selectType);

        public DataTable(bool show = false)
            : base(show) => Initalize(null);

        public void Initalize(DataTableSelectType? selectType = null)
        {
            if (selectType.HasValue)
                this.SelectType = selectType.Value;

            this.TopLeftPoint = TerminalPoint.GetCurrent();
            this.TopRightPoint = this.TopLeftPoint.AddX(Terminal.UsableWidth);

            this.BottomLeftPoint = new TerminalPoint(this.TopLeftPoint.Left, Terminal.UsableBottomn);
            this.BottomRightPoint = this.BottomLeftPoint.AddX(Terminal.UsableWidth);

            this.MaxLines = this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 3;
            this.ShowHeader = true;
        }
        #endregion Constructors

        #region Public Methods

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

        public override void RedrawAll()
        {
            if (!this.Visible)
                return;

            if (this.ShowHeader == true)
                this.DrawHeader();

            this.Redraw();
        }

        

        public override void Redraw()
        {
            // TODO: Should we be calling this.Clear() instead?
            // erase would remove the entire table, header included but then 
            // the standard redraw would only draw the data rows.. right?
            this.Erase();

            this.DrawRows();
        }

        public void AbortMenu()
            => throw new NotImplementedException();
            // => _canceled = true;

        #endregion

        #region Private Methods
        private void DrawHeader()
        {            
            if (!this.Visible | !this.ShowHeader)
                return;

            using (this.HeaderPoint.GetMove())
            {

                int remainingChars = this.TableWidth;

                for (int i = 0; i < this.Columns.Count; i++)
                {
                    DataTableColumn column = this.Columns[i];
                    
                    Terminal.Write(column.LabelFormatted);

                    remainingChars -= column.Label.Length;

                    if (i < (this.Columns.Count-1))
                        Terminal.Write("   ");
                }

                Terminal.NextLine();
                if (_headerLine == null)
                    _headerLine = new HorizontalLine(ConsoleColor.White, LineType.ThinTripleDash, this.TableWidth);

                _headerLine.Show();

            }
        }

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
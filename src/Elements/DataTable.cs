using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TerminalUI.Elements
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

    public enum DataTableSelectType
    {
        None = 0,
        Single = 1,
        Multiple = 2
    }

    public class DataTable : Element
    {
        // TODO:
        // -- Add ability to define columns and format as a table view
        // -- Add ability to edit entries in table mode 
        #region Public Properties
        public List<DataTableColumn> Columns { get; set; } = null;
        public IList DataStore { get; set; } = null;
        public DataTableSelectType SelectType { get; private set; }
        public int MaxLines { get; private set; }
        public int MaxWidth => this.TopRightPoint.Left - this.TopLeftPoint.Left;
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
        private bool _canceled = false;
        private Dictionary<int, bool> _selectedIndexes = new Dictionary<int, bool>();
        private List<object> _choosenItems = null;
        private HorizontalLine _headerLine = null;
        #endregion Private Fields

        #region Constructors
        public DataTable(DataTableSelectType selectType)
            => Initalize(selectType);

        public DataTable()
            => Initalize(null);

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

        public async Task<List<object>> ShowAsync(bool clearScreen = true)
        {
            _choosenItems = null;

            this.MaxLines = this.BottomLeftPoint.Top - this.TopLeftPoint.Top - 1;

            this.Redraw();

            // this.SetupStatusBar();

            return await Task.Run(async () => 
            {
                // delay until the user does something
                while (_choosenItems == null && _canceled == false)
                    await Task.Delay(10);

                if (_canceled)
                    return null;

                return _choosenItems;
            });
        }

        public override void RedrawAll()
        {
            if (this.Visible)
            {
                if (this.ShowHeader == true)
                    this.DrawHeader();

                this.Redraw();
            }
        }

        

        public override void Redraw()
        {
            this.Erase();

            this.DrawRows();
            // foreach (DataTableHeader<TKey> entry in _entries.Skip(_entryOffset).Take(MaxLines))
            //     WriteMenuEntry(entry);
        }

        public void AbortMenu()
            => _canceled = true;

        #endregion

        #region Private Methods
        private void DrawHeader()
        {            
            if (this.Visible && this.ShowHeader)
            {
                TerminalPoint prevPt = TerminalPoint.GetCurrent();
                this.HeaderPoint.MoveTo();

                int remainingChars = this.MaxWidth;

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
                    _headerLine = new HorizontalLine(ConsoleColor.White, LineType.ThinTripleDash, this.MaxWidth);

                _headerLine.Show();

                prevPt.MoveTo();
            }
        }

        private void DrawRows()
        {
            if (this.Visible)
            {
                TerminalPoint prevPt = TerminalPoint.GetCurrent();

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

                prevPt.MoveTo();
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
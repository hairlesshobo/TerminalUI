# DataTable

Namespace: FoxHollow.TerminalUI.Elements

The data table element is meant for displaying data in a table format
 
 At some point in the future, it will also be able to be used for editing 
 data in a table, but we aren't there yet

```csharp
public class DataTable : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [DataTable](./foxhollow.terminalui.elements.datatable.md)

## Properties

### **Columns**

Columns that are to be visible in the table

```csharp
public List<DataTableColumn> Columns { get; private set; }
```

#### Property Value

[List&lt;DataTableColumn&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **DataStore**

Data store to populate the table with

```csharp
public IList DataStore { get; private set; }
```

#### Property Value

[IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist)<br>

### **SelectType**

Row selection mode

```csharp
public DataTableSelectType SelectType { get; private set; }
```

#### Property Value

[DataTableSelectType](./foxhollow.terminalui.types.datatableselecttype.md)<br>

### **MaxLines**

Maximum number of lines

```csharp
public int MaxLines { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **HeaderPoint**

TerminalPoint that describes the header

```csharp
public TerminalPoint HeaderPoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **DataPoint**

Terminal point that describes where the data rows start

```csharp
public TerminalPoint DataPoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **ShowHeader**

Flag indicating whether to show the header

```csharp
public bool ShowHeader { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Width**

Width of the element

```csharp
public int Width { get;  set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Height**

Height of the element

```csharp
public int Height { get;  set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxWidth**

Maximum width that the element may use, optionally constrained by the
 specified TerminalArea

```csharp
public int MaxWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxHeight**

Maximum height that the element may use, optionally constrained by the
 specified TerminalArea

```csharp
public int MaxHeight { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Visible**

Flag indicating whether the element is currently visible

```csharp
public bool Visible { get;  set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **TopLeftPoint**

TerminalPoint that represents the top-left-most point of the current element

```csharp
public TerminalPoint TopLeftPoint { get;  set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **TopRightPoint**

TerminalPoint that represents the top-right-most point of the current element

```csharp
public TerminalPoint TopRightPoint { get;  set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **BottomLeftPoint**

If the element spans multiple lines, this will be a TerminalPoint that represents 
 the bottom-left-most point of the current element. If the element is a single line
 element, this value will be null

```csharp
public TerminalPoint BottomLeftPoint { get;  set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **BottomRightPoint**

If the element spans multiple lines, this will be a TerminalPoint that represents 
 the bottom-right-most point of the current element. If the element is a single line
 element, this value will be null

```csharp
public TerminalPoint BottomRightPoint { get;  set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **Area**

TerminalArea that this element is constrainted to

```csharp
public TerminalArea Area { get;  set; }
```

#### Property Value

[TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>

## Constructors

### **DataTable(IList, List&lt;DataTableColumn&gt;, DataTableSelectType, Boolean, Int32, TerminalArea, Boolean)**

Constuct a new instance of the data table element

```csharp
public DataTable(IList dataStore, List<DataTableColumn> columns, DataTableSelectType selectType, bool showHeader, int rows, TerminalArea area, bool show)
```

#### Parameters

`dataStore` [IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist)<br>
Data store to pull from

`columns` [List&lt;DataTableColumn&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Column definitions used to generate the table

`selectType` [DataTableSelectType](./foxhollow.terminalui.types.datatableselecttype.md)<br>
Row selection type to use

`showHeader` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Flag indicating whether to display a header

`rows` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Specifies how many terminal rows should be consumed by the data table
            
                     0 = all remaining rows of the area
               above 0 = fixed number
               below 0 = reminaing rows - absolute value

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Are to constrain the element to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if true, immediately show the element upon creation

## Methods

### **RecalculateAndRedraw()**

Recalculate and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **SetDataStore(IList)**

Set the data store to the object provided

```csharp
public void SetDataStore(IList dataStore)
```

#### Parameters

`dataStore` [IList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ilist)<br>
New data store

### **ShowAsync(Boolean)**

Not implemented yet

```csharp
public Task<List<object>> ShowAsync(bool clearScreen)
```

#### Parameters

`clearScreen` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[Task&lt;List&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **RedrawAll()**

Redraw the entire data table

```csharp
public void RedrawAll()
```

### **Redraw()**

Redraw the data rows

```csharp
public void Redraw()
```

### **AbortMenu()**

Not implemented yet

```csharp
public void AbortMenu()
```

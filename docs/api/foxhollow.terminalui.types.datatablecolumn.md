# DataTableColumn

Namespace: FoxHollow.TerminalUI.Types

Describes a column that is to be displayed on a DataTable

```csharp
public class DataTableColumn
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [DataTableColumn](./foxhollow.terminalui.types.datatablecolumn.md)

## Fields

### **Format**

Callback function that allows for custom formatting of the cell contents

```csharp
public Func<object, string> Format;
```

## Properties

### **Name**

Name of the column. This needs to be the name of the property in the list
 that is being passed. It is best to use `nameof()`

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Label**

Label to show in the header, if enabled

```csharp
public string Label { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **LabelFormatted**

Label that has been formatted with any alignment and padding

```csharp
public string LabelFormatted { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Width**

Width of the column

```csharp
public int Width { get; set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **AllowEdit**

If true, this will allow values in this column to be edited
 !! NOT IMPLEMENTED YET !!

```csharp
public bool AllowEdit { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ForegroundColor**

Foreground color to use when drawing this column

```csharp
public ConsoleColor ForegroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **BackgroundColor**

Background color to use when drawing this column

```csharp
public ConsoleColor BackgroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

## Constructors

### **DataTableColumn(String, String, Int32)**

Construct a new DataTableColumn object

```csharp
public DataTableColumn(string name, string label, int width)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Name of the property

`label` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Optional label to use in the header, if enabled

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Width of the column

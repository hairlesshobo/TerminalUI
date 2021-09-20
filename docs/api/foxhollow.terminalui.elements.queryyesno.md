# QueryYesNo

Namespace: FoxHollow.TerminalUI.Elements

Simple element that is used to await a yes/no answer from the user

```csharp
public class QueryYesNo : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [QueryYesNo](./foxhollow.terminalui.elements.queryyesno.md)

## Properties

### **QueryText**

Text to display to end user

```csharp
public string QueryText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ForegroundColor**

Foreground color to display the query as. Uses default terminal foreground 
 if no color is provided

```csharp
public Nullable<ConsoleColor> ForegroundColor { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

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

### **QueryYesNo(String, Nullable&lt;ConsoleColor&gt;, TerminalArea)**

Construct a new isntance of the query element

```csharp
public QueryYesNo(string queryText, Nullable<ConsoleColor> foregroundColor, TerminalArea area)
```

#### Parameters

`queryText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Foreground color to use when drawing the message

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Area to constrain the query box to

## Methods

### **RecalculateAndRedraw()**



```csharp
internal void RecalculateAndRedraw()
```

### **Redraw()**

Not supported by this element

```csharp
public void Redraw()
```

### **SetForegroundColor(Nullable&lt;ConsoleColor&gt;)**

Set the foreground color

```csharp
public void SetForegroundColor(Nullable<ConsoleColor> foregroundColor)
```

#### Parameters

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New color to use

### **QueryAsync(CancellationToken)**

Asynchronously query the user for a response

```csharp
public Task<Nullable<bool>> QueryAsync(CancellationToken cToken)
```

#### Parameters

`cToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Token used to cancel the query

#### Returns

[Task&lt;Nullable&lt;Boolean&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

                 The result of the task can be one of three states:
            
                 true  = user entered yes
                 false = user entered no
                 null  = query was canceled before a valid answer was provided

### **Show()**

Not supported

```csharp
public void Show()
```

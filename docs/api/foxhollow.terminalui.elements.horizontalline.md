# HorizontalLine

Namespace: FoxHollow.TerminalUI.Elements

A horizontal line element is exactly what it sounds like.. a horizontal
 line that is used to separate a line above and below it

```csharp
public class HorizontalLine : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [HorizontalLine](./foxhollow.terminalui.elements.horizontalline.md)

## Properties

### **LineType**

Type of line to draw

```csharp
public LineType LineType { get; private set; }
```

#### Property Value

[LineType](./foxhollow.terminalui.types.linetype.md)<br>

### **ForegroundColor**

Foreground color to use when drawing the line

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

### **HorizontalLine(Nullable&lt;ConsoleColor&gt;, LineType, Int32, TerminalArea, Boolean)**

Construct a new horizontal line

```csharp
public HorizontalLine(Nullable<ConsoleColor> foregroundColor, LineType lineType, int width, TerminalArea area, bool show)
```

#### Parameters

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Color to use when drawing the line

`lineType` [LineType](./foxhollow.terminalui.types.linetype.md)<br>
Type of line to draw

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Width to draw the line to.. 
                if 0, it will automatically fill the provided area
                if below 0, it will be the area width - the absolute value provided
                if above 0, it will be a fixed width as provided

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Area to fill

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, element will be displayed immediately upon construction

## Methods

### **RecalculateAndRedraw()**

Recalculate and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **Redraw()**

Redraw the element, if visible

```csharp
public void Redraw()
```

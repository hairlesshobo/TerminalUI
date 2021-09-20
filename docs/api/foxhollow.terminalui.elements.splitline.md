# SplitLine

Namespace: FoxHollow.TerminalUI.Elements

Split-line element. This is used for displaying two pieces of text on a line, 
 one is left-justified and the other is right justified

```csharp
public class SplitLine : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [SplitLine](./foxhollow.terminalui.elements.splitline.md)

## Properties

### **LeftForegroundColor**

Color used for the foreground of the left piece of text

```csharp
public Nullable<ConsoleColor> LeftForegroundColor { get; set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **RightForegroundColor**

Color used for the foreground of the right piece of text

```csharp
public Nullable<ConsoleColor> RightForegroundColor { get; set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **LeftText**

Text that is displayed on the left

```csharp
public string LeftText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RightText**

Text that is displayed on the right

```csharp
public string RightText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

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

### **SplitLine(String, String, Nullable&lt;ConsoleColor&gt;, Nullable&lt;ConsoleColor&gt;, TerminalArea, Boolean)**

Construct a new instance of the split-line element

```csharp
public SplitLine(string leftText, string rightText, Nullable<ConsoleColor> leftColor, Nullable<ConsoleColor> rightColor, TerminalArea area, bool show)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the left

`rightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the right

`leftColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Color to use for the left

`rightColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Color to use for the right

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
TerminalArea to be confined to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if true, the element will be shown immediately

## Methods

### **RecalculateAndRedraw()**

Recalculate the layout and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **Redraw()**

Redraw the element

```csharp
public void Redraw()
```

### **Update(String, String, Boolean)**

Update the left and right text and redraw the element

```csharp
public void Update(string leftText, string rightText, bool noRedraw)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to draw on the left

`rightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to draw on the right

`noRedraw` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the element will not be redrawn

### **UpdateLeft(String, Boolean)**

Update the left text and redraw the element

```csharp
public void UpdateLeft(string leftText, bool noRedraw)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to draw on the left

`noRedraw` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the element will not be redrawn

### **UpdateRight(String, Boolean)**

Update the right text and redraw the element

```csharp
public void UpdateRight(string rightText, bool noRedraw)
```

#### Parameters

`rightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to draw on the right

`noRedraw` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the element will not be redrawn

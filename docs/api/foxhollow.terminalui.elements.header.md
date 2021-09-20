# Header

Namespace: FoxHollow.TerminalUI.Elements

A header element is a two-line element that is comprised of two 
 other elements. The first line is a [SplitLine](./foxhollow.terminalui.elements.splitline.md) element
 the second element is a [HorizontalLine](./foxhollow.terminalui.elements.horizontalline.md)

```csharp
public class Header : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [Header](./foxhollow.terminalui.elements.header.md)

## Properties

### **LeftText**

Text to display on the left side of the SplitLine

```csharp
public string LeftText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **RightText**

Text to display on the right side of the SplitLine

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

### **Header(String, String, TerminalArea, Boolean)**

Construct a new header

```csharp
public Header(string leftText, string rightText, TerminalArea area, bool show)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to display on left side of the header

`rightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to display on the right side of the header

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
TerminalArea to use when calcualting the layout

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the header will display automatically upon construction

## Methods

### **RecalculateAndRedraw()**



```csharp
internal void RecalculateAndRedraw()
```

### **UpdateLeft(String)**

Update the text on the left

```csharp
public void UpdateLeft(string leftText)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
text on the left

### **UpdateRight(String)**

Update the text on the right

```csharp
public void UpdateRight(string right)
```

#### Parameters

`right` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text on the right

### **UpdateHeader(String, String)**

Update both left and right text in the header

```csharp
public void UpdateHeader(string leftText, string rightText)
```

#### Parameters

`leftText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text on the left side of the header

`rightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text on the right side of the header

### **Redraw()**

Redraw the text portion of the header

```csharp
public void Redraw()
```

### **RedrawAll()**

Redraw the entire element

```csharp
public void RedrawAll()
```

### **Show()**

Show the header

```csharp
public void Show()
```

### **Hide()**

Hide the header

```csharp
public void Hide()
```

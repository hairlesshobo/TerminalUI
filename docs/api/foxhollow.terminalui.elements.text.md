# Text

Namespace: FoxHollow.TerminalUI.Elements

Text element
 This is a very simple element that just displays text by itself

```csharp
public class Text : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [Text](./foxhollow.terminalui.elements.text.md)

## Properties

### **ForegroundColor**

Current foreground color in use by the text element. Null if the default
 terminal foreground color should be used, as defined in
 FoxHollow.TerminalUI.TerminalColor.DefaultForeground

```csharp
public Nullable<ConsoleColor> ForegroundColor { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TextValue**

Current text value being displayed by the text element

```csharp
public string TextValue { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **TextWidth**

The effective width of the text

```csharp
public int TextWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxLength**

Maximum length the string may be

```csharp
public int MaxLength { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

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

### **Text(String, Int32, Nullable&lt;ConsoleColor&gt;, TerminalArea, Boolean)**

Construct a new Text element, using the provided text as the initial 
 text to be displayed

```csharp
public Text(string text, int maxLength, Nullable<ConsoleColor> foregroundColor, TerminalArea area, bool show)
```

#### Parameters

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Initial text to display

`maxLength` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Maximum number of characters the text field may contain

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Color to use when drawing the text

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Terminal area to constrain the element to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, element will automatically be shown once constructed

## Methods

### **RecalculateAndRedraw()**

Recalculate the layout and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **Redraw()**

If visible, redraw the text element

```csharp
public void Redraw()
```

### **SetForegroundColor(Nullable&lt;ConsoleColor&gt;)**

Set the foreground color of the text element

```csharp
public void SetForegroundColor(Nullable<ConsoleColor> foregroundColor)
```

#### Parameters

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

                Foreground color, null if the element should use the default foreground color

### **UpdateValue(ConsoleColor, String)**

Update the text value and foreground color then redraw the text element

```csharp
public void UpdateValue(ConsoleColor foregroundColor, string newText)
```

#### Parameters

`foregroundColor` ConsoleColor<br>
New foreground color to use

`newText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
New text to display

### **UpdateValue(String)**

Update the text value and redraw the text element

```csharp
public void UpdateValue(string newText)
```

#### Parameters

`newText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
New text to display

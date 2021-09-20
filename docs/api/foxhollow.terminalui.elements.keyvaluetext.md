# KeyValueText

Namespace: FoxHollow.TerminalUI.Elements

A class to display a KeyValue textual item. A KeyValueText element
 consists of two pieces of text.. a "Key", which is a "static" string,
 and a "Value" which is a dynamic and frequently changing string

```csharp
public class KeyValueText : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [KeyValueText](./foxhollow.terminalui.elements.keyvaluetext.md)

## Properties

### **KeyText**

Text to use for the key, that is, on the left side of the element

```csharp
public string KeyText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ValueText**

Text to display on the right side of the element

```csharp
public string ValueText { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **KeyColor**

Foreground color to use for the "Key" string

```csharp
public ConsoleColor KeyColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **ValueColor**

Foreground color to use for the "Value" string

```csharp
public ConsoleColor ValueColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **MaxValueLength**

Maximum length the value may be

```csharp
public int MaxValueLength { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **LeftWidth**

How wide the left side of the element is

```csharp
public int LeftWidth { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **RightWidth**

Width of the right part of the element

```csharp
public int RightWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Separator**

String that is used as the separator between the left and the right

```csharp
public string Separator { get; set; }
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

### **KeyValueText(String, String, Int32, Int32, String, TerminalArea, Boolean)**

Construct a new KeyValueText element

```csharp
public KeyValueText(string keyText, string valueText, int leftWidth, int rightMaxLength, string separator, TerminalArea area, bool show)
```

#### Parameters

`keyText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to use for the "key"

`valueText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to use for the "value"

`leftWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                The optional fixed width to use for the "key" side of the element.
            
                - If the provided value is 0, no padding and therefore no fixed width is applied
                - If the provided value is less than 0, the value text is to be right-justified 
                  with a fixed width using the absolute width as provided
                - If the provided value is greater than 0, the value text is to be left-justified
                  with a fixed width using the absolutely width as provided

`rightMaxLength` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Maximum string length the right part of the element may contain

`separator` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String used to separate the left and the right sides

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
TerminalArea the element should be constrainted to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the element will be shown immediately

## Methods

### **RecalculateAndRedraw()**

Recalculate the layout and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **SetSeparator(String)**

Change the separator that is used between left and right

```csharp
public void SetSeparator(string separator)
```

#### Parameters

`separator` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
new separator string

### **RedrawAll()**

Redraw the entire element, both the static "Key" and the dynamic "Value" strings

```csharp
public void RedrawAll()
```

### **Redraw()**

Redraw only the "value" side of the element

```csharp
public void Redraw()
```

### **UpdateValue(String)**

Update the value displayed by this element

```csharp
public void UpdateValue(string newText)
```

#### Parameters

`newText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
New text to display

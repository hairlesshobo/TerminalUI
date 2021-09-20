# NotificationBox

Namespace: FoxHollow.TerminalUI.Elements

A notification box is meant to be displayed above the rest of the 
 elements for a notification or, at some point in the future, maybe
 used to query the user for information of some sort

```csharp
public class NotificationBox : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [NotificationBox](./foxhollow.terminalui.elements.notificationbox.md)

## Properties

### **DefaultBackgroundColor**

Default background to use for text lines when no other color is specified

```csharp
public Nullable<ConsoleColor> DefaultBackgroundColor { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **BorderForegroundColor**

Foreground color to use when drawing the border

```csharp
public Nullable<ConsoleColor> BorderForegroundColor { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **BorderBackgroundColor**

Background color to use when drawing the border

```csharp
public Nullable<ConsoleColor> BorderBackgroundColor { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **TextLineCount**

Value indicates the number of text lines available in this notification box

```csharp
public int TextLineCount { get; private set; }
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

### **NotificationBox(Int32, Int32, Nullable&lt;ConsoleColor&gt;, Nullable&lt;ConsoleColor&gt;, Nullable&lt;ConsoleColor&gt;, TerminalArea, Boolean)**

Construct a new notificatiom box element

```csharp
public NotificationBox(int height, int width, Nullable<ConsoleColor> borderForegroundColor, Nullable<ConsoleColor> borderBackgroundColor, Nullable<ConsoleColor> defaultBackgroundColor, TerminalArea area, bool show)
```

#### Parameters

`height` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Height of the new box. 
            
                Note: 2 lines will be used for the border

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Width of the new box.
            
                Noite: 2  of the columns will be used for the border

`borderForegroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Foreground color to use when drawing the border of the box

`borderBackgroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Background color to use when drawing the border of the box

`defaultBackgroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Background color to use by default when drawing the background of any line

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Area to constrain the notification box to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the element will be shown immediately upon creation

## Methods

### **RecalculateAndRedraw()**

Recalculate the layout and position, then redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **SetLineColor(Int32, Nullable&lt;ConsoleColor&gt;)**

Set the specified line to the provided color

```csharp
public void SetLineColor(int index, Nullable<ConsoleColor> color)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Line index to change

`color` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New color

### **SetTextJustify(Int32, TextJustify)**

Set the specified line to the provided justification

```csharp
public void SetTextJustify(int index, TextJustify justify)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Line index to change

`justify` [TextJustify](./foxhollow.terminalui.types.textjustify.md)<br>
New text justification to use

### **SetBorderColors(Nullable&lt;ConsoleColor&gt;, Nullable&lt;ConsoleColor&gt;)**

Change the foreground and background color of the box border

```csharp
public void SetBorderColors(Nullable<ConsoleColor> foreground, Nullable<ConsoleColor> background)
```

#### Parameters

`foreground` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New foreground color

`background` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New background color

### **UpdateText(Int32, String)**

Update the text at the specified line index

```csharp
public void UpdateText(int index, string newText)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Index to change

`newText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
New text to display

### **UpdateLine(Int32, String, Nullable&lt;TextJustify&gt;, Nullable&lt;ConsoleColor&gt;, Nullable&lt;ConsoleColor&gt;)**

Update one or more aspect of a single line. All values are only updated 
 if the value provided is not null

```csharp
public void UpdateLine(int index, string text, Nullable<TextJustify> justify, Nullable<ConsoleColor> foregroundColor, Nullable<ConsoleColor> backgroundColor)
```

#### Parameters

`index` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Index to change

`text` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
New text to display, if not null

`justify` [Nullable&lt;TextJustify&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New justify to set

`foregroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New foreground color to set

`backgroundColor` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
New background color to set

### **RedrawAll()**

Redraw the entire element.. that is, redraw the border and the text lines

```csharp
public void RedrawAll()
```

### **Redraw()**

Redraw the text lines

```csharp
public void Redraw()
```

### **Hide()**

Hide the notification box from the terminal

```csharp
public void Hide()
```

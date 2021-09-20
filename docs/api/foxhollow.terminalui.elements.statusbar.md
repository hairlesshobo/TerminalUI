# StatusBar

Namespace: FoxHollow.TerminalUI.Elements

A status bar element is a special element that can only exist once
 and will be shown on the very bottom line of the screen. The status
 bar is also the entry point for any terminal key input

```csharp
public class StatusBar : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [StatusBar](./foxhollow.terminalui.elements.statusbar.md)

## Properties

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

## Methods

### **RecalculateAndRedraw()**

Recalculate and redraw the status bar

```csharp
internal void RecalculateAndRedraw()
```

### **SetDefaultItems(StatusBarItem[])**



```csharp
internal void SetDefaultItems(StatusBarItem[] items)
```

#### Parameters

`items` [StatusBarItem[]](./foxhollow.terminalui.types.statusbaritem.md)<br>

### **SetItems(StatusBarItem[])**



```csharp
internal void SetItems(StatusBarItem[] items)
```

#### Parameters

`items` [StatusBarItem[]](./foxhollow.terminalui.types.statusbaritem.md)<br>

### **ShowItems(StatusBarItem[])**

Show the provided list of items on the status bar

```csharp
public void ShowItems(StatusBarItem[] items)
```

#### Parameters

`items` [StatusBarItem[]](./foxhollow.terminalui.types.statusbaritem.md)<br>
Items to display

### **Redraw()**

Redraw the status bar

```csharp
public void Redraw()
```

### **RemoveItemByName(String)**

Remove an item from the status bar by the provided name

```csharp
public void RemoveItemByName(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
name of the item to remove

### **Reset()**

Reset the status bar back to the default items

```csharp
public void Reset()
```

### **GetInstance(Boolean)**

Get the existing instance of the StatusBar or create a new one

```csharp
public static StatusBar GetInstance(bool show)
```

#### Parameters

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

#### Returns

[StatusBar](./foxhollow.terminalui.elements.statusbar.md)<br>
StatusBar instance

# Menu

Namespace: FoxHollow.TerminalUI.Elements

Basic menu element that allows listing multiple options and allows the 
 user to interactively select one or more (multiple select not yet implemented)
 items

```csharp
public class Menu : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [Menu](./foxhollow.terminalui.elements.menu.md)

## Properties

### **EnableCancel**

If true, the menu will show "Cancel" instead of "Quit"

```csharp
public bool EnableCancel { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **MultiSelect**

If true, multi-select functionality is enabled

```csharp
public bool MultiSelect { get; private set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **QuitCallback**

Callback that is executed when the user requests to quit the menu

```csharp
public Func<Task> QuitCallback { get; set; }
```

#### Property Value

[Func&lt;Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-1)<br>

### **SelectedValues**

List of values that are selected

```csharp
public List<object> SelectedValues { get; }
```

#### Property Value

[List&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>

### **SelectedEntries**

Entries that are currently selected

```csharp
public IReadOnlyList<object> SelectedEntries { get; }
```

#### Property Value

[IReadOnlyList&lt;Object&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)<br>

### **MaxLines**

Maximum number of lines that can be displayed

```csharp
public int MaxLines { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **LeftPad**

How many spaces to pad the menu with on the left

```csharp
public int LeftPad { get; set; }
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

### **Menu(List&lt;MenuEntry&gt;, Boolean, Boolean, TerminalArea)**

Constuct a new instance of the menu element

```csharp
public Menu(List<MenuEntry> entries, bool multiSelect, bool enableCancel, TerminalArea area)
```

#### Parameters

`entries` [List&lt;MenuEntry&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Menu entries to display

`multiSelect` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Allow the user to select multiple entries

`enableCancel` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, "cancel" will be displayed on status bar instead of "quit"

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
TerminalArea to draw the element in

## Methods

### **RecalculateAndRedraw()**

Recalculate the layout and redraw, if showing

```csharp
internal void RecalculateAndRedraw()
```

### **ShowAsync&lt;TResult&gt;(CancellationToken)**

Show the menu asynchronously and either wait for the user
 to make a selection, or to cancel out of the menu. Cast the
 results to the specified type

```csharp
public Task<List<TResult>> ShowAsync<TResult>(CancellationToken cToken)
```

#### Type Parameters

`TResult`<br>
Type tp cast the return list to

#### Parameters

`cToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Token used to cancel the menu

#### Returns

Task&lt;List&lt;TResult&gt;&gt;<br>
Task that returns an object, or null if canceled

### **ShowAsync(CancellationToken)**

Show the menu asynchronously and either wait for the user
 to make a selection, or to cancel out of the menu

```csharp
public Task<List<object>> ShowAsync(CancellationToken cToken)
```

#### Parameters

`cToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Token used to cancel the menu

#### Returns

[Task&lt;List&lt;Object&gt;&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
Task that returns an object, or null if canceled

### **Redraw()**

Redraw the current menu

```csharp
public void Redraw()
```

### **AbortMenu()**

Abort the current menu

```csharp
public void AbortMenu()
```

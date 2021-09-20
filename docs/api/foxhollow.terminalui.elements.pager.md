# Pager

Namespace: FoxHollow.TerminalUI.Elements

Pager element that is used for displaying multi-line text that overflows
 beyond what the terminal is able to display. This element can be scrolled
 to allow for easy viewing of the content.

```csharp
public class Pager : FoxHollow.TerminalUI.Types.Element, System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [Pager](./foxhollow.terminalui.elements.pager.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **AutoScroll**

If true, the display will automatically scroll to the bottom as new lines are added
 
 Note: this only works when the pager has been started, otherwise scrolling must be done
 manually from outside the element

```csharp
public bool AutoScroll { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **ShowLineNumbers**

If true, show line numbers for each line.
 
 Note: This will slow down rendering slightly if the background color is different from that
 of the default terminal background color

```csharp
public bool ShowLineNumbers { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **LineNumberBackground**

Background color to use for line numbers. If null, the default defined in
 [TerminalColor](./foxhollow.terminalui.terminalcolor.md) will be used

```csharp
public Nullable<ConsoleColor> LineNumberBackground { get; set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **LineNumberForeground**

Foreground color to use for line numbers. If null, the default defined in
 [TerminalColor](./foxhollow.terminalui.terminalcolor.md) will be used

```csharp
public Nullable<ConsoleColor> LineNumberForeground { get; set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **ShowHeader**

If true, the header provided in FoxHollow.TerminalUI.Elements.Pager.HeaderText will be displayed

```csharp
public bool ShowHeader { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HeaderText**

Header text to display, if FoxHollow.TerminalUI.Elements.Pager.ShowHeader is enabled

```csharp
public string HeaderText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **HighlightText**

Text that is to be highlighted in the output

```csharp
public string HighlightText { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Highlight**

Flag indicating whether to activate text highlighting

```csharp
public bool Highlight { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **HighlightForegroundColor**

Foreground color to use when highlighting text

```csharp
public Nullable<ConsoleColor> HighlightForegroundColor { get; set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **LineNumberWidth**

Width of the line number column (does NOT include the extra space between line number column and start of lines)

```csharp
public int LineNumberWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PagerHeight**

Total height of the pager

```csharp
public int PagerHeight { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **PagerWidth**

Total width of the pager

```csharp
public int PagerWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **MaxLines**

Maximum number of lines to display on the pager

```csharp
public int MaxLines { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **DeferDraw**

Defer drawing until all data is received

```csharp
public bool DeferDraw { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **FirstTextLinePoint**

Terminal point that indicates the first line of text

```csharp
public TerminalPoint FirstTextLinePoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **HeaderLinePoint**

Terminal point that indicates where the header, if present, begins

```csharp
public TerminalPoint HeaderLinePoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **CancellationToken**

Cancellation token that is triggered when the user exits the pager

```csharp
public CancellationToken CancellationToken { get; }
```

#### Property Value

[CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

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

### **Pager(Boolean, Boolean, String, String, List&lt;String&gt;, TerminalArea, Boolean)**

Constuct a new pager

```csharp
public Pager(bool autoScroll, bool showLineNumbers, string headerText, string highlightText, List<string> lines, TerminalArea area, bool show)
```

#### Parameters

`autoScroll` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, automatically scroll output

`showLineNumbers` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Show the line numbers

`headerText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to display as a header

`highlightText` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to highlight

`lines` [List&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)<br>
Lines to start pager with

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
TerminalArea to consume

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, element will be shown upon construction

## Methods

### **RecalculateAndRedraw()**

Recalculate the lauout and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **RunAsync(CancellationToken)**

Run the pager asynchronously. This automatically sets up the status bar for navigation 
 and will continue running until the user cancels it, or the passed cToken is canceled

```csharp
public Task RunAsync(CancellationToken cToken)
```

#### Parameters

`cToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Cancellation token

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
Task

### **Stop()**

Immediately stop the pager, if it is running

```csharp
public void Stop()
```

### **Start()**

Start the pager as a background task

```csharp
public void Start()
```

### **StartNew()**

Shortcut method to start a create and start a new pager

```csharp
public static Pager StartNew()
```

#### Returns

[Pager](./foxhollow.terminalui.elements.pager.md)<br>
New pager instance with default config

### **WaitForQuitAsync()**

Wait until the pager is finished

```csharp
public Task WaitForQuitAsync()
```

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
Task

### **Redraw()**

Redraw the entire pager

```csharp
public void Redraw()
```

### **AppendLine()**

Append a blank line to the pager

```csharp
public void AppendLine()
```

### **AppendLine(String)**

Append a line to the pager

```csharp
public void AppendLine(string line)
```

#### Parameters

`line` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Line to append

### **ScrollToTop()**

Scroll to the top line

```csharp
public void ScrollToTop()
```

### **ScrollToBottom()**

Scroll to the bottom line

```csharp
public void ScrollToBottom()
```

### **UpLine()**

Move up one line

```csharp
public void UpLine()
```

### **UpPage()**

Move up one page

```csharp
public void UpPage()
```

### **DownLine()**

Move down one line

```csharp
public void DownLine()
```

### **DownPage()**

Move down one page

```csharp
public void DownPage()
```

### **Dispose()**

Cleanup the pager resources

```csharp
public void Dispose()
```

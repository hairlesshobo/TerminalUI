# Terminal

Namespace: FoxHollow.TerminalUI

Static helper class for simplifying a console-based, interactive user interface

```csharp
public static class Terminal
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Terminal](./foxhollow.terminalui.terminal.md)

## Properties

### **Header**

Header object. Will be null until the first time FoxHollow.TerminalUI.Terminal.InitHeader(System.String,System.String,System.Boolean) is called

```csharp
public static Header Header { get; private set; }
```

#### Property Value

[Header](./foxhollow.terminalui.elements.header.md)<br>

### **StatusBar**

StatusBar object. Will be null until the first time FoxHollow.TerminalUI.Terminal.InitStatusBar(FoxHollow.TerminalUI.Types.StatusBarItem[]) is called

```csharp
public static StatusBar StatusBar { get; private set; }
```

#### Property Value

[StatusBar](./foxhollow.terminalui.elements.statusbar.md)<br>

### **RootPoint**

The root point of the terminal, that is, the Top-leftmost position that is 
 not part of the header

```csharp
public static TerminalPoint RootPoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **UsableHeight**

Number of lines that are usable by the application. This accounts for and 
 deducts any lines that are in use by the header and/or status bar

```csharp
public static int UsableHeight { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **UsableWidth**

Number of colums that are usable by the application

```csharp
public static int UsableWidth { get; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **BackgroundColor**

Background color of the terminal. Only makes the call to change the 
 color if the background is different from the requested color

```csharp
public static ConsoleColor BackgroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **ForegroundColor**

Foreground color of the terminal. Only makes the call to change the 
 color if the foreground is different from the requested color

```csharp
public static ConsoleColor ForegroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **CursorVisible**

Get or set the status of the cursor visibility

```csharp
public static bool CursorVisible { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **Write(Char)**

Proxy method for System.Console.Write(System.Char). Reserved for future use

```csharp
public static void Write(char input)
```

#### Parameters

`input` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>
Character to write to the terminal

### **Write(String)**

Proxy method for System.Console.Write(System.String). Reserved for future use

```csharp
public static void Write(string input)
```

#### Parameters

`input` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String to write to the terminal

### **WriteColor(ConsoleColor, Char)**

Write a character to the terminal with the specified foreground color

```csharp
public static void WriteColor(ConsoleColor color, char inputChar)
```

#### Parameters

`color` ConsoleColor<br>
Color to write

`inputChar` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>
char to write

### **WriteColor(Nullable&lt;ConsoleColor&gt;, Char)**

Write a character to the terminal with the specified foreground color, but only
 if the color isn't null and doesn't match the default foreground color of the terminal

```csharp
public static void WriteColor(Nullable<ConsoleColor> color, char inputChar)
```

#### Parameters

`color` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Possible color to use

`inputChar` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>
Character to write

### **WriteColor(ConsoleColor, String)**

Write a string to the terminal using the specified foreground color

```csharp
public static void WriteColor(ConsoleColor color, string inputString)
```

#### Parameters

`color` ConsoleColor<br>
Color to write

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
string to write

### **WriteColor(Nullable&lt;ConsoleColor&gt;, String)**

Write a string to the terminal with the specified foreground color, but only
 if the color isn't null and doesn't match the default foreground color of the terminal

```csharp
public static void WriteColor(Nullable<ConsoleColor> color, string inputString)
```

#### Parameters

`color` [Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Possible color to use

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
string to write

### **WriteColorBG(ConsoleColor, String)**

Write a string to the terminal using the specified background color

```csharp
public static void WriteColorBG(ConsoleColor backgroundColor, string inputString)
```

#### Parameters

`backgroundColor` ConsoleColor<br>
Background color to use

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String to write

### **WriteLine()**

Write a blank line to the terminal (in other words, just move to the next line)

```csharp
public static void WriteLine()
```

### **WriteLine(String)**

Write a string to the terminal then move to the next line

```csharp
public static void WriteLine(string inputString)
```

#### Parameters

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
string to write

### **WriteLineColor(ConsoleColor, Char)**

Write a character to the terminal using the specified foreground color 
 then move to the next line

```csharp
public static void WriteLineColor(ConsoleColor foregroundColor, char inputChar)
```

#### Parameters

`foregroundColor` ConsoleColor<br>
Color to write

`inputChar` [Char](https://docs.microsoft.com/en-us/dotnet/api/system.char)<br>
character to write

### **WriteLineColor(ConsoleColor, String)**

Write a string to the terminal using the specified foreground
 color then move to the next line

```csharp
public static void WriteLineColor(ConsoleColor color, string inputString)
```

#### Parameters

`color` ConsoleColor<br>
Color to write

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
string to write

### **WriteLineColorBG(ConsoleColor, String)**

Write a string to the terminal using the specified background color
 then move to the next line

```csharp
public static void WriteLineColorBG(ConsoleColor backgroundColor, string inputString)
```

#### Parameters

`backgroundColor` ConsoleColor<br>
background color to use

`inputString` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
string to write

### **NextLine()**

Move to the next line and position the cursor all the way to the left

```csharp
public static void NextLine()
```

### **NextLine(TerminalArea)**

Move to the next line and position the cursor all the weay to the left 
 of the specified area

```csharp
public static void NextLine(TerminalArea area)
```

#### Parameters

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
area to use for determining left position

### **ResetColor()**

Reset both the foreground and the background color of the terminal

```csharp
public static void ResetColor()
```

### **ResetForeground()**

Reset the foreground color to the default specified by 
 FoxHollow.TerminalUI.TerminalColor.DefaultForeground

```csharp
public static void ResetForeground()
```

### **ResetBackground()**

Reset the background color to the default specified by 
 FoxHollow.TerminalUI.TerminalColor.DefaultBackground

```csharp
public static void ResetBackground()
```

### **RawClear(Boolean)**



```csharp
internal static void RawClear(bool preserveElements)
```

#### Parameters

`preserveElements` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Clear(Boolean)**

Clear the terminal

```csharp
public static void Clear(bool preserveElements)
```

#### Parameters

`preserveElements` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
if true, the elements that are currently registered will NOT be unregistered

### **Initialize()**

Initialize the Terminal application with all default settings

```csharp
public static void Initialize()
```

### **Initialize(String, String)**

Initialize the Terminal UI application. Does NOT need to be called
 if using either FoxHollow.TerminalUI.Terminal.Run(System.Func{System.Threading.CancellationTokenSource,System.Threading.Tasks.Task})
 or FoxHollow.TerminalUI.Terminal.Run(System.String,System.String,System.Func{System.Threading.CancellationTokenSource,System.Threading.Tasks.Task})
 to start the terminal application

```csharp
public static void Initialize(string headerLeft, string headerRight)
```

#### Parameters

`headerLeft` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to display on the left of the header

`headerRight` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to display on the right of the header

### **InitHeader(String, String, Boolean)**

Initialize the header

```csharp
internal static Header InitHeader(string left, string right, bool show)
```

#### Parameters

`left` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the left of the header

`right` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the right of the header

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, the header will be shown immediately

#### Returns

[Header](./foxhollow.terminalui.elements.header.md)<br>
Header object

### **InitStatusBar(StatusBarItem[])**

Initialize the status bar

```csharp
public static StatusBar InitStatusBar(StatusBarItem[] items)
```

#### Parameters

`items` [StatusBarItem[]](./foxhollow.terminalui.types.statusbaritem.md)<br>
Items to include in the status bar

#### Returns

[StatusBar](./foxhollow.terminalui.elements.statusbar.md)<br>
Status bar object

### **SetCursorPosition(Int32, Int32)**

Move the cursor to the specified position

```csharp
public static void SetCursorPosition(int left, int top)
```

#### Parameters

`left` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
left position

`top` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
top position

### **StartAsync(CancellationTokenSource)**

Start the main loop.. this starts listening for input keys

```csharp
public static Task StartAsync(CancellationTokenSource cts)
```

#### Parameters

`cts` [CancellationTokenSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource)<br>

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
Task

### **Stop()**

Shut down the main loop

```csharp
public static void Stop()
```

### **WaitForStop()**

Wait for the main loop to stop processing

```csharp
public static void WaitForStop()
```

### **Run(Func&lt;CancellationTokenSource, Task&gt;)**

Run the terminal application with the provided main entry point

```csharp
public static void Run(Func<CancellationTokenSource, Task> mainEntryPoint)
```

#### Parameters

`mainEntryPoint` [Func&lt;CancellationTokenSource, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Main entry point of the entire application

### **Run(String, String, Func&lt;CancellationTokenSource, Task&gt;)**

Run the terminal application with the provided main entry point and setup the
 header using the provided strings

```csharp
public static void Run(string headerLeft, string headerRight, Func<CancellationTokenSource, Task> mainEntryPoint)
```

#### Parameters

`headerLeft` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the left side of the header

`headerRight` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show on the right side of the header

`mainEntryPoint` [Func&lt;CancellationTokenSource, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Main entry point of the entire application

### **TerminalSizeChanged()**

Method to call when the terminal size has chaned

```csharp
internal static void TerminalSizeChanged()
```

### **RedrawAllElements()**

This method will redraw all elements that are currently visible 
 on the terminal, aside from the header and status bar

```csharp
internal static void RedrawAllElements()
```

### **RegisterElement(Element)**

Method that is executed whenever a new element is constructed

```csharp
internal static void RegisterElement(Element element)
```

#### Parameters

`element` [Element](./foxhollow.terminalui.types.element.md)<br>
Element that is being constructed

### **UnregisterElement(Element)**

Unregister an existing element

```csharp
internal static void UnregisterElement(Element element)
```

#### Parameters

`element` [Element](./foxhollow.terminalui.types.element.md)<br>
Element that is to be unregistered

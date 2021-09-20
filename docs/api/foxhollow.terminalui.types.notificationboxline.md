# NotificationBoxLine

Namespace: FoxHollow.TerminalUI.Types

Class that defines a line that is to be displayed in a notification box

```csharp
public class NotificationBoxLine
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [NotificationBoxLine](./foxhollow.terminalui.types.notificationboxline.md)

## Properties

### **Text**

Text to display on this line

```csharp
public string Text { get; internal set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ForegroundColor**

Foreground color to use when drawing this line

```csharp
public Nullable<ConsoleColor> ForegroundColor { get; internal set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **BackgroundColor**

Background color to use when drawing this line

```csharp
public Nullable<ConsoleColor> BackgroundColor { get; internal set; }
```

#### Property Value

[Nullable&lt;ConsoleColor&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Justify**

How the text should be justified when drawing

```csharp
public TextJustify Justify { get; internal set; }
```

#### Property Value

[TextJustify](./foxhollow.terminalui.types.textjustify.md)<br>

### **RootPoint**

TerminalPoint where the text line begins

```csharp
public TerminalPoint RootPoint { get; internal set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

## Constructors

### **NotificationBoxLine()**



```csharp
public NotificationBoxLine()
```

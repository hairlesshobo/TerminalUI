# MenuEntry

Namespace: FoxHollow.TerminalUI.Types

Represents a single menu entry that is to be displayed on a Menu element

```csharp
public class MenuEntry
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [MenuEntry](./foxhollow.terminalui.types.menuentry.md)

## Properties

### **Name**

Name of the menu entry (the text that is displayed to the end user)

```csharp
public string Name { get; set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Task**

Task to perform if this is the menu entry that is selected.
 
 Note: This task is only executed if the menu is in single-select mode.
 It will not be selected if the menu is in multi-select mode

```csharp
public Func<CancellationToken, Task> Task { get; set; }
```

#### Property Value

[Func&lt;CancellationToken, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **Disabled**

If true, the menu entry is disabled and cannot be selected by the end user

```csharp
public bool Disabled { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **Header**

If true, the menu entry is a header

```csharp
public bool Header { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### **SelectedValue**

Value that is to be returned if this menu entry is selected

```csharp
public object SelectedValue { get; set; }
```

#### Property Value

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### **ForegroundColor**

Foreground color for this entry

```csharp
public ConsoleColor ForegroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **BackgroundColor**

Background color for this entry

```csharp
public ConsoleColor BackgroundColor { get; set; }
```

#### Property Value

ConsoleColor<br>

### **Selected**

Flag indicating whether this entry is selected

```csharp
public bool Selected { get; internal set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **MenuEntry()**



```csharp
public MenuEntry()
```

# StatusBarItem

Namespace: FoxHollow.TerminalUI.Types

Describes an item that is to be displayed on the status bar

```csharp
public class StatusBarItem
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [StatusBarItem](./foxhollow.terminalui.types.statusbaritem.md)

## Properties

### **Name**

Name of the status bar item. This is also the text that is shown
 on the status bar

```csharp
public string Name { get; private set; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Keys**

Keys that are to be associated with this status bar item. If any of the 
 provided keys are pressed, the task associated with this item is executed

```csharp
public Key[] Keys { get; private set; }
```

#### Property Value

[Key[]](./foxhollow.terminalui.types.key.md)<br>

### **Task**

Task to run when any of the provided keys are pressed

```csharp
public Func<Key, Task> Task { get; private set; }
```

#### Property Value

[Func&lt;Key, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>

### **ShowKey**

Flag indicating whether to show the key information in the status bar

```csharp
public bool ShowKey { get; set; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Constructors

### **StatusBarItem(String)**

Construct a text-only status bar item

```csharp
public StatusBarItem(string name)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show

### **StatusBarItem(String, Func&lt;Key, Task&gt;, Key[])**

Constuct a new status bar item

```csharp
public StatusBarItem(string name, Func<Key, Task> task, Key[] keys)
```

#### Parameters

`name` [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Text to show

`task` [Func&lt;Key, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Task to perform

`keys` [Key[]](./foxhollow.terminalui.types.key.md)<br>
Key(s) that will trigger this task

## Methods

### **Remove()**

Remove this item from the status bar

```csharp
public void Remove()
```

### **AddRemoveCallback(Action)**

Used to bolt the status bar with the status bar item remove callback

```csharp
internal void AddRemoveCallback(Action callback)
```

#### Parameters

`callback` [Action](https://docs.microsoft.com/en-us/dotnet/api/system.action)<br>

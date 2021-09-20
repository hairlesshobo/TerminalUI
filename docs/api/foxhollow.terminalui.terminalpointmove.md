# TerminalPointMove

Namespace: FoxHollow.TerminalUI

TerminalPoint helper class that allows to wrap a TerminalPoint
 move action into a using() { } block so that, upon exiting the 
 block, the cursor is automatically returned to the previous location

```csharp
public class TerminalPointMove : System.IDisposable
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TerminalPointMove](./foxhollow.terminalui.terminalpointmove.md)<br>
Implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable)

## Properties

### **PreviousPoint**

Previous point where the cursor was location prior to the move

```csharp
public TerminalPoint PreviousPoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

### **DestinationPoint**

Point where the cursor is temporarily being moved to

```csharp
public TerminalPoint DestinationPoint { get; private set; }
```

#### Property Value

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>

## Methods

### **Dispose()**

Dispose of the object and move back to the previous cursor point

```csharp
public void Dispose()
```

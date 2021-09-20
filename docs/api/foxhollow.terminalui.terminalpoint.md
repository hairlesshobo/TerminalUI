# TerminalPoint

Namespace: FoxHollow.TerminalUI

Class used to make interacting with terminal positioning much easier

```csharp
public class TerminalPoint
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [TerminalPoint](./foxhollow.terminalui.terminalpoint.md)

## Properties

### **Left**

Left index of cursor

```csharp
public int Left { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Top**

Top index of cursor

```csharp
public int Top { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **TerminalPoint(Int32, Int32)**

Construct a new TerminalPoint from the provided left and top coordinates

```csharp
public TerminalPoint(int left, int top)
```

#### Parameters

`left` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
left index

`top` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
top index

## Methods

### **SetX(Int32)**

Set the left coordinate to the specific value

```csharp
public TerminalPoint SetX(int value)
```

#### Parameters

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
new value

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
This object

### **SetY(Int32)**

Set the top coordinate to the specific value

```csharp
public TerminalPoint SetY(int value)
```

#### Parameters

`value` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
new value

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
This object

### **AddX(Int32)**

Create a copy of the current terminal point and add the specified number of columns to it

```csharp
public TerminalPoint AddX(int amount)
```

#### Parameters

`amount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of columns to move

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New TerminalPoint object

### **AddY(Int32)**

Create a copy of the current terminal point and add the specified number of row to it

```csharp
public TerminalPoint AddY(int amount)
```

#### Parameters

`amount` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of rows to move

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New TerminalPoint object

### **Add(Int32, Int32)**

Create a copy of the current terminal point and add the specified number of columns to it

```csharp
public TerminalPoint Add(int amountX, int amountY)
```

#### Parameters

`amountX` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of columns to move

`amountY` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of rows to move

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New TerminalPoint object

### **GetLeftPoint(TerminalArea)**

Get the left-most index of the specified terminal area while staying
 on the same line

```csharp
internal static TerminalPoint GetLeftPoint(TerminalArea area)
```

#### Parameters

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Terminal area to calculate left point for

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New terminal point that is all the way to the left of the current area and on the same line

### **GetRightPoint(TerminalArea)**

Get the right-most index of the specified terminal area while staying
 on the same line

```csharp
internal static TerminalPoint GetRightPoint(TerminalArea area)
```

#### Parameters

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Terminal area to calculate right point for

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New terminal point that is all the way to the right of the current area and on the same line

### **GetBottomPoint(TerminalArea)**

Get the bottom-most index of the specified terminal area while staying
 on the same column

```csharp
internal static TerminalPoint GetBottomPoint(TerminalArea area)
```

#### Parameters

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Terminal area to calculate bottom point for

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New terminal point that is all the way to the bottom of the current area and on the same column

### **GetAreaBounds(TerminalArea)**

Get the four TerminalPoint objects that mark the outer bounds of a particular area

```csharp
internal static ValueTuple<TerminalPoint, TerminalPoint, TerminalPoint, TerminalPoint> GetAreaBounds(TerminalArea area)
```

#### Parameters

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Area to describe

#### Returns

[ValueTuple&lt;TerminalPoint, TerminalPoint, TerminalPoint, TerminalPoint&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.valuetuple-4)<br>

                Tuple that contains the 4 outer corners of the area. 
                0 = Top Left
                1 = Top Right
                2 = Bottom Left
                3 = Bottom Right

### **Clone()**

Make a clone of this TerminalPoint

```csharp
public TerminalPoint Clone()
```

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New TerminalPoint that describes the same coordinates as the current

### **GetCurrent()**

Get the current cursor positoin as a new TerminalPoint

```csharp
public static TerminalPoint GetCurrent()
```

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
New terminal point representing current cursor position

### **MoveTo()**

Move to the terminal point described by the the TerminalPoint object

```csharp
public TerminalPoint MoveTo()
```

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
itself

### **GetMove()**

Get the TerminalPointMove object for this point

```csharp
public TerminalPointMove GetMove()
```

#### Returns

[TerminalPointMove](./foxhollow.terminalui.terminalpointmove.md)<br>
TerminalPointMove object that is meant to be wrapped in a using block

### **MoveToWithCurrent()**

Move to the terminal point described by the the TerminalPoint object

```csharp
public TerminalPoint MoveToWithCurrent()
```

#### Returns

[TerminalPoint](./foxhollow.terminalui.terminalpoint.md)<br>
Current terminal point, prior to the move

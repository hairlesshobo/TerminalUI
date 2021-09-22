# ProgressBar

Namespace: FoxHollow.TerminalUI.Elements

Progress bar element is designed for quickly representing a progress
 as a percentage on a line bar

```csharp
public class ProgressBar : FoxHollow.TerminalUI.Types.Element
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Element](./foxhollow.terminalui.types.element.md) → [ProgressBar](./foxhollow.terminalui.elements.progressbar.md)

## Properties

### **Display**

Where to display the progress percent as text

```csharp
public ProgressDisplay Display { get; private set; }
```

#### Property Value

[ProgressDisplay](./foxhollow.terminalui.types.progressdisplay.md)<br>

### **Mode**

Additional mode that the progress bar is operating in

```csharp
public ProgressMode Mode { get; private set; }
```

#### Property Value

[ProgressMode](./foxhollow.terminalui.types.progressmode.md)<br>

### **CurrentPercent**

Current percentage represented by the progress bar. 
 
 Note: value will be between 0 and 1 as a decimal

```csharp
public double CurrentPercent { get; private set; }
```

#### Property Value

[Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### **ExplicitWidth**

If specified, this will be the fixed width of the progress bar values 
 when operating in "explicit" mode

```csharp
public int ExplicitWidth { get; private set; }
```

#### Property Value

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### **Numerator**

The numerator to use when operating in explicit mode

```csharp
public long Numerator { get; private set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

### **Divisor**

The divisor to use when operating in explicit mode

```csharp
public long Divisor { get; private set; }
```

#### Property Value

[Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>

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

### **ProgressBar(Int32, ProgressDisplay, ProgressMode, Double, Int32, TerminalArea, Boolean)**

Constuct a new instance of the progress bar element

```csharp
public ProgressBar(int width, ProgressDisplay display, ProgressMode mode, double startPercent, int explicitWidth, TerminalArea area, bool show)
```

#### Parameters

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

                Width to use for the progress bar.
            
                      0 = use max width available in the specified area
                below 0 = use max width minus the absolute value provided
                above 0 = set the entire element to a fixed width

`display` [ProgressDisplay](./foxhollow.terminalui.types.progressdisplay.md)<br>

                An enum that specifies if and where to display the progress as text

`mode` [ProgressMode](./foxhollow.terminalui.types.progressmode.md)<br>
Additional mode to operate under

`startPercent` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
Initial percentage to use when first constructing the progress bar

`explicitWidth` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Width to pad the explicit values to, if displayed

`area` [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Area to constrain the progress bar to

`show` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
If true, immediately show the progress bar upon creation

## Methods

### **RecalculateAndRedraw()**

Recalculate and redraw the entire element

```csharp
internal void RecalculateAndRedraw()
```

### **SetExplicitWidth(Int32)**

Set the fixed width of the explicit count display to the value provided

```csharp
public void SetExplicitWidth(int width)
```

#### Parameters

`width` [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>
Number of characters to pad explicit values to

### **SetMode(ProgressMode)**

Set the progress mode

```csharp
public void SetMode(ProgressMode mode)
```

#### Parameters

`mode` [ProgressMode](./foxhollow.terminalui.types.progressmode.md)<br>
new mode to use

### **Redraw()**

Redraw the progress bar

```csharp
public void Redraw()
```

### **UpdateProgress(Double)**

Update the progress bar using the provided percentage.
 
 Note: The percentage must a decimal value between 0 and 1

```csharp
public void UpdateProgress(double newPercent)
```

#### Parameters

`newPercent` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
Percentage value to be displayed

### **UpdateProgress(Int64, Int64)**

Update the progress bar using the provided explicit values and automatically
 calculate the percentage

```csharp
public void UpdateProgress(long numerator, long divisor)
```

#### Parameters

`numerator` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
Top number of the fraction

`divisor` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
Bottom number of the fraction

### **UpdateProgress(Int64, Int64, Double)**

Update the progress. The provided currentPercent value is used for the progress bar
 and the value and divisor are only updated for display purposes

```csharp
public void UpdateProgress(long numerator, long divisor, double newPercent)
```

#### Parameters

`numerator` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The top value of the fraction

`divisor` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
The bottom value of the fraction

`newPercent` [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
The progress current percentage

### **UpdateProgress(Int64, Int64, Boolean)**

Update the progress using the provided explicit values, and optionally
 recalculate the fixed-width of the explicit display

```csharp
public void UpdateProgress(long numerator, long divisor, bool calcWidth)
```

#### Parameters

`numerator` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
Top value of the fraction

`divisor` [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64)<br>
Bottom number of the fraction

`calcWidth` [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

                If true, set the new fixed width to the length of the divisor

## Example

```csharp
static async Task RunExample(CancellationTokenSource cts)
{
    Text text = new Text("Loading the matrix ...", show: true);
    Terminal.NextLine();
    
    ProgressBar progress = new ProgressBar(show: true);

    for (int i = 0; i <= 10; i++)
    {
        if (cts.IsCancellationRequested)
            return;

        progress.UpdateProgress((double)i / 10.0);
        await TaskHelpers.Delay(400, cts.Token);
    }
}
```

![ProgressBar Example](../media/ProgressBar.gif "ProgressBar Example")

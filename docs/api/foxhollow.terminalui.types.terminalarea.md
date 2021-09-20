# TerminalArea

Namespace: FoxHollow.TerminalUI.Types

Enum that allows specifying which "area" an element should reside in
 For example, if "RightHalf" is specified, that will have the left side
 of the element be placed at the center of the terminal

```csharp
public enum TerminalArea
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [TerminalArea](./foxhollow.terminalui.types.terminalarea.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| Default | 0 | No special constraints are placed on the element and the 
                current cursor position is used |
| EntireTerminal | 1 | The entire terminal is available for use and, for most elements,
                the cursor will be returned to the left-most position of the line
                during positioning |
| LeftHalf | 2 | Right half of terminal is available for use |
| RightHalf | 3 | Left half of terminal is available for use |

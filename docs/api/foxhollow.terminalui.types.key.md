# Key

Namespace: FoxHollow.TerminalUI.Types

Class used to describe a terminal input key

```csharp
public class Key
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [Key](./foxhollow.terminalui.types.key.md)

## Properties

### **RootKey**

Key (without modifier) that this key represents

```csharp
public ConsoleKey RootKey { get; private set; }
```

#### Property Value

ConsoleKey<br>

### **Modifiers**

Modifiers (ctrl, shift, alt, etc...) that this key requires

```csharp
public Nullable<ConsoleModifiers> Modifiers { get; private set; }
```

#### Property Value

[Nullable&lt;ConsoleModifiers&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### **Character**

Character to be displayed on the status bar

```csharp
public string Character { get; }
```

#### Property Value

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

## Methods

### **MakeKey(ConsoleKey, Nullable&lt;ConsoleModifiers&gt;)**

Create a new key from the provided root key and modifier(s)

```csharp
public static Key MakeKey(ConsoleKey rootKey, Nullable<ConsoleModifiers> modifiers)
```

#### Parameters

`rootKey` ConsoleKey<br>
root key to use

`modifiers` [Nullable&lt;ConsoleModifiers&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
one or more modifier keys to use

#### Returns

[Key](./foxhollow.terminalui.types.key.md)<br>
new key object

### **FromConsoleKeyInfo(ConsoleKeyInfo)**

Create new key from ConsoleKeyInfo object

```csharp
public static Key FromConsoleKeyInfo(ConsoleKeyInfo keyInfo)
```

#### Parameters

`keyInfo` ConsoleKeyInfo<br>
KeyInfo object to create key from

#### Returns

[Key](./foxhollow.terminalui.types.key.md)<br>
new key object

### **ToString()**

Convert the key to a string representation

```csharp
public string ToString()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
String

### **GetRootKeyCharacter()**

Get the character described by the key

```csharp
public string GetRootKeyCharacter()
```

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>
Textual representation of the key

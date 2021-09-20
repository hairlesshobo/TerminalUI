# KeyInput

Namespace: FoxHollow.TerminalUI

Class used for reading key input from the terminal

```csharp
public static class KeyInput
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [KeyInput](./foxhollow.terminalui.keyinput.md)

## Properties

### **Listening**

If true, the class is currently listening for key input from the terminal

```csharp
public static bool Listening { get; }
```

#### Property Value

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

## Methods

### **RegisterKey(Key, Func&lt;Key, Task&gt;)**

Register a callback to a specific console key

```csharp
public static bool RegisterKey(Key key, Func<Key, Task> callback)
```

#### Parameters

`key` [Key](./foxhollow.terminalui.types.key.md)<br>
Key to listen for

`callback` [Func&lt;Key, Task&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.func-2)<br>
Callback to execute if the key is received

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the key wa ssuccessfully registerde, false otherwise

### **UnregisterKey(Key)**

Unregister any callback that may exist for the specified key

```csharp
public static bool UnregisterKey(Key key)
```

#### Parameters

`key` [Key](./foxhollow.terminalui.types.key.md)<br>
Key to unregister

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>
true if the key previously existed and was successfully unregistered, false otherwise

### **StartLoop(CancellationTokenSource)**

Start the main loop that listens for keyboard input

```csharp
public static Task StartLoop(CancellationTokenSource cts)
```

#### Parameters

`cts` [CancellationTokenSource](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtokensource)<br>
CancellationTokenSource used to cancel listening

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
Task

### **ClearAllKeys()**

Clear all registered keys

```csharp
public static void ClearAllKeys()
```

### **StopListening()**

Stop listening for key input

```csharp
internal static void StopListening()
```

### **WaitForStop()**

Synchronously wait until the listener stops before returning to the caller

```csharp
internal static void WaitForStop()
```

### **ReadStringAsync(CancellationToken)**

Asynchronously read an string from the terminal

```csharp
public static Task<string> ReadStringAsync(CancellationToken cToken)
```

#### Parameters

`cToken` [CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
Token used to cancel the key input

#### Returns

[Task&lt;String&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
Task that provides a string. Null if the input was canceled

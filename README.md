# About
This project is meant to provide a very simple Terminal "UI" for dotnet 
projects. There are no movable windows, no mouse support, etc. It really 
is meant to provide some useful "widgets" or "elements" that can be created 
and easily updated in-place. A great example of this is a `ProgressBar`. 
Once created, you have a handle to the `ProgressBar` and can easily 
updated. For example:

```csharp
// This will create a progress bar at the current
// cursor index (x and y) that consumes the remaining
// terminal width. It will then set the percentage to
// 42%.
ProgressBar progressBar = new ProgressBar();
progressBar.UpdateProgress(0.42);
```


# To-do list
* Add ability for elements to register themselves with the terminal class
* During a Termianl.Clear().. all elements will be unregistered
* Add a "RecalcAndDraw()" method to the Element abstract class that 
  recalculates positions and constraints based on the TerminalArea provided
  during construction
* if a terminal resize is detected, call "RecalcAndDraw()" on all registered 
  elements. This should pave the way to support terminal resizing in the future

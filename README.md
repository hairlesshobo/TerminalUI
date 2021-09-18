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

# Element list
  * CliMenu
  * DataTable
  * Header
  * HorizontalLine
  * KeyValueText
  * NotificationBox
  * Pager
  * ProgressBar
  * QueryYesNo
  * SplitLine
  * Text

  * StatusBar **

# To-do list
* Add "area" parameter to all elements
  * CliMenu
  * NotificationBox
  * QueryYesNo
* Add "show" parameter to all elements
  * CliMenu
  * NotificationBox
  * QueryYesNo

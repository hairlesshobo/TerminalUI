# Elements

## Menu

> Basic menu element that allows listing multiple options and allows the user to 
> interactively select one or more (multiple select not yet implemented) items

![Menu Example](./media/Menu.gif "Menu Example")

[*API reference for the `Menu` element*](/api/foxhollow.terminalui.elements.menu)

## DataTable

> The data table element is meant for displaying data in a table format
> 
> At some point in the future, it will also be able to be used for editing 
> data in a table, but we aren't there yet

[*API reference for the `DataTable` element*](/api/foxhollow.terminalui.elements.datatable)

## Header

> A header element is a two-line element that is comprised of two other elements. 
> The first line is a `SplitLine` element the second element is a `HorizontalLine`
> element. By default, this element is used at the top of a TerminalUI application
> when it is started with `Terminal.Run()`

[*API reference for the `Header` element*](/api/foxhollow.terminalui.elements.header)

## HorizontalLine

> A horizontal line element is exactly what it sounds like.. a horizontal line that 
> is used to separate a line above and below it

[*API reference for the `HorizontalLine` element*](/api/foxhollow.terminalui.elements.horizontalline)

## KeyValueText

> A class to display a KeyValue textual item. A KeyValueText element consists of two 
> pieces of text.. a "Key", which is a "static" string, and a "Value" which is a dynamic 
> and frequently changing string

[*API reference for the `KeyValueText` element*](/api/foxhollow.terminalui.elements.keyvaluetext)

## NotificationBox

> A notification box is meant to be displayed above the rest of the elements for a 
> notification or, at some point in the future, maybe used to query the user for 
> information of some sort

[*API reference for the `NotificationBox` element*](/api/foxhollow.terminalui.elements.notificationbox)

## Pager

> Pager element that is used for displaying multi-line text that overflows beyond what the 
> terminal is able to display. This element can be scrolled to allow for easy viewing of the content.

[*API reference for the `Pager` element*](/api/foxhollow.terminalui.elements.pager)

## ProgressBar

> A Progress bar element is designed for quickly displaying a visual representation of 
> progress as a percentage on a line bar

![ProgressBar Example](./media/ProgressBar.gif "ProgressBar Example")

[*API reference for the `ProgressBar` element*](/api/foxhollow.terminalui.elements.progressbar)

## QueryYesNo

> Simple element that is used to await a yes/no answer from the user

[*API reference for the `QueryYesNo` element*](/api/foxhollow.terminalui.elements.queryyesno)

## SplitLine

> Split-line element. This is used for displaying two pieces of text on a line, one is 
> left-justified and the other is right justified

[*API reference for the `SplitLine` element*](/api/foxhollow.terminalui.elements.splitline)

## Text

> This is a very simple element that just displays text by itself

[*API reference for the `Text` element*](/api/foxhollow.terminalui.elements.text)

## StatusBar **

> A status bar element is a special element that can only exist once and will be shown on the very 
> bottom line of the screen. The status bar is also the entry point for any terminal key input

[*API reference for the `StatusBar` element*](/api/foxhollow.terminalui.elements.header)

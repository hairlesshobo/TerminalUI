# TerminalUI - Simple terminal widgets for C#

## Overview
This project is meant to provide a very simple Terminal "UI" for dotnet 
projects. There are no movable windows, no mouse support, etc. It really 
is meant to provide some useful "widgets" or "elements" that can be created 
and easily updated in-place. A great example of this is a `ProgressBar`. 
Once created, you have a handle to the `ProgressBar` and can easily 
updated. For example:

```csharp
// This will create a progress bar at the current cursor index (x and y) that 
// consumes the remaining terminal width. It will then set the percentage to 42%.
ProgressBar progressBar = new ProgressBar();
progressBar.UpdateProgress(0.42);
```

## Demo
Here you can see a screen grab of a few of these elements in use:

![demo](/_media/QuickArchiverDemo.gif)

## License
TerminalUI is licensed under the GNU Lesser General Public License (LGPL) v2.1 or later

Copyright © 2021 Steve Cross

> This program is free software; you can redistribute it and/or modify  
> it under the terms of the GNU Lesser General Public License as published by  
> the Free Software Foundation; either version 2.1 of the License, or  
> (at your option) any later version.
>  
> This program is distributed in the hope that it will be useful,  
> but WITHOUT ANY WARRANTY; without even the implied warranty of  
> MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the  
> GNU Lesser General Public License for more details.  
>  
> You should have received a copy of the GNU Lesser General Public License  
> along with this program; if not, see <http://www.gnu.org/licenses/>.

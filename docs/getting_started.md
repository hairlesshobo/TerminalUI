# Getting Started

## Download

To start using TerminalUI, you have two choices.. you can either add the package from NuGet or you can include the source directly in your code.
I highly recommend that you go the NuGet route so that you can easily get new versions of the library as things are fixed

### Install from NuGet

The library can be found on NuGet at the following location:

<https://www.nuget.org/packages/FoxHollow.TerminalUI/>

For dotnet core / dotnet 5.0+, run the following command

```
dotnet add package FoxHollow.TerminalUI
```


### Install from release

To install the library from source, you can either use a release version or you can directly clone the code with `git`.


* Release can be downloaded directly in [Gitea](https://git.foxhollow.cc/hairlesshobo/TerminalUI/releases)
* Code can be cloned via `git`:
```
git clone https://git.foxhollow.cc/hairlesshobo/TerminalUI.git
```

When installing from source, you need to add the project as a reference to your project, and optionally your `.sln` file (if you have one). Example:

```
dotnet add reference ./TerminalUI/src/
dotnet sln add ./TerminalUI/src/
```
# Inno Setup ❤️ DotNet

This is a project containing boiler plate to communicate between `C#` and `Inno Setup`

### Building
---
- `Build.ps1`
  - Will build `C#` via `/Dotnet/PublishToInnoSetup.ps1`:
  - Generate `dotnet.iss` file with references to our binary
  - Publish the `dotnet.iss` and `[Files]` to the correct `Inno Setup` location
  - Build Inno Setup to `Output/mysetup.exe`

---
### Best practices
---

- Build the `C#` project as x86 to work with InnoSetup
  - [64-bit Installation Limitations](https://jrsoftware.org/ishelp/index.php?topic=64bitlimitations)

- Exceptions can be handled across languages, but they will not contain any useful information. Therefore any logging must be done before the exception crosses the language barrier.

- Return `False` (or `int` status code) when abort is required.
  - It is not possible to `Abort` from within C#. Creating an `Abort()` callback does nothing.
  - Throwing an exception is also possible, then catching it in Pascal and aborting in the `except`, but it is not clear is it is an 'expected' exception or not, so this is not recommended.

- ⚠️ The project `InnoSetup.Bindings` cannot be debugged, due to the unmanaged exports rewriting the `IL`. Therefor this project should only contain the minimal bindings required, and the logic should go into `InnoSetup.Logic`, which is a regular C# project.

- Use `embedded` PDB export. InnoSetup extracts files lazily, so a seperate .pdb file would require custom code to extract it.

---
### Exporting functions:
---

This project uses [UnmanagedExports.Repack](https://www.nuget.org/packages/UnmanagedExports.Repack) to export unmanaged functions, without having to write a `C++/CLI` wrapper.

Callbacks can be added to `InnoSetup.Bindings.Callbacks.cs`

Entrypoints can be added to `InnoSetup.Bindings.EntryPoints.cs`

After a new function has been exported, a reference must be added to the parent InnoSetup project, in `InitializeDotnet.iss`   

---
### Passing strings between Inno Setup and CSharp
---

Situation: String from Pascal to C#
- C#: `[MarshalAs(UnmanagedType.LPWStr)] string {name}`
- Pascal: `{name}: string`

Situation: String from C# to Pascal
- C#: `[MarshalAs(UnmanagedType.BStr)] out string {name}`
- Pascal: `out {name}: WideString`

Note that for returning a string, the `out` parameter is used, and the `Marshal` type is now `BStr`.

For more explanation: https://stackoverflow.com/questions/9331026/why-can-delphi-dlls-use-widestring-without-using-sharemem

---
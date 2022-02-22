[Code]
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Called during Setup's initialization. Return False to abort Setup, True otherwise.
function InitializeSetup(): Boolean;
begin
  if not IsDotNetInstalled(net472, 0) then
  begin
    MessageBox(ExpandConstant('{#MyAppName} needs Microsoft .NET Framework 4.7.2 or later. Please install this software first.'), MB_ICONERROR);
    Abort();
  end;

  InitializeDotnet();

  Result := Example();
end;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
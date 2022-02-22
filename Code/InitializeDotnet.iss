[Code]
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Callbacks

procedure ExpandConstantWrapper(const toExpandString: string; out expandedString: WideString);
begin
  expandedString := ExpandConstant('{' + toExpandString + '}');
end;

function LogWrapper(const message: string): string;
begin
  Log(message);
end;

procedure ExtractTemporaryFileWrapper(const fileName: string);
begin
  ExtractTemporaryFile(fileName);
end;

procedure RegisterLogCallback(callback: Longword);
external 'RegisterLogCallback@files:InnoSetup.Bindings.dll stdcall delayload';

procedure RegisterExpandConstantCallback(callback: Longword);
external 'RegisterExpandConstantCallback@files:InnoSetup.Bindings.dll stdcall delayload';

procedure RegisterMessageBoxCallback(callback: Longword);
external 'RegisterMessageBoxCallback@files:InnoSetup.Bindings.dll stdcall delayload';

procedure RegisterExtractTemporaryFileCallback(callback: Longword);
external 'RegisterExtractTemporaryFileCallback@files:InnoSetup.Bindings.dll stdcall delayload';

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Entry Points

procedure FinishSetupBindings();
external 'FinishSetupBindings@files:InnoSetup.Bindings.dll stdcall delayload';

function Example(): boolean;
external 'Example@files:InnoSetup.Bindings.dll stdcall delayload';

function AddConstant(name, value: string): boolean;
external 'AddConstant@files:InnoSetup.Bindings.dll stdcall delayload';

procedure InitializeDotnet;
begin
  RegisterExtractTemporaryFileCallback(CreateCallback(@ExtractTemporaryFileWrapper));
  RegisterLogCallback(CreateCallback(@LogWrapper));
  RegisterExpandConstantCallback(CreateCallback(@ExpandConstantWrapper));
  RegisterMessageBoxCallback(CreateCallback(@MessageBox));
  FinishSetupBindings();
  AddConstant('MyAppName', '{#MyAppName}');
end;


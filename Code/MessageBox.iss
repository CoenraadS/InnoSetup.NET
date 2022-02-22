[Code]
  
function _MessageBoxW_(hWnd: Integer; lpText, lpCaption: String; uType: Cardinal): Integer;
  external 'MessageBoxW@user32.dll stdcall';

const
  MB_ICONERROR = $00000010;

function MessageBox(const message: String; const flags: Integer): Integer;
begin
  Result :=
    _MessageBoxW_(StrToInt(ExpandConstant('{wizardhwnd}')), message, ExpandConstant('{#MyAppName}'), flags);
end;
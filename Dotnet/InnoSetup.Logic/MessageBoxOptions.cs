﻿using System;

namespace InnoSetup.Logic
{
    [Flags]
    public enum MessageBoxOptions : uint
    {
        // Buttons
        MB_ABORTRETRYIGNORE = 0x00000002,
        MB_CANCELTRYCONTINUE = 0x00000006,
        MB_HELP = 0x00004000,
        MB_OK = 0x00000000,
        MB_OKCANCEL = 0x00000001,
        MB_RETRYCANCEL = 0x00000005,
        MB_YESNO = 0x00000004,
        MB_YESNOCANCEL = 0x00000003,

        // Icons
        MB_ICONEXCLAMATION = 0x00000030,
        MB_ICONWARNING = 0x00000030,
        MB_ICONINFORMATION = 0x00000040,
        MB_ICONASTERISK = 0x00000040,
        MB_ICONQUESTION = 0x00000020,
        MB_ICONSTOP = 0x00000010,
        MB_ICONERROR = 0x00000010,
        MB_ICONHAND = 0x00000010,

        // Default Buttons
        MB_DEFBUTTON1 = 0x00000000,
        MB_DEFBUTTON2 = 0x00000100,
        MB_DEFBUTTON3 = 0x00000200,
        MB_DEFBUTTON4 = 0x00000300,

        // Modality
        MB_APPLMODAL = 0x00000000,
        MB_SYSTEMMODAL = 0x00001000,
        MB_TASKMODAL = 0x00002000,

        // Other Options
        MB_DEFAULT_DESKTOP_ONLY = 0x00020000,
        MB_RIGHT = 0x00080000,
        MB_RTLREADING = 0x00100000,
        MB_SETFOREGROUND = 0x00010000,
        MB_TOPMOST = 0x00040000,
        MB_SERVICE_NOTIFICATION = 0x00200000
    }

    public enum MessageBoxValues : int
    {
        IDABORT = 3,
        IDCANCEL = 2,
        IDCONTINUE = 11,
        IDIGNORE = 5,
        IDNO = 7,
        IDOK = 1,
        IDRETRY = 4,
        IDTRYAGAIN = 10,
        IDYES = 6
    }
}

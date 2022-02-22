using System;

namespace InnoSetup.Logic
{
    /// <summary>
    /// This class is to convert from the Pascal specific delegates to regular Actions and Funcs.
    /// </summary>
    public interface ICallback
    {
        /// <summary>
        /// Changes all constants in S to their values. For example, ExpandConstant('{srcexe}') is changed to the filename of Setup.
        // An exception will be raised if there was an error expanding the constants.
        /// https://jrsoftware.org/ishelp/index.php?topic=isxfunc_expandconstant
        /// </summary>
        Func<string, string> ExpandConstant { get; }

        /// <summary>
        /// Logs the specified string in Setup's log file.
        /// Calls to this function are ignored if logging is not enabled via the /LOG command line parameter or the SetupLogging [Setup] section directive.
        /// https://jrsoftware.org/ishelp/index.php?topic=isxfunc_log
        /// </summary>
        Action<string> Log { get; }

        /// <summary>
        /// Extracts the specified file from the [Files] section to a temporary directory. To find the location of the temporary directory, use ExpandConstant('{tmp}').
        /// The extracted files are automatically deleted when Setup exits.
        /// https://jrsoftware.org/ishelp/index.php?topic=isxfunc_extracttemporaryfile
        /// </summary>
        Action<string> ExtractTemporaryFile { get; }

        /// <summary>
        /// Display a message box. (Message, Flags)
        /// Caption is defined in Pascal
        /// https://www.pinvoke.net/default.aspx/user32.messagebox
        /// </summary>
        Func<string, MessageBoxOptions, MessageBoxValues> MessageBox { get; }
    }
}

using System.Runtime.InteropServices;
using InnoSetup.Logic;
using RGiesecke.DllExport;

namespace InnoSetup.Bindings
{
    // This class should only be used after Callbacks.FinishSetupBindings() has executed
    public static class EntryPoints
    {
        static readonly Constants constants = new Constants();

        [DllExport]
        public static void AddConstant([MarshalAs(UnmanagedType.LPWStr)] string key, [MarshalAs(UnmanagedType.LPWStr)] string value) => constants.Lookup[key] = value;

        [DllExport]
        public static bool Example()
        {
            return new Example(Callback.Instance, constants).Execute();
        }
    }
}

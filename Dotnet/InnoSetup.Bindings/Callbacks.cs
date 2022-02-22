using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using InnoSetup.Logic;
using RGiesecke.DllExport;

namespace InnoSetup.Bindings
{
    /// <summary>
    /// These should be mapped in InitializeDotnet.iss in the parent InnoSetup project
    /// </summary>
    public static class Callbacks
    {
        #region AssemblyResolve
        static Callbacks()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveFromTemporaryFile;
        }

        private static Assembly ResolveFromTemporaryFile(object sender, ResolveEventArgs args)
        {
            void Log(string message) => LogCallback?.Invoke(message);

            try
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                var location = currentAssembly.Location;
                var dll = new AssemblyName(args.Name).Name + ".dll";

                var path = Path.Combine(Path.GetDirectoryName(location), dll);
                Log("Trying to resolve: " + path);
                ExtractTemporaryFileCallback(dll);

                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(path);
                }
                return null;
            }
            catch (Exception ex)
            {
                Log(ex.ToString());
                throw;
            }
        }
        #endregion

        public delegate int MessageBoxDelegate([MarshalAs(UnmanagedType.LPWStr)] string message, uint options);
        static MessageBoxDelegate MessageBoxCallback;
        [DllExport] public static void RegisterMessageBoxCallback(MessageBoxDelegate callback) => MessageBoxCallback = callback;


        public delegate void LogDelegate([MarshalAs(UnmanagedType.LPWStr)] string input);
        static LogDelegate LogCallback;
        [DllExport] public static void RegisterLogCallback(LogDelegate callback) => LogCallback = callback;


        public delegate void ExtractTemporaryFileDelegate([MarshalAs(UnmanagedType.LPWStr)] string input);
        static ExtractTemporaryFileDelegate ExtractTemporaryFileCallback;
        [DllExport] public static void RegisterExtractTemporaryFileCallback(ExtractTemporaryFileDelegate callback) => ExtractTemporaryFileCallback = callback;


        public delegate bool ExpandConstantDelegate([MarshalAs(UnmanagedType.LPWStr)] string input, [MarshalAs(UnmanagedType.BStr)] out string output);
        static ExpandConstantDelegate ExpandConstantCallback;
        [DllExport] public static void RegisterExpandConstantCallback(ExpandConstantDelegate callback) => ExpandConstantCallback = callback;

        [DllExport]
        // From this function onwards it is save use use types from other projects
        public static void FinishSetupBindings()
        {
            var callback = new Callback();
            callback.MessageBox = (message, options) => (MessageBoxValues)MessageBoxCallback(message, (uint)options);
            callback.Log = message => LogCallback(message);
            callback.ExtractTemporaryFile = fileName => ExtractTemporaryFileCallback(fileName);
            callback.ExpandConstant = request =>
            {
                ExpandConstantCallback(request, out var response);
                return response;
            };

            Callback.Instance = callback;
        }
    }
}

using System;

namespace InnoSetup.Logic
{
    public static class Guard
    {
        /// <summary>
        /// Throw NullReferenceException if null
        /// </summary>
        /// <param name="obj">Parameter to check</param>
        /// <param name="name">Name of parameter</param>
        /// <exception cref="NullReferenceException"></exception>
        public static void NotNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new NullReferenceException($"Guard Triggered: {name} is null");
            }
        }

        public static T LogException<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                var callback = Callback.Instance;

                if (callback == null)
                {
                    throw;
                }

                var message = ex.ToString();

                callback.Log?.Invoke(message);
#if DEBUG
                message += Environment.NewLine + @"
               🦋           🐝                        
                                                      🐌
                     Press [Retry] to launch the debugger          🐛";

                var response = callback.MessageBox?.Invoke(message, MessageBoxOptions.MB_ICONERROR | MessageBoxOptions.MB_RETRYCANCEL);

                if (response == MessageBoxValues.IDRETRY)
                {
                    System.Diagnostics.Debugger.Launch();
                }
#endif
                throw;
            }
        }
    }
}

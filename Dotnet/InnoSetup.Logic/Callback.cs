using System;

namespace InnoSetup.Logic
{
    /// <inheritdoc />
    public class Callback : ICallback
    {
        public static Callback Instance { get; set; }
        /// <inheritdoc />
        public Action<string> Log { get; set; }

        /// <inheritdoc />
        public Action<string> ExtractTemporaryFile { get; set; }

        /// <inheritdoc />
        public Func<string, string> ExpandConstant { get; set; }

        /// <inheritdoc />
        public Func<string, MessageBoxOptions, MessageBoxValues> MessageBox { get; set; }
    }
}

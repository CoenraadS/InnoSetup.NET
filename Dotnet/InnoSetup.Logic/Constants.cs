using System.Collections.Generic;

namespace InnoSetup.Logic
{
    public class Constants : IConstants
    {
        public readonly Dictionary<string, string> Lookup = new Dictionary<string, string>();

        public string MyAppName => Lookup[nameof(MyAppName)];
    }
}

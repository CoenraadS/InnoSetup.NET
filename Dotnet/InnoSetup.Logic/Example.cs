namespace InnoSetup.Logic
{
    public class Example
    {
        readonly ICallback callback;
        readonly IConstants constants;

        public Example(ICallback callback, IConstants constants)
        {
            Guard.NotNull(callback, nameof(callback));
            Guard.NotNull(constants, nameof(constants));

            this.callback = callback;
            this.constants = constants;
        }

        public bool Execute()
        {
            return Guard.LogException(ShowMessage);
        }

        private bool ShowMessage()
        {
            callback.MessageBox($"Hello World from {constants.MyAppName}", MessageBoxOptions.MB_OK);
            return true;
        }
    }
}

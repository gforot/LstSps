using ListaSpesa.Resources;

namespace ListaSpesa.Utils
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
            _localizedResources = new AppResources();
        }

        private static AppResources _localizedResources;

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}

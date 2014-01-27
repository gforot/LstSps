using RateMyApp.Resources;

namespace RateMyApp
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
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
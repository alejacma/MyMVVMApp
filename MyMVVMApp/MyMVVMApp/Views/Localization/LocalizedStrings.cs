using MyMVVMApp.Resources;

namespace MyMVVMApp.Views.Localization
{
    // Sacado de la plantilla de Visual Studio
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();

        public AppResources LocalizedResources { get { return _localizedResources; } }
    }
}
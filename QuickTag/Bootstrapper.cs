using QuickTag.Services;
using QuickTag.ViewModels;
using Splat;

namespace QuickTag
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            BootstrapSettings();
            BootstrapServices();
            BootstrapViewModels();

            SplatRegistrations.SetupIOC();
        }

        private static void BootstrapSettings()
        {
            var settings = UserSettings.Load();
            SplatRegistrations.RegisterConstant<IUserSettings>(settings);
        }

        private static void BootstrapServices()
        {
            SplatRegistrations.Register<ITrackService, TrackService>();
        }

        private static void BootstrapViewModels()
        {

            SplatRegistrations.Register<MainWindowViewModel>();
            SplatRegistrations.Register<TrackListViewModel>();
            SplatRegistrations.Register<TrackListItemViewModel>();
        }
    }
}

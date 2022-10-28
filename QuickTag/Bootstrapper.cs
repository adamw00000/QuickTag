using Avalonia.Controls;
using QuickTag.Services;
using QuickTag.ViewModels;
using Splat;

namespace QuickTag
{
    public static class Bootstrapper
    {
        private static readonly IMutableDependencyResolver _container = Locator.CurrentMutable;

        public static void Bootstrap()
        {
            BootstrapServices();
            BootstrapViewModels();

            SplatRegistrations.SetupIOC();
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

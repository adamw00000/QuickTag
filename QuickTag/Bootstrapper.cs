using QuickTag.Services;
using QuickTag.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag
{
    public static class Bootstrapper
    {
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
            SplatRegistrations.Register<TrackViewModel>();
        }
    }
}

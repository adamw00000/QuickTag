using QuickTag.Design.Services;
using QuickTag.Design.ViewModels;
using QuickTag.Services;
using QuickTag.ViewModels;

namespace QuickTag.Design
{
    public static class DesignViewModelLocator
    {
        public static MainWindowViewModel MainWindow => new MainWindowViewModelDesign();
        public static TrackListViewModel TrackList => new TrackListViewModelDesign();
        public static TrackWindowViewModel TrackWindow => new TrackWindowViewModelDesign();
        public static ITrackService TrackService => new TrackServiceDesign();
    }
}

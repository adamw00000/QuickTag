using QuickTag.ViewModels;

namespace QuickTag.Design.ViewModels
{
    internal class TrackListViewModelDesign: TrackListViewModel
    {
        public TrackListViewModelDesign(): base(DesignViewModelLocator.TrackService, DesignData.Settings) { }
    }
}

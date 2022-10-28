using QuickTag.Design;

namespace QuickTag.ViewModels
{
    public class MainWindowViewModel: ViewModelBase
    {
        public TrackListViewModel TrackList { get; }

        public MainWindowViewModel(TrackListViewModel trackList)
        {
            TrackList = trackList;
        }
    }
}

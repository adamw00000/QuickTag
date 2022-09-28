using QuickTag.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Text;

namespace QuickTag.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly TrackService _trackService = new();

        public ObservableCollection<TrackViewModel> Tracks { get; } = new()
        {
            new TrackViewModel(new Models.Track("test1", "test2")),
            new TrackViewModel(new Models.Track("test3", "test4")),
        };

        public MainWindowViewModel()
        {
            RxApp.MainThreadScheduler.Schedule(LoadTracks);
        }

        private async  void LoadTracks()
        {
            Tracks.Clear();

            foreach (var track in _trackService.LoadTracks("G:/z telefonu"))
            {
                var trackVm = new TrackViewModel(track);
                Tracks.Add(trackVm);
            }
        }
    }
}

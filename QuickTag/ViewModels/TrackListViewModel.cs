using QuickTag.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace QuickTag.ViewModels
{
    public class TrackListViewModel: ViewModelBase
    {
        private const string ROOTDIR = "G:\\z telefonu";
        private readonly ITrackService _trackService;

        public int CoverMiniatureSize { get; } = 30;

        [Reactive]
        public int TracksLoaded { get; private set; }
        [Reactive]
        public int NumTracks { get; private set; }
        [ObservableAsProperty]
        public string TrackLoadingMessage { get; } = string.Empty;
        [Reactive]
        public bool IsLoading { get; private set; } = false;

        public ObservableCollection<TrackListItemViewModel> Tracks { get; } = new();

        public TrackListViewModel(ITrackService trackService)
        {
            _trackService = trackService;

            this.WhenAnyValue(x => x.TracksLoaded, x => x.NumTracks, (i, total) => i < total ? $"Loading tracks: {i}/{total}" : "Loading finished!")
                .ToPropertyEx(this, x => x.TrackLoadingMessage);

            RxApp.MainThreadScheduler.Schedule(LoadTracks);
        }

        private async void LoadTracks()
        {
            Tracks.Clear();
            TracksLoaded = 0;
            NumTracks = await Observable.Start(() => _trackService.CountTracks(ROOTDIR), RxApp.TaskpoolScheduler);

            IsLoading = true;

            foreach (var track in await Observable.Start(() => _trackService.LoadTracks(ROOTDIR), RxApp.TaskpoolScheduler))
            {
                var trackVm = new TrackListItemViewModel(track);
                Tracks.Add(trackVm);
                await Observable.Start(() => trackVm.LoadCover(CoverMiniatureSize));

                TracksLoaded++;
            }

            RxApp.MainThreadScheduler.Schedule(TimeSpan.FromSeconds(3), () => IsLoading = false);
        }
    }
}

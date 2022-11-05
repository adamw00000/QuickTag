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
        private readonly ITrackService _trackService;
        private readonly IUserSettings _settings;

        [Reactive]
        public int TracksLoaded { get; protected set; }
        [Reactive]
        public int NumTracks { get; protected set; }
        [ObservableAsProperty]
        public string TrackLoadingMessage { get; } = string.Empty;
        [Reactive]
        public bool IsLoading { get; protected set; } = false;

        public ObservableCollection<TrackListItemViewModel> Tracks { get; } = new();

        public TrackListViewModel(ITrackService trackService, IUserSettings settings)
        {
            _trackService = trackService;
            _settings = settings;

            _settings.PropertyChanged += CheckMusicLibraryPath;

            this.WhenAnyValue(x => x.TracksLoaded, x => x.NumTracks, (i, total) => i < total ? $"Loading tracks: {i}/{total}" : "Loading finished!")
                .ToPropertyEx(this, x => x.TrackLoadingMessage);

            RxApp.MainThreadScheduler.Schedule(LoadTracks);
        }

        private void CheckMusicLibraryPath(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(_settings.MusicLibraryPath))
                return;

            RxApp.MainThreadScheduler.Schedule(LoadTracks);
        }

        private async void LoadTracks()
        {
            Tracks.Clear();
            TracksLoaded = 0;
            NumTracks = await Observable.Start(() => _trackService.CountTracks(_settings.MusicLibraryPath), RxApp.TaskpoolScheduler);

            IsLoading = true;

            foreach (var track in await Observable.Start(() => _trackService.LoadTracks(_settings.MusicLibraryPath), RxApp.TaskpoolScheduler))
            {
                var trackVm = new TrackListItemViewModel(track, _settings);
                Tracks.Add(trackVm);
                await Observable.Start(() => trackVm.LoadCover(_settings.TrackListCoverMiniatureSize));

                TracksLoaded++;
            }

            // Hide loading message after delay
            RxApp.MainThreadScheduler.Schedule(TimeSpan.FromSeconds(2), () => IsLoading = false);
        }
    }
}

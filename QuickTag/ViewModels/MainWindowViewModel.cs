using QuickTag.Models;
using QuickTag.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private const string ROOTDIR = "G:\\z telefonu";
        private readonly TrackService _trackService = new();

        public int CoverMiniatureSize { get; set; } = 30;
        [Reactive]
        public int TracksLoaded { get; set; }
        [Reactive]
        public int NumTracks { get; set; }
        [ObservableAsProperty]
        public string TrackLoadingMessage { get; }
        [Reactive]
        public bool IsLoading { get; set; } = false;

        public ObservableCollection<TrackViewModel> Tracks { get; } = new();

        public MainWindowViewModel()
        {
            RxApp.MainThreadScheduler.Schedule(LoadTracks);
            this.WhenAnyValue(x => x.TracksLoaded, x => x.NumTracks, (i, total) => i < total ? $"Loading tracks: {i}/{total}" : "Loading finished!")
                .ToPropertyEx(this, x => x.TrackLoadingMessage);
        }

        private async void LoadTracks()
        {
            Tracks.Clear();
            TracksLoaded = 0;
            NumTracks = await _trackService.CountTracks(ROOTDIR);

            IsLoading = true;

            foreach (var track in await Observable.Start(() => _trackService.LoadTracks(ROOTDIR), RxApp.TaskpoolScheduler))
            {
                var trackVm = new TrackViewModel(track);
                Tracks.Add(trackVm);
                await Observable.Start(() => trackVm.LoadCover(CoverMiniatureSize));

                TracksLoaded++;
            }

            RxApp.MainThreadScheduler.Schedule(new TimeSpan(0, 0, 5), () =>
            {
                IsLoading = false;
            });
        }
    }
}

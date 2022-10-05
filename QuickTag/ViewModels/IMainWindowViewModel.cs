using QuickTag.Models;
using QuickTag.Services;
using QuickTag.ViewModels.Factories;
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
    public interface IMainWindowViewModel
    {
        int CoverMiniatureSize { get; }
        int TracksLoaded { get; }
        int NumTracks { get; }
        string TrackLoadingMessage { get; }
        bool IsLoading { get; }
    }

    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private const string ROOTDIR = "G:\\z telefonu";
        private readonly ITrackService _trackService;
        private readonly ITrackViewModelFactory _trackViewModelFactory;

        public int CoverMiniatureSize { get; } = 30;
        [Reactive]
        public int TracksLoaded { get; private set; }
        [Reactive]
        public int NumTracks { get; private set; }
        [ObservableAsProperty]
        public string TrackLoadingMessage { get; } = string.Empty;
        [Reactive]
        public bool IsLoading { get; private set; } = false;

        public ObservableCollection<ITrackViewModel> Tracks { get; } = new();

        public MainWindowViewModel(ITrackService trackService, ITrackViewModelFactory trackViewModelFactory)
        {
            _trackService = trackService;
            _trackViewModelFactory = trackViewModelFactory;

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
                var trackVm = _trackViewModelFactory.Create(track);
                Tracks.Add(trackVm);
                await Observable.Start(() => trackVm.LoadCover(CoverMiniatureSize));

                TracksLoaded++;
            }

            RxApp.MainThreadScheduler.Schedule(TimeSpan.FromSeconds(3), () => IsLoading = false);
        }
    }
}

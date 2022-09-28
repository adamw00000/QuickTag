using QuickTag.Models;
using QuickTag.Services;
using ReactiveUI;
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
        private const string ROOT = "G:\\z telefonu";
        private readonly TrackService _trackService = new();

        public int CoverMiniatureSize { get; set; } = 30;

        public ObservableCollection<TrackViewModel> Tracks { get; } = new();


        public MainWindowViewModel()
        {
            RxApp.MainThreadScheduler.Schedule(LoadTracks);
        }

        private async void LoadTracks()
        {
            Tracks.Clear();

            foreach (var track in await Observable.Start(() => _trackService.LoadTracks(ROOT), RxApp.TaskpoolScheduler))
            {
                var trackVm = new TrackViewModel(track);
                Tracks.Add(trackVm);
                await Observable.Start(() => trackVm.LoadCover(CoverMiniatureSize));
            }
        }
    }
}

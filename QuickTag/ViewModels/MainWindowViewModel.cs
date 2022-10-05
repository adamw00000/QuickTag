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
        public TrackListViewModel TrackList { get; }

        public MainWindowViewModel(TrackListViewModel trackList)
        {
            TrackList = trackList;
        }
    }
}

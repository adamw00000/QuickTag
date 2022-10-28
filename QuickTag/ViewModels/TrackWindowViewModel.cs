using System;
using System.Collections.Generic;
using QuickTag.Models;
using ReactiveUI;

namespace QuickTag.ViewModels
{
	public class TrackWindowViewModel: ViewModelBase
	{
        private readonly MusicTrack _track;

        public TrackWindowViewModel(MusicTrack track)
        {
            _track = track;
        }

        public string Title => _track.Title;
    }
}
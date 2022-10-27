using System;
using System.Collections.Generic;
using QuickTag.Models;
using ReactiveUI;

namespace QuickTag.ViewModels
{
	public class TrackWindowViewModel: ViewModelBase
	{
        private readonly Track _track;

        public TrackWindowViewModel(Track track)
        {
            _track = track;
        }

        public string Title => _track.Title;
    }
}
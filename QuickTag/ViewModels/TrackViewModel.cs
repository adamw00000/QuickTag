using QuickTag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTag.ViewModels
{
    public class TrackViewModel
    {
        private readonly Track _track;

        public TrackViewModel(Track track)
        {
            _track = track;
        }

        public string Title { get => _track.Title; set => _track.Title = value; }
        public string Artist { get => _track.Artist; set => _track.Artist = value; }
    }
}
